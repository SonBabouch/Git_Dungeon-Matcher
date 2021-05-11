using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleScale : MonoBehaviour
{
    private bool currentlyOnAnimation = false;

    // Update is called once per frame
    void Update()
    {
       if(Management.MenuManager.Instance.matchManager.matchList.Count == Management.MenuManager.Instance.listManager.listMaxSize[PlayerLevel.playerLevel - 1] && !currentlyOnAnimation && Management.MenuManager.currentGameStateMenu != Management.MenuManager.Menu.List)
       {
            Debug.Log("Start");
            currentlyOnAnimation = true;
            StartCoroutine(Scale());
       }
       else if(Management.MenuManager.currentGameStateMenu == Management.MenuManager.Menu.List)
       {
            StopCoroutine(Scale());
            gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
            currentlyOnAnimation = false;
       }
    }

    private IEnumerator Scale()
    {
        gameObject.transform.localScale = new Vector3(0f, 0f, 0f);
        Vector3 scaleVector = new Vector3(1.15f, 1.15f, 1.15f);
        gameObject.GetComponent<Tweener>().TweenScaleTo(scaleVector,1f,Easings.Ease.SmootherStep);
        yield return null;
        yield return new WaitForSeconds(1f);
        if (currentlyOnAnimation)
        {
            StartCoroutine(Scale());
        }
    }
}
