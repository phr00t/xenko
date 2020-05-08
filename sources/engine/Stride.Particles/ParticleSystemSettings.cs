// Copyright (c) Stride contributors (https://stride3d.net) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System.ComponentModel;
using Stride.Core;
using Stride.Core.Annotations;

namespace Stride.Particles
{
    [DataContract("ParticleSystemSettings")]
    [Display("Settings")]
    public class ParticleSystemSettings
    {
        /// <summary>
        /// Warm-up time is the amount of time the system should spend in the background pre-simulation the first time it is started
        /// </summary>
        /// <userdoc>
        /// Warm-up time is the amount of time the system should spend in the background pre-simulation the first time it is started (warming up). So when it is started it will appear as if it has been running for some time already
        /// </userdoc>
        [DataMember(10)]
        [Display("Warm-up time")]
        [DataMemberRange(0, 5, 0.01, 1, 3)]
        [DefaultValue(0f)]
        public float WarmupTime { get; set; } = 0f;

        /// <summary>
        /// There is a crashing bug on Linux, with mesa drivers, with Vulkan when drawing particles.
        /// ParticleEmitterRenderFeature does some memory PerView voodoo to set matricies, and Linux MESA explodes with memory access violations.
        /// Setting this to true is a workaround to allow a game to otherwise run, but obviously will not render particles.
        /// </summary>
        public static bool DisableAllDrawing = false;
    }
}
