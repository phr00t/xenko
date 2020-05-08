// Copyright (c) Stride contributors (https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using System.ComponentModel;
using Stride.Core;
using Stride.Core.Mathematics;
using Stride.Graphics;

namespace Stride.Rendering.Images {
    /// <summary>
    /// A fog filter.
    /// </summary>
    [DataContract("Fog")]
    public class Fog : ImageEffect {
        private readonly ImageEffectShader fogFilter;
        private Texture depthTexture;
        private float zMin, zMax;

        /// <summary>
        /// Initializes a new instance of the <see cref="Fog"/> class.
        /// </summary>
        public Fog()
            : this("FogEffect") {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Fog"/> class.
        /// </summary>
        /// <param name="brightPassShaderName">Name of the bright pass shader.</param>
        public Fog(string ShaderName) : base(ShaderName) {
            if (ShaderName == null) throw new ArgumentNullException("fogFilterName");
            fogFilter = new ImageEffectShader(ShaderName);
        }

        [DataMember(10)]
        public float Density { get; set; } = 0.1f;

        [DataMember(20)]
        public Color4 Color { get; set; } = new Color4(1.0f);

        protected override void InitializeCore() {
            base.InitializeCore();
            ToLoadAndUnload(fogFilter);
        }

        /// <summary>
        /// Provides a color buffer and a depth buffer to apply the fog to.
        /// </summary>
        /// <param name="colorBuffer">A color buffer to process.</param>
        /// <param name="depthBuffer">The depth buffer corresponding to the color buffer provided.</param>
        public void SetColorDepthInput(Texture colorBuffer, Texture depthBuffer, float zMin, float zMax) {
            SetInput(0, colorBuffer);
            depthTexture = depthBuffer;
            this.zMin = zMin;
            this.zMax = zMax;
        }

        protected override void SetDefaultParameters() {
            Color = new Color4(1.0f);
            Density = 0.1f;
            base.SetDefaultParameters();
        }

        protected override void DrawCore(RenderDrawContext context) {
            Texture color = GetInput(0);
            Texture output = GetOutput(0);
            if (color == null || output == null || depthTexture == null) {
                return;
            }

            fogFilter.Parameters.Set(FogEffectKeys.FogColor, Color);
            fogFilter.Parameters.Set(FogEffectKeys.Density, Density);
            fogFilter.Parameters.Set(FogEffectKeys.DepthTexture, depthTexture);
            fogFilter.Parameters.Set(FogEffectKeys.zFar, zMax);
            fogFilter.Parameters.Set(FogEffectKeys.zNear, zMin);

            fogFilter.SetInput(0, color);
            fogFilter.SetOutput(output);
            ((RendererBase)fogFilter).Draw(context);
        }
    }
}
