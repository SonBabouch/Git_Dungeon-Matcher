using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    //Prefabs To Instantiate.
    [SerializeField] private GameObject SmallMessagePlayer;
    [SerializeField] private GameObject ChargingMessagePlayer;
    [SerializeField] private GameObject BigMessagePlayer;
    [SerializeField] private GameObject EmojiPlayer;

    [SerializeField] private GameObject SmallMessageEnemy;
    [SerializeField] private GameObject ChargingMessageEnemy;
    [SerializeField] private GameObject BigMessageEnemy;
    [SerializeField] private GameObject EmojiEnemy;

    [SerializeField] private Sprite longMessage;

    //MessagePositions
    [SerializeField] private GameObject playerParentMsg;
    public List<GameObject> playerMsgPositions = new List<GameObject>();
    [SerializeField] private GameObject enemyParentMsg;
    public List<GameObject> enemyMsgPositions = new List<GameObject>();

    public int spawnPositionIndex;

    public GameObject[] allMsg;

    public static ConversationManager Instance;

    public int numberOfMessageMoved =0;
    public int numberOfMessageTotal =0;

    public enum typeToSpawn { Null,PlayerSmall,PlayerBig,PlayerEmote, EnemySmall, EnemyBig, EnemyEmote }
    public typeToSpawn messageToSpawn;

    public bool canAttack = true;

    private void Awake()
    {
        Instance = this;
    }

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

        Debug.Log(allMsg.Length);
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)){

            //A mettre dans le break.
            StopCoroutine(Player.Instance.ChargeAttack(Player.Instance.lastPlayerCompetence));
            Player.Instance.isCharging = false;
            CancelPosition();
        }
        
    }*/
    //1 - Le joueur appuie sur un bouton, ca lance SendMessage (enemy ou allié).
    public void SendMessagesPlayer(Skill capacity)
    {
        canAttack = false;
        switch (capacity.messageType)
        {
            case Skill.typeOfMessage.Small:
                messageToSpawn = typeToSpawn.PlayerSmall;
                UpdatePosition();
                break;
            case Skill.typeOfMessage.Big:
                messageToSpawn = typeToSpawn.PlayerBig;
                UpdatePosition();
                break;
            case Skill.typeOfMessage.Emoji:
                messageToSpawn = typeToSpawn.PlayerEmote;
                UpdatePosition();
                break;
            default:
                break;
        }
    }

    public void SendMessagesEnemy(Skill capacity)
    {
        canAttack = false;
        switch (capacity.messageType)
        {
            case Skill.typeOfMessage.Small:
                messageToSpawn = typeToSpawn.EnemySmall;
                UpdatePosition();
                break;
            case Skill.typeOfMessage.Big:
                messageToSpawn = typeToSpawn.EnemyBig;
                UpdatePosition();
                break;
            case Skill.typeOfMessage.Emoji:
                messageToSpawn = typeToSpawn.EnemyEmote;
                UpdatePosition();
                break;
            default:
                break;
        }
    }

    public void UpdatePosition()
    {
        numberOfMessageTotal = 0;

        if (allMsg[allMsg.Length - 1] != null)
        {
            GameObject currentGO = allMsg[allMsg.Length - 1];
            //Activer La Fonction ici pour les Emojis LeaveBattle();
            allMsg[allMsg.Length - 1] = null;

            Destroy(currentGO);
        }

        for (int i = allMsg.Length - 2; i > -1; i--)
        {
            if (allMsg[i] != null)
            {
                numberOfMessageTotal++;
                StartCoroutine(MessageMovement(allMsg[i], i, allMsg[i].GetComponent<MessageBehaviour>().ally)); ;
            }
        }

        if (numberOfMessageTotal==0)
        {
            PrintMessage();
        }
    }


    #region ChargingRegion
    public void UpdateLastMessageState()
    {
        if (allMsg[0].GetComponent<MessageBehaviour>().big && allMsg[0] != null)
        {
            if (allMsg[0].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Player)
            {
                Destroy(allMsg[0]);
                GameObject msg = Instantiate(BigMessagePlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
                msg.transform.SetParent(playerMsgPositions[0].transform);
                allMsg[0] = msg;
                canAttack = true;
            }
            else
            {
                Destroy(allMsg[0]);
                GameObject msg = Instantiate(BigMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
                msg.transform.SetParent(enemyMsgPositions[0].transform);
                allMsg[0] = msg;
                
            }
            
        }
    }

    public void CancelPosition()
    {
        Destroy(allMsg[0]);

        //Start à 1 car 0 est vide car détruit;
        for (int i = 1; i < allMsg.Length - 1; i++)
        {
            if (allMsg[i] != null)
            {
                //augmenter Current Position;
                allMsg[i-1] = allMsg[i];

                if (allMsg[i].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Player)
                {
                    allMsg[i] = null;
                    allMsg[i - 1].transform.SetParent(playerMsgPositions[i - 1].transform);
                    allMsg[i - 1].transform.localPosition = Vector3.zero;
                }
                else
                {
                    allMsg[i] = null;
                    allMsg[i - 1].transform.SetParent(enemyMsgPositions[i - 1].transform);
                    allMsg[i - 1].transform.localPosition = Vector3.zero;
                }
            }
        }
    }
    #endregion


    IEnumerator MessageMovement(GameObject message, int index, bool ally)
    {
        //Déplacer message joueur
        if (message.GetComponent<MessageBehaviour>().ally)
        {
            while (message.transform.position.y < playerMsgPositions[index + 1].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, 4f, 0f);
                message.transform.Translate(translateVector);
                yield return null;

            }
            numberOfMessageMoved++;
            message.transform.SetParent(playerMsgPositions[index + 1].transform);
            message.transform.localPosition = Vector3.zero;
        }
        else //Déplacer message ennemi
        {

        }

        if(numberOfMessageMoved == numberOfMessageTotal)
        {
            UpdateArrayIndex();
        }

        yield return null;

    }

    void UpdateArrayIndex()
    {
        print("Called");

        numberOfMessageMoved = 0;

        for (int i = allMsg.Length - 2; i > 0; i--)
        {
            print(i - 1 + " " + (i));

            allMsg[i] = allMsg[i - 1];
        }
       
        PrintMessage();
    }

    void PrintMessage()
    {
        switch (messageToSpawn)
        {
            case typeToSpawn.PlayerSmall:
                PlayerSmallMessage();
                break;
            case typeToSpawn.PlayerBig:
                PlayerLargeMessage();
                break;
            case typeToSpawn.PlayerEmote:
                PlayerEmojis();
                break;
            case typeToSpawn.EnemySmall:
                EnemySmallMessage();
                break;
            case typeToSpawn.EnemyBig:
                EnemyLargeMessage();
                break;
            case typeToSpawn.EnemyEmote:
                EnemyEmojis();
                break;
            default:
                break;

        }
    }

    #region Player
    public void PlayerSmallMessage()
    {
      
        GameObject msg = Instantiate(SmallMessagePlayer.gameObject, playerMsgPositions[0].transform.position,Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void PlayerLargeMessage()
    {
       
        GameObject msg = Instantiate(ChargingMessagePlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null; 
        
        //Lancer Methode pour le Text;
    }

    public void PlayerEmojis()
    {
        
        GameObject msg = Instantiate(EmojiPlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }
    #endregion

    #region Enemy
    public void EnemySmallMessage()
    {

        //A lancer à la fin de la coroutine.
        GameObject msg = Instantiate(SmallMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void EnemyLargeMessage()
    {
     
        GameObject msg = Instantiate(ChargingMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void EnemyEmojis()
    {
      
        GameObject msg = Instantiate(EmojiEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }
    #endregion


}
