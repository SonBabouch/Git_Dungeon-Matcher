using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour
{
    public static float currentExperience =0;
    public static int playerLevel = 1;
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
            currentExperience++;
            experienceToGet++;
            Management.MenuManager.Instance.canvasManager.matchCanvas.UpdateExperience();
            yield return new WaitForSeconds(0.05f);
            CheckLevelUp();
            StartCoroutine(GiveExperience(numberOfExperience));
        }
    }

    public void GiveExpérienceCheat()
    {

    }

    public void CheckLevelUp()
    {
        if (currentExperience >= requiredExperience[playerLevel - 1])
        {
            currentExperience = 0;
            playerLevel++;

            //Permet de Update le Visuel et l'état des monstres dans le Jeu
            Management.MenuManager.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
        }
      Management.MenuManager.Instance.canvasManager.bagCanvas.SortBag();
    }
}
