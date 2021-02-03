﻿using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using RTSEngine.Core;

namespace RTSEngine.Manager
{
    public class SelectionManagerBehaviour : MonoBehaviour
    {
        [SerializeField] private SelectableObjectRuntimeSetSO selectableList;
        [SerializeField] private SelectionSettingsSO settings;
        [SerializeField] private RectTransform selectionBox;

        private ISelectionManager<ISelectableObject, SelectionTypeEnum> manager;

        [Inject]
        private void Construct(ISelectionManager<ISelectableObject, SelectionTypeEnum> manager)
        {
            this.manager = manager;
            selectableList.GetList().Clear();
            this.manager.SetMainList(this.selectableList.GetList());
            this.manager.SetSettings(settings);
        }

        private void Update()
        {
            ActivateSelectionBox();
        }

        private void ActivateSelectionBox()
        {
            if (!selectionBox)
            {
                return;
            }
            if (manager.IsSelecting())
            {
                if (!selectionBox.gameObject.activeInHierarchy)
                {
                    selectionBox.gameObject.SetActive(true);
                }
                DrawSelectionBox();
            }
            else
            {

                if (selectionBox.gameObject.activeInHierarchy)
                {
                    selectionBox.gameObject.SetActive(false);
                }
            }

        }

        private void DrawSelectionBox()
        {
            selectionBox.position = SelectionUtil.GetAreaCenter(manager.GetInitialScreenPosition(), manager.GetFinalScreenPosition());
            selectionBox.sizeDelta = SelectionUtil.GetAreaSize(manager.GetInitialScreenPosition(), manager.GetFinalScreenPosition());
        }


    }
}