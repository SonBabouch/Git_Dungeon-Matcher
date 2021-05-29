using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSV_ReaderAlt : MonoBehaviour
{
    public bool isTuto;

    public string[] playerLines;
    public string[] enemyLines;

    public string[] playerLinesAlt;
    public string[] enemyLinesAlt;

    public string playerFilePath = "PlayerMessages";
    public string enemyFilePath = "EnemyMessages";

    private void Start()
    {
        if (!isTuto)
        {
            ReadPlayerCSV();
            ReadEnemyCSV();
        }
        else
        {
            ReadPlayerCSVTuto();
            ReadEnemyCSVTuto();
        }
    }

    void ReadPlayerCSVTuto()
    {
        //1 : Récupère notre fichier.
        TextAsset t = Resources.Load<TextAsset>(playerFilePath);
        //2 : Split les lignes du CSV. (Au total 52).
        playerLines = t.text.Split(new char[] { '\n' });
        //3 : La dernière pause problème, on lui injecte des valeurs.
        playerLines[53] = "1; 2; 3; 4; 5; 6; 7";
        //Debug.Log(playerLines);

        for (int i = 1; i < playerLines.Length - 1; i++)
        {
            //int totalColumns = playerLines[0].Split(';').Length; //7;

            playerLinesAlt = playerLines[i].Split(';');

            // charged / Break / Charme / Windstorm / Defense / Dégats / Drains / Para / Heal / DivineTouch

            switch (i)
            {
                case 1:
                    MessageManager.instance.playerIndex1Charged = playerLinesAlt;
                    break;
                case 2:
                    MessageManager.instance.playerIndex2Charged = playerLinesAlt;
                    break;
                case 3:
                    MessageManager.instance.playerIndex3Charged = playerLinesAlt;
                    break;
                case 4:
                    MessageManager.instance.playerIndex4Charged = playerLinesAlt;
                    break;
                case 5:
                    MessageManager.instance.playerIndex5Charged = playerLinesAlt;
                    break;
                case 6:
                    MessageManager.instance.playerIndex1Break = playerLinesAlt;
                    break;
                case 7:
                    MessageManager.instance.playerIndex2Break = playerLinesAlt;
                    break;
                case 8:
                    MessageManager.instance.playerIndex3Break = playerLinesAlt;
                    break;
                case 9:
                    MessageManager.instance.playerIndex4Break = playerLinesAlt;
                    break;
                case 10:
                    MessageManager.instance.playerIndex5Break = playerLinesAlt;
                    break;
                case 11:
                    MessageManager.instance.playerIndex1Charm = playerLinesAlt;
                    break;
                case 12:
                    MessageManager.instance.playerIndex2Charm = playerLinesAlt;
                    break;
                case 13:
                    MessageManager.instance.playerIndex3Charm = playerLinesAlt;
                    break;
                case 14:
                    MessageManager.instance.playerIndex4Charm = playerLinesAlt;
                    break;
                case 15:
                    MessageManager.instance.playerIndex5Charm = playerLinesAlt;
                    break;
                case 16:
                    MessageManager.instance.playerIndex1Windstorm = playerLinesAlt;
                    break;
                case 17:
                    MessageManager.instance.playerIndex2Windstorm = playerLinesAlt;
                    break;
                case 18:
                    MessageManager.instance.playerIndex3Windstorm = playerLinesAlt;
                    break;
                case 19:
                    MessageManager.instance.playerIndex4Windstorm = playerLinesAlt;
                    break;
                case 20:
                    MessageManager.instance.playerIndex5Windstorm = playerLinesAlt;
                    break;
                case 21:
                    MessageManager.instance.playerIndex1Defense = playerLinesAlt;
                    break;
                case 22:
                    MessageManager.instance.playerIndex2Defense = playerLinesAlt;
                    break;
                case 23:
                    MessageManager.instance.playerIndex3Defense = playerLinesAlt;
                    break;
                case 24:
                    MessageManager.instance.playerIndex4Defense = playerLinesAlt;
                    break;
                case 25:
                    MessageManager.instance.playerIndex5Defense = playerLinesAlt;
                    break;
                case 26:
                    MessageManager.instance.playerIndex1Attack = playerLinesAlt;
                    break;
                case 27:
                    MessageManager.instance.playerIndex2Attack = playerLinesAlt;
                    break;
                case 28:
                    MessageManager.instance.playerIndex3Attack = playerLinesAlt;
                    break;
                case 29:
                    MessageManager.instance.playerIndex4Attack = playerLinesAlt;
                    break;
                case 30:
                    MessageManager.instance.playerIndex5Attack = playerLinesAlt;
                    break;
                case 31:
                    MessageManager.instance.playerIndex1Drain = playerLinesAlt;
                    break;
                case 32:
                    MessageManager.instance.playerIndex2Drain = playerLinesAlt;
                    break;
                case 33:
                    MessageManager.instance.playerIndex3Drain = playerLinesAlt;
                    break;
                case 34:
                    MessageManager.instance.playerIndex4Drain = playerLinesAlt;
                    break;
                case 35:
                    MessageManager.instance.playerIndex5Drain = playerLinesAlt;
                    break;
                case 36:
                    MessageManager.instance.playerIndex1Paralysis = playerLinesAlt;
                    break;
                case 37:
                    MessageManager.instance.playerIndex2Paralysis = playerLinesAlt;
                    break;
                case 38:
                    MessageManager.instance.playerIndex3Paralysis = playerLinesAlt;
                    break;
                case 39:
                    MessageManager.instance.playerIndex4Paralysis = playerLinesAlt;
                    break;
                case 40:
                    MessageManager.instance.playerIndex5Paralysis = playerLinesAlt;
                    break;
                case 41:
                    MessageManager.instance.playerIndex1Heal = playerLinesAlt;
                    break;
                case 42:
                    MessageManager.instance.playerIndex2Heal = playerLinesAlt;
                    break;
                case 43:
                    MessageManager.instance.playerIndex3Heal = playerLinesAlt;
                    break;
                case 44:
                    MessageManager.instance.playerIndex4Heal = playerLinesAlt;
                    break;
                case 45:
                    MessageManager.instance.playerIndex5Heal = playerLinesAlt;
                    break;
                case 46:
                    MessageManager.instance.playerIndex1DivineTouch = playerLinesAlt;
                    break;
                case 47:
                    MessageManager.instance.playerIndex2DivineTouch = playerLinesAlt;
                    break;
                case 48:
                    MessageManager.instance.playerIndex3DivineTouch = playerLinesAlt;
                    break;
                case 49:
                    MessageManager.instance.playerIndex4DivineTouch = playerLinesAlt;
                    break;
                case 50:
                    MessageManager.instance.playerIndex5DivineTouch = playerLinesAlt;
                    break;
                case 52:
                    MessageManager.instance.cursedMessagePlayer = playerLinesAlt;
                    break;
                default:
                    break;
            }



        }
    }
    void ReadEnemyCSVTuto()
    {
        //1 : Récupère notre fichier.
        TextAsset t = Resources.Load<TextAsset>(enemyFilePath);
        //2 : Split les lignes du CSV. (Au total 52).
        enemyLines = t.text.Split(new char[] { '\n' });
        //3 : La dernière pause problème, on lui injecte des valeurs.
        enemyLines[52] = "1; 2; 3; 4; 5; 6; 7";
        //Debug.Log(playerLines);

        for (int i = 1; i < enemyLines.Length - 1; i++)
        {
            //int totalColumns = playerLines[0].Split(';').Length; //7;

            enemyLinesAlt = enemyLines[i].Split(';');

            // charged / Break / Charme / Windstorm / Defense / Dégats / Drains / Para / Heal / DivineTouch

            switch (i)
            {
                case 1:
                    MessageManager.instance.enemyIndex1Charged = enemyLinesAlt;
                    break;
                case 2:
                    MessageManager.instance.enemyIndex2Charged = enemyLinesAlt;
                    break;
                case 3:
                    MessageManager.instance.enemyIndex3Charged = enemyLinesAlt;
                    break;
                case 4:
                    MessageManager.instance.enemyIndex4Charged = enemyLinesAlt;
                    break;
                case 5:
                    MessageManager.instance.enemyIndex5Charged = enemyLinesAlt;
                    break;
                case 6:
                    MessageManager.instance.enemyIndex1Break = enemyLinesAlt;
                    break;
                case 7:
                    MessageManager.instance.enemyIndex2Break = enemyLinesAlt;
                    break;
                case 8:
                    MessageManager.instance.enemyIndex3Break = enemyLinesAlt;
                    break;
                case 9:
                    MessageManager.instance.enemyIndex4Break = enemyLinesAlt;
                    break;
                case 10:
                    MessageManager.instance.enemyIndex5Break = enemyLinesAlt;
                    break;
                case 11:
                    MessageManager.instance.enemyIndex1Charm = enemyLinesAlt;
                    break;
                case 12:
                    MessageManager.instance.enemyIndex2Charm = enemyLinesAlt;
                    break;
                case 13:
                    MessageManager.instance.enemyIndex3Charm = enemyLinesAlt;
                    break;
                case 14:
                    MessageManager.instance.enemyIndex4Charm = enemyLinesAlt;
                    break;
                case 15:
                    MessageManager.instance.enemyIndex5Charm = enemyLinesAlt;
                    break;
                case 16:
                    MessageManager.instance.enemyIndex1Windstorm = enemyLinesAlt;
                    break;
                case 17:
                    MessageManager.instance.enemyIndex2Windstorm = enemyLinesAlt;
                    break;
                case 18:
                    MessageManager.instance.enemyIndex3Windstorm = enemyLinesAlt;
                    break;
                case 19:
                    MessageManager.instance.enemyIndex4Windstorm = enemyLinesAlt;
                    break;
                case 20:
                    MessageManager.instance.enemyIndex5Windstorm = enemyLinesAlt;
                    break;
                case 21:
                    MessageManager.instance.enemyIndex1Defense = enemyLinesAlt;
                    break;
                case 22:
                    MessageManager.instance.enemyIndex2Defense = enemyLinesAlt;
                    break;
                case 23:
                    MessageManager.instance.enemyIndex3Defense = enemyLinesAlt;
                    break;
                case 24:
                    MessageManager.instance.enemyIndex4Defense = enemyLinesAlt;
                    break;
                case 25:
                    MessageManager.instance.enemyIndex5Defense = enemyLinesAlt;
                    break;
                case 26:
                    MessageManager.instance.enemyIndex1Attack = enemyLinesAlt;
                    break;
                case 27:
                    MessageManager.instance.enemyIndex2Attack = enemyLinesAlt;
                    break;
                case 28:
                    MessageManager.instance.enemyIndex3Attack = enemyLinesAlt;
                    break;
                case 29:
                    MessageManager.instance.enemyIndex4Attack = enemyLinesAlt;
                    break;
                case 30:
                    MessageManager.instance.enemyIndex5Attack = enemyLinesAlt;
                    break;
                case 31:
                    MessageManager.instance.enemyIndex1Drain = enemyLinesAlt;
                    break;
                case 32:
                    MessageManager.instance.enemyIndex2Drain = enemyLinesAlt;
                    break;
                case 33:
                    MessageManager.instance.enemyIndex3Drain = enemyLinesAlt;
                    break;
                case 34:
                    MessageManager.instance.enemyIndex4Drain = enemyLinesAlt;
                    break;
                case 35:
                    MessageManager.instance.enemyIndex5Drain = enemyLinesAlt;
                    break;
                case 36:
                    MessageManager.instance.enemyIndex1Paralysis = enemyLinesAlt;
                    break;
                case 37:
                    MessageManager.instance.enemyIndex2Paralysis = enemyLinesAlt;
                    break;
                case 38:
                    MessageManager.instance.enemyIndex3Paralysis = enemyLinesAlt;
                    break;
                case 39:
                    MessageManager.instance.enemyIndex4Paralysis = enemyLinesAlt;
                    break;
                case 40:
                    MessageManager.instance.enemyIndex5Paralysis = enemyLinesAlt;
                    break;
                case 41:
                    MessageManager.instance.enemyIndex1Heal = enemyLinesAlt;
                    break;
                case 42:
                    MessageManager.instance.enemyIndex2Heal = enemyLinesAlt;
                    break;
                case 43:
                    MessageManager.instance.enemyIndex3Heal = enemyLinesAlt;
                    break;
                case 44:
                    MessageManager.instance.enemyIndex4Heal = enemyLinesAlt;
                    break;
                case 45:
                    MessageManager.instance.enemyIndex5Heal = enemyLinesAlt;
                    break;
                case 46:
                    MessageManager.instance.enemyIndex1DivineTouch = enemyLinesAlt;
                    break;
                case 47:
                    MessageManager.instance.enemyIndex2DivineTouch = enemyLinesAlt;
                    break;
                case 48:
                    MessageManager.instance.enemyIndex3DivineTouch = enemyLinesAlt;
                    break;
                case 49:
                    MessageManager.instance.enemyIndex4DivineTouch = enemyLinesAlt;
                    break;
                case 50:
                    MessageManager.instance.enemyIndex5DivineTouch = enemyLinesAlt;
                    break;
                default:
                    break;
            }



        }
    }

    
    void ReadPlayerCSV()
    {
        //1 : Récupère notre fichier.
        TextAsset t = Resources.Load<TextAsset>(playerFilePath);
        //2 : Split les lignes du CSV. (Au total 52).
        playerLines = t.text.Split(new char[] { '\n' });
        //3 : La dernière pause problème, on lui injecte des valeurs.
        playerLines[53] = "1; 2; 3; 4; 5; 6; 7";
        //Debug.Log(playerLines);

        for (int i = 1; i < playerLines.Length-1; i++)
        {
            //int totalColumns = playerLines[0].Split(';').Length; //7;

            playerLinesAlt = playerLines[i].Split(';');

            // charged / Break / Charme / Windstorm / Defense / Dégats / Drains / Para / Heal / DivineTouch

            switch (i)
            {
                case 1:
                    MessageManager.instance.playerIndex1Charged = playerLinesAlt;
                    break;
                case 2:
                    MessageManager.instance.playerIndex2Charged = playerLinesAlt;
                    break;
                case 3:
                    MessageManager.instance.playerIndex3Charged = playerLinesAlt;
                    break;
                case 4:
                    MessageManager.instance.playerIndex4Charged = playerLinesAlt;
                    break;
                case 5:
                    MessageManager.instance.playerIndex5Charged = playerLinesAlt;
                    break;
                case 6:
                    MessageManager.instance.playerIndex1Break = playerLinesAlt;
                    break;
                case 7:
                    MessageManager.instance.playerIndex2Break = playerLinesAlt;
                    break;
                case 8:
                    MessageManager.instance.playerIndex3Break = playerLinesAlt;
                    break;
                case 9:
                    MessageManager.instance.playerIndex4Break = playerLinesAlt;
                    break;
                case 10:
                    MessageManager.instance.playerIndex5Break = playerLinesAlt;
                    break;
                case 11:
                    MessageManager.instance.playerIndex1Charm = playerLinesAlt;
                    break;
                case 12:
                    MessageManager.instance.playerIndex2Charm = playerLinesAlt;
                    break;
                case 13:
                    MessageManager.instance.playerIndex3Charm = playerLinesAlt;
                    break;
                case 14:
                    MessageManager.instance.playerIndex4Charm = playerLinesAlt;
                    break;
                case 15:
                    MessageManager.instance.playerIndex5Charm = playerLinesAlt;
                    break;
                case 16:
                    MessageManager.instance.playerIndex1Windstorm = playerLinesAlt;
                    break;
                case 17:
                    MessageManager.instance.playerIndex2Windstorm = playerLinesAlt;
                    break;
                case 18:
                    MessageManager.instance.playerIndex3Windstorm = playerLinesAlt;
                    break;
                case 19:
                    MessageManager.instance.playerIndex4Windstorm = playerLinesAlt;
                    break;
                case 20:
                    MessageManager.instance.playerIndex5Windstorm = playerLinesAlt;
                    break;
                case 21:
                    MessageManager.instance.playerIndex1Defense = playerLinesAlt;
                    break;
                case 22:
                    MessageManager.instance.playerIndex2Defense = playerLinesAlt;
                    break;
                case 23:
                    MessageManager.instance.playerIndex3Defense = playerLinesAlt;
                    break;
                case 24:
                    MessageManager.instance.playerIndex4Defense = playerLinesAlt;
                    break;
                case 25:
                    MessageManager.instance.playerIndex5Defense = playerLinesAlt;
                    break;
                case 26:
                    MessageManager.instance.playerIndex1Attack = playerLinesAlt;
                    break;
                case 27:
                    MessageManager.instance.playerIndex2Attack = playerLinesAlt;
                    break;
                case 28:
                    MessageManager.instance.playerIndex3Attack = playerLinesAlt;
                    break;
                case 29:
                    MessageManager.instance.playerIndex4Attack = playerLinesAlt;
                    break;
                case 30:
                    MessageManager.instance.playerIndex5Attack = playerLinesAlt;
                    break;
                case 31:
                    MessageManager.instance.playerIndex1Drain = playerLinesAlt;
                    break;
                case 32:
                    MessageManager.instance.playerIndex2Drain = playerLinesAlt;
                    break;
                case 33:
                    MessageManager.instance.playerIndex3Drain = playerLinesAlt;
                    break;
                case 34:
                    MessageManager.instance.playerIndex4Drain = playerLinesAlt;
                    break;
                case 35:
                    MessageManager.instance.playerIndex5Drain = playerLinesAlt;
                    break;
                case 36:
                    MessageManager.instance.playerIndex1Paralysis = playerLinesAlt;
                    break;
                case 37:
                    MessageManager.instance.playerIndex2Paralysis = playerLinesAlt;
                    break;
                case 38:
                    MessageManager.instance.playerIndex3Paralysis = playerLinesAlt;
                    break;
                case 39:
                    MessageManager.instance.playerIndex4Paralysis = playerLinesAlt;
                    break;
                case 40:
                    MessageManager.instance.playerIndex5Paralysis = playerLinesAlt;
                    break;
                case 41:
                    MessageManager.instance.playerIndex1Heal = playerLinesAlt;
                    break;
                case 42:
                    MessageManager.instance.playerIndex2Heal = playerLinesAlt;
                    break;
                case 43:
                    MessageManager.instance.playerIndex3Heal = playerLinesAlt;
                    break;
                case 44:
                    MessageManager.instance.playerIndex4Heal = playerLinesAlt;
                    break;
                case 45:
                    MessageManager.instance.playerIndex5Heal = playerLinesAlt;
                    break;
                case 46:
                    MessageManager.instance.playerIndex1DivineTouch = playerLinesAlt;
                    break;
                case 47:
                    MessageManager.instance.playerIndex2DivineTouch = playerLinesAlt;
                    break;
                case 48:
                    MessageManager.instance.playerIndex3DivineTouch = playerLinesAlt;
                    break;
                case 49:
                    MessageManager.instance.playerIndex4DivineTouch = playerLinesAlt;
                    break;
                case 50:
                    MessageManager.instance.playerIndex5DivineTouch = playerLinesAlt;
                    break;
                case 52:
                    MessageManager.instance.cursedMessagePlayer = playerLinesAlt;
                    break;
                default:
                    break;
            }



        }
    }

    void ReadEnemyCSV()
    {
        //1 : Récupère notre fichier.
        TextAsset t = Resources.Load<TextAsset>(enemyFilePath);
        //2 : Split les lignes du CSV. (Au total 52).
        enemyLines = t.text.Split(new char[] { '\n' });
        //3 : La dernière pause problème, on lui injecte des valeurs.
        enemyLines[52] = "1; 2; 3; 4; 5; 6; 7";
        //Debug.Log(playerLines);

        for (int i = 1; i < enemyLines.Length - 1; i++)
        {
            //int totalColumns = playerLines[0].Split(';').Length; //7;

            enemyLinesAlt = enemyLines[i].Split(';');

            // charged / Break / Charme / Windstorm / Defense / Dégats / Drains / Para / Heal / DivineTouch

            switch (i)
            {
                case 1:
                    MessageManager.instance.enemyIndex1Charged = enemyLinesAlt;
                    break;
                case 2:
                    MessageManager.instance.enemyIndex2Charged = enemyLinesAlt;
                    break;
                case 3:
                    MessageManager.instance.enemyIndex3Charged = enemyLinesAlt;
                    break;
                case 4:
                    MessageManager.instance.enemyIndex4Charged = enemyLinesAlt;
                    break;
                case 5:
                    MessageManager.instance.enemyIndex5Charged = enemyLinesAlt;
                    break;
                case 6:
                    MessageManager.instance.enemyIndex1Break = enemyLinesAlt;
                    break;
                case 7:
                    MessageManager.instance.enemyIndex2Break = enemyLinesAlt;
                    break;
                case 8:
                    MessageManager.instance.enemyIndex3Break = enemyLinesAlt;
                    break;
                case 9:
                    MessageManager.instance.enemyIndex4Break = enemyLinesAlt;
                    break;
                case 10:
                    MessageManager.instance.enemyIndex5Break = enemyLinesAlt;
                    break;
                case 11:
                    MessageManager.instance.enemyIndex1Charm = enemyLinesAlt;
                    break;
                case 12:
                    MessageManager.instance.enemyIndex2Charm = enemyLinesAlt;
                    break;
                case 13:
                    MessageManager.instance.enemyIndex3Charm = enemyLinesAlt;
                    break;
                case 14:
                    MessageManager.instance.enemyIndex4Charm = enemyLinesAlt;
                    break;
                case 15:
                    MessageManager.instance.enemyIndex5Charm = enemyLinesAlt;
                    break;
                case 16:
                    MessageManager.instance.enemyIndex1Windstorm = enemyLinesAlt;
                    break;
                case 17:
                    MessageManager.instance.enemyIndex2Windstorm = enemyLinesAlt;
                    break;
                case 18:
                    MessageManager.instance.enemyIndex3Windstorm = enemyLinesAlt;
                    break;
                case 19:
                    MessageManager.instance.enemyIndex4Windstorm = enemyLinesAlt;
                    break;
                case 20:
                    MessageManager.instance.enemyIndex5Windstorm = enemyLinesAlt;
                    break;
                case 21:
                    MessageManager.instance.enemyIndex1Defense = enemyLinesAlt;
                    break;
                case 22:
                    MessageManager.instance.enemyIndex2Defense = enemyLinesAlt;
                    break;
                case 23:
                    MessageManager.instance.enemyIndex3Defense = enemyLinesAlt;
                    break;
                case 24:
                    MessageManager.instance.enemyIndex4Defense = enemyLinesAlt;
                    break;
                case 25:
                    MessageManager.instance.enemyIndex5Defense = enemyLinesAlt;
                    break;
                case 26:
                    MessageManager.instance.enemyIndex1Attack = enemyLinesAlt;
                    break;
                case 27:
                    MessageManager.instance.enemyIndex2Attack = enemyLinesAlt;
                    break;
                case 28:
                    MessageManager.instance.enemyIndex3Attack = enemyLinesAlt;
                    break;
                case 29:
                    MessageManager.instance.enemyIndex4Attack = enemyLinesAlt;
                    break;
                case 30:
                    MessageManager.instance.enemyIndex5Attack = enemyLinesAlt;
                    break;
                case 31:
                    MessageManager.instance.enemyIndex1Drain = enemyLinesAlt;
                    break;
                case 32:
                    MessageManager.instance.enemyIndex2Drain = enemyLinesAlt;
                    break;
                case 33:
                    MessageManager.instance.enemyIndex3Drain = enemyLinesAlt;
                    break;
                case 34:
                    MessageManager.instance.enemyIndex4Drain = enemyLinesAlt;
                    break;
                case 35:
                    MessageManager.instance.enemyIndex5Drain = enemyLinesAlt;
                    break;
                case 36:
                    MessageManager.instance.enemyIndex1Paralysis = enemyLinesAlt;
                    break;
                case 37:
                    MessageManager.instance.enemyIndex2Paralysis = enemyLinesAlt;
                    break;
                case 38:
                    MessageManager.instance.enemyIndex3Paralysis = enemyLinesAlt;
                    break;
                case 39:
                    MessageManager.instance.enemyIndex4Paralysis = enemyLinesAlt;
                    break;
                case 40:
                    MessageManager.instance.enemyIndex5Paralysis = enemyLinesAlt;
                    break;
                case 41:
                    MessageManager.instance.enemyIndex1Heal = enemyLinesAlt;
                    break;
                case 42:
                    MessageManager.instance.enemyIndex2Heal = enemyLinesAlt;
                    break;
                case 43:
                    MessageManager.instance.enemyIndex3Heal = enemyLinesAlt;
                    break;
                case 44:
                    MessageManager.instance.enemyIndex4Heal = enemyLinesAlt;
                    break;
                case 45:
                    MessageManager.instance.enemyIndex5Heal = enemyLinesAlt;
                    break;
                case 46:
                    MessageManager.instance.enemyIndex1DivineTouch = enemyLinesAlt;
                    break;
                case 47:
                    MessageManager.instance.enemyIndex2DivineTouch = enemyLinesAlt;
                    break;
                case 48:
                    MessageManager.instance.enemyIndex3DivineTouch = enemyLinesAlt;
                    break;
                case 49:
                    MessageManager.instance.enemyIndex4DivineTouch = enemyLinesAlt;
                    break;
                case 50:
                    MessageManager.instance.enemyIndex5DivineTouch = enemyLinesAlt;
                    break;
                default:
                    break;
            }



        }
    }
    
}
