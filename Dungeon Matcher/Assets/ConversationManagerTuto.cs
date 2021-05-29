using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConversationManagerTuto : MonoBehaviour
{
    public MessageManagerTuto messageManager;

    public Color cursedColor;

    //Prefabs To Instantiate.
    [SerializeField] private GameObject SmallMessagePlayer;
    [SerializeField] private GameObject BigMessagePlayer;
    [SerializeField] private GameObject EmojiPlayer;

    [SerializeField] private GameObject SmallMessageEnemy;
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

    public static ConversationManagerTuto Instance;

    public int numberOfMessageMoved = 0;
    public int numberOfMessageTotal = 0;

    public int futurEmojiEffect = 0;

    public enum typeToSpawn { Null, PlayerSmall, PlayerBig, PlayerCharging, PlayerEmote, EnemySmall, EnemyBig, EnemyEmote, EnemyCharging }
    public typeToSpawn messageToSpawn;

    public bool canAttack = true;


    public GameObject playerChargingAttack;
    public GameObject enemyChargingAttack;
    [SerializeField] private Color cyanColor;


    [Header("FallingPosition")]
    public GameObject tweenPositionPlayer;
    public GameObject tweenPositionEnemy;


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

        allMsg = new GameObject[enemyMsgPositions.Count];

        //Debug.Log(allMsg.Length);
    }

    //1 - Le joueur appuie sur un bouton, ca lance SendMessage (enemy ou allié). Mettre 0 en EmojiEffect si l'attaque n'est pas un Emoji
    public void SendMessagesPlayer(SkillTuto capacity, int numberForEmojiEffect)
    {
        //Le joueur ne peut pas attaquer tant que l'animation n'est pas finie
        canAttack = false;
        switch (capacity.messageType)
        {
            case SkillTuto.typeOfMessage.Small:
                messageToSpawn = typeToSpawn.PlayerSmall;
                UpdatePosition(capacity);
                break;
            case SkillTuto.typeOfMessage.Charging:
                PlayerChargingMessage(capacity);
                break;
            case SkillTuto.typeOfMessage.Big:
                messageToSpawn = typeToSpawn.PlayerBig;
                UpdatePosition(capacity);
                break;
            case SkillTuto.typeOfMessage.Emoji:
                messageToSpawn = typeToSpawn.PlayerEmote;
                futurEmojiEffect = numberForEmojiEffect;
                UpdatePosition(capacity);
                break;
            default:
                break;
        }
    }
    public void SendMessagesEnemy(SkillTuto capacity, int numberForEmojiEffect)
    {
        canAttack = false;
        switch (capacity.messageType)
        {
            case SkillTuto.typeOfMessage.Small:
                messageToSpawn = typeToSpawn.EnemySmall;
                UpdatePosition(capacity);
                break;
            case SkillTuto.typeOfMessage.Charging:
                EnemyChargingMessage(capacity);
                break;
            case SkillTuto.typeOfMessage.Big:
                messageToSpawn = typeToSpawn.EnemyBig;
                UpdatePosition(capacity);
                break;
            case SkillTuto.typeOfMessage.Emoji:
                messageToSpawn = typeToSpawn.EnemyEmote;
                futurEmojiEffect = numberForEmojiEffect;
                UpdatePosition(capacity);
                break;
            default:
                break;
        }
    }
    //2 - Enleve le dernier message
    public void UpdatePosition(SkillTuto skillType)
    {
        numberOfMessageTotal = 0;

        //2 - Enleve le dernier message
        if (allMsg[allMsg.Length - 1] != null)
        {
            //Si le dernier message est un emoji
            if (allMsg[allMsg.Length - 1].GetComponent<MessageBehaviour>().emoji)
            {
                //On enleve sont effet et on le suprime de la liste d'update des effets.
                allMsg[allMsg.Length - 1].GetComponent<MessageBehaviour>().EmojiEffectEnd();

                if (emojis.Count > 0)
                {
                    emojis.Remove(emojis[allMsg.Length - 1]);
                }
            }

            //Destroy le message
            Destroy(allMsg[allMsg.Length - 1]);
            allMsg[allMsg.Length - 1] = null;
        }

        //2 - Enleve l'avant dernier message
        if (allMsg[allMsg.Length - 2] != null)
        {
            //Si le dernier message est un emoji
            if (allMsg[allMsg.Length - 2].GetComponent<MessageBehaviour>().emoji)
            {
                //On enleve sont effet et on le suprime de la liste d'update des effets.
                allMsg[allMsg.Length - 2].GetComponent<MessageBehaviour>().EmojiEffectEnd();

                if (emojis.Count > 0)
                {
                    emojis.Remove(allMsg[allMsg.Length - 2]);
                }
            }

            Destroy(allMsg[allMsg.Length - 2]);
            allMsg[allMsg.Length - 2] = null;
        }

        //Si un gros message va être instancier, on déplace de deux
        if (skillType.messageType == Skill.typeOfMessage.Big)
        {
            //Debug.Log("Double");
            //3- Compte le nombre de message présent. Donc le nombre de message à déplacer. //Les deux derniers messages sont déja supp.
            for (int i = allMsg.Length - 3; i > 0; i--)
            {
                if (allMsg[i] != null)
                {

                    numberOfMessageTotal++;
                    StartCoroutine(MessageMovementDouble(allMsg[i], i, allMsg[i].GetComponent<MessageBehaviour>().ally, skillType));
                }
            }

            if (numberOfMessageTotal == 0)
            {
                PrintMessage(skillType);
            }
        }
        else
        {
            for (int i = allMsg.Length - 1; i > 0; i--)
            {
                if (allMsg[i] != null)
                {
                    numberOfMessageTotal++;
                    StartCoroutine(MessageMovementSimple(allMsg[i], i, allMsg[i].GetComponent<MessageBehaviour>().ally, skillType));
                }
            }

            if (numberOfMessageTotal == 0)
            {
                PrintMessage(skillType);
            }
        }
    }


    #region ChargingRegion
    public void UpdateLastMessageState(SkillTuto skill)
    {
        if (skill.side == SkillTuto.monsterSide.Ally)
        {
            SendMessagesPlayer(skill, 0);
        }
        else
        {
            SendMessagesEnemy(skill, 0);
        }
    }





    //4- Animation des Messages.
    IEnumerator MessageMovementSimple(GameObject message, int index, bool ally, SkillTuto skillToSpawn)
    {
        //Déplacer message joueur
        if (message.GetComponent<MessageBehaviour>().ally)
        {
            while (message.transform.position.y < playerMsgPositions[index + 1].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, 20f, 0f);
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
                Vector3 translateVector = new Vector3(0f, 20f, 0f);
                message.transform.Translate(translateVector);
                yield return null;

            }
            numberOfMessageMoved++;
            message.transform.SetParent(enemyMsgPositions[index + 1].transform);
            message.transform.localPosition = Vector3.zero;
        }

        if (numberOfMessageMoved == numberOfMessageTotal)
        {   //5 - Quand tous les messages ont bougés, je peux update leur emplacement dans l'array.
            UpdateArrayIndex(skillToSpawn);
        }

        yield return null;

    }

    IEnumerator MessageMovementDouble(GameObject message, int index, bool ally, SkillTuto skillToSpawn)
    {
        //Déplacer message joueur
        if (message.GetComponent<MessageBehaviour>().ally)
        {
            while (message.transform.position.y < playerMsgPositions[index + 2].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, 20f, 0f);
                message.transform.Translate(translateVector);
                yield return null;

            }
            numberOfMessageMoved++;
            message.transform.SetParent(playerMsgPositions[index + 2].transform);
            message.transform.localPosition = Vector3.zero;
        }
        else //Déplacer message ennemi
        {
            while (message.transform.position.y < enemyMsgPositions[index + 2].transform.position.y)
            {
                Vector3 translateVector = new Vector3(0f, 20f, 0f);
                message.transform.Translate(translateVector);
                yield return null;

            }
            numberOfMessageMoved++;
            message.transform.SetParent(enemyMsgPositions[index + 2].transform);
            message.transform.localPosition = Vector3.zero;
        }

        if (numberOfMessageMoved == numberOfMessageTotal)
        {   //5 - Quand tous les messages ont bougés, je peux update leur emplacement dans l'array.
            UpdateArrayIndexDouble(skillToSpawn);
        }

        yield return null;

    }

    void UpdateArrayIndexDouble(SkillTuto skillToSpawn)
    {
        //print("Called");

        //6 - Reset des messages à bouger.
        numberOfMessageMoved = 0;

        for (int i = allMsg.Length - 3; i > 0; i--)
        {
            allMsg[i + 2] = allMsg[i];
            allMsg[i] = null;
            /*if (allMsg[i] != null)
            {
                //print(i - 1 + " " + (i));
                //7 - Tous les messages augmente d'un index (en partant du haut).
                
            }*/
        }

        //8- Update des Effets des Emojis.
        UpdateEmojiEffect(skillToSpawn);

    }
    void UpdateArrayIndex(SkillTuto skillToSpawn)
    {
        //print("Called");

        //6 - Reset des messages à bouger.
        numberOfMessageMoved = 0;


        for (int i = allMsg.Length - 3; i > 0; i--)
        {
            allMsg[i + 1] = allMsg[i];
            allMsg[i] = null;
            //print(i - 1 + " " + (i));
            //7 - Tous les messages augmente d'un index (en partant du haut).

        }

        //8- Update des Effets des Emojis.
        UpdateEmojiEffect(skillToSpawn);
    }

    //rejoue les effets des emojis si jamais ils sont en double pas de soucis;
    void UpdateEmojiEffect(SkillTuto skillToSpawn)
    {
        for (int i = 0; i < emojis.Count - 1; i++)
        {
            emojis[i].GetComponent<MessageBehaviour>().EmojiEffectBegin();
        }

        //9- Instancie le message
        PrintMessage(skillToSpawn);
    }

    //Instancier le message en fonction de la compétences.
    void PrintMessage(SkillTuto capacity)
    {
        switch (messageToSpawn)
        {
            case typeToSpawn.PlayerSmall:
                PlayerSmallMessage(capacity);
                break;
            case typeToSpawn.PlayerCharging:
                PlayerChargingMessage(capacity);
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
            case typeToSpawn.EnemyCharging:
                EnemyChargingMessage(capacity);
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

    #endregion

    #region Player
    public void PlayerSmallMessage(SkillTuto skill)
    {
        GameObject msg = Instantiate(SmallMessagePlayer.gameObject, playerMsgPositions[1].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[1].transform);
        allMsg[1] = msg;

        skill.messageOwner = msg;

        if (skill.comesFromCurse)
        {
            skill.messageOwner.GetComponent<Image>().color = ConversationManagerTuto.Instance.cursedColor;

        }

        CombatManagerTuto.Instance.MessageIcon(msg, skill);
        //Debug.Log(skill);
        messageManager.ChoseArray(skill, msg);
        skill.PlayerEffect();
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void PlayerChargingMessage(SkillTuto skill)
    {
        Debug.Log("ChargingAttack");
        playerChargingAttack.SetActive(true);
        skill.messageOwner = playerChargingAttack;
        skill.messageType = SkillTuto.typeOfMessage.Big;
        PlayerTuto.Instance.isCharging = true;

        if (skill.comesFromCurse)
        {
            skill.messageOwner.GetComponent<Image>().color = ConversationManager.Instance.cursedColor;
        }
        //else
        //{
        //    //Mettre la couleur Cyan
        //    skill.messageOwner.GetComponent<Image>().color = cyanColor;
        //}

        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        Player.Instance.canAttack = false;
        StartCoroutine(Player.Instance.EndPlayerChargeAttack(skill));
    }

    public void PlayerLargeMessage(SkillTuto skill)
    {
        playerChargingAttack.SetActive(false);
        PlayerTuto.Instance.isCharging = false;


        Debug.Log("BigMessage");
        GameObject msg = Instantiate(BigMessagePlayer.gameObject, playerMsgPositions[2].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[2].transform);
        allMsg[2] = msg;
        skill.messageOwner = msg;


        if (skill.comesFromCurse)
        {
            skill.messageOwner.GetComponent<Image>().color = ConversationManager.Instance.cursedColor;

        }

        CombatManager.Instance.MessageIcon(msg, skill);
        messageManager.ChoseArray(skill, msg);

        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        PlayerTuto.Instance.canAttack = true;
        skill.messageType = SkillTuto.typeOfMessage.Charging;
        //Lancer Methode pour le Text;
    }

    public void PlayerEmojis(Skill skill)
    {
        GameObject msg = Instantiate(EmojiPlayer.gameObject, playerMsgPositions[1].transform.position, Quaternion.identity);
        msg.transform.SetParent(playerMsgPositions[1].transform);
        allMsg[1] = msg;
        skill.messageOwner = msg;
        if (skill.comesFromCurse)
        {
            skill.messageOwner.GetComponent<Image>().color = ConversationManagerTuto.Instance.cursedColor;
            skill.comesFromCurse = false;
        }
        CombatManagerTuto.Instance.EmojiIcon(msg.GetComponent<Image>(), skill);
        CombatManagerTuto.Instance.MessageIcon(msg, skill);
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
    public void EnemySmallMessage(SkillTuto skill)
    {
        //A lancer à la fin de la coroutine.
        GameObject msg = Instantiate(SmallMessageEnemy.gameObject, enemyMsgPositions[1].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[1].transform);
        allMsg[1] = msg;
        skill.messageOwner = msg;
        if (skill.comesFromCurse)
        {
            skill.messageOwner.GetComponent<Image>().color = ConversationManager.Instance.cursedColor;

        }

        CombatManagerTuto.Instance.MessageIcon(msg, skill);
        messageManager.ChoseArray(skill, msg);
        skill.MonsterEffect();
        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        //Lancer Methode pour le Text;
    }

    public void EnemyChargingMessage(SkillTuto skill)
    {
        //Initialisation.
        enemyChargingAttack.SetActive(true);
        skill.messageOwner = enemyChargingAttack;
        skill.messageType = SkillTuto.typeOfMessage.Big;
        EnemyTuto.Instance.isCharging = true;

        //Changer la couleur du message qui se charge en fonction de si il est curse ou non.
        if (skill.comesFromCurse)
        {
            skill.messageOwner.GetComponent<Image>().color = ConversationManagerTuto.Instance.cursedColor;

        }
        //else
        //{
        //    skill.messageOwner.GetComponent<Image>().color = Color.red;
        //}

        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        EnemyTuto.Instance.canAttack = false;
        StartCoroutine(EnemyTuto.Instance.EndEnemyChargeAttack(skill));
    }

    public void EnemyLargeMessage(SkillTuto skill)
    {
        enemyChargingAttack.SetActive(false);
        EnemyTuto.Instance.isCharging = false;
        GameObject msg = Instantiate(BigMessageEnemy.gameObject, enemyMsgPositions[2].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[2].transform);
        allMsg[2] = msg;
        skill.messageOwner = msg;

        if (skill.comesFromCurse)
        {
            skill.messageOwner.GetComponent<Image>().color = ConversationManager.Instance.cursedColor;

        }

        CombatManagerTuto.Instance.MessageIcon(msg, skill);
        messageManager.ChoseArray(skill, msg);

        messageToSpawn = typeToSpawn.Null;
        canAttack = true;
        EnemyTuto.Instance.canAttack = true;
        skill.messageType = SkillTuto.typeOfMessage.Charging;
    }

    public void EnemyEmojis(Skill skill)
    {
        GameObject msg = Instantiate(EmojiEnemy.gameObject, enemyMsgPositions[1].transform.position, Quaternion.identity);
        msg.transform.SetParent(enemyMsgPositions[1].transform);
        allMsg[1] = msg;
        skill.messageOwner = msg;
        CombatManagerTuto.Instance.EmojiIcon(msg.GetComponent<Image>(), skill);
        CombatManagerTuto.Instance.MessageIcon(msg, skill);
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
