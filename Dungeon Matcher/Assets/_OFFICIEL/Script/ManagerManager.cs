using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerManager : MonoBehaviour
{
    public static ManagerManager Instance;

    public enum gameState { combat, Menu};
    public gameState currentGameState;

    public GameObject menuManager;
    public GameObject combatManager;
    public GameObject transitionMenu;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        GoToMenu();
    }

    public void GoToCombat()
    {
        menuManager.SetActive(false);
        combatManager.SetActive(true);
    }

    public void GoToMenu()
    {
        menuManager.SetActive(true);
        combatManager.SetActive(false);
    }
}
