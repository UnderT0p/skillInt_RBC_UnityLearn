using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{

    [SerializeField] private GameObject winMenu;
    [SerializeField] private GameObject losseMenu;

    void Start()
    {
        Time.timeScale = 1;
        SharedGameData.SharedData.ResetParametr();
    }

    private void OnEnable()
    {
        EventManager.eventOnBonusFind += UpdateScore;
        EventManager.eventOnHealthDecrease += DecreaseHealth;
        EventManager.eventOnPlayerDead += PlayerDead;
        EventManager.eventOnPlayerWin += PlayerWin;

    }
    private void OnDisable()
    {
        EventManager.eventOnBonusFind -= UpdateScore;
        EventManager.eventOnHealthDecrease -= DecreaseHealth;
        EventManager.eventOnPlayerDead -= PlayerDead;
        EventManager.eventOnPlayerWin -= PlayerWin;
    }
    private void UpdateScore()
    {
        SharedGameData.SharedData.Score+=1;
        EventManager.CallOnScoreUpdate();
    }
    private void DecreaseHealth()
    {
        SharedGameData.SharedData.Hp -= 10;
        EventManager.CallOnCansvasHealthUpdate();
        if (SharedGameData.SharedData.Hp<=0)
        {
            PlayerDead();
        }
    }
    private void PlayerDead()
    {
        losseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    private void PlayerWin()
    {
        winMenu.SetActive(true);
        Time.timeScale = 0;
    }

}
