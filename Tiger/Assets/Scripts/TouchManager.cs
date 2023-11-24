using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;

public class TouchManager : MonoBehaviour
{
    public event EventHandler OnTap;

    public static TouchManager Instance { get; private set; }

    [SerializeField]
    private GameObject magnet;

    private bool isHolded;
    private bool isTapped;

    private float pressStartTime;
    private float holdThreshold = 0.18f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        magnet.SetActive(false);
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {


        if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count > 0)
        {
            if (!EventSystem.current.IsPointerOverGameObject() && Time.timeScale != 0)
            {
                if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Began)
                {
                    
                    pressStartTime = Time.time;
                    
                }
                else if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Stationary)
                {
                    float pressDuration = Time.time - pressStartTime;
                    if (pressDuration > holdThreshold)
                    {
                        isHolded = true;
                        magnet.SetActive(true);
                    }
                }
                else if (UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[0].phase == UnityEngine.InputSystem.TouchPhase.Ended)
                {
                    isHolded = false;
                    magnet.SetActive(false);

                    float pressDuration = Time.time - pressStartTime;
                    if (pressDuration <= holdThreshold)
                    {
                        isTapped = true;
                        OnTap?.Invoke(this, EventArgs.Empty);
                    }
                }
            }
        }
    }

    public bool GetIsTouched() { return isHolded; }

    public bool GetIsTapped() { return isTapped; }


}
