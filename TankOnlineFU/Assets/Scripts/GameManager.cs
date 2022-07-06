using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player1Prefag;
    public GameObject player2Prefag;

    public GameObject box;
    
    public bool isPowerUpSpawn = false;

    private float delayPowerUp = 10f;
    public float countTime = 0f;

    private GameObject player1;
    private GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        FindingTank();
    }

    // Update is called once per frame
    void Update()
    {
        countTime += Time.deltaTime;
        if (player1 == null || player2 == null)
        {
            ReviveTank();
            FindingTank();
        }
        if (isPowerUpSpawn || countTime < delayPowerUp) return;
        SpawnPowerUp();
    }

    void FindingTank()
    {
        player1 = GameObject.Find("Player1");
        player2 = GameObject.Find("Player2");
    }

    void ReviveTank()
    {
        float randomX = Random.Range(-11f, 11f);
        float randomY = Random.Range(-2.48f, 2.48f);
        if (player1 == null)
        {
            GameObject tank = Instantiate(player1Prefag, new Vector3(randomX, randomY), new Quaternion(0, 0, 0, 0));
            tank.name = "Player1";
        }
        if (player2 == null)
        {
            GameObject tank = Instantiate(player2Prefag, new Vector3(randomX, randomY), new Quaternion(0, 0, 0, 0));
            tank.name = "Player2";
        }
    }

    void SpawnPowerUp()
    {
        float randomX = Random.Range(-11f, 11f);
        float randomY = Random.Range(-2.48f, 2.48f);
        Instantiate(box, new Vector3(randomX, randomY), new Quaternion(0, 0, 0, 0));
        countTime = 0f;
        isPowerUpSpawn = true;

        //int index = Random.Range(0, powerUp.Length);
        //Debug.Log(index);
        //Instantiate(powerUp[index], new Vector3(randomX, randomY), new Quaternion(0, 0, 0, 0));
        //countTime = 0f;
        //isPowerUpSpawn = true;
    }
}
