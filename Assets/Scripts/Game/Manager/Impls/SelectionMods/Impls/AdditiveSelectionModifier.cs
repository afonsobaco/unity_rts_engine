using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Collections.Generic;
using RTSEngine.Core;
using UnityEngine;
using Zenject;

namespace RTSEngine.Manager
{
    public class AdditiveSelectionModifier : ISelectionModifier
    {

        private ISelectionManager<ISelectableObjectBehaviour, IBaseSelectionMod, SelectionTypeEnum> selectionManager;

        [Inject]
        public void Construct(ISelectionManager<ISelectableObjectBehaviour, IBaseSelectionMod, SelectionTypeEnum> selectionManager)
        {
            this.selectionManager = selectionManager;
        }

        public SelectionArgsXP Apply(SelectionArgsXP args, params object[] other)
        {
            if (selectionManager.IsAdditive())
            {
                AddOrRemoveFromSelection(args);
            }
            return args;
        }

        private void AddOrRemoveFromSelection(SelectionArgsXP args)
        {
            HashSet<ISelectableObjectBehaviour> aux = new HashSet<ISelectableObjectBehaviour>(args.OldSelection);
            aux.UnionWith(args.ToBeAdded);
            if (ContainsAllSelected(args.OldSelection, args.NewSelection))
            {
                aux.RemoveWhere(x => args.ToBeAdded.Contains(x));
            }
            args.ToBeAdded = aux;
        }

        private bool ContainsAllSelected(HashSet<ISelectableObjectBehaviour> oldSelection, HashSet<ISelectableObjectBehaviour> newSelection)
        {
            bool oldContainsNew = newSelection.All(x => oldSelection.Contains(x));
            HashSet<ISelectableObjectBehaviour> aux = new HashSet<ISelectableObjectBehaviour>(oldSelection);
            aux.RemoveWhere(x => newSelection.Contains(x));
            return oldContainsNew && aux.Count > 0;
        }
    }
}
