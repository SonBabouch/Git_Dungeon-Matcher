using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public static float currentExperience = 0;
    public static int playerLevel = 1;
    
    public int[] requiredExperience;

    public void Update()
    {
        //Traque le moment ou le joueur passe de niveau;
        if(currentExperience >= requiredExperience[playerLevel - 1])
        {
            currentExperience = 0;
            playerLevel++;

            //Permet de Update le Visuel et l'état des monstres dans le Jeu
            Management.MenuManager.Instance.monsterEncyclopedie.UpdateMonsterEncyclopedie();
        }
    }
}
