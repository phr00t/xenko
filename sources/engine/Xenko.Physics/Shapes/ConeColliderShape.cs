// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.

using System;
using Xenko.Core.Mathematics;
using Xenko.Extensions;
using Xenko.Graphics;
using Xenko.Graphics.GeometricPrimitives;
using Xenko.Rendering;

namespace Xenko.Physics
{
    public class ConeColliderShape : ColliderShape
    {
        public readonly float Height;
        public readonly float Radius;
        public readonly ShapeOrientation Orientation;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConeColliderShape"/> class.
        /// </summary>
        /// <param name="orientation">Up axis.</param>
        /// <param name="radius">The radius of the cone</param>
        /// <param name="height">The height of the cone</param>
        public ConeColliderShape(float heightParam, float radiusParam, ShapeOrientation orientationParam, Vector3? offset = null, Quaternion? localrot = null)
        {
            Type = ColliderShapeTypes.Cone;
            Is2D = false; //always false for cone
            Height = heightParam;
            Radius = radiusParam;
            Orientation = orientationParam;

            Matrix rotation;

            cachedScaling = Vector3.One;

            switch (Orientation)
            {
                case ShapeOrientation.UpX:
                    InternalShape = new BulletSharp.ConeShapeX(Radius, Height)
                    {
                        LocalScaling = cachedScaling,
                    };
                    rotation = Matrix.RotationZ((float)Math.PI / 2.0f);
                    break;
                case ShapeOrientation.UpY:
                    InternalShape = new BulletSharp.ConeShape(Radius, Height)
                    {
                        LocalScaling = cachedScaling,
                    };
                    rotation = Matrix.Identity;
                    break;
                case ShapeOrientation.UpZ:
                    InternalShape = new BulletSharp.ConeShapeZ(Radius, Height)
                    {
                        LocalScaling = cachedScaling,
                    };
                    rotation = Matrix.RotationX((float)Math.PI / 2.0f);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(Orientation));
            }

            DebugPrimitiveMatrix = Matrix.Scaling(new Vector3(Radius * 2, Height, Radius * 2) * DebugScaling) * rotation;

            if (offset.HasValue || localrot.HasValue)
            {
                LocalOffset = offset ?? Vector3.Zero;
                LocalRotation = localrot ?? Quaternion.Identity;
                UpdateLocalTransformations();
            }
        }

        public override MeshDraw CreateDebugPrimitive(GraphicsDevice device)
        {
            return GeometricPrimitive.Cone.New(device).ToMeshDraw();
        }
    }
}
