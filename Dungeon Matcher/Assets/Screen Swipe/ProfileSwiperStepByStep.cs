using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ProfileSwiperStepByStep : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{

    //1- Variable

    private Vector2 panelLocation;
    private Quaternion panelRotation;

    public float percentThreshold = 0.45f;

    //1.1- Variable pour le mapping
    public float fromLowX = -250f;
    public float fromHighX = 250f;
    public float toLowQ = -25f;
    public float toHighQ = 25f;
    float properRotation;

    //2.1- Void Start
    void Start()
    {

        panelLocation = transform.position;
        panelRotation = transform.rotation;

    }

    //2.2- Drag on start
    public void OnBeginDrag(PointerEventData eventData)
    {

        print(eventData);

    }

    //3- On Drag void
    public void OnDrag(PointerEventData data)
    {

        //3.1- Récupération des donner X et Y 
        float differenceX = data.pressPosition.x - data.position.x;
        float differenceY = data.pressPosition.y - data.position.y;
        //print(differenceX);

        //3.2- Mapping du X en rotation
        float m = 0.05f; //formule de base m = (toHighQ - toLowQ) / (fromHighX - fromLowX);
        float c = fromLowX - m * toLowQ;
        properRotation = m * differenceX + c;

        //3.3- Modification rotation et position
        transform.position = panelLocation - new Vector2(differenceX, differenceY);

        Quaternion rotationToApply = Quaternion.Euler(0, 0, properRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationToApply, Time.deltaTime * 1);

    }

    //4- On End Drag
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

                print("Like");

            }
            else if (percentage < 0)//vers la gauche
            {

                print("Dislike");

            }

            panelLocation = newLocation; //Attribution de la nouvelle position du panel holder

        }
        else //Le swip n'est pas validé
        {

            transform.position = panelLocation;
            transform.rotation = panelRotation;

            print("position de base");

        }

    }


}
