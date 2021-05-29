using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleTapTuto : MonoBehaviour
{
    //1- Variables
    private float firstClickTime, timeBetweenClicks;
    private bool coroutineAllowed;
    private int clickCounter;
    public BagButtonBehaviourTuto bagButton;

    //2- Start
    void Start()
    {
        firstClickTime = 0f;
        timeBetweenClicks = 0.2f;
        clickCounter = 0;
        coroutineAllowed = true;
    }

    //3- Update 
    public void FirstClick()
    {
        if (Input.GetMouseButtonUp(0))
            clickCounter += 1;

        if (clickCounter == 1 && coroutineAllowed)
        {
            firstClickTime = Time.time;
            StartCoroutine(DoubleClickCheck());
        }
    }

    //4- Coroutine pour le double check
    private IEnumerator DoubleClickCheck()
    {
        coroutineAllowed = false;
        while (Time.time < firstClickTime + timeBetweenClicks)
        {
            if (clickCounter == 2)
            {
                bagButton.Equip();
                break;
            }
            yield return new WaitForEndOfFrame();
        }
        if (clickCounter == 1 && Time.time > firstClickTime + timeBetweenClicks)
            bagButton.Details();

        clickCounter = 0;
        firstClickTime = 0f;
        coroutineAllowed = true;
    }
}
