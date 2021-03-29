﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static Enemy Instance;
    public float health;
    public float maxHealth;
    public float minHealth;
    public float energy;
    public float maxEnergy;
    public int trueEnergy;
    public GameObject currentMonster;
    [SerializeField]
    private List<GameObject> enemyMonsters = new List<GameObject>();
    [SerializeField]
    private List<Skill> enemySkills = new List<Skill>();
    [SerializeField]
    private List<Skill> enemyHand = new List<Skill>();
    [SerializeField]
    private List<Skill> enemyDraw = new List<Skill>();

    public Skill lastEnemyCompetence;

    public bool isCramp = false;
    public bool isCharging = false;
    public bool isDefending = false;
    public bool isCurse = false;

    public bool isCombo = false;
    [SerializeField] private float comboTime;
    [SerializeField] private float maxComboTime;

    public bool isBoosted = false;
    public float boostAttack = 1f;


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
        InitializeMonster();
        SetEnemyHandAndDraw();
    }

    public void InitializeMonster()
    {
        foreach (GameObject monster in enemyMonsters)
        {
            monster.GetComponent<MonsterToken>().Initialize();
        }

        currentMonster = enemyMonsters[0];
        health = currentMonster.GetComponent<MonsterToken>().health;
        maxHealth = currentMonster.GetComponent<MonsterToken>().maxHealth;
        minHealth = currentMonster.GetComponent<MonsterToken>().minHealth;

        foreach (Skill skill in currentMonster.GetComponent<MonsterToken>().ennemySkills)
        {
            skill.side = Skill.monsterSide.Enemy;
            enemySkills.Add(skill);
        }
        ShuffleEnemySkill();
    }

    public void ShuffleEnemySkill()
    {
        enemySkills.ShuffleFisherYates();
    }
    public void SetEnemyHandAndDraw()
    {
        for (int i = 0; i < enemyHand.Count; i++)
        {
            enemyHand[i] = enemySkills[i];
            enemyDraw[i] = enemySkills[i + 4];
        }
    }

    public void EnemySwapSkill(int index)
    {
        enemyDraw.Add(enemyHand[index]);
        enemyHand.RemoveAt(index);
        enemyHand.Insert(index, enemyDraw[0]);
        enemyDraw.RemoveAt(0);
    }

    #region Combo
    public IEnumerator TimerCombo()
    {
        if (isCombo)
        {
            if (comboTime < maxComboTime)
            {
                comboTime++;
                yield return new WaitForSeconds(0.05f);
                StartCoroutine(TimerCombo());
            }
            else
            {
                isCombo = false;
                comboTime = 0;
            }
        }
    }

    public IEnumerator EnemyCombo()
    {
        isCombo = true;
        comboTime = 0;
        StopCoroutine(TimerCombo());
        StartCoroutine(TimerCombo());
        yield return null;
    }
    #endregion
}
