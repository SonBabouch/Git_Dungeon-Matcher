using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Monster;

public class Player : MonoBehaviour
{
    public static Player Instance;
    [SerializeField]
    private List<GameObject> monsters;
    public List<Skill> playerSkills;
    public float health;

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
        foreach (GameObject monster in monsters)
        {
            foreach (Skill skill in monster.GetComponent<MonsterToken>().allySkills)
            {
                playerSkills.Add(skill);
            }
        }
    }
    private void Update()
    {

    }
}
