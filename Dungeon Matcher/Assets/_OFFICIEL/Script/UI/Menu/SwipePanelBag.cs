using UnityEngine.EventSystems;
using UnityEngine;
using Management;

public class SwipePanelBag : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;

    [SerializeField] private GameObject maxPosition;
    [SerializeField] private GameObject minPosition;

    // Start is called before the first frame update
    void Start()
    {
        panelLocation = transform.localPosition;
    }

    public void OnDrag(PointerEventData data)
    {
        if(MenuManager.Instance.bagManager.GetComponent<BagManager>().detailShow == false)
        {
            float difference = data.pressPosition.y - data.position.y;

            transform.localPosition = panelLocation - new Vector3(0, difference, 0);

            /*if (transform.localPosition.y > maxPosition.transform.position.y)
            {
                transform.localPosition = new Vector3(0,maxPosition.transform.position.y,0);
            }

            if (panelLocation.y < minPosition.transform.position.y)
            {
                transform.localPosition = new Vector3(0, minPosition.transform.position.y, 0);
            }*/
        }
    }





    public void OnEndDrag(PointerEventData data)
    { 
            //Debug.Log(panelLocation);
            panelLocation = transform.localPosition;

    }
}
