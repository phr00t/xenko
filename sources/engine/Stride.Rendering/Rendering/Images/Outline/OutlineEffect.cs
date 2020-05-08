// <auto-generated>
// Do not edit this file yourself!
//
// This code was generated by Stride Shader Mixin Code Generator.
// To generate it yourself, please install Stride.VisualStudio.Package .vsix
// and re-save the associated .sdfx.
// </auto-generated>

using System;
using Stride.Core;
using Stride.Rendering;
using Stride.Graphics;
using Stride.Shaders;
using Stride.Core.Mathematics;
using Buffer = Stride.Graphics.Buffer;

namespace Stride.Rendering
{
    internal static partial class OutlineEffectKeys
    {
        public static readonly ValueParameterKey<Vector2> ScreenDiffs = ParameterKeys.NewValue<Vector2>();
        public static readonly ValueParameterKey<float> zFar = ParameterKeys.NewValue<float>(1000f);
        public static readonly ValueParameterKey<float> zNear = ParameterKeys.NewValue<float>(0.1f);
        public static readonly ValueParameterKey<float> NormalWeight = ParameterKeys.NewValue<float>(2f);
        public static readonly ValueParameterKey<float> DepthWeight = ParameterKeys.NewValue<float>(0.2f);
        public static readonly ValueParameterKey<float> NormalNearCutoff = ParameterKeys.NewValue<float>(0.1f);
        public static readonly ObjectParameterKey<Texture> DepthTexture = ParameterKeys.NewObject<Texture>();
    }
}
