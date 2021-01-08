﻿
using System.Collections.Generic;
using UnityEngine;


namespace RTSEngine.Manager
{
    public class PlayerInputManager : MonoBehaviour
    {
        public KeyCode AditiveSelectionKeyCode = KeyCode.LeftControl;
        public KeyCode SameTypeSelectionKeyCode = KeyCode.LeftShift;
        public KeyCode groupKeyCode = KeyCode.LeftControl;
        private Vector2 initialMousePosition;
        private Dictionary<KeyCode, int> groupKeys = new Dictionary<KeyCode, int>()
    {
        {KeyCode.Alpha1, 1},
        {KeyCode.Alpha2, 2},
        {KeyCode.Alpha3, 3},
        {KeyCode.Alpha4, 4},
        {KeyCode.Alpha5, 5},
        {KeyCode.Alpha6, 6},
        {KeyCode.Alpha7, 7},
        {KeyCode.Alpha8, 8},
        {KeyCode.Alpha9, 9},
        {KeyCode.Alpha0, 10}
    };

        void Update()
        {
            SetMouseClick();

            // DoGroupSelection();

            SetSelectionKeys();
        }

        private void SetSelectionKeys()
        {
            SelectionManager.Instance.IsAditiveSelection = Input.GetKey(AditiveSelectionKeyCode);
            SelectionManager.Instance.IsSameTypeSelection = Input.GetKey(SameTypeSelectionKeyCode);
            DoGroupSelection();
        }

        private void DoGroupSelection()
        {
            int keyPressed = GetAnyGroupKeyPressed();
            if (keyPressed > 0)
            {
                if (Input.GetKey(groupKeyCode))
                {
                    // SelectionManager.Instance.SetGroup(keyPressed);
                }
                else
                {
                    // SelectionManager.Instance.GetGroup(keyPressed);
                }
            }
        }

        private int GetAnyGroupKeyPressed()
        {
            foreach (KeyValuePair<KeyCode, int> entry in groupKeys)
            {
                if (Input.GetKeyDown(entry.Key))
                {
                    return entry.Value;
                }
            }
            return 0;
        }

        private void SetMouseClick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectionManager.Instance.StartOfSelection(Input.mousePosition);
            }

            if (Input.GetMouseButton(0))
            {
                SelectionManager.Instance.DoPreSelection(Input.mousePosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                SelectionManager.Instance.EndOfSelection(Input.mousePosition);
            }
        }

    }

}
