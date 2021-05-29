using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerTuto : MonoBehaviour
{
    public static MenuManagerTuto Instance;

    //référence Canvas


    //Script Reference
    public MatchManagerTuto matchManager;
    public EnergyManagerTuto energyManager;
    public ListManagerTuto listManager;
    public PlayerLevelTuto playerLevel;
    public BagManagerTuto bagManager;
    public MonsterEncyclopedieTuto monsterEncyclopedie;
    public CanvasManagerTuto canvasManager;

    public enum Menu { Shop, Match, List, Bag }  //Index 0 à 3 (dans l'ordre).
    public static Menu currentGameStateMenu;

    public Menu stillcurrent;
    public bool blockAction = false;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentGameStateMenu = Menu.Match;
    }
    private void Update()
    {
        stillcurrent = currentGameStateMenu;
    }
}
