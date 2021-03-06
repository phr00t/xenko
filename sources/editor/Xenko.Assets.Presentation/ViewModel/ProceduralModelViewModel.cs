// Copyright (c) Xenko contributors (https://xenko.com) and Silicon Studio Corp. (https://www.siliconstudio.co.jp)
// Distributed under the MIT license. See the LICENSE.md file in the project root for more information.
using System.Linq;
using Xenko.Core.Assets.Editor.ViewModel;
using Xenko.Core.Quantum;
using Xenko.Assets.Models;
using Xenko.Rendering;
using Xenko.Rendering.ProceduralModels;

namespace Xenko.Assets.Presentation.ViewModel
{
    [AssetViewModel(typeof(ProceduralModelAsset))]
    public class ProceduralModelViewModel : AssetViewModel<ProceduralModelAsset>
    {
        private readonly IMemberNode typeNode;

        public ProceduralModelViewModel(AssetViewModelConstructionParameters parameters)
            : base(parameters)
        {
            typeNode = AssetRootNode[nameof(ProceduralModelAsset.Type)];
            typeNode.ValueChanged += TypeChanged;
        }

        public override void Destroy()
        {
            typeNode.ValueChanged -= TypeChanged;
            base.Destroy();
        }

        private void TypeChanged(object sender, MemberNodeChangeEventArgs e)
        {
            // If the type of procedural has been changed by the user himself, and is not an undo/redo,
            // We would like to transfer the materials from the previous type to the new type.
            if (!PropertyGraph.UpdatingPropertyFromBase && !UndoRedoService.UndoRedoInProgress)
            {
                var oldType = (IProceduralModel)e.OldValue;
                var newType = (IProceduralModel)e.NewValue;
                if (oldType != null && newType != null)
                {
                    var newMaterials = newType.MaterialInstances.ToDictionary(x => x.Key, x => x.Value);

                    foreach (var oldMaterial in oldType.MaterialInstances)
                    {
                        MaterialInstance newMaterial;
                        if (newMaterials.TryGetValue(oldMaterial.Key, out newMaterial))
                        {
                            newMaterial.Material = oldMaterial.Value.Material;
                        }
                    }
                }
            }
        }
    }
}
