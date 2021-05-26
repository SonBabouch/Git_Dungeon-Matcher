﻿using UnityEngine;
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
            if(currentTest == MenuManager.Instance.matchManager.matchList.Count)
            {
                currentTest = 0;
                MenuManager.Instance.blockAction = true;

                for (int i = 0; i < listPrefab.Count; i++)
                {
                    StartCoroutine(listPrefab[i].GetComponent<CombatProfilList>().DispawnPrefab());
                }

                StartCoroutine(EndAnimation());
                //Attendre 1 Secondes et redonner le controle au joueur.

                //C'est la fin.
                //Faire despawn tous les profils.

            }
            else
            {
                popButton.SetActive(false);
                StartCoroutine(MenuManager.Instance.listManager.listPrefab[currentTest].GetComponent<CombatProfilList>().UpdateVisualEndCombat());
                currentTest++;
            }
        }

        public IEnumerator EndAnimation()
        {
            yield return new WaitForSeconds(1f);
            for (int i = 0; i < MenuManager.Instance.listManager.listPrefab.Count; i++)
            {
                Destroy(MenuManager.Instance.listManager.listPrefab[i]);
            }
            popButton.SetActive(false);
            MenuManager.Instance.listManager.listPrefab.Clear();
            MenuManager.Instance.blockAction = false;
        }

    }
}

