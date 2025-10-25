using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Update is called once per frame
    public void LoseGame()
    {
        SceneManager.LoadScene("Lost");
    }
    
    public void WinGame()
    {
        SceneManager.LoadScene("Win");
    }
}
