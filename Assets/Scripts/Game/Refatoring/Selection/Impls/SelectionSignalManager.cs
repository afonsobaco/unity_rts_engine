using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using RTSEngine.Core;
using RTSEngine.Signal;
using RTSEngine.Utils;
using Zenject;

namespace RTSEngine.Refactoring
{
    public class SelectionSignalManager
    {

        private Selection _selection;
        private SelectionManager _selectionManager;
        private IRuntimeSet<ISelectable> _mainList;
        private bool _blockAreaSelection;
        public bool BlockAreaSelection { get => _blockAreaSelection; set => _blockAreaSelection = value; }
        private GameSignalBus _signalBus;

        public SelectionSignalManager(Selection selection, SelectionManager selectionManager, IRuntimeSet<ISelectable> mainList, GameSignalBus signalBus)
        {
            this._selection = selection;
            _selectionManager = selectionManager;
            _mainList = mainList;
            _signalBus = signalBus;
        }

        public void OnAreaSelectionSignal(AreaSelectionSignal signal)
        {
            if (!BlockAreaSelection)
            {
                var selection = _selectionManager.GetAreaSelection(GetMainList(), signal.StartPoint, signal.EndPoint);
                var result = _selection.DoSelection(selection, SelectionType.MULTIPLE);
                _signalBus.Fire(new SelectionUpdateSignal() { Selection = result });
            }
            BlockAreaSelection = false;
        }

        public void OnChangeSelectionSignal(ChangeSelectionSignal signal)
        {
            var result = _selection.DoSelection(signal.Selection, SelectionType.MULTIPLE);
            _signalBus.Fire(new SelectionUpdateSignal() { Selection = result });
        }

        public void OnIndividualSelectionSignal(IndividualSelectionSignal signal)
        {
            this.BlockAreaSelection = true;
            var selection = _selectionManager.GetIndividualSelection(GetMainList(), signal.Clicked);
            ISelectable[] result;
            if (signal.OnSelection)
            {
                result = _selection.DoSelection(selection, SelectionType.INDIVIDUAL_ON_SELECTION);
            }
            else
            {
                result = _selection.DoSelection(selection, SelectionType.INDIVIDUAL);
            }
            _signalBus.Fire(new SelectionUpdateSignal() { Selection = result });
        }

        public void OnSelectableObjectCreatedSignal(SelectableObjectCreatedSignal signal)
        {
            _mainList.Add(signal.Selectable);
        }

        public void OnSelectableObjectDeletedSignal(SelectableObjectDeletedSignal signal)
        {
            _mainList.Remove(signal.Selectable);
        }

        public virtual ISelectable[] GetMainList()
        {
            return _mainList.GetAllItems().ToArray();
        }
    }
}