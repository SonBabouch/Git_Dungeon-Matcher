using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisuelCombo : MonoBehaviour
{
    private Image image;
    public bool isTuto = false;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTuto)
        {
            image.fillAmount = Player.Instance.comboTime / 100;
        }
        else
        {
            image.fillAmount = PlayerTuto.Instance.comboTime / 100;
        }

       
    }
}
