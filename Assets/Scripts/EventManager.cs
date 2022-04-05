using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventManager
{
    public static Action eventOnBonusFind; // Invoke when player take bonus
    public static void CallOnBonusFind()
    {
        eventOnScoreUpdate?.Invoke();
    }
    public static Action eventOnScoreUpdate; // Invoke when GameMaster Increase Score
    public static void CallOnScoreUpdate()
    {
        eventOnScoreUpdate?.Invoke();
    }
    public static Action eventOnHealthDecrease;
    public static void CallOnHealthDecrease()
    {
        eventOnHealthDecrease?.Invoke();
    }
    public static Action eventOnCansvasHealthUpdate;
    public static void CallOnCansvasHealthUpdate()
    {
        eventOnCansvasHealthUpdate?.Invoke();
    }
    public static Action eventOnPlayerDead;
    public static void CallOnPlayerDead()
    {
        eventOnPlayerDead?.Invoke();
    }
    public static Action eventOnPlayerWin;
    public static void CallOnPlayerWin()
    {
        eventOnPlayerWin?.Invoke();
    }
    public static Action eventOnPlayerJump;
    public static void CallOnPlayerJump()
    {
        eventOnPlayerJump?.Invoke();
    }
    public static Action eventOnPlayerLanding;
    public static void CallOnPlayerLanding()
    {
        eventOnPlayerLanding?.Invoke();
    }
    public static Action eventOnPlayerSuricaneAttack;
    public static void CallOnPlayerSuricaneAttack()
    {
        eventOnPlayerSuricaneAttack?.Invoke();
    }
    public static Action eventOnBombExplouse;
    public static void CallOnBombExplouse()
    {
        eventOnBombExplouse?.Invoke();
    }
}
