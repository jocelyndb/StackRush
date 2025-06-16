using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private int _score;
    public int score
    {
        get
        {
            return _score;
        }
        set
        {
            _score = value;
            if (LevelCount >= BlocksPerLevel)
            {
                AdvanceLevel();
            }
            LevelCount++;
        }
    }
    public int level = 1;
    public int BlocksPerLevel = 10;
    public int LevelCount = 1;
    public int LevelBonus = 100;
    public double LevelSizeModifier = 0.9;
    public float MinLevelSize = 0.45f;
    public float InitialBlockSize = 5f;
    public float BaseSpringFactor = 18f;
    public float springFactor = 18f;
    public float BaseMoveSpeed = 50f;
    public float moveSpeed = 50f;
    public List<Powerup> powerups = new List<Powerup>();
    // public List<Powerup> powerupDeleteQueue = new List<Powerup>();
    private static string[] levelNames =
    {
        "Grease Goblin",
        "Deep Disher",
        "Pizza Rat",
        "Dollar Slicer",
        "Frozen Pizza Enjoyer",
        "Pineapple Hater",
        "Pizza Bagel Fan",
        "Slice Stealer",
        "Delivery Boy",
        "One Biter",
        "The Noid",
        "Ninja Turtle",
        "Pizza Man",
        "Pie Slinger",
        "Napolitano",
        "Wood Firer",
        "Pizzaiolo",
        "Sauce Boss",
        "Parm Daddy",
        "Dr Oetker",
        "Lord of the Pies",
        "Pizza God",
        "Nonna",
    };

    private AudioManager audioManager;

    public Vector3 BlockSize
    {
        get
        {
            return new Vector3(InitialBlockSize * Math.Max(MinLevelSize, (float)Math.Pow(LevelSizeModifier, level - 1)), 1f, InitialBlockSize * Math.Max(MinLevelSize, (float)Math.Pow(LevelSizeModifier, level - 1)));
        }
    }

    private Rigidbody _stackTopRB;
    public Rigidbody StackTopRB
    {
        get
        {
            if (!_stackTopRB)
            {
                _stackTopRB = GameObject.Find("StackBottom").GetComponent<Rigidbody>();
            }
            return _stackTopRB;
        }
        set
        {
            _stackTopRB = value;
        }
    }
    private BoxCollider _stackTopCollider;
    public BoxCollider StackTopCollider
    {
        get
        {
            if (!_stackTopCollider)
            {
                _stackTopCollider = GameObject.Find("StackBottom").GetComponent<BoxCollider>();
            }
            return _stackTopCollider;
        }
        set
        {
            // // handle changing top of stack with powerups
            // foreach (Powerup powerup in powerups)
            // {
            //     print("Deactivating powerups while switching stack top");
            //     powerup.DeactivatePowerup();
            // }
            _stackTopCollider = value;
            ReactivatePowerUps();
        }
    }

    private static GameManager _Instance;
    public static GameManager Instance
    {
        get
        {
            if (!_Instance)
            {
                _Instance = new GameObject().AddComponent<GameManager>();
                // name it for easy recognition
                _Instance.name = _Instance.GetType().ToString();
                // mark root as DontDestroyOnLoad();
                // DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    public static void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AdvanceLevel();
        }
        // Debug.Log(Instance.Score);
    }

    // public static void FixedUpdate()
    // {
    //     if (Instance.powerupDeleteQueue.Count > 0)
    //     {
    //         foreach (Powerup powerup in Instance.powerupDeleteQueue)
    //         {
    //             powerup.DeactivatePowerup();
    //             Instance.powerups.Remove(powerup);
    //             GameObject.Destroy(powerup.gameObject);
    //         }
    //         Instance.powerupDeleteQueue.Clear();
    //         foreach (Powerup powerup in Instance.powerups)
    //         {
    //             powerup.ActivatePowerup();
    //         }
    //     }
    // }

    private void Start()
    { 
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        audioManager.PlayMusic();
    }
    private void OnDestroy()
    {
        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        Debug.Log("GameManager Destroyed");
        DeactivatePowerUps();
        audioManager.PauseMusic();
    }

    public static void DeletePowerUp(Powerup powerup)
    {
        Instance.powerups.Remove(powerup);
        ReactivatePowerUps();
    }

    public static void ReactivatePowerUps()
    {
        foreach (Powerup pwr in Instance.powerups)
        {
            pwr.ActivatePowerup();
        }
    }

    public static void DeactivatePowerUps()
    {
        foreach (Powerup pwr in Instance.powerups)
        {
            pwr.DeactivatePowerup();
        }
    }

    public static void TogglePause(bool changingScene = false)
    {
        if (Time.timeScale == 0f)
        {
            Instance.audioManager.PlayMusic();
            Time.timeScale = 1f;
            ReactivatePowerUps();
        }
        else
        {
            Instance.audioManager.PauseMusic();
            Time.timeScale = 0;
        }

        if (changingScene)
        {
            Instance.audioManager.PauseMusic();
        }
    }

    public static void AdvanceLevel()
    {
        // Update level, reset score and block counter
        Instance.level++;
        Instance.LevelCount = 0;
        Instance.score += Instance.LevelBonus;
        Instance.LevelCount = 0;
        // Delete all blocks from stack (and any falling)
        foreach (GameObject obj in FindObjectsByType<GameObject>(FindObjectsSortMode.None))
        {
            if (obj.layer == LayerMask.NameToLayer("Stack") || obj.layer == LayerMask.NameToLayer("Falling"))
            {
                // if (obj.tag == "FallingBlock")
                // {
                //     Destroy(obj);
                // }
                if (obj.tag == "Powerup")
                {
                    continue;
                }
                if (obj.tag == "StackBottom")
                {
                    obj.GetComponent<Rigidbody>().detectCollisions = true;
                }
                else
                {
                    Destroy(obj);
                }

            }
        }
    }

    public static string GetLevelName()
    {
        if (Instance.level > levelNames.Length)
            return levelNames.Last() + " x " + (Instance.level - levelNames.Length + 1);
        return levelNames[Instance.level - 1];
    }

    public static void GameOver()
    {
        UpdateHighScore();
        AudioManager audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // audioManager.PauseMusic();
        audioManager.PlaySFX(audioManager.death);
        SceneManager.LoadScene("GameOverScene");
    }

    public static void UpdateHighScore()
    {
        if (!PlayerPrefs.HasKey("Level") || PlayerPrefs.GetInt("Level") < Instance.level)
        {
            PlayerPrefs.SetInt("Level", Instance.level);
            PlayerPrefs.SetString("Rank", GetLevelName());
        }
        PlayerPrefs.SetString("LastRank", GetLevelName());
    }
}