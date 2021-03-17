using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;


namespace Management
{
    /// <summary>
    /// XP_ Ce script referenci les objets composant l'UI du MENU MATCH. 
    /// </summary>
    public class MatchCanvasManager : MonoBehaviour
    {
        //Apparition du GameObject Profil à un point précis dans la scène.
        public GameObject profilPrefab;
        public GameObject spawnPosition;

        //Player Information
        public TextMeshProUGUI energy;
        public GameObject energySpend;
        public TextMeshProUGUI playerLevelText;
        public Image playerExperienceBar;

        //ShowExpérience
        public bool switchExp = false;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private Image experienceShower;

        public GameObject[] profilPosition;

        private bool zeroFinish = false;
        private bool oneFinish = false;
        
        void Start()
        {
            UpdateEnergy();
        }

        //Change le Game State selon le menu actuel
        public void SwitchToListMenu()
        {
            MenuManager.currentGameState = MenuManager.gameState.List;
        }

        //A appeler à chaque fois que le joueur perd ou gagne de l'energie.
        public void UpdateEnergy()
        {
            energy.text = EnergyManager.energy.ToString() + "/" + EnergyManager.maxEnergy.ToString();
        }

        //Update le constamment les infos car on peut pas traquer le moment ou le joueur passe de niveau.
        private void Update()
        {
            playerLevelText.text = "Niveau : " + PlayerLevel.playerLevel.ToString();
            playerExperienceBar.fillAmount = (PlayerLevel.currentExperience / MenuManager.Instance.playerLevel.requiredExperience[PlayerLevel.playerLevel - 1]);

            if(zeroFinish && oneFinish)
            {
                zeroFinish = false;
                oneFinish = false;
                MenuManager.Instance.matchManager.monsterSpawned[0].SetActive(true);
                MenuManager.Instance.matchManager.canMatch = true;
            }
        }

        public void ShowExpérience()
        {
            experienceText.text = PlayerLevel.currentExperience.ToString() + " / " + MenuManager.Instance.playerLevel.requiredExperience[PlayerLevel.playerLevel - 1].ToString();

            if (!switchExp)
            {
                switchExp = true;
            }
            else if (switchExp)
            {
                switchExp = false;
            }

            if (switchExp)
            {
                experienceShower.GetComponent<Animator>().SetTrigger("Show");

            }
            else
            {
                experienceShower.GetComponent<Animator>().SetTrigger("Unshow");

            }


        }

        public void UpdateFirstPosition()
        {
            for (int i = 0; i < MenuManager.Instance.matchManager.monsterSpawned.Count; i++)
            {
                MenuManager.Instance.matchManager.monsterSpawned[i].transform.SetParent(profilPosition[i].transform);
                MenuManager.Instance.matchManager.monsterSpawned[i].transform.localPosition = Vector3.zero;
            }
        }

        public void UpdateProfilPosition()
        {

            StartCoroutine(StarterCoroutine());

        }


        IEnumerator StarterCoroutine()
        {
            StartCoroutine(ProfilMovement(MenuManager.Instance.matchManager.monsterSpawned[2], 2));
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ProfilMovement(MenuManager.Instance.matchManager.monsterSpawned[1], 1));
        }

        IEnumerator ProfilMovement(GameObject currentProfil, int currentLoop)
        {
            if (currentProfil.transform.position.y < profilPosition[currentLoop].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, 1.2f, 0f);
                currentProfil.transform.Translate(translateVector);
                yield return new WaitForSeconds(0.001f);
                StartCoroutine(ProfilMovement(currentProfil, currentLoop));
            }
            else
            {
                if(currentLoop == 1)
                {
                    zeroFinish = true;
                }

                if(currentLoop == 2)
                {
                    oneFinish = true;
                }

                currentProfil.transform.SetParent(profilPosition[currentLoop].transform);
                currentProfil.transform.localPosition = Vector3.zero;
                
                yield return null;
            }
           

        }

    }     
    
    
}

