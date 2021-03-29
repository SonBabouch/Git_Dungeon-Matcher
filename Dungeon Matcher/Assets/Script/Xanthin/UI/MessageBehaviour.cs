using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBehaviour : MonoBehaviour
{
    public enum team{Player,Enemy};
    public team teamMsg;

    public bool ally;
    public bool big;

    public int currentPosition = 0;

}
