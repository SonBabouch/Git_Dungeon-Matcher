using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManager : MonoBehaviour
{
    public MessageManager messageManager;

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

    public List<GameObject> emojis = new List<GameObject>();

    public static ConversationManager Instance;

    public int numberOfMessageMoved = 0;
    public int numberOfMessageTotal = 0;

    public int futurEmojiEffect = 0;

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

        //Debug.Log(allMsg.Length);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Player.Instance.isCharging)
        {
            //A mettre dans le break.
            StopCoroutine(Player.Instance.PlayerChargeAttack(Player.Instance.lastPlayerCompetence));
            Player.Instance.isCharging = false;
            canAttack = false;
            CancelPosition(); 
        }
    }

    //1 - Le joueur appuie sur un bouton, ca lance SendMessage (enemy ou allié). Mettre 0 en EmojiEffect si l'attaque n'est pas un Emoji
    public void SendMessagesPlayer(Skill capacity, int numberForEmojiEffect)
    {
        //Le joueur ne peut pas attaquer tant que l'animation n'est pas finie
        canAttack = false;
        switch (capacity.messageType)
        {
            case Skill.typeOfMessage.Small:
                messageToSpawn = typeToSpawn.PlayerSmall;
                UpdatePosition(capacity);
                break;
            case Skill.typeOfMessage.Big:
                messageToSpawn = typeToSpawn.PlayerBig;
                UpdatePosition(capacity);
                break;
            case Skill.typeOfMessage.Emoji:
                messageToSpawn = typeToSpawn.PlayerEmote;
                futurEmojiEffect = numberForEmojiEffect;
                UpdatePosition(capacity);
                break;
            default:
                break;
        }
    }
    public void SendMessagesEnemy(Skill capacity, int numberForEmojiEffect)
    {
        canAttack = false;
        switch (capacity.messageType)
        {
            case Skill.typeOfMessage.Small:
                messageToSpawn = typeToSpawn.EnemySmall;
                UpdatePosition(capacity);
                break;
            case Skill.typeOfMessage.Big:
                messageToSpawn = typeToSpawn.EnemyBig;
                UpdatePosition(capacity);
                break;
            case Skill.typeOfMessage.Emoji:
                messageToSpawn = typeToSpawn.EnemyEmote;
                futurEmojiEffect = numberForEmojiEffect;
                UpdatePosition(capacity);
                break;
            default:
                break;
        }
    }
    //2 - Enleve le dernier message
    public void UpdatePosition(Skill skillType)
    {
        numberOfMessageTotal = 0;
        //2 - Enleve le dernier message
        if (allMsg[allMsg.Length-2] != null)
        {
            //Debug.Log("Destroy");

            if (allMsg[allMsg.Length - 2].GetComponent<MessageBehaviour>().emoji)
            {
                allMsg[allMsg.Length - 2].GetComponent<MessageBehaviour>().EmojiEffectEnd();
                
                if(emojis.Count > 0)
                {
                    emojis.Remove(emojis[emojis.Count - 1]);
                }
            }

            //Destroy(allMsg[allMsg.Length - 1]);
            //Debug.Log(allMsg[allMsg.Length - 1]);
        }
        //3- Compte le nombre de message présent. Donc le nombre de message à déplacer.
        for (int i = allMsg.Length - 2; i > -1; i--)
        {
            if (allMsg[i] != null)
            {
                numberOfMessageTotal++;
                StartCoroutine(MessageMovement(allMsg[i], i, allMsg[i].GetComponent<MessageBehaviour>().ally,skillType)); ;
            }
        }

        if (numberOfMessageTotal==0)
        {
            PrintMessage(skillType);
        }
    }


    #region ChargingRegion
    public void UpdateLastMessageState(Skill skill)
    {
        if (allMsg[0].GetComponent<MessageBehaviour>().big && allMsg[0] != null)
        {
            if (allMsg[0].GetComponent<MessageBehaviour>().teamMsg == MessageBehaviour.team.Player)
            {
                Destroy(allMsg[0]);
                GameObject msg = Instantiate(BigMessagePlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
                msg.transform.SetParent(playerMsgPositions[0].transform);
                allMsg[0] = msg;

                messageManager.ChoseArray(skill, msg);

                canAttack = true;
            }
            else
            {
                Destroy(allMsg[0]);
                GameObject msg = Instantiate(BigMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
                msg.transform.SetParent(enemyMsgPositions[0].transform);
                allMsg[0] = msg;

                messageManager.ChoseArray(skill, msg);

            }
            
        }
    }

    public void CancelPosition()
    {
        //Mettre feedback Break

        Destroy(allMsg[0]);
        allMsg[0] = null;

        numberOfMessageTotal = 0;

        //3- Compte le nombre de message présent. Donc le nombre de message à déplacer.
        for (int i = 1; i < allMsg.Length-1; i++)
        {
            if (allMsg[i] != null)
            {
                numberOfMessageTotal++;
                StartCoroutine(CancelMessageMovement(allMsg[i], i, allMsg[i].GetComponent<MessageBehaviour>().ally)); ;
            }
        }
    }
    #endregion

    
    IEnumerator CancelMessageMovement(GameObject message, int index, bool ally)
    {
        //Déplacer message joueur
        if (message.GetComponent<MessageBehaviour>().ally)
        {
            while (message.transform.position.y > playerMsgPositions[index - 1].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, -4f, 0f);
                message.transform.Translate(translateVector);
                yield return null;

            }
            numberOfMessageMoved++;
            message.transform.SetParent(playerMsgPositions[index - 1].transform);
            message.transform.localPosition = Vector3.zero;
        }
        else //Déplacer message ennemi
        {
            while (message.transform.position.y < enemyMsgPositions[index - 1].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, -4f, 0f);
                message.transform.Translate(translateVector);
                yield return null;

            }
            numberOfMessageMoved++;
            message.transform.SetParent(enemyMsgPositions[index - 1].transform);
            message.transform.localPosition = Vector3.zero;
        }

        if (numberOfMessageMoved == numberOfMessageTotal)
        {   //5 - Quand tous les messages ont bougés, je peux update leur emplacement dans l'array.
            UpdateArrayIndexCancel();
        }

        yield return null;
    }
    

    //4- Animation des Messages.
    IEnumerator MessageMovement(GameObject message, int index, bool ally, Skill skillToSpawn)
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
            while (message.transform.position.y < enemyMsgPositions[index + 1].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, 4f, 0f);
                message.transform.Translate(translateVector);
                yield return null;

            }
            numberOfMessageMoved++;
            message.transform.SetParent(enemyMsgPositions[index + 1].transform);
            message.transform.localPosition = Vector3.zero;
        }

        if(numberOfMessageMoved == numberOfMessageTotal)
        {   //5 - Quand tous les messages ont bougés, je peux update leur emplacement dans l'array.
            UpdateArrayIndex(skillToSpawn);
        }

        yield return null;

    }

    
    void UpdateArrayIndexCancel()
    {
        //6 - Reset des messages à bouger.
        numberOfMessageMoved = 0;
        numberOfMessageTotal = 0;

        for (int i = 1; i < allMsg.Length - 1; i++)
        {
            //7 - Tous les messages augmente d'un index (en partant du haut).
            allMsg[i-1] = allMsg[i];
        }

        canAttack = true;
    }
    
    void UpdateArrayIndex(Skill skillToSpawn)
    {
        //print("Called");

        //6 - Reset des messages à bouger.
        numberOfMessageMoved = 0;

        for (int i = allMsg.Length - 2; i > 0; i--)
        {
            //print(i - 1 + " " + (i));
            //7 - Tous les messages augmente d'un index (en partant du haut).
            allMsg[i] = allMsg[i - 1];
        }

        //8- Update des Effets des Emojis.
        UpdateEmojiEffect(skillToSpawn);
        
    }
    
    //rejoue les effets des emojis si jamais ils sont en double pas de soucis;
    void UpdateEmojiEffect(Skill skillToSpawn)
    {
        for (int i = 0; i < emojis.Count-1; i++)
        {
            emojis[i].GetComponent<MessageBehaviour>().EmojiEffectBegin();
        }

        //9- Instancie le message
        PrintMessage(skillToSpawn);
    }

    //Instancier le message en fonction de la compétences.
    void PrintMessage(Skill capacity)
    {
        switch (messageToSpawn)
        {
            case typeToSpawn.PlayerSmall:
                PlayerSmallMessage(capacity);
                break;
            case typeToSpawn.PlayerBig:
                PlayerLargeMessage(capacity);
                break;
            case typeToSpawn.PlayerEmote:
                PlayerEmojis(capacity);
                break;
            case typeToSpawn.EnemySmall:
                EnemySmallMessage(capacity);
                break;
            case typeToSpawn.EnemyBig:
                EnemyLargeMessage(capacity);
                break;
            case typeToSpawn.EnemyEmote:
                EnemyEmojis(capacity);
                break;
            default:
                break;

        }
    }

    #region Player
    public void PlayerSmallMessage(Skill skill)
    {
      
        GameObject msg = Instantiate(SmallMessagePlayer.gameObject, playerMsgPositions[0].transform.position,Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;

        //Debug.Log(skill);
        messageManager.ChoseArray(skill, msg);

        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void PlayerLargeMessage(Skill skill)
    {
       
        GameObject msg = Instantiate(ChargingMessagePlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;

        messageToSpawn = typeToSpawn.Null; 
        
        //Lancer Methode pour le Text;
    }

    public void PlayerEmojis(Skill skill)
    {
        GameObject msg = Instantiate(EmojiPlayer.gameObject, playerMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[0].transform);
        allMsg[0] = msg;
        msg.GetComponent<MessageBehaviour>().teamMsg = MessageBehaviour.team.Player;
        msg.GetComponent<MessageBehaviour>().GetEffect(futurEmojiEffect);
        msg.GetComponent<MessageBehaviour>().EmojiEffectBegin();
        futurEmojiEffect = 0;
        
        //On stock l'emoji dans un tableau, pour réactiver son effet;
        emojis.Add(msg);

        //reset des valeurs pour instancier le prochain message.
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
    }
    #endregion

    #region Enemy
    public void EnemySmallMessage(Skill skill)
    {

        //A lancer à la fin de la coroutine.
        GameObject msg = Instantiate(SmallMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void EnemyLargeMessage(Skill skill)
    {
     
        GameObject msg = Instantiate(ChargingMessageEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void EnemyEmojis(Skill skill)
    {
      
        GameObject msg = Instantiate(EmojiEnemy.gameObject, enemyMsgPositions[0].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[0].transform);
        allMsg[0] = msg;
        msg.GetComponent<MessageBehaviour>().teamMsg = MessageBehaviour.team.Enemy;
        msg.GetComponent<MessageBehaviour>().GetEffect(futurEmojiEffect);
        futurEmojiEffect = 0;
        msg.GetComponent<MessageBehaviour>().EmojiEffectBegin();
        emojis.Add(msg);

        //reset des valeurs pour instancier le prochain message.
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
    }
    #endregion


}
