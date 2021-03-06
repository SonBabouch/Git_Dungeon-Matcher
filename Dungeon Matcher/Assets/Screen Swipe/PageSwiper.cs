using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{

    //1- Variables
    private Vector2 panelLocation; // vector2 pour la position du panel holder
    public float percentThreshold = 0.75f; //pourcentage de la surface de l'écran a drag
    public float easing = 0.5f; //seconds pour changer d'écran
    public int currentPanelNumber = 0;

    //2- Void Start
    void Start()
    {

        panelLocation = transform.position; //récupère les position du panel holder

    }

    //3- On Drag void
    public void OnDrag(PointerEventData data)
    {

        float difference = data.pressPosition.x - data.position.x; //Différence entre les deux positions pour savoir vers quel coté on swip, positif à droite et négatif à gauche
        //C'est aussi la position exact du pointer sur le l'écran lorsqu'il drag

        if (currentPanelNumber > -1 && currentPanelNumber < 2)
        {

            transform.position = panelLocation - new Vector2(difference, 0); //Déplacer le pannel holder en focntion de la position du pointer

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

    }

    //5- Coroutine pour une transition
    IEnumerator SmoothMove(Vector2 startPos, Vector2 endPos, float seconds)
    {

        float t = 0f; //float pour le timer

        //Le timer
        while (t <= 1.0)
        {

            t += Time.deltaTime / seconds;

            transform.position = Vector2.Lerp(startPos, endPos, Mathf.SmoothStep(0f, 1f, t));

            yield return null;

        }

    }
}
