using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioMaster : MonoBehaviour
{
    [SerializeField] private AudioSource stepSource, jumpSource, suricaneSource,bombSource;
    [SerializeField] private AudioClip jumpClip, LandingClip;
    [SerializeField] private AudioClip[] stepClips;

    
    private void OnEnable()
    {
        EventManager.eventOnPlayerJump += PlayJumpSound;
        EventManager.eventOnPlayerLanding += PlayLendingSound;
        EventManager.eventOnPlayerSuricaneAttack += PlayerSuricaneAttackSound;
        EventManager.eventOnBombExplouse += BombExplouseSound;
    }
    private void OnDisable()
    {
        EventManager.eventOnPlayerJump -= PlayJumpSound;
        EventManager.eventOnPlayerLanding -= PlayLendingSound;
        EventManager.eventOnPlayerSuricaneAttack -= PlayerSuricaneAttackSound;
        EventManager.eventOnBombExplouse -= BombExplouseSound;
    }


    private void PlayJumpSound()
    {
        jumpSource.clip = jumpClip;
        jumpSource.volume = 1;
        jumpSource.Play();
    }
    private void PlayLendingSound()
    {
        jumpSource.clip = LandingClip; // выбор клипа
        jumpSource.volume = 0.6f;
        jumpSource.Play();
    }
    private void PlayerSuricaneAttackSound()
    {
        suricaneSource.Play();
    }
    public void PlayRandomStep() // на этот метод ссылаетс€ скрипт StepPlayer, который закреплен на спрайте с аниматором.  оторый дергает вызов метода по Animation Event
    {
        stepSource.clip = stepClips[Random.Range(0, stepClips.Length)];
        stepSource.Play();
    }
    private void BombExplouseSound()
    {
        bombSource.Play();
    }
}
