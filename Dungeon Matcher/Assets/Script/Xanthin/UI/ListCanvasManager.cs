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

        //changement de la State machine
        public void SwitchToMatchMenu()
        {
            MenuManager.currentGameState = MenuManager.gameState.Match;
        }

        //appeler à chaque match pour update le visuel du menu
        public void UpdateList()
        {
            listeState.text = "Taille " + MenuManager.Instance.listManager.GetComponent<ListManager>().listCurrentSize + " / " + MenuManager.Instance.listManager.GetComponent<ListManager>().listMaxSize[PlayerLevel.playerLevel-1];
        }

        //Instancier le profil à une position particulière. 
        public void InstantiateProfil()
        {
            GameObject instantiatedProfil =  Instantiate(listPrefab, listPosition[MenuManager.Instance.listManager.GetComponent<ListManager>().listCurrentSize - 1].transform.position, Quaternion.identity);
            instantiatedProfil.transform.SetParent(listPosition[MenuManager.Instance.listManager.GetComponent<ListManager>().listCurrentSize - 1].transform);
            instantiatedProfil.transform.localScale = new Vector3(1f, 1f, 1f);
            instantiatedProfil.GetComponent<CombatProfilList>().numberCombat = MenuManager.Instance.listManager.GetComponent<ListManager>().listCurrentSize;
            instantiatedProfil.GetComponent<CombatProfilList>().UpdateVisualMatch();
        }
    }

    

}

