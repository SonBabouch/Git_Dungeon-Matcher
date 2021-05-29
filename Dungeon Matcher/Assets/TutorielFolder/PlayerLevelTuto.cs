using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelTuto : MonoBehaviour
{
    public static float currentExperience = 0;
    public static int playerLevel = 1;
    private int experienceToGet = 0;

    public int[] requiredExperience;

    public void Update()
    {
        //Traque le moment ou le joueur passe de niveau;

    }

    public IEnumerator GiveExperience(int numberOfExperience)
    {
        if (experienceToGet == numberOfExperience)
        {
            CheckLevelUp();
            experienceToGet = 0;
            yield return null;
        }
        else
        {
            currentExperience++;
            experienceToGet++;
            MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateExperience();
            yield return new WaitForSeconds(0.05f);
            CheckLevelUp();
            StartCoroutine(GiveExperience(numberOfExperience));
        }
    }

    public void CheckLevelUp()
    {
        if (currentExperience >= requiredExperience[playerLevel - 1])
        {
            currentExperience = 0;
            playerLevel++;

            //Permet de Update le Visuel et l'état des monstres dans le Jeu
            MenuManagerTuto.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
        }
        MenuManagerTuto.Instance.canvasManager.bagCanvas.SortBag();
    }
}
