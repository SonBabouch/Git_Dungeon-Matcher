using UnityEngine;
using UnityEngine.SceneManagement;

namespace Management
{
    /// <summary>
    /// XP_ Ce sript gère toute la partie Update de la Liste etc etc...
    /// </summary>
    public class ListManager : MonoBehaviour
    {
        public int listCurrentSize;
        public int[] listMaxSize;

        public void StartCombat()
        {
            SceneManager.LoadScene("PRBA_TestingScene", LoadSceneMode.Single);
        }
    }
   
}

