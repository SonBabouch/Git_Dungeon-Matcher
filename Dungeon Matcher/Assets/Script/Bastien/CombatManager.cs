using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Management;
using Monster;

public class CombatManager : MonoBehaviour
{
    public static CombatManager Instance;
    public TextMeshProUGUI timerDisplay;
    public int secondsLeft = 60;
    [HideInInspector]
    public bool takingTimeAway = false;
    public List<MonsterCard> combatList = new List<MonsterCard>();

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
        timerDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
    }

    private void Update()
    {
        RunningTimer();
    }

    public IEnumerator CombatTimer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        secondsLeft -= 1;
        if(secondsLeft < 10)
        {
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "00:0" + secondsLeft;
        }
        else
        {
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "00:" + secondsLeft;
        }
        takingTimeAway = false;
    }

    void RunningTimer()
    {
        if (!takingTimeAway && secondsLeft > 0)
        {
            StartCoroutine(CombatTimer());
        }
    }
}
