using DefaultNamespace;
using Entity;
using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TankController : MonoBehaviour
{
    // Start is called before the first frame update
    public Tank tank;

    public Sprite tankUp;
    public Sprite tankDown;
    public Sprite tankLeft;
    public Sprite tankRight;
    private TankMover _tankMover;
    public int HP = 10;
    //private CameraController _cameraController;
    private SpriteRenderer _renderer;

    public Slider[] sliderAll;

    KeyCode keyCodeLeft;
    KeyCode keyCodeRight;
    KeyCode keyCodeUp;
    KeyCode keyCodeDown;
    KeyCode keyFire;

    public Sprite tankExp;
    private Slider slider;
    private Text score;
    private AudioSource expAudio;
    private bool isAlive = true;
    private float timePowerUp = 0f;

    private GameManager gameManager;
    private TankFirer tf;

    private void Start()
    {
        tank = new Tank
        {
            Name = "Default",
            Direction = Direction.Down,
            Hp = 10,
            Point = 0,
            Damage = 1,
            Position = transform.position,
            Guid = GUID.Generate()
        };
        gameObject.transform.position = tank.Position;
        _tankMover = gameObject.GetComponent<TankMover>();
        //_cameraController = camera.GetComponent<CameraController>();
        _renderer = gameObject.GetComponent<SpriteRenderer>();
        expAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        tf = GetComponent<TankFirer>();

        if (gameObject.CompareTag("Player1"))
        {
            slider = GameObject.Find("HeartBar_Player1").GetComponent<Slider>();
            score = GameObject.Find("Score_Player2").GetComponent<Text>();
        }
        else if (gameObject.CompareTag("Player2"))
        {
            slider = GameObject.Find("HeartBar_Player2").GetComponent<Slider>();
            score = GameObject.Find("Score_Player1").GetComponent<Text>();
        }

        slider.value = 10;

        Move(Direction.Down);


        if (gameObject.CompareTag("Player1"))
        {
            keyCodeLeft = KeyCode.A;
            keyCodeRight = KeyCode.D;
            keyCodeUp = KeyCode.W;
            keyCodeDown = KeyCode.S;
            keyFire = KeyCode.Space;
        }
        else
        {
            keyCodeLeft = KeyCode.LeftArrow;
            keyCodeRight = KeyCode.RightArrow;
            keyCodeUp = KeyCode.UpArrow;
            keyCodeDown = KeyCode.DownArrow;
            keyFire = KeyCode.Return;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAlive)
        {
            if (Input.GetKey(keyCodeLeft))
            {
                Move(Direction.Left);
            }
            else if (Input.GetKey(keyCodeDown))
            {
                Move(Direction.Down);
            }
            else if (Input.GetKey(keyCodeRight))
            {
                Move(Direction.Right);
            }
            else if (Input.GetKey(keyCodeUp))
            {
                Move(Direction.Up);
            }

            if (Input.GetKey(keyFire))
            {
                Fire();
            }

            if(timePowerUp > 0)
            {
                timePowerUp -= Time.deltaTime;
            }
            else
            {
                ResetPowerUp();
            }
        }
    }

    private void Move(Direction direction)
    {
        tank.Position = _tankMover.Move(direction);
        tank.Direction = direction;
        //_cameraController.Move(_tank.Position);
        _renderer.sprite = direction switch
        {
            Direction.Down => tankDown,
            Direction.Up => tankUp,
            Direction.Left => tankLeft,
            Direction.Right => tankRight,
            _ => _renderer.sprite
        };
    }

    private void Fire()
    {
        var b = new Bullet
        {
            Direction = tank.Direction,
            Tank = tank,
            InitialPosition = tank.Position
        };
        GetComponent<TankFirer>().Fire(b);
    }

    public void TakeDamage()
    {
        tank.Hp-=tank.Damage;
        slider.value = tank.Hp;
        Debug.Log(tank.Hp);
        if(tank.Hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        isAlive = false;
        _renderer.sprite = tankExp;
        expAudio.Play();
        Invoke("DestroyTank", 0.5f);
    }

    private void DestroyTank()
    {
        if (gameObject.CompareTag("Player1"))
        {
            Score.SCORE_PLAYER2++;
            score.text = Score.SCORE_PLAYER2 + "";
            Debug.Log(Score.SCORE_PLAYER2);
        }
        else
        {
            Score.SCORE_PLAYER1++;
            score.text = Score.SCORE_PLAYER1 + "";
            Debug.Log(Score.SCORE_PLAYER1);
        }
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.CompareTag("PowerUp Bullet"))
        {
            tf.delay = 0.5f;
            Destroy(collision.gameObject);
            gameManager.isPowerUpSpawn = false;
            timePowerUp = 5f;
        }
    }

    void ResetPowerUp()
    {
        tf.delay = 1f;
    }
}
