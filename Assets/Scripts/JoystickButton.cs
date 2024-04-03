using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class JoystickButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public static bool pressed;

    void Start() 
    { 
    }

    void Update() 
    {
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }
}