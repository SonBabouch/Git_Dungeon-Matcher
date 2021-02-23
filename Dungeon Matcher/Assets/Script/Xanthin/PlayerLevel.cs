using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public static int playerLevel = 1;
    public static float currentExperience;
    public float[] requiredExperience;

    public void Update()
    {
        if(currentExperience >= requiredExperience[playerLevel - 1])
        {
            currentExperience = 0;
            playerLevel++;
           
        }
    }

   

}
