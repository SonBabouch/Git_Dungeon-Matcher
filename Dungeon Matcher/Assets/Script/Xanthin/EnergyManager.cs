using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Management;

public class EnergyManager : MonoBehaviour
{
    public static int energy;
    public static int maxEnergy;

    public float productionTime;

    // Start is called before the first frame update
    void Start()
    {
        maxEnergy = 10;
        energy = maxEnergy;
        GameManager.Instance.canvasManager.GetComponent<CanvasManager>().matchCanvas.GetComponent<MatchCanvasManager>().UpdateEnergy();
    }

}
