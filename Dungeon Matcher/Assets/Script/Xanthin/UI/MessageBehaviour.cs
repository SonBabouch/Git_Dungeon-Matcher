using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBehaviour : MonoBehaviour
{
    public enum team{Player,Enemy};
    public team teamMsg;

    //Changer BoostInspiration & SlowInspiration plutot que inspirationLimitation;
    public enum effect {InspirationLimitation, AttiranceLimitation, Curse, Mark, Charm, Silence, Lock, Cramp };
    public effect EmojiEffect;

    public bool ally;
    public bool big;
    public bool emoji;

    public void GetEffect(int number)
    {
        switch (number)
        {
            //0 = Null;
            case 1:
                EmojiEffect = effect.InspirationLimitation;
                break;
            case 2:
                EmojiEffect = effect.AttiranceLimitation;
                break;
            case 3:
                EmojiEffect = effect.Curse;
                break;
            case 4:
                EmojiEffect = effect.Mark;
                break;
            case 5:
                EmojiEffect = effect.Silence;
                break;
            case 6:
                EmojiEffect = effect.Lock;
                break;
            case 7:
                EmojiEffect = effect.Cramp;
                break;
            default:
                break;
        }
    }

    public void EmojiEffectBegin()
    {
        //Changer ce qu'il faut quand la compétence est lancé chez le joueur
        if(teamMsg == team.Player)
        {
            switch (EmojiEffect)
            {
                case effect.InspirationLimitation:
                    //Change la bool en true;
                    break;
                case effect.AttiranceLimitation:
                    //Change la bool en true;
                    break;
                case effect.Curse:
                    //Change la bool en true;
                    break;
                case effect.Mark:
                    Player.Instance.isBoosted = true;
                    break;
                case effect.Charm:
                    //Change la bool en true;
                    break;
                case effect.Silence:
                    //Change la bool en true;
                    break;
                case effect.Lock:
                    //Change la bool en true;
                    break; ;
                case effect.Cramp:
                    Enemy.Instance.isCramp = true;
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (EmojiEffect)
            {
                case effect.InspirationLimitation:
                    //Change la bool en false;
                    break;
                case effect.AttiranceLimitation:
                    //Change la bool en false;
                    break;
                case effect.Curse:
                    //Change la bool en false;
                    break;
                case effect.Mark:
                    Enemy.Instance.isBoosted = true;
                    break;
                case effect.Charm:
                    //Change la bool en false;
                    break;
                case effect.Silence:
                    //Change la bool en false;
                    break;
                case effect.Lock:
                    //Change la bool en false;
                    break; ;
                case effect.Cramp:
                    Player.Instance.isCramp = true;
                    break;
                default:
                    break;
            }
        }
    }
    public void EmojiEffectEnd()
    {

        Debug.Log("CancelCramp");
        //Changer ce qu'il faut quand la compétence est lancé chez le joueur
        if (teamMsg == team.Player)
        {
            switch (EmojiEffect)
            {
                case effect.InspirationLimitation:
                    //Effet
                    break;
                case effect.AttiranceLimitation:
                    //Effet
                    break;
                case effect.Curse:
                    //Effet
                    break;
                case effect.Mark:
                    Player.Instance.isBoosted = false;
                    break;
                case effect.Charm:
                    //Effet
                    break;
                case effect.Silence:
                    //Effet
                    break;
                case effect.Lock:
                    //Effet
                    break; ;
                case effect.Cramp:
                    Enemy.Instance.isCramp = false;
                    
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (EmojiEffect)
            {
                case effect.InspirationLimitation:
                    //Effet
                    break;
                case effect.AttiranceLimitation:
                    //Effet
                    break;
                case effect.Curse:
                    //Effet
                    break;
                case effect.Mark:
                    Enemy.Instance.isBoosted = false;
                    break;
                case effect.Charm:
                    //Effet
                    break;
                case effect.Silence:
                    //Effet
                    break;
                case effect.Lock:
                    //Effet
                    break; ;
                case effect.Cramp:

                    Player.Instance.isCramp = false;
                    CombatManager.Instance.ButtonsUpdate();
                    break;
                default:
                    break;
            }
        }
    }


   


}
