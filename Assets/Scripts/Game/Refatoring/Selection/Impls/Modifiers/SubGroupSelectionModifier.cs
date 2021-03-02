using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using Zenject;
using RTSEngine.Core;
using RTSEngine.Commons;
using System;

namespace RTSEngine.Refactoring
{
    public class SubGroupSelectionModifier : BaseSelectionModifier
    {

        [Space]
        [Header("Modifier attributes")]
        [SerializeField] private KeyCode _key = KeyCode.LeftControl;

        private EqualityComparerComponent _equalityComparer;
        private IAreaSelectionType _areaSelectionType;
        private IRuntimeSet<ISelectable> _mainList;
        private IViewportHelper _viewportHelper;

        private Modifier _modifier;

        [Inject]
        public void Construct(EqualityComparerComponent equalityComparer, IAreaSelectionType areaSelectionType, IRuntimeSet<ISelectable> mainList, IViewportHelper viewportHelper)
        {
            _equalityComparer = equalityComparer;
            _mainList = mainList;
            _areaSelectionType = areaSelectionType;
            _viewportHelper = viewportHelper;
        }

        public override void StartVariables()
        {
            if (_modifier == null)
            {
                _modifier = new Modifier();
            }
            _modifier.ViewportHelper = _viewportHelper;
            _modifier.MainList = _mainList;
            _modifier.AreaSelectionType = _areaSelectionType;
            _modifier.EqualityComparer = _equalityComparer;
        }

        public override ISelectable[] Apply(SelectionInfo info)
        {
            StartVariables();
            return this._modifier.Apply(Input.GetKey(_key), info.OldSelection, info.ActualSelection, info.SelectionType);
        }

        public class Modifier
        {
            public EqualityComparerComponent EqualityComparer { get; set; }
            public IAreaSelectionType AreaSelectionType { get; set; }
            public IRuntimeSet<ISelectable> MainList { get; set; }
            public IViewportHelper ViewportHelper { get; set; }

            public ISelectable[] Apply(bool active, ISelectable[] oldSelection, ISelectable[] actualSelection, SelectionType type)
            {
                if (active && actualSelection.Length == 1)
                {
                    ISelectable[] selectables = type == SelectionType.INDIVIDUAL_ON_SELECTION ? oldSelection : GetAllOnScreenArea();
                    return FilterBySubGroup(selectables, actualSelection.First());
                }
                return actualSelection;
            }

            public virtual ISelectable[] GetAllOnScreenArea()
            {
                return AreaSelectionType.GetAllInsideViewportArea(GetMainList(), this.ViewportHelper.InitialViewportPoint, this.ViewportHelper.FinalViewportPoint);
            }

            public virtual ISelectable[] GetMainList()
            {
                return MainList.GetAllItems().ToArray();
            }

            public virtual ISelectable[] FilterBySubGroup(ISelectable[] selectables, ISelectable selected)
            {
                return SubGroupUtil.FilterBySubGroup(selectables, selected, EqualityComparer);
            }
        }
    }
}
