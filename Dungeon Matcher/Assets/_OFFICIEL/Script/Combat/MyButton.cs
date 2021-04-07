using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class MyButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public Button button;
    public bool isPressed;
    //EventTrigger trigger;
    public void OnPointerDown(PointerEventData eventData)
    {
        isPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isPressed = false;
    }
}
