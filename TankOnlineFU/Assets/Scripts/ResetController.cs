using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResetController : MonoBehaviour
{
    public GameObject canvas;
    public Image fade;
    public Image tank;
    private Animator tankAni;
    private Animator fadeAni;
    // Start is called before the first frame update
    void Start()
    {
        tankAni = tank.GetComponent<Animator>();
        fadeAni = fade.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickRetry()
    {
        canvas.SetActive(true);
        tankAni.SetTrigger("retry");
        fadeAni.SetTrigger("retry");
        Invoke("ResetGame", 3f);
    }

    void ResetGame()
    {
        Score.SCORE_PLAYER1 = 0;
        Score.SCORE_PLAYER2 = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
