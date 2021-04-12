using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Management;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    public bool canChange = true;

    public bool onDrag = false;

    //1- Variables
    private Vector2 panelLocation; // vector2 pour la position du panel holder
    public float percentThreshold = 0.75f; //pourcentage de la surface de l'écran a drag
    public float easing = 0.5f; //seconds pour changer d'écran
    public int currentPanelNumber = 0;

    public GameObject[] panelLocations; 

    //2- Void Start
    void Start()
    {

        panelLocation = transform.position; //récupère les position du panel holder

    }

    //3- On Drag void
    public void OnDrag(PointerEventData data)
    {
        onDrag = true;
        MenuManager.Instance.canvasManager.matchCanvas.dislikeMarker.SetActive(false);
        MenuManager.Instance.canvasManager.matchCanvas.likeMarker.SetActive(false);

        float difference = data.pressPosition.x - data.position.x; //Différence entre les deux positions pour savoir vers quel coté on swip, positif à droite et négatif à gauche
        //C'est aussi la position exact du pointer sur le l'écran lorsqu'il drag

        if (currentPanelNumber > -1 && currentPanelNumber < 2)
        {

            transform.position = panelLocation - new Vector2(difference, 0); //Déplacer le panel holder en fonction de la position du pointer

        }
        else
        {
            transform.position = panelLocation;
        }
    }

    //4- On End Drag void
    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width; //calcul du pourcentage de l'écran dragué 

        //4.1- Vérification pour savoir si le pourcentage de l'écran dragué est superieur ou égale au pourcentage à drag pour valider la saisi
        if (Mathf.Abs(percentage) >= percentThreshold) //Le swip est validé
        {
            Vector2 newLocation = panelLocation; //on récupère la position du panel pour pouvoir la changer juste après

            //4.1.1- Modification de la futur position du panel holder vers le nouveau panel
            if (percentage > 0)//Vers la droite
            {
                if (currentPanelNumber < 2)
                {
                    newLocation += new Vector2(-Screen.width, 0);

                    currentPanelNumber = currentPanelNumber + 1;
                    UpdateStateAccordingPanelNumber();
                }
                else
                {
                    StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
                }

            }
            else if (percentage < 0)//vers la gauche
            {
                if (currentPanelNumber > -1)
                {
                    newLocation += new Vector2(Screen.width, 0);

                    currentPanelNumber = currentPanelNumber - 1;
                    UpdateStateAccordingPanelNumber();
                }
                else
                {
                    StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
                }

            }

            //4.1.2- Début de la coroutine pour faire une transition smooth vers la nouvelle position
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));

            panelLocation = newLocation; //Attribution de la nouvelle position du panel holder

        }
        else //Le swip n'est pas validé
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
            
        }
        onDrag = false;
    }

    void UpdateStateAccordingPanelNumber()
    {
        switch (currentPanelNumber)
        {   
            case -1:
                MenuManager.currentGameState = MenuManager.gameState.Shop;
                break;
            case 0:
                MenuManager.currentGameState = MenuManager.gameState.Match;
                break;
            case 1:
                MenuManager.currentGameState = MenuManager.gameState.List;
                break;
            case 2:
                MenuManager.currentGameState = MenuManager.gameState.Bag;
                break;
            default:
                break;
        }
    }

    void CancelMatch()
    {
        if (currentPanelNumber == 0 && canChange)
        {
            MenuManager.Instance.matchManager.canMatch = true;
        }
        else
        {
            MenuManager.Instance.matchManager.canMatch = false;
            MenuManager.Instance.canvasManager.matchCanvas.dislikeMarker.SetActive(false);
            MenuManager.Instance.canvasManager.matchCanvas.likeMarker.SetActive(false);
        }
    }

    //5- Coroutine pour une transition
    IEnumerator SmoothMove(Vector2 startPos, Vector2 endPos, float seconds)
    {
        canChange = false;
        CancelMatch();
        float t = 0f; //float pour le timer

        //Le timer
        while (t <= 1.0)
        {

            t += Time.deltaTime / seconds;

            transform.position = Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));

            yield return null;

        }
        panelLocation = endPos;
        canChange = true;
        MenuManager.Instance.canvasManager.matchCanvas.UpdateParticuleSystemRare();
        CancelMatch();
    }

    public void GoToList()
    {
        if (canChange)
        {
            Vector2 newLocation = panelLocation;

            switch (currentPanelNumber)
            {
                case -1:
                    //Je viens du Shop (donc de la gauche);
                    //Anim *2
                   
                    newLocation += new Vector2(-Screen.width * 2, 0);
                    currentPanelNumber = 1;
                    
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 2));
                    MenuManager.currentGameState = MenuManager.gameState.List;
                    break;

                case 0:
                    //Je viens du Match (donc de la gauche);
                    //Anim *1
                    
                    newLocation += new Vector2(-Screen.width, 0);
                    currentPanelNumber = 1;
                    
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 1));
                    MenuManager.currentGameState = MenuManager.gameState.List;
                    break;

                case 2:
                    //Je viens du Bag (donc de la droite);
                    //Anim *1
                    
                    newLocation += new Vector2(Screen.width, 0);
                    currentPanelNumber = 1;
                    
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 1));
                    MenuManager.currentGameState = MenuManager.gameState.List;
                    break;
                default:
                    break;
            }
            
        }
        
    }

    public void GoToBag()
    {
        if (canChange)
        {
            Vector2 newLocation = panelLocation;

            switch (currentPanelNumber)
            {
                case -1:
                   
                    //Je viens du Shop (donc de la gauche);
                    //Anim *3
                    newLocation += new Vector2(-Screen.width * 3, 0);
                    currentPanelNumber = 2;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 3));
                    MenuManager.currentGameState = MenuManager.gameState.Bag;
                    break;

                case 0:
                    //Je viens du Match (donc de la gauche);
                    //Anim *2
                    
                    newLocation += new Vector2(-Screen.width * 2, 0);
                    currentPanelNumber = 2;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 2));
                    MenuManager.currentGameState = MenuManager.gameState.Bag;
                    break;

                case 1:
                    //Je viens du list (donc de la gauche);
                    //Anim *1
                   
                    newLocation += new Vector2(-Screen.width, 0);
                    currentPanelNumber = 2;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 1));
                    MenuManager.currentGameState = MenuManager.gameState.Bag;
                    break;
                default:
                    break;
            }
        }
        
    }

    public void GoToShop()
    {
        if (canChange)
        {
            Vector2 newLocation = panelLocation;

            switch (currentPanelNumber)
            {
                case 2:
                    //Je viens du Bag (donc de la gauche);
                    //Anim *2
                   
                    newLocation += new Vector2(Screen.width * 3, 0);
                    currentPanelNumber = -1;
                    Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 3));
                    MenuManager.currentGameState = MenuManager.gameState.Shop;
                    break;

                case 1:
                    //Je viens du List (donc de la gauche);
                    //Anim *1
                    
                    newLocation += new Vector2(Screen.width * 2, 0);
                    currentPanelNumber = -1;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 2));
                    MenuManager.currentGameState = MenuManager.gameState.Shop;
                    break;

                case 0:
                    //Je viens du match (donc de la droite);
                    //Anim *1
                   
                    newLocation += new Vector2(Screen.width, 0);
                    currentPanelNumber = -1;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 1));
                    MenuManager.currentGameState = MenuManager.gameState.Shop;
                    break;
                default:
                    break;
            }
            
        }
        
    }

    public void GoToMatch()
    {
        if (canChange)
        {
            Vector2 newLocation = panelLocation;

            switch (currentPanelNumber)
            {
                case -1:
                    //Je viens du Shop (donc de la gauche);
                    //Anim *2
                   
                    newLocation += new Vector2(-Screen.width, 0);
                    currentPanelNumber = 0;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing));
                    MenuManager.currentGameState = MenuManager.gameState.Match;
                    break;

                case 1:
                    //Je viens de list (donc de la droite);
                    //Anim *1
                    
                    newLocation += new Vector2(Screen.width, 0);
                    currentPanelNumber = 0;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing));
                    MenuManager.currentGameState = MenuManager.gameState.Match;
                    break;

                case 2:
                    //Je viens du Shop (donc de la droite);
                    //Anim *1
                   
                    newLocation += new Vector2(Screen.width * 2, 0);
                    currentPanelNumber = 0;
                    //Debug.Log("Called");
                    StartCoroutine(SmoothMove(transform.position, newLocation, easing * 2));
                    MenuManager.currentGameState = MenuManager.gameState.Match;
                    break;
                default:
                    break;
            }
            
        }

        

    }
}
