using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    //Prefabs To Instantiate.
    [SerializeField] private GameObject SmallMessagePlayer;
    [SerializeField] private GameObject BigMessagePlayer;
    [SerializeField] private GameObject EmojiPlayer;

    [SerializeField] private GameObject SmallMessageEnemy;
    [SerializeField] private GameObject BigMessageEnemy;
    [SerializeField] private GameObject EmojiEnemy;

    //MessagePositions
    [SerializeField] private GameObject playerParentMsg;
    public List<GameObject> playerMsgPositions = new List<GameObject>();
    [SerializeField] private GameObject enemyParentMsg;
    public List<GameObject> enemyMsgPositions = new List<GameObject>();

    public int spawnPositionIndex;

    public GameObject[] allMsg; 

    // Start is called before the first frame update
    void Start()
    {
        //Récupération des positions Enfants. Ces enfants sont les répères pour l'apparition des mesages.
        foreach (Transform child in playerParentMsg.transform)
        {
            playerMsgPositions.Add(child.gameObject);
        }

        foreach (Transform child in enemyParentMsg.transform)
        {
            enemyMsgPositions.Add(child.gameObject);
        }

        allMsg = new  GameObject [enemyMsgPositions.Count];
    }

   

    public void UpdatePosition()
    {
        if (allMsg[allMsg.Length-1] != null)
        {
            GameObject currentGO = allMsg[allMsg.Length-1];

            allMsg[allMsg.Length - 1] = null;
            //Activer La Fonction ici pour les Emojis LeaveBattle();
            

            Destroy(currentGO);
        }

        for (int i = allMsg.Length -1; i > -1; i--)
        {
            if(allMsg[i] != null)
            {
                    //augmenter Current Position;
                    allMsg[i + 1] = allMsg[i];

                    if (allMsg[i + 1].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Player)
                    {
                        allMsg[i] = null;
                        allMsg[i + 1].transform.SetParent(playerMsgPositions[i + 1].transform);
                        allMsg[i + 1].transform.localPosition = Vector3.zero;
                    }
                    else
                    {
                        allMsg[i] = null;
                        allMsg[i + 1].transform.SetParent(enemyMsgPositions[i + 1].transform);
                        allMsg[i + 1].transform.localPosition = Vector3.zero;
                    }
            }
        }
    }


    #region Player
    public void PlayerSmallMessage()
    {
        UpdatePosition();
        GameObject msg = Instantiate(SmallMessagePlayer.gameObject, playerMsgPositions[0].transform.position,Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;
        
    }

    public void PlayerLargeMessage()
    {
        UpdatePosition();
        GameObject msg = Instantiate(BigMessagePlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;
    }

    public void PlayerEmojis()
    {
        UpdatePosition();
        GameObject msg = Instantiate(EmojiPlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;
    }
    #endregion

    #region Enemy
    public void EnemySmallMessage()
    {
        UpdatePosition();
        GameObject msg = Instantiate(SmallMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
    }

    public void EnemyLargeMessage()
    {
        UpdatePosition();
        GameObject msg = Instantiate(BigMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
    }

    public void EnemyEmojis()
    {
        UpdatePosition();
        GameObject msg = Instantiate(EmojiEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
    }
    #endregion

    
}
