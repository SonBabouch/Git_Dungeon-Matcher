using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public GameObject timerDisplay;
    public int secondsLeft = 60;
    public bool takingTimeAway = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        timerDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
    }

    private void Update()
    {
        if (!takingTimeAway && secondsLeft > 0)
        {
            StartCoroutine(Timer());
        }
    }
    public IEnumerator Timer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if(secondsLeft < 10)
        {
            timerDisplay.GetComponent<Text>().text = "00:0" + secondsLeft;
        }
        else
        {
            timerDisplay.GetComponent<Text>().text = "00:" + secondsLeft;
        }
        takingTimeAway = false;
    }
}
