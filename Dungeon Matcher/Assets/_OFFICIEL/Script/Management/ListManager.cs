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
            StartCoroutine(TestClaimEnum());
        }

        //Tous les Tests ne se feront pas à l'écran car on affiche que 3 combat atm;
        private IEnumerator TestClaimEnum()
        {
            MenuManager.Instance.canvasManager.listCanvas.combatButton.SetActive(false);

            yield return new WaitForSeconds(1f);
            //Tout les tests ont été fait;
            if (currentTest == listCurrentSize)
            {
                currentTest = 0;
                popButton.SetActive(false);

                for (int i = 0; i < listPrefab.Count; i++)
                {
                    Vector3 scaleVector = new Vector3(0, 0, 0);
                    listPrefab[i].GetComponent<Tweener>().TweenScaleTo(scaleVector, 1f, Easings.Ease.SmoothStep);
                }
                yield return new WaitForSeconds(1f);

                for (int j = 0; j < listPrefab.Count; j++)
                {
                    GameObject objectToRemove = listPrefab[j];
                    listPrefab.Remove(objectToRemove);
                    Destroy(objectToRemove); 
                }
                
                
                for (int i = 0; i < MenuManager.Instance.matchManager.monsterSpawned.Count; i++)
                {
                    MenuManager.Instance.matchManager.monsterSpawned[i].GetComponent<ProfilBehaviour>().Initialisation(false);
                }

               
                
                //Full Reset
                listCurrentSize = 0;
                
                MenuTransitionCombat.Instance.storedValue = new List<float>();
                MenuManager.Instance.matchManager.matchList = new List<GameObject>();
                MenuManager.Instance.canvasManager.listCanvas.UpdateList();
                MenuManager.Instance.canvasManager.listCanvas.UpdateCombatButton();
                PlayerLevel.currentExperience += 7 * ((currentTest+1) * 2);
                MenuManager.Instance.playerLevel.CheckLevelUp();
                MenuManager.Instance.blockAction = false;
                PageSwiper.canChange = true;
            }
            else   //Il reste des tests à faire.
            {
                MenuManager.Instance.blockAction = true;
                PageSwiper.canChange = false;
                popButton.SetActive(false);

                int randomTest = Random.Range(0, 100);
                //Debug.Log(randomTest);
                CombatProfilList CPL = listPrefab[currentTest].GetComponent<CombatProfilList>();
                if (randomTest < CPL.chanceClaim)
                {
                    //Test Réussi;
                    CPL.monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Claim;
                    //afficher FeedBack
                    Vector3 scaleVectorFeedback = new Vector3(1, 1, 1);
                    CPL.claimFeedback.GetComponent<Tweener>().TweenScaleTo(scaleVectorFeedback, 1f, Easings.Ease.SmoothStep);
                }
                else
                {
                   
                    //Test Failed;
                    listPrefab[currentTest].GetComponent<CombatProfilList>().monsterContainer.GetComponent<MonsterToken>().statement = MonsterToken.statementEnum.Disponible;
                    Vector3 scaleVectorFeedback = new Vector3(1, 1, 1);
                    CPL.noClaimFeedback.GetComponent<Tweener>().TweenScaleTo(scaleVectorFeedback, 1f, Easings.Ease.SmoothStep);
                    //afficher FeedBack;
                }

                

                currentTest++;
                popButton.SetActive(true);
            }
        }
    }
}

