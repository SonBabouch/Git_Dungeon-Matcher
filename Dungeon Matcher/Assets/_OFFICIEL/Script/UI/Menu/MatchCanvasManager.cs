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
        public GameObject[] profilPosition;

        //Player Information
        public TextMeshProUGUI energy;
        public GameObject energySpend;
        public TextMeshProUGUI playerLevelText;
        public Image playerExperienceBar;

        //ShowExpérience
        public bool switchExp = false;
        [SerializeField] private TextMeshProUGUI experienceText;
        [SerializeField] private Image experienceShower;

        //Animation Finish;
        private bool zeroFinish = false;
        private bool oneFinish = false;

        //rareStuff;
        [SerializeField] private Image rareBar;
        [SerializeField] private GameObject rareParticules;
        [SerializeField] private ParticleSystem rareSystemParticule;
        public bool ThereIsARare = false;

        //Marker
        public GameObject dislikeMarker;
        public GameObject likeMarker;

        //Advertissing dislike to much.
        [SerializeField] private GameObject advertising;
        public Tweener tweener;
        [SerializeField] private GameObject textBubble;
        [SerializeField] private GameObject buttonSkip;
        [SerializeField] private GameObject initialPosition;
        [SerializeField] private GameObject tweenPosition;
       
        void Start()
        {
            tweener = advertising.GetComponent<Tweener>();

            rareSystemParticule = rareParticules.GetComponent<ParticleSystem>();

            var emission = rareSystemParticule.emission;
            emission.enabled = false;

            UpdateEnergy();

            UpdateExperience();
        }

        //Change le Game State selon le menu actuel
        public void SwitchToListMenu()
        {
            MenuManager.currentGameStateMenu = MenuManager.Menu.List;
        }

        //A appeler à chaque fois que le joueur perd ou gagne de l'energie.
        public void UpdateEnergy()
        {
            energy.text = EnergyManager.energy.ToString() + "/" + EnergyManager.maxEnergy.ToString();
        }

        public void IncreaseRareBar()
        {
            int NeedToFill = 10;

            StartCoroutine(IncreaseBar(NeedToFill));
        }

        //Update le constamment les infos car on peut pas traquer le moment ou le joueur passe de niveau.
        private void Update()
        {
            if (zeroFinish && oneFinish)
            {
                zeroFinish = false;
                oneFinish = false;
                MenuManager.Instance.matchManager.monsterSpawned[0].SetActive(true);
                MenuManager.Instance.matchManager.canMatch = true;
            }
        }

        public void UpdateExperience()
        {
            playerLevelText.text = "Niveau : " + PlayerLevel.playerLevel.ToString();
            playerExperienceBar.fillAmount = (PlayerLevel.currentExperience / MenuManager.Instance.playerLevel.requiredExperience[PlayerLevel.playerLevel - 1]);
        }

        public void ResetBarMethod()
        {
            StartCoroutine(ResetBar());
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
            
            UpdateParticuleSystemRare();

            StartCoroutine(StarterCoroutine());

        }

        public void UpdateParticuleSystemRare()
        {
            if (ThereIsARare && MenuManager.currentGameStateMenu == MenuManager.Menu.Match)
            {
                var emission = rareSystemParticule.emission;
                emission.enabled = true;
            }
            else
            {
                var emission = rareSystemParticule.emission;
                emission.enabled = false;
            }
        }

        IEnumerator IncreaseBar(int lenght)
        {
            for (int i = 0; i < lenght; i++)
            {
                MenuManager.Instance.matchManager.ChanceCount++;
                rareBar.fillAmount +=0.01f; 
                yield return new WaitForSeconds(0.05f);
                
            }
        }

        IEnumerator ResetBar()
        {
            for (int i = 0; i < 100; i++)
            {
                rareBar.fillAmount -= 0.01f;
                yield return new WaitForSeconds(0.001f);
            }
            
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

        public IEnumerator alerteDislike()
        {
            //Debug.Log("wallah match");
            MenuManager.Instance.blockAction = true;
            PageSwiper.canChange = false;
            //Changer la boule pour désactiver les boutons.
            tweener.TweenPositionTo(tweenPosition.transform.localPosition,1f,Easings.Ease.SmoothStep,true);
            yield return new WaitForSeconds(1f);
            textBubble.SetActive(true);
            yield return new WaitForSeconds(1f);
            buttonSkip.SetActive(true);
            //Debug.Log("End");
        }

        public void AlerteDislike()
        {
            StartCoroutine(alerteDislike());
        }
        public void EndAlerteDislike()
        {
            StartCoroutine(endAlerteDislike());
        }

        public IEnumerator endAlerteDislike()
        {
            buttonSkip.SetActive(false);
            textBubble.SetActive(false);
            tweener.TweenPositionTo(initialPosition.transform.localPosition, 1f, Easings.Ease.SmoothStep, true);
            yield return new WaitForSeconds(1f);
            MenuManager.Instance.blockAction = false;
            PageSwiper.canChange = true;
            //changer la bool pour pouvoir réutiliser les boutons.
        }

    }     
    
    
}

