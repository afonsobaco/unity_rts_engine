﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTSEngine.Manager
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private int cameraSpeed = 20;
        [SerializeField] private int panSpeed = 50;
        [SerializeField] private float boundriesOffset = 0.03f;
        [SerializeField] private float zoomScale = 100;
        [SerializeField] private float minZoom = 3;
        [SerializeField] private float maxZoom = 20;
        [SerializeField] private bool rotateCameraWhenZooming = false;
        [SerializeField] private float axisPressure = 0.3f;

        [SerializeField] private Camera mainCamera;

        private Vector3 origin;
        private bool isPanning;


        public static CameraManager Instance { get; private set; }
        public Vector3 Origin { get => origin; set => origin = value; }
        public bool IsPanning { get => isPanning; set => isPanning = value; }

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void DoCameraPanning(Vector2 mouseAxis)
        {
            if (!isPanning)
            {
                return;
            }

            Vector3 desiredMove = new Vector3(-mouseAxis.x, 0, -mouseAxis.y);

            desiredMove *= (panSpeed * mainCamera.transform.position.y);
            desiredMove *= Time.deltaTime;
            desiredMove = Quaternion.Euler(new Vector3(0f, transform.eulerAngles.y, 0f)) * desiredMove;
            desiredMove = mainCamera.transform.InverseTransformDirection(desiredMove);
            mainCamera.transform.Translate(desiredMove, Space.Self);
        }

        public void DoCameraMovement(float horizontal, float vertical, Vector3 mousePosition)
        {
            if (!isPanning)
            {
                if ((Mathf.Abs(horizontal) > axisPressure || Mathf.Abs(vertical) > axisPressure))
                {
                    DoAxisCameraMovement(horizontal, vertical);
                }
                else
                {
                    DoMouseCameraMovement(mousePosition);
                }
            }
        }

        private void DoAxisCameraMovement(float horizontal, float vertical)
        {
            MoveCameraHorizontal(horizontal);
            MoveCameraVertical(vertical);
        }

        private void DoMouseCameraMovement(Vector3 mousePosition)
        {
            var mousePos = Camera.main.ScreenToViewportPoint(mousePosition);
            if (mousePos.x >= 0 && mousePos.x <= 1 && mousePos.y >= 0 && mousePos.y <= 1)
            {
                DoMouseCameraMovementBy(mousePos);
            }
        }

        private void DoMouseCameraMovementBy(Vector3 mousePos)
        {
            DoHorizontalMouseMovement(mousePos);
            DoVerticalMouseMovement(mousePos);
        }

        private void DoVerticalMouseMovement(Vector3 mousePos)
        {
            if (mousePos.y >= 0 && mousePos.y < (boundriesOffset))
            {
                MoveCameraVertical(-1);
            }
            else if (mousePos.y <= 1 && mousePos.y > (1 - boundriesOffset))
            {
                MoveCameraVertical(1);
            }
        }

        private void DoHorizontalMouseMovement(Vector3 mousePos)
        {
            if (mousePos.x >= 0 && mousePos.x < (boundriesOffset))
            {
                MoveCameraHorizontal(-1);
            }
            else if (mousePos.x <= 1 && mousePos.x > (1 - boundriesOffset))
            {
                MoveCameraHorizontal(1);
            }
        }

        private void MoveCameraVertical(float value)
        {
            mainCamera.transform.position += new Vector3(0, 0, value * cameraSpeed * Time.deltaTime);

        }

        private void MoveCameraHorizontal(float value)
        {
            mainCamera.transform.position += new Vector3(value * cameraSpeed * Time.deltaTime, 0, 0);

        }

        public void ZoomCamera(float y)
        {
            Vector3 vZoom = mainCamera.transform.position + mainCamera.transform.forward * (zoomScale * Time.deltaTime * y);
            if (vZoom.y < minZoom)
            {
                vZoom = new Vector3(mainCamera.transform.position.x, minZoom, mainCamera.transform.position.z);
            }
            else if (vZoom.y > maxZoom)
            {
                vZoom = new Vector3(mainCamera.transform.position.x, maxZoom, mainCamera.transform.position.z);
            }
            mainCamera.transform.position = vZoom;
        }

        public void CenterCameraToPosition()
        {
            Vector3 midPoint = SelectionManager.Instance.GetSelectionMainPoint();
            float z = midPoint.z - (mainCamera.transform.position.y * Mathf.Tan((90 - mainCamera.transform.rotation.eulerAngles.x) * Mathf.Deg2Rad));
            var pos = new Vector3(midPoint.x, mainCamera.transform.position.y, (float)z);
            if (pos != Vector3.zero)
                mainCamera.transform.position = pos;
        }
    }


}