using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class EnergyManager : MonoBehaviour
{
    //Valeurs liées à l'energie
    public static int energy;
    public static int maxEnergy;

    public float productionTime;

    // Start is called before the first frame update
    void Start()
    {
        //Initialisation
        maxEnergy = 10;
        energy = maxEnergy;
        MenuManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().UpdateEnergy();
    }

}
