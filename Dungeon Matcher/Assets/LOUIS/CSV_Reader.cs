using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CSV_Reader : MonoBehaviour
{
    public string playerFilePath = "PlayerMessages";
    public string enemyFilePath = "EnemyMessages";
    public int indexAmount = 5;
    List<Dictionary<string, List<string>>> enemyDataStructure = new List<Dictionary<string, List<string>>>();
    List<Dictionary<string, List<string>>> playerDataStructure = new List<Dictionary<string, List<string>>>();

    public string[] playerLines;
    public string[] enemyLines;

    private void Awake()
    {
        ReadPlayerCSV();
        ReadEnemyCSV();
    }

    void ReadPlayerCSV()
    {
        Debug.Log("coucou");
        TextAsset t = Resources.Load<TextAsset>(playerFilePath);
        playerLines = t.text.Split(new char[] { '\n' });
        playerLines[52] = "1; 2; 3; 4; 5; 6; 7";
        Debug.Log(playerLines);

        int totalColumns = playerLines[0].Split(';').Length;
        Debug.Log("totalColumns" + totalColumns);

        string[,] tableData = new string[totalColumns, playerLines.Length - 1];
        for (int y = 0; y < playerLines.Length - 1; y++)
        {
            string[] row = playerLines[y].Split(';');
            for (int x = 0; x < row.Length; x++)
            {
                tableData[x, y] = row[x];
            }
        }

        for (int i = 0; i < indexAmount; i++)
        {
            playerDataStructure.Add(new Dictionary<string, List<string>>());
        }

        //On remplit ces listes les données du tableau
        for (int y = 1; y < tableData.GetLength(1)-1; y++)
        {
            Debug.Log(tableData[1, y]);
            int index = int.Parse(tableData[1, y]);//Erreur
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
        enemyLines = t.text.Split(new char[] { '\n' });

        int totalColumns = enemyLines[0].Split(';').Length;

        string[,] tableData = new string[totalColumns, enemyLines.Length];
        for (int y = 0; y < enemyLines.Length; y++)
        {
            string[] row = enemyLines[y].Split(';');
            for (int x = 0; x < row.Length; x++)
            {
                tableData[x, y] = row[x];
            }
        }

        for (int i = 0; i < indexAmount; i++)
        {
            enemyDataStructure.Add(new Dictionary<string, List<string>>());
        }

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

    /*
    public void PutLinesInTabels()
    {
        MessageManager.instance.playerIndex1Attack = new List<string>(playerDataStructure[0]["Dégâts"].Count);
        MessageManager.instance.playerIndex2Attack = new List<string>(playerDataStructure[1]["Dégâts"].Count);
        MessageManager.instance.playerIndex3Attack = new List<string>(playerDataStructure[2]["Dégâts"].Count);
        MessageManager.instance.playerIndex4Attack = new List<string>(playerDataStructure[3]["Dégâts"].Count);
        MessageManager.instance.playerIndex5Attack = new List<string>(playerDataStructure[4]["Dégâts"].Count);

        MessageManager.instance.playerIndex1Defense = new List<string>(playerDataStructure[0]["Défense"].Count);
        MessageManager.instance.playerIndex2Defense = new List<string>(playerDataStructure[1]["Défense"].Count);
        MessageManager.instance.playerIndex3Defense = new List<string>(playerDataStructure[2]["Défense"].Count);
        MessageManager.instance.playerIndex4Defense = new List<string>(playerDataStructure[3]["Défense"].Count);
        MessageManager.instance.playerIndex5Defense = new List<string>(playerDataStructure[4]["Défense"].Count);

        MessageManager.instance.playerIndex1Heal = new List<string>(playerDataStructure[0]["Soins / Drain"].Count);
        MessageManager.instance.playerIndex2Heal = new List<string>(playerDataStructure[1]["Soins / Drain"].Count);
        MessageManager.instance.playerIndex3Heal = new List<string>(playerDataStructure[2]["Soins / Drain"].Count);
        MessageManager.instance.playerIndex4Heal = new List<string>(playerDataStructure[3]["Soins / Drain"].Count);
        MessageManager.instance.playerIndex5Heal = new List<string>(playerDataStructure[4]["Soins / Drain"].Count);

        MessageManager.instance.playerIndex1Break = new List<string>(playerDataStructure[0]["Break"].Count);
        MessageManager.instance.playerIndex2Break = new List<string>(playerDataStructure[1]["Break"].Count);
        MessageManager.instance.playerIndex3Break = new List<string>(playerDataStructure[2]["Break"].Count);
        MessageManager.instance.playerIndex4Break = new List<string>(playerDataStructure[3]["Break"].Count);
        MessageManager.instance.playerIndex5Break = new List<string>(playerDataStructure[4]["Break"].Count);

        MessageManager.instance.playerIndex1Drain = new List<string>(playerDataStructure[0]["Drain"].Count);
        MessageManager.instance.playerIndex2Drain = new List<string>(playerDataStructure[1]["Drain"].Count);
        MessageManager.instance.playerIndex3Drain = new List<string>(playerDataStructure[2]["Drain"].Count);
        MessageManager.instance.playerIndex4Drain = new List<string>(playerDataStructure[3]["Drain"].Count);
        MessageManager.instance.playerIndex5Drain = new List<string>(playerDataStructure[4]["Drain"].Count);

        MessageManager.instance.playerIndex1Paralysis = new List<string>(playerDataStructure[0]["Paralysie"].Count);
        MessageManager.instance.playerIndex2Paralysis = new List<string>(playerDataStructure[1]["Paralysie"].Count);
        MessageManager.instance.playerIndex3Paralysis = new List<string>(playerDataStructure[2]["Paralysie"].Count);
        MessageManager.instance.playerIndex4Paralysis = new List<string>(playerDataStructure[3]["Paralysie"].Count);
        MessageManager.instance.playerIndex5Paralysis = new List<string>(playerDataStructure[4]["Paralysie"].Count);
        
        MessageManager.instance.playerIndex1Charm = new List<string>(playerDataStructure[0]["Charme"].Count);
        MessageManager.instance.playerIndex2Charm = new List<string>(playerDataStructure[1]["Charme"].Count);
        MessageManager.instance.playerIndex3Charm = new List<string>(playerDataStructure[2]["Charme"].Count);
        MessageManager.instance.playerIndex4Charm = new List<string>(playerDataStructure[3]["Charme"].Count);
        MessageManager.instance.playerIndex5Charm = new List<string>(playerDataStructure[4]["Charme"].Count);

        MessageManager.instance.playerIndex1DivineTouch = new List<string>(playerDataStructure[0]["Toucher Divin"].Count);
        MessageManager.instance.playerIndex2DivineTouch = new List<string>(playerDataStructure[1]["Toucher Divin"].Count);
        MessageManager.instance.playerIndex3DivineTouch = new List<string>(playerDataStructure[2]["Toucher Divin"].Count);
        MessageManager.instance.playerIndex4DivineTouch = new List<string>(playerDataStructure[3]["Toucher Divin"].Count);
        MessageManager.instance.playerIndex5DivineTouch = new List<string>(playerDataStructure[4]["Toucher Divin"].Count);

        MessageManager.instance.playerIndex1Windstorm = new List<string>(playerDataStructure[0]["Coup de vent"].Count);
        MessageManager.instance.playerIndex2Windstorm = new List<string>(playerDataStructure[1]["Coup de vent"].Count);
        MessageManager.instance.playerIndex3Windstorm = new List<string>(playerDataStructure[2]["Coup de vent"].Count);
        MessageManager.instance.playerIndex4Windstorm = new List<string>(playerDataStructure[3]["Coup de vent"].Count);
        MessageManager.instance.playerIndex5Windstorm = new List<string>(playerDataStructure[4]["Coup de vent"].Count);

        MessageManager.instance.playerIndex1Charged = new List<string>(playerDataStructure[0]["ATK Chargée"].Count);
        MessageManager.instance.playerIndex2Charged = new List<string>(playerDataStructure[1]["ATK Chargée"].Count);
        MessageManager.instance.playerIndex3Charged = new List<string>(playerDataStructure[2]["ATK Chargée"].Count);
        MessageManager.instance.playerIndex4Charged = new List<string>(playerDataStructure[3]["ATK Chargée"].Count);
        MessageManager.instance.playerIndex5Charged = new List<string>(playerDataStructure[4]["ATK Chargée"].Count);


        MessageManager.instance.enemyIndex1Attack = new List<string>(enemyDataStructure[0]["Dégâts"].Count);
        MessageManager.instance.enemyIndex2Attack = new List<string>(enemyDataStructure[1]["Dégâts"].Count);
        MessageManager.instance.enemyIndex3Attack = new List<string>(enemyDataStructure[2]["Dégâts"].Count);
        MessageManager.instance.enemyIndex4Attack = new List<string>(enemyDataStructure[3]["Dégâts"].Count);
        MessageManager.instance.enemyIndex5Attack = new List<string>(enemyDataStructure[4]["Dégâts"].Count);

        MessageManager.instance.enemyIndex1Defense = new List<string>(enemyDataStructure[0]["Défense"].Count);
        MessageManager.instance.enemyIndex2Defense = new List<string>(enemyDataStructure[1]["Défense"].Count);
        MessageManager.instance.enemyIndex3Defense = new List<string>(enemyDataStructure[2]["Défense"].Count);
        MessageManager.instance.enemyIndex4Defense = new List<string>(enemyDataStructure[3]["Défense"].Count);
        MessageManager.instance.enemyIndex5Defense = new List<string>(enemyDataStructure[4]["Défense"].Count);

        MessageManager.instance.enemyIndex1Heal = new List<string>(enemyDataStructure[0]["Soins / Drain"].Count);
        MessageManager.instance.enemyIndex2Heal = new List<string>(enemyDataStructure[1]["Soins / Drain"].Count);
        MessageManager.instance.enemyIndex3Heal = new List<string>(enemyDataStructure[2]["Soins / Drain"].Count);
        MessageManager.instance.enemyIndex4Heal = new List<string>(enemyDataStructure[3]["Soins / Drain"].Count);
        MessageManager.instance.enemyIndex5Heal = new List<string>(enemyDataStructure[4]["Soins / Drain"].Count);

        MessageManager.instance.enemyIndex1Break = new List<string>(enemyDataStructure[0]["Break"].Count);
        MessageManager.instance.enemyIndex2Break = new List<string>(enemyDataStructure[1]["Break"].Count);
        MessageManager.instance.enemyIndex3Break = new List<string>(enemyDataStructure[2]["Break"].Count);
        MessageManager.instance.enemyIndex4Break = new List<string>(enemyDataStructure[3]["Break"].Count);
        MessageManager.instance.enemyIndex5Break = new List<string>(enemyDataStructure[4]["Break"].Count);

        MessageManager.instance.enemyIndex1Drain = new List<string>(enemyDataStructure[0]["Drain"].Count);
        MessageManager.instance.enemyIndex2Drain = new List<string>(enemyDataStructure[1]["Drain"].Count);
        MessageManager.instance.enemyIndex3Drain = new List<string>(enemyDataStructure[2]["Drain"].Count);
        MessageManager.instance.enemyIndex4Drain = new List<string>(enemyDataStructure[3]["Drain"].Count);
        MessageManager.instance.enemyIndex5Drain = new List<string>(enemyDataStructure[4]["Drain"].Count);

        MessageManager.instance.enemyIndex1Paralysis = new List<string>(enemyDataStructure[0]["Paralysie"].Count);
        MessageManager.instance.enemyIndex2Paralysis = new List<string>(enemyDataStructure[1]["Paralysie"].Count);
        MessageManager.instance.enemyIndex3Paralysis = new List<string>(enemyDataStructure[2]["Paralysie"].Count);
        MessageManager.instance.enemyIndex4Paralysis = new List<string>(enemyDataStructure[3]["Paralysie"].Count);
        MessageManager.instance.enemyIndex5Paralysis = new List<string>(enemyDataStructure[4]["Paralysie"].Count);

        MessageManager.instance.enemyIndex1Charm = new List<string>(enemyDataStructure[0]["Charme"].Count);
        MessageManager.instance.enemyIndex2Charm = new List<string>(enemyDataStructure[1]["Charme"].Count);
        MessageManager.instance.enemyIndex3Charm = new List<string>(enemyDataStructure[2]["Charme"].Count);
        MessageManager.instance.enemyIndex4Charm = new List<string>(enemyDataStructure[3]["Charme"].Count);
        MessageManager.instance.enemyIndex5Charm = new List<string>(enemyDataStructure[4]["Charme"].Count);

        MessageManager.instance.enemyIndex1DivineTouch = new List<string>(enemyDataStructure[0]["Toucher Divin"].Count);
        MessageManager.instance.enemyIndex2DivineTouch = new List<string>(enemyDataStructure[1]["Toucher Divin"].Count);
        MessageManager.instance.enemyIndex3DivineTouch = new List<string>(enemyDataStructure[2]["Toucher Divin"].Count);
        MessageManager.instance.enemyIndex4DivineTouch = new List<string>(enemyDataStructure[3]["Toucher Divin"].Count);
        MessageManager.instance.enemyIndex5DivineTouch = new List<string>(enemyDataStructure[4]["Toucher Divin"].Count);

        MessageManager.instance.enemyIndex1Windstorm = new List<string>(enemyDataStructure[0]["Coup de vent"].Count);
        MessageManager.instance.enemyIndex2Windstorm = new List<string>(enemyDataStructure[1]["Coup de vent"].Count);
        MessageManager.instance.enemyIndex3Windstorm = new List<string>(enemyDataStructure[2]["Coup de vent"].Count);
        MessageManager.instance.enemyIndex4Windstorm = new List<string>(enemyDataStructure[3]["Coup de vent"].Count);
        MessageManager.instance.enemyIndex5Windstorm = new List<string>(enemyDataStructure[4]["Coup de vent"].Count);

        MessageManager.instance.enemyIndex1Charged = new List<string>(enemyDataStructure[0]["ATK Chargée"].Count);
        MessageManager.instance.enemyIndex2Charged = new List<string>(enemyDataStructure[1]["ATK Chargée"].Count);
        MessageManager.instance.enemyIndex3Charged = new List<string>(enemyDataStructure[2]["ATK Chargée"].Count);
        MessageManager.instance.enemyIndex4Charged = new List<string>(enemyDataStructure[3]["ATK Chargée"].Count);
        MessageManager.instance.enemyIndex5Charged = new List<string>(enemyDataStructure[4]["ATK Chargée"].Count);

        //On attribue les valeurs aux tableaux
        playerDataStructure[0]["Dégâts"] = MessageManager.instance.playerIndex1Attack;
        playerDataStructure[1]["Dégâts"] = MessageManager.instance.playerIndex2Attack;
        playerDataStructure[2]["Dégâts"] = MessageManager.instance.playerIndex3Attack;
        playerDataStructure[3]["Dégâts"] = MessageManager.instance.playerIndex4Attack;
        playerDataStructure[4]["Dégâts"] = MessageManager.instance.playerIndex5Attack;

        playerDataStructure[0]["Défense"] = MessageManager.instance.playerIndex1Defense;
        playerDataStructure[1]["Défense"] = MessageManager.instance.playerIndex2Defense;
        playerDataStructure[2]["Défense"] = MessageManager.instance.playerIndex3Defense;
        playerDataStructure[3]["Défense"] = MessageManager.instance.playerIndex4Defense;
        playerDataStructure[4]["Défense"] = MessageManager.instance.playerIndex5Defense;

        playerDataStructure[0]["Soins / Drain"] = MessageManager.instance.playerIndex1Heal;
        playerDataStructure[1]["Soins / Drain"] = MessageManager.instance.playerIndex2Heal;
        playerDataStructure[2]["Soins / Drain"] = MessageManager.instance.playerIndex3Heal;
        playerDataStructure[3]["Soins / Drain"] = MessageManager.instance.playerIndex4Heal;
        playerDataStructure[4]["Soins / Drain"] = MessageManager.instance.playerIndex5Heal;

        playerDataStructure[0]["Break"] = MessageManager.instance.playerIndex1Break;
        playerDataStructure[1]["Break"] = MessageManager.instance.playerIndex2Break;
        playerDataStructure[2]["Break"] = MessageManager.instance.playerIndex3Break;
        playerDataStructure[3]["Break"] = MessageManager.instance.playerIndex4Break;
        playerDataStructure[4]["Break"] = MessageManager.instance.playerIndex5Break;

        playerDataStructure[0]["Drain"] = MessageManager.instance.playerIndex1Drain;
        playerDataStructure[1]["Drain"] = MessageManager.instance.playerIndex2Drain;
        playerDataStructure[2]["Drain"] = MessageManager.instance.playerIndex3Drain;
        playerDataStructure[3]["Drain"] = MessageManager.instance.playerIndex4Drain;
        playerDataStructure[4]["Drain"] = MessageManager.instance.playerIndex5Drain;

        playerDataStructure[0]["Paralysie"] = MessageManager.instance.playerIndex1Paralysis;
        playerDataStructure[1]["Paralysie"] = MessageManager.instance.playerIndex2Paralysis;
        playerDataStructure[2]["Paralysie"] = MessageManager.instance.playerIndex3Paralysis;
        playerDataStructure[3]["Paralysie"] = MessageManager.instance.playerIndex4Paralysis;
        playerDataStructure[4]["Paralysie"] = MessageManager.instance.playerIndex5Paralysis;

        playerDataStructure[0]["Charme"] = MessageManager.instance.playerIndex1Charm;
        playerDataStructure[1]["Charme"] = MessageManager.instance.playerIndex2Charm;
        playerDataStructure[2]["Charme"] = MessageManager.instance.playerIndex3Charm;
        playerDataStructure[3]["Charme"] = MessageManager.instance.playerIndex4Charm;
        playerDataStructure[4]["Charme"] = MessageManager.instance.playerIndex5Charm;

        playerDataStructure[0]["Coup de Vent"] = MessageManager.instance.playerIndex1Windstorm;
        playerDataStructure[1]["Coup de Vent"] = MessageManager.instance.playerIndex2Windstorm;
        playerDataStructure[2]["Coup de Vent"] = MessageManager.instance.playerIndex3Windstorm;
        playerDataStructure[3]["Coup de Vent"] = MessageManager.instance.playerIndex4Windstorm;
        playerDataStructure[4]["Coup de Vent"] = MessageManager.instance.playerIndex5Windstorm;

        playerDataStructure[0]["Toucher Divin"] = MessageManager.instance.playerIndex1DivineTouch;
        playerDataStructure[1]["Toucher Divin"] = MessageManager.instance.playerIndex2DivineTouch;
        playerDataStructure[2]["Toucher Divin"] = MessageManager.instance.playerIndex3DivineTouch;
        playerDataStructure[3]["Toucher Divin"] = MessageManager.instance.playerIndex4DivineTouch;
        playerDataStructure[4]["Toucher Divin"] = MessageManager.instance.playerIndex5DivineTouch;

        playerDataStructure[0]["ATK Chargée"] = MessageManager.instance.playerIndex1Charged;
        playerDataStructure[1]["ATK Chargée"] = MessageManager.instance.playerIndex2Charged;
        playerDataStructure[2]["ATK Chargée"] = MessageManager.instance.playerIndex3Charged;
        playerDataStructure[3]["ATK Chargée"] = MessageManager.instance.playerIndex4Charged;
        playerDataStructure[4]["ATK Chargée"] = MessageManager.instance.playerIndex5Charged;


        enemyDataStructure[0]["Dégâts"] = MessageManager.instance.enemyIndex1Attack;
        enemyDataStructure[1]["Dégâts"] = MessageManager.instance.enemyIndex2Attack;
        enemyDataStructure[2]["Dégâts"] = MessageManager.instance.enemyIndex3Attack;
        enemyDataStructure[3]["Dégâts"] = MessageManager.instance.enemyIndex4Attack;
        enemyDataStructure[4]["Dégâts"] = MessageManager.instance.enemyIndex5Attack;

        enemyDataStructure[0]["Défense"] = MessageManager.instance.enemyIndex1Defense;
        enemyDataStructure[1]["Défense"] = MessageManager.instance.enemyIndex2Defense;
        enemyDataStructure[2]["Défense"] = MessageManager.instance.enemyIndex3Defense;
        enemyDataStructure[3]["Défense"] = MessageManager.instance.enemyIndex4Defense;
        enemyDataStructure[4]["Défense"] = MessageManager.instance.enemyIndex5Defense;

        enemyDataStructure[0]["Soins / Drain"] = MessageManager.instance.enemyIndex1Heal;
        enemyDataStructure[1]["Soins / Drain"] = MessageManager.instance.enemyIndex2Heal;
        enemyDataStructure[2]["Soins / Drain"] = MessageManager.instance.enemyIndex3Heal;
        enemyDataStructure[3]["Soins / Drain"] = MessageManager.instance.enemyIndex4Heal;
        enemyDataStructure[4]["Soins / Drain"] = MessageManager.instance.enemyIndex5Heal;

        enemyDataStructure[0]["Break"] = MessageManager.instance.enemyIndex1Break;
        enemyDataStructure[1]["Break"] = MessageManager.instance.enemyIndex2Break;
        enemyDataStructure[2]["Break"] = MessageManager.instance.enemyIndex3Break;
        enemyDataStructure[3]["Break"] = MessageManager.instance.enemyIndex4Break;
        enemyDataStructure[4]["Break"] = MessageManager.instance.enemyIndex5Break;

        enemyDataStructure[0]["Drain"] = MessageManager.instance.enemyIndex1Drain;
        enemyDataStructure[1]["Drain"] = MessageManager.instance.enemyIndex2Drain;
        enemyDataStructure[2]["Drain"] = MessageManager.instance.enemyIndex3Drain;
        enemyDataStructure[3]["Drain"] = MessageManager.instance.enemyIndex4Drain;
        enemyDataStructure[4]["Drain"] = MessageManager.instance.enemyIndex5Drain;

        enemyDataStructure[0]["Paralysie"] = MessageManager.instance.enemyIndex1Paralysis;
        enemyDataStructure[1]["Paralysie"] = MessageManager.instance.enemyIndex2Paralysis;
        enemyDataStructure[2]["Paralysie"] = MessageManager.instance.enemyIndex3Paralysis;
        enemyDataStructure[3]["Paralysie"] = MessageManager.instance.enemyIndex4Paralysis;
        enemyDataStructure[4]["Paralysie"] = MessageManager.instance.enemyIndex5Paralysis;

        enemyDataStructure[0]["Charme"] = MessageManager.instance.enemyIndex1Charm;
        enemyDataStructure[1]["Charme"] = MessageManager.instance.enemyIndex2Charm;
        enemyDataStructure[2]["Charme"] = MessageManager.instance.enemyIndex3Charm;
        enemyDataStructure[3]["Charme"] = MessageManager.instance.enemyIndex4Charm;
        enemyDataStructure[4]["Charme"] = MessageManager.instance.enemyIndex5Charm;

        enemyDataStructure[0]["Coup de Vent"] = MessageManager.instance.enemyIndex1Windstorm;
        enemyDataStructure[1]["Coup de Vent"] = MessageManager.instance.enemyIndex2Windstorm;
        enemyDataStructure[2]["Coup de Vent"] = MessageManager.instance.enemyIndex3Windstorm;
        enemyDataStructure[3]["Coup de Vent"] = MessageManager.instance.enemyIndex4Windstorm;
        enemyDataStructure[4]["Coup de Vent"] = MessageManager.instance.enemyIndex5Windstorm;

        enemyDataStructure[0]["Toucher Divin"] = MessageManager.instance.enemyIndex1DivineTouch;
        enemyDataStructure[1]["Toucher Divin"] = MessageManager.instance.enemyIndex2DivineTouch;
        enemyDataStructure[2]["Toucher Divin"] = MessageManager.instance.enemyIndex3DivineTouch;
        enemyDataStructure[3]["Toucher Divin"] = MessageManager.instance.enemyIndex4DivineTouch;
        enemyDataStructure[4]["Toucher Divin"] = MessageManager.instance.enemyIndex5DivineTouch;

        enemyDataStructure[0]["ATK Chargée"] = MessageManager.instance.enemyIndex1Charged;
        enemyDataStructure[1]["ATK Chargée"] = MessageManager.instance.enemyIndex2Charged;
        enemyDataStructure[2]["ATK Chargée"] = MessageManager.instance.enemyIndex3Charged;
        enemyDataStructure[3]["ATK Chargée"] = MessageManager.instance.enemyIndex4Charged;
        enemyDataStructure[4]["ATK Chargée"] = MessageManager.instance.enemyIndex5Charged;
        //_______________________________________
        enemyDataStructure[0]["ATK Chargée"] = MessageManager.instance.enemyIndex1Charged;
    }
    */

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
