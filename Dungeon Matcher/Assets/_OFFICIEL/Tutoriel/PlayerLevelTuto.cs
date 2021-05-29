using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelTuto : MonoBehaviour
{
    public static float currentExperienceTuto = 0;
    public static int playerLevelTuto = 1;
    private int experienceToGet = 0;

    public int[] requiredExperience;


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
            currentExperienceTuto++;
            experienceToGet++;
            MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateExperience();
            yield return new WaitForSeconds(0.05f);
            CheckLevelUp();
            StartCoroutine(GiveExperience(numberOfExperience));
        }
    }

    public void CheckLevelUp()
    {
        if (currentExperienceTuto >= requiredExperience[playerLevelTuto - 1])
        {
            currentExperienceTuto = 0;
            playerLevelTuto++;

            //Permet de Update le Visuel et l'état des monstres dans le Jeu
            MenuManagerTuto.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
        }
        MenuManagerTuto.Instance.canvasManager.bagCanvas.SortBag();
    }
}
