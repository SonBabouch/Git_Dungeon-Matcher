using UnityEngine;
using System.Collections;

public class PlayerLevel : MonoBehaviour
{
    public static float currentExperience =0;
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
            Debug.Log("else");
            CheckLevelUp();
            experienceToGet = 0;
            yield return null;
        }
        else
        {
            Debug.Log(experienceToGet);
            currentExperience++;
            experienceToGet++;
            Management.MenuManager.Instance.canvasManager.matchCanvas.UpdateExperience();
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
            Management.MenuManager.Instance.canvasManager.matchCanvas.UpdateExperience();
            //Permet de Update le Visuel et l'état des monstres dans le Jeu
            Management.MenuManager.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
        }
    }
}
