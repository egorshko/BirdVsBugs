﻿using UnityEngine;

public class InputController : MonoBehaviour
{
    //следит за пальцем на экране
    public Vector3 TouchPosition;
    public bool InputStarted = false;
    private Camera CameraForInput;
    private Touch touch;

    private void Start()
    {
        CameraForInput = FindObjectOfType<Camera>();
        TouchPosition = new Vector3(0, 0);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                InputStarted = true;
                TouchPosition = CameraForInput.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                TouchPosition = CameraForInput.ScreenToWorldPoint(touch.position);
            }
        }
        else
        {
            InputStarted = false;
        }
    }
}
