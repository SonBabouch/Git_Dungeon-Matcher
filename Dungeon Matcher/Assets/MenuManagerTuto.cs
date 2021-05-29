using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManagerTuto : MonoBehaviour
{
    public static MenuManagerTuto Instance;

    private void Awake()
    {
        Instance = this;
    }

    //Référence Canvas
    public CanvasManagerTuto canvasManager; //Dans ce script se trouve les 3 différents GameObjects responsables des Canvas de : Match, List & Bag.

    //Script Référence.
    public MatchManagerTuto matchManager;
    public EnergyManagerTuto energyManager;
    public ListManagerTuto listManager;
    public PlayerLevelTuto playerLevel;
    public BagManagerTuto bagManager;
    public MonsterEncyclopedieTuto monsterEncyclopedie;

    public enum Menu { Shop, Match, List, Bag }  //Index 0 à 3 (dans l'ordre).
    public static Menu currentGameStateMenu;

    public Menu stillcurrent;
    public bool blockAction = false;


  
    private void Start()
    {
        currentGameStateMenu = Menu.Match;
    }
    private void Update()
    {
        stillcurrent = currentGameStateMenu;
    }
}
