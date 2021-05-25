using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class EnergyManager : MonoBehaviour
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
        MenuManager.Instance.canvasManager.matchCanvas.UpdateEnergy();
    }

    public void GiveEnergy()
    {
        energy = maxEnergy;
        MenuManager.Instance.canvasManager.matchCanvas.UpdateEnergy();
    }

    public void GivePlayerExperience()
    {
        MenuManager.Instance.playerLevel.StartCoroutine(MenuManager.Instance.playerLevel.GiveExperience(5));
    }

    public void GiveSuperLike()
    {
        superlikeCount++;
        MenuManager.Instance.canvasManager.matchCanvas.UpdateSuperLike();
        
    }
}
