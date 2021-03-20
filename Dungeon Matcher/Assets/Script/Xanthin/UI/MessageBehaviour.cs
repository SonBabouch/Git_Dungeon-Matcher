using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBehaviour : MonoBehaviour
{
    public enum typeOf {Small,Big };
    public typeOf typeOfMsg;

    public enum team{Player,Enemy};
    public team teamMsg;

    public int currentPosition = 0;

}
