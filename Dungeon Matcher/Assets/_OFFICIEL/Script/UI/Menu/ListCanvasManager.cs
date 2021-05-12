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
        [SerializeField] private TextMeshProUGUI currentSizeText;
        [SerializeField] private TextMeshProUGUI maxSizeText;
        public List<GameObject> listPosition = new List<GameObject>();
        [SerializeField] private GameObject parentPositions;
        public TextMeshProUGUI playerHealth;

        //Bas d'écran
        public GameObject combatButton;
        [SerializeField] private GameObject alerteCombat;
        [SerializeField] private TextMeshProUGUI alerteCombatText;

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

        private void Start()
        {
            UpdateCombatButton();
            UpdateList();
        }

        public void UpdateCombatButton()
        {
            if(MenuManager.Instance.matchManager.matchList.Count == 0)
            {
                alerteCombatText.text = "Va matcher un Monstre.";
            }
            else if(MenuManager.Instance.bagManager.GetComponent<BagManager>().monsterTeam.Count != 2)
            {
                alerteCombatText.text = "Ton équipe n'est pas complète.";
            }

            

            if (MenuManager.Instance.bagManager.GetComponent<BagManager>().monsterTeam.Count != 2)
            {
                alerteCombat.SetActive(true);
                combatButton.SetActive(false);
            }
            else if (MenuManager.Instance.bagManager.GetComponent<BagManager>().monsterTeam.Count == 2 && MenuManager.Instance.matchManager.matchList.Count >= 1)
            {
                combatButton.SetActive(true);
                alerteCombat.SetActive(false);
            }
        }

        //changement de la State machine
        public void SwitchToMatchMenu()
        {
            MenuManager.currentGameStateMenu = MenuManager.Menu.Match;
        }

        //appeler à chaque match pour update le visuel du menu
        public void UpdateList()
        {
            currentSizeText.text = MenuManager.Instance.listManager.listCurrentSize.ToString();
            maxSizeText.text = MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1].ToString();
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
            MenuManager.Instance.listManager.listPrefab.Add(instantiatedProfil);
        }

       
    }

    

}

