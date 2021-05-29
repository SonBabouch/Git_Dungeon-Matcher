using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyManagerTuto : MonoBehaviour
{
    
    //Valeurs liées à l'energie
    public static int energy;
    public static int maxEnergy;

    public static int superlikeCount;

    public float productionTime;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation
        maxEnergy = 30;
        superlikeCount = 5;
        energy = maxEnergy;
        MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateEnergy();
    }

    public void GiveEnergy()
    {
        energy = maxEnergy;
        MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateEnergy();
    }

    public void GivePlayerExperience()
    {
        MenuManagerTuto.Instance.playerLevel.StartCoroutine(MenuManagerTuto.Instance.playerLevel.GiveExperience(5));
    }

    public void GiveSuperLike()
    {
        superlikeCount++;
        MenuManagerTuto.Instance.canvasManager.matchCanvas.UpdateSuperLike();

        if(TutorielManager.Instance.currentIndex == 15)
        {
            TutorielManager.Instance.textBulle.AfterSuperlike();
        }
    }
}
