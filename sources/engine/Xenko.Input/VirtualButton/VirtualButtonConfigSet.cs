// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System.Collections.ObjectModel;

namespace Xenko.Input
{
    /// <summary>
    /// A collection of <see cref="VirtualButtonConfig"/>.
    /// </summary>
    /// <remarks>
    /// Several virtual button configurations can be stored in this instance. 
    /// For example, User0 config could be stored on index 0, User1 on index 1...etc.
    /// </remarks>
    public class VirtualButtonConfigSet : Collection<VirtualButtonConfig>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualButtonConfigSet" /> class.
        /// </summary>
        public VirtualButtonConfigSet()
        {
        }

        public virtual float GetValue(int configIndex, object name)
        {
            if (configIndex < 0 || configIndex >= Count)
            {
                return 0.0f;
            }

            var config = this[configIndex];
            return config != null ? config.GetValue(name) : 0.0f;
        }
    }
}
