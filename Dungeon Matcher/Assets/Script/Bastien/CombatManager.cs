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
    public int maxSecondsLeft;
    public int minSecondsLeft;
    [HideInInspector]
    public bool takingTimeAway = false;
    public List<GameObject> combatList = new List<GameObject>();
    public List<Button> combatButtons = new List<Button>();
    [SerializeField]
    private Image enemyhealthBar;
    public float enemyEnergy;


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
        foreach(GameObject monster in combatList)
        {
            monster.GetComponent<MonsterToken>().Initialize();
        }
        Debug.Log(combatList[0].GetComponent<MonsterToken>().health);
        timerDisplay.GetComponent<TextMeshProUGUI>().text = "" + secondsLeft;
    }

    private void Update()
    {
        RunningTimer();
        enemyhealthBar.fillAmount = combatList[0].GetComponent<MonsterToken>().health / 100;

    }

    public IEnumerator CombatTimer()
    {
        takingTimeAway = true;
        yield return new WaitForSeconds(1);
        combatList[0].GetComponent<MonsterToken>().health--;
        Debug.Log(combatList[0].GetComponent<MonsterToken>().health);
        secondsLeft -= 1;
        if(secondsLeft < 10)
        {
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "" + secondsLeft;
        }
        else
        {
            timerDisplay.GetComponent<TextMeshProUGUI>().text = "" + secondsLeft;
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
