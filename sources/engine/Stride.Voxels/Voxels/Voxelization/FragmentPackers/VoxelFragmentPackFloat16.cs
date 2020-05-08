﻿using System;
using System.Collections.Generic;
using System.Text;
using Stride.Core;
using Stride.Shaders;
using Stride.Rendering.Materials;

namespace Stride.Rendering.Voxels
{
    [DataContract(DefaultMemberMode = DataMemberMode.Default)]
    [Display("Float16")]
    public class VoxelFragmentPackFloat16 : IVoxelFragmentPacker
    {
        ShaderSource source = new ShaderClassSource("VoxelFragmentPackFloat16");
        public ShaderSource GetShader()
        {
            return source;
        }
        public int GetBits(int channels)
        {
            return channels * 16;
        }
    }
}
