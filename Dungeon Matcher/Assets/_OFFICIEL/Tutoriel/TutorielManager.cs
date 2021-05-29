using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorielManager : MonoBehaviour
{
    public static TutorielManager Instance;
    public MenuTransitionTuto menuTransition;
    public TextBulle textBulle;
    public MenuManagerTuto menuManager;
    public ShadowMaskGestion shadowMask;

    public GameObject MenuGO;
    public GameObject CombatGO;

    public int currentIndex = 0;
    public int numberOfmatch = 0;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(StartTuto());
    }

    private IEnumerator StartTuto()
    {
        menuTransition.TransitionSlideOut();
        yield return new WaitForSeconds(1f);
        textBulle.Initialisaition();
    }



}
