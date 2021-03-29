using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Skills/Defense")]

public class Defense : Skill
{

    //1- Variables
    [SerializeField]
    private MonsterToken monster;
    [SerializeField]
    private GameObject owner;

    public override void Initialize(GameObject obj)
    {

        owner = obj;

        monster = owner.GetComponent<MonsterToken>();

    }

    public override void Use()
    {



    }

    public override void PlayerEffect()
    {



    }

    public override void MonsterEffect()
    {



    }

}
