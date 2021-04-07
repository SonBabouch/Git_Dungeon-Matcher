using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisuelCombo : MonoBehaviour
{
    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        image.fillAmount = Player.Instance.comboTime / 100;
    }
}
