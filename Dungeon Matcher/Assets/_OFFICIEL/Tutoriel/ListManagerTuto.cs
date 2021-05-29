using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListManagerTuto : MonoBehaviour
{
    public int listCurrentSize;
    public int[] listMaxSize;

    public int currentTest = 0;

    public GameObject popButton;

    public List<GameObject> listPrefab = new List<GameObject>();

    public void TestClaim()
    {
        if (currentTest == MenuManagerTuto.Instance.matchManager.matchList.Count)
        {
            currentTest = 0;
            MenuManagerTuto.Instance.blockAction = true;

            for (int i = 0; i < listPrefab.Count; i++)
            {
                StartCoroutine(listPrefab[i].GetComponent<CombatProfilList>().DispawnPrefab());
            }

            StartCoroutine(EndAnimation());
        }
        else
        {
            MenuManagerTuto.Instance.blockAction = true;
            popButton.SetActive(false);
            StartCoroutine(MenuManagerTuto.Instance.listManager.listPrefab[currentTest].GetComponent<CombatProfilList>().UpdateVisualEndCombat());
            currentTest++;
        }
    }

    public IEnumerator EndAnimation()
    {
        popButton.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < MenuManagerTuto.Instance.listManager.listPrefab.Count; i++)
        {
            Destroy(MenuManagerTuto.Instance.listManager.listPrefab[i]);
        }

        float value = 1f;

        MatchSoundManager.Instance.resultsScreen = false;
        StartCoroutine(MenuManagerTuto.Instance.canvasManager.ScreenFade(value, MenuManagerTuto.Instance.canvasManager.listCanvas.BackGroundResultat));
        StartCoroutine(MenuManagerTuto.Instance.canvasManager.TextFade(value, MenuManagerTuto.Instance.canvasManager.listCanvas.BackGroundResultatText));


        MenuManagerTuto.Instance.listManager.listPrefab.Clear();
        MenuManagerTuto.Instance.matchManager.matchList.Clear();
        MenuManagerTuto.Instance.listManager.listCurrentSize = 0;
        currentTest = 0;
        MenuManagerTuto.Instance.canvasManager.listCanvas.UpdateList();
        MenuManagerTuto.Instance.blockAction = false;
    }
}
