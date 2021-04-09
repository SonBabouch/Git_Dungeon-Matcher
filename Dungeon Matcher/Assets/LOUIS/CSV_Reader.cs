using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CSV_Reader : MonoBehaviour
{
    public string playerFilePath = "PlayerMessages"; // va chercher directement dans le fichier Resources, donc prendre le path à partir de là. Ne PAS inclure l'extension du fichier (".csv").
    public string enemyFilePath = "EnemyMessages";
    public int indexAmount = 5; // combien d'index de message différents tu as;
    List<Dictionary<string, List<string>>> enemyDataStructure = new List<Dictionary<string, List<string>>>();
    List<Dictionary<string, List<string>>> playerDataStructure = new List<Dictionary<string, List<string>>>(); //une structure un peu complexe mais ça donne:
                                                                                                  
    //   Index 01 (dictionary)
    //    |
    //    |__ Capacité 01 (string)
    //    |       |
    //    |       |__ liste des phrases (1,2,3,4) (liste de string)
    //    |
    //    |__ Capacité 2
    //    |       |
    //    |       |__ liste des phrases(1,2,3,4)
    //    |
    //     etc
        

    

    private void Awake()
    {
        ReadPlayerCSV();
        ReadEnemyCSV();
    }

    void ReadPlayerCSV()
    {
        TextAsset t = Resources.Load<TextAsset>(playerFilePath);
        string[] lines = t.text.Split(new char[] { '\n' });

        // trouver le nombre de colonne
        int totalColumns = lines[0].Split(';').Length;

        // convertir le tableau .csv en un tableau 2D de string
        string[,] tableData = new string[totalColumns, lines.Length];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = lines[y].Split(';');
            for (int x = 0; x < row.Length; x++)
            {
                tableData[x, y] = row[x];
            }
        }

        //on initialise les listes dans notre structure de données
        for (int i = 0; i < indexAmount; i++)
        {
            playerDataStructure.Add(new Dictionary<string, List<string>>());
        }

        //on remplit ces listes avec les données du tableau
        for (int y = 1; y < tableData.GetLength(1)-1; y++)
        {
            Debug.Log(tableData[1, y]);
            int index = int.Parse(tableData[1, y]);
            if (!playerDataStructure[index].ContainsKey(tableData[2, y]))
            {
                List<string> randomLines = new List<string>();
                for (int l = 3; l < tableData.GetLength(0); l++)
                {
                    randomLines.Add(tableData[l, y]);
                }
                playerDataStructure[index].Add(tableData[2, y], randomLines);
            }
        }
    }

    void ReadEnemyCSV()
    {
        TextAsset t = Resources.Load<TextAsset>(enemyFilePath);
        string[] lines = t.text.Split(new char[] { '\n' });

        // trouver le nombre de colonne
        int totalColumns = lines[0].Split(';').Length;

        // convertir le tableau .csv en un tableau 2D de string
        string[,] tableData = new string[totalColumns, lines.Length];
        for (int y = 0; y < lines.Length; y++)
        {
            string[] row = lines[y].Split(';');
            for (int x = 0; x < row.Length; x++)
            {
                tableData[x, y] = row[x];
            }
        }

        //on initialise les listes dans notre structure de données
        for (int i = 0; i < indexAmount; i++)
        {
            enemyDataStructure.Add(new Dictionary<string, List<string>>());
        }

        //on remplit ces listes avec les données du tableau
        for (int y = 1; y < tableData.GetLength(1) - 1; y++)
        {
            Debug.Log(tableData[1, y]);
            int index = int.Parse(tableData[1, y]);
            if (!enemyDataStructure[index].ContainsKey(tableData[2, y]))
            {
                List<string> randomLines = new List<string>();
                for (int l = 3; l < tableData.GetLength(0); l++)
                {
                    randomLines.Add(tableData[l, y]);
                }
                enemyDataStructure[index].Add(tableData[2, y], randomLines);
            }
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("up"))
        {
            //GetRandomLineForEnemy(indexAmount, enemyDataStructure[indexAmount][]);

        }
    }

    public string GetRandomLineForPlayer(int playerMessageIndex, string playerAbility)
    {
        int randomIndex = Random.Range(0, playerDataStructure[1][playerAbility].Count);
        return playerDataStructure[1][playerAbility][randomIndex];
    }

    public string GetRandomLineForEnemy(int enemyMessageIndex, string enemyAbility)
    {
        int randomIndex = Random.Range(0, playerDataStructure[1][enemyAbility].Count);
        return playerDataStructure[1][enemyAbility][randomIndex];
    }

}
