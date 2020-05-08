// Copyright (c) Stride contributors (https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
#if STRIDE_GRAPHICS_API_VULKAN && !STRIDE_GRAPHICS_NO_DESCRIPTOR_COPIES
using System;
using SharpVulkan;

namespace Stride.Graphics
{
    public partial class DescriptorPool
    {
        private uint[] allocatedTypeCounts;
        private uint allocatedSetCount;

        internal SharpVulkan.DescriptorPool NativeDescriptorPool;

        public void Reset()
        {
            GraphicsDevice.DescriptorPools.RecycleObject(GraphicsDevice.NextFenceValue, NativeDescriptorPool);
            NativeDescriptorPool = GraphicsDevice.DescriptorPools.GetObject();

            allocatedSetCount = 0;
            for (int i = 0; i < DescriptorSetLayout.DescriptorTypeCount; i++)
            {
                allocatedTypeCounts[i] = 0;
            }
        }

        private DescriptorPool(GraphicsDevice graphicsDevice, DescriptorTypeCount[] counts) : base(graphicsDevice)
        {
            Recreate();
        }

        internal unsafe SharpVulkan.DescriptorSet AllocateDescriptorSet(DescriptorSetLayout descriptorSetLayout)
        {
            // Keep track of descriptor pool usage
            bool isPoolExhausted = ++allocatedSetCount > GraphicsDevice.MaxDescriptorSetCount;
            for (int i = 0; i < DescriptorSetLayout.DescriptorTypeCount; i++)
            {
                allocatedTypeCounts[i] += descriptorSetLayout.TypeCounts[i];
                if (allocatedTypeCounts[i] > GraphicsDevice.MaxDescriptorTypeCounts[i])
                {
                    isPoolExhausted = true;
                    break;
                }
            }

            if (isPoolExhausted)
            {
                return SharpVulkan.DescriptorSet.Null;
            }

            // Allocate new descriptor set
            var nativeLayoutCopy = descriptorSetLayout.NativeLayout;
            var allocateInfo = new DescriptorSetAllocateInfo
            {
                StructureType = StructureType.DescriptorSetAllocateInfo,
                DescriptorPool = NativeDescriptorPool,
                DescriptorSetCount = 1,
                SetLayouts = new IntPtr(&nativeLayoutCopy)
            };

            SharpVulkan.DescriptorSet descriptorSet;
            GraphicsDevice.NativeDevice.AllocateDescriptorSets(ref allocateInfo, &descriptorSet);
            return descriptorSet;
        }

        private void Recreate()
        {
            NativeDescriptorPool = GraphicsDevice.DescriptorPools.GetObject();
            
            allocatedTypeCounts = new uint[DescriptorSetLayout.DescriptorTypeCount];
            allocatedSetCount = 0;
        }

        /// <inheritdoc/>
        protected internal override bool OnRecreate()
        {
            Recreate();
            return true;
        }

        /// <inheritdoc/>
        protected internal override void OnDestroyed()
        {
            GraphicsDevice.DescriptorPools.RecycleObject(GraphicsDevice.NextFenceValue, NativeDescriptorPool);

            base.OnDestroyed();
        }
    }
}
#endif
