using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetGame()
    {
        Score.SCORE_PLAYER1 = 0;
        Score.SCORE_PLAYER2 = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
