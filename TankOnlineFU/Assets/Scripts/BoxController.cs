using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public GameObject[] powerUp;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropPowerUp()
    {
        int random = Random.Range(0, 2);
        if(random == 1)
        {
            int index = Random.Range(0,powerUp.Length);
            Instantiate(powerUp[index], transform.position, transform.rotation);
            gameManager.countTime = 0f;
        }
        else
        {
            gameManager.isPowerUpSpawn = false;
        }
        Destroy(gameObject);
    }
}
