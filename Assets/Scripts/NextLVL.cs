using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLVL : MonoBehaviour
{
    
   public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void Repeat()
    {
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
