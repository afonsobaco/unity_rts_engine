﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Refactoring
{
    public class SelectionBox
    {

        private Vector3 _startScreenPoint;
        private RectTransform _selectionBox;

        public SelectionBox(RectTransform rectTransform)
        {
            _selectionBox = rectTransform;
        }

        public void Activate(Vector3 startPosition)
        {
            if (!_selectionBox)
            {
                return;
            }
            _startScreenPoint = startPosition;
            this._selectionBox.gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            if (!_selectionBox)
            {
                return;
            }
            this._selectionBox.gameObject.SetActive(false);
        }

        public void DrawSelectionBox(Vector3 finalPosition)
        {
            if (!_selectionBox)
            {
                return;
            }
            if (this._selectionBox.gameObject.activeInHierarchy)
            {
                this._selectionBox.position = GetAreaCenter(_startScreenPoint, finalPosition);
                this._selectionBox.sizeDelta = GetAreaSize(_startScreenPoint, finalPosition);
            }
        }

        private Vector2 GetAreaSize(Vector2 initialScreenPosition, Vector2 finalScreenPosition)
        {
            return new Vector2(Mathf.Abs(initialScreenPosition.x - finalScreenPosition.x), Mathf.Abs(initialScreenPosition.y - finalScreenPosition.y));
        }

        private Vector2 GetAreaCenter(Vector2 initialScreenPosition, Vector2 finalScreenPosition)
        {
            return (initialScreenPosition + finalScreenPosition) / 2;
        }
    }
}
