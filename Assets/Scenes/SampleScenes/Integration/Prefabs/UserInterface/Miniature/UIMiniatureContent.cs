﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using RTSEngine.RTSUserInterface;
using RTSEngine.Core;
using RTSEngine.Signal;
using System;

namespace RTSEngine.Integration.Scene
{
    public class UIMiniatureContent : UIContent, IPointerClickHandler
    {
        [SerializeField] private Image _picture;
        [SerializeField] private UIMiniatureStatusBar _healthBar;
        [SerializeField] private UIMiniatureStatusBar _manaBar;
        [SerializeField] private List<GameObject> _highlights;

        public override void UpdateAppearance()
        {
            var selectable = (Info as UISceneIntegratedContentInfo).Selectable as IntegrationSceneObject;
            _picture.sprite = selectable.Picture;
            SetStatuses(selectable);
            _highlights.ForEach(x => x.SetActive(selectable.IsHighlighted));
        }

        private void SetStatuses(IntegrationSceneObject selectable)
        {
            SetHealthBar<HealthStatus>(selectable, _healthBar);
            SetHealthBar<ManaStatus>(selectable, _manaBar);
            //TODO set other statuses
        }

        private void SetHealthBar<T>(IntegrationSceneObject selectable, UIMiniatureStatusBar component) where T : DefaultStatus
        {
            var status = selectable.GetComponent<T>();
            if (status)
            {
                component.gameObject.SetActive(status.MaxValue != 0);
                component.StatusBar.fillAmount = status.Value / (float)status.MaxValue;
            }
            else
            {
                component.gameObject.SetActive(false);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            var info = this.Info as UISceneIntegratedContentInfo;
            SignalBus.Fire(new ChangeSelectionSignal() { Selection = new ISelectable[] { info.Selectable } });
        }
    }
}