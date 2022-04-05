using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharedGameData : MonoBehaviour
{
    public static SharedGameData SharedData { get; private set; }

    

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (SharedData == null)
        {
            SharedData = this;
        }
        else if (SharedData != this)
        {
            Destroy(gameObject);
        }
    }
    
    private float hp=100;

    public float Hp
    {
        get { return hp; }
        set { hp = value; }
    }

    
    private int score = 0;

    public int Score
    {
        get { return score; }
        set { score = value; }
    }


    public void ResetParametr()
    {
        hp = 100;
        score = 0;
    }
    

}
