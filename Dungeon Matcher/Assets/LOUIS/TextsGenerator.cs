using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextsGenerator : MonoBehaviour
{
    private void Awake()
    {
        List<Dictionary<string, object>> data = CSVReader.Read("NeutralExcel");
        for (var i = 0; i < data.Count; i++)
        {
            print("nomenclature" + data[i]["nomenclature"] + " " + "index" + data[i]["index"] + " " + "capacite" + data[i]["capacite"] + " " + "P1" + data[i]["P1"] + " " + "P2" + data[i]["P2"] + " " + "P3" + data[i]["P3"] + " " + "P4" + data[i]["P4"] + " ");
        }
    }
}
