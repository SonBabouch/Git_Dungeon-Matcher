using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Collections;

namespace Management
{
    /// <summary>
    /// XP_ Ce sript gère toute la partie Update de la Liste etc etc...
    /// </summary>
    public class ListManager : MonoBehaviour
    {
        public int listCurrentSize;
        public int[] listMaxSize;

        public int currentTest = 0;

        public GameObject popButton;

        public List<GameObject> listPrefab = new List<GameObject>();

        public void TestClaim()
        {
            popButton.SetActive(false);

            if (currentTest <= MenuManager.Instance.matchManager.matchList.Count)
            {
                StartCoroutine(MenuManager.Instance.listManager.listPrefab[currentTest].GetComponent<CombatProfilList>().UpdateVisualEndCombat());
                currentTest++;
            }
            else
            {
                MenuManager.Instance.blockAction = true;
                currentTest = 0;

                for (int i = 0; i < listPrefab.Count; i++)
                {
                    StartCoroutine(listPrefab[i].GetComponent<CombatProfilList>().DispawnPrefab());
                }

               
                //C'est la fin.
                //Faire despawn tous les profils.

            }    
        }

    }  
}

