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

    public List<GameObject> playerMsg = new List<GameObject>();
    public List<GameObject> enemyMsg = new List<GameObject>();
    public List<GameObject> allMsg = new List<GameObject>();

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
    }


    public void UpdatePosition()
    {
        //Différencier les différents Types de messages

        for (int i = allMsg.Count - 1; i > 0 ; i--)
        {
            if(allMsg[i].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Player)
            {
                if(allMsg[i].GetComponent<MessageBehaviour>().typeOfMsg == MessageBehaviour.typeOf.Small)
                {
                    playerMsg[i].transform.position = playerMsgPositions[i + 1].transform.position;
                    playerMsg[i].transform.SetParent(playerMsgPositions[i + 1].transform);
                    playerMsg[i].transform.localPosition = Vector3.zero;
                }
                else
                {
                    playerMsg[i].transform.position = playerMsgPositions[i + 2].transform.position;
                    playerMsg[i].transform.SetParent(playerMsgPositions[i + 2].transform);
                    playerMsg[i].transform.localPosition = Vector3.zero;
                }
            }
            else
            {
                if (allMsg[i].GetComponent<MessageBehaviour>().typeOfMsg == MessageBehaviour.typeOf.Small)
                {
                    enemyMsg[i].transform.position = enemyMsgPositions[i + 1].transform.position;
                    enemyMsg[i].transform.SetParent(enemyMsgPositions[i + 1].transform);
                    enemyMsg[i].transform.localPosition = Vector3.zero;
                }
                else
                {
                    enemyMsg[i].transform.position = enemyMsgPositions[i + 2].transform.position;
                    enemyMsg[i].transform.SetParent(enemyMsgPositions[i + 2].transform);
                    enemyMsg[i].transform.localPosition = Vector3.zero;
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
        allMsg.Add(msg);
        playerMsg.Add(msg);
    }

    public void PlayerLargeMessage()
    {
        UpdatePosition();
        GameObject msg = Instantiate(BigMessagePlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg.Add(msg);
        playerMsg.Add(msg);
    }

    public void PlayerEmojis()
    {
        UpdatePosition();
        GameObject msg = Instantiate(EmojiPlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg.Add(msg);
        playerMsg.Add(msg);
    }
    #endregion

    #region Enemy
    public void EnemySmallMessage()
    {
        UpdatePosition();
        GameObject msg = Instantiate(SmallMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg.Add(msg);
        enemyMsg.Add(msg);
    }

    public void EnemyLargeMessage()
    {
        UpdatePosition();
        GameObject msg = Instantiate(BigMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg.Add(msg);
        enemyMsg.Add(msg);
    }

    public void EnemyEmojis()
    {
        UpdatePosition();
        GameObject msg = Instantiate(EmojiEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg.Add(msg);
        enemyMsg.Add(msg);
    }
    #endregion
}
