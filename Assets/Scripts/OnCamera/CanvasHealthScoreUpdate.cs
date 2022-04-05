using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasHealthScoreUpdate : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ScoreText, HPText;
    
    void Start()
    {
        ScoreText.text = "Glyphs: " + SharedGameData.SharedData.Score;
        HPText.text = "HP: " + SharedGameData.SharedData.Hp;
    }
    void Update()
    {

    }
    private void OnEnable()
    {
        EventManager.eventOnScoreUpdate += UpdateCanvasScore;
        EventManager.eventOnCansvasHealthUpdate += UpdateCanvasHealth;

    }
    private void OnDisable()
    {
        EventManager.eventOnScoreUpdate -= UpdateCanvasScore;
        EventManager.eventOnCansvasHealthUpdate -= UpdateCanvasHealth;
    }
    
    private void UpdateCanvasScore()
    {
        ScoreText.text = "Glyphs: " + SharedGameData.SharedData.Score;
    }
    private void UpdateCanvasHealth()
    {
        HPText.text = "Health: " + SharedGameData.SharedData.Hp;
    }
}
