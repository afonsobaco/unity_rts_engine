﻿using System;
using System.Collections.Generic;
using UnityEngine;
using RTSEngine.Core;
using RTSEngine.Selection.Mod;
namespace RTSEngine.Selection.Util
{
    public class SelectionUtil
    {

        public static List<T> GetAllObjectsInsideSelectionArea<T>(List<T> mainList, Vector3 initialScreenPosition, Vector3 finalScreenPosition, Camera camera) where T : MonoBehaviour
        {
            List<T> list = new List<T>();
            foreach (var obj in mainList)
            {
                var screenPos = camera.WorldToScreenPoint(obj.transform.position);
                if (IsPositionInsideArea(screenPos, initialScreenPosition, finalScreenPosition))
                {
                    list.Add(obj);
                }
            }
            return list;
        }

        private static bool IsPositionInsideArea(Vector3 screenPos, Vector3 initialScreenPosition, Vector3 finalScreenPosition)
        {
            var min = GetMinAreaPosition(initialScreenPosition, finalScreenPosition);
            var max = GetMaxAreaPosition(initialScreenPosition, finalScreenPosition);
            return screenPos.x > min.x && screenPos.x < max.x && screenPos.y > min.y && screenPos.y < max.y;
        }

        private static Vector2 GetMaxAreaPosition(Vector3 initialScreenPosition, Vector3 finalScreenPosition)
        {
            Vector2 size = GetAreaSize(initialScreenPosition, finalScreenPosition);
            Vector3 center = GetAreaCenter(initialScreenPosition, finalScreenPosition);
            return new Vector2(center.x + (size.x / 2), center.y + (size.y / 2));
        }

        private static Vector2 GetMinAreaPosition(Vector3 initialScreenPosition, Vector3 finalScreenPosition)
        {
            Vector2 size = GetAreaSize(initialScreenPosition, finalScreenPosition);
            Vector3 center = GetAreaCenter(initialScreenPosition, finalScreenPosition);
            return new Vector2(center.x - (size.x / 2), center.y - (size.y / 2));
        }

        public static T GetObjectClicked<T>(Vector3 initialScreenPosition, Vector3 finalScreenPosition, Camera camera) where T : MonoBehaviour
        {
            var initialObject = GetObjectInScreenPoint<T>(initialScreenPosition, camera);
            var finalObject = GetObjectInScreenPoint<T>(finalScreenPosition, camera);
            if (initialObject != null && initialObject.Equals(finalObject))
            {
                return initialObject;
            }
            return null;
        }

        public static T GetObjectInScreenPoint<T>(Vector3 mousePosition, Camera camera) where T : MonoBehaviour
        {
            Ray ray = camera.ScreenPointToRay(mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100))
            {
                return hit.transform.gameObject.GetComponent<T>();
            }
            return null;
        }

        public static Vector2 GetAreaSize(Vector2 initialScreenPosition, Vector3 finalScreenPosition)
        {
            return new Vector2(Mathf.Abs(initialScreenPosition.x - finalScreenPosition.x), Mathf.Abs(initialScreenPosition.y - finalScreenPosition.y));
        }

        public static Vector2 GetAreaCenter(Vector3 initialScreenPosition, Vector3 finalScreenPosition)
        {
            return (initialScreenPosition + finalScreenPosition) / 2;
        }
    }
}
