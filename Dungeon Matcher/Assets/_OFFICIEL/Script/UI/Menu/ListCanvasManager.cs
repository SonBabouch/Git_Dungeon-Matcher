using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Management
{
    /// <summary>
    /// XP_ Ce script permet de gérer le visuel du menu LIST.
    /// </summary>
    public class ListCanvasManager : MonoBehaviour
    {
        //Elements D'UI
        public TextMeshProUGUI listeState;
        public List<GameObject> listPosition = new List<GameObject>();
        [SerializeField] private GameObject parentPositions;

        //Bas d'écran
        [SerializeField] private GameObject combatButton;
        [SerializeField] private GameObject alerteCombat;

        //Prefab à instancier quand le joueur match
        [SerializeField] private GameObject listPrefab;

        private void Awake()
        {
            //Récupère tous les enfants de l'objet parent pour les ajouter dans un tableau.
            foreach (Transform child in parentPositions.transform)
            {
                listPosition.Add(child.gameObject);
            }
        }

        private void Update()
        {
            if(MenuManager.Instance.bagManager.GetComponent<BagManager>().monsterTeam.Count != 2 )
            {
                alerteCombat.SetActive(true);
                combatButton.SetActive(false);
            }
            else if(MenuManager.Instance.bagManager.GetComponent<BagManager>().monsterTeam.Count == 2 && MenuManager.Instance.matchManager.matchList.Count >=1)
            {
                combatButton.SetActive(true);
                alerteCombat.SetActive(false);
            }
        }

        //changement de la State machine
        public void SwitchToMatchMenu()
        {
            MenuManager.currentGameState = MenuManager.gameState.Match;
        }

        //appeler à chaque match pour update le visuel du menu
        public void UpdateList()
        {
            listeState.text = "Taille " + MenuManager.Instance.listManager.listCurrentSize + " / " + MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel-1];
        }

        //Instancier le profil à une position particulière. 
        public void InstantiateProfil()
        {
            GameObject instantiatedProfil =  Instantiate(listPrefab, listPosition[MenuManager.Instance.listManager.listCurrentSize - 1].transform.position, Quaternion.identity);
            instantiatedProfil.transform.SetParent(listPosition[MenuManager.Instance.listManager.listCurrentSize - 1].transform);
            instantiatedProfil.transform.localScale = new Vector3(1f, 1f, 1f);
            instantiatedProfil.GetComponent<CombatProfilList>().numberCombat = MenuManager.Instance.listManager.listCurrentSize;
            //Debug.Log("2");
            instantiatedProfil.GetComponent<CombatProfilList>().UpdateVisualMatch();
            
        }
    }

    

}

