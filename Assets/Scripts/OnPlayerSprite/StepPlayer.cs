using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepPlayer : MonoBehaviour
{
    [SerializeField] private GameObject audioMaster;
    public void PlayStep()
    {
        audioMaster.GetComponent<AudioMaster>().PlayRandomStep();
    }
}
