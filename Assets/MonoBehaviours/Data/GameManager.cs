using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using NUnit.Framework;
using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SocialPlatforms.Impl;

public class GameManager : MonoBehaviour
{

    private int _score;
    public int Score
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
    public int Level = 1;
    public int BlocksPerLevel = 10;
    public int LevelCount = 1;
    public int LevelBonus = 100;
    public double LevelSizeModifier = 0.9;
    public float minLevelSize = 0.45f;
    public float InitialBlockSize = 5f;
    public float baseSpringFactor = 18f;
    public float springFactor = 18f;
    public float baseMoveSpeed = 50f;
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

    public Vector3 BlockSize
    {
        get
        {
            return new Vector3(InitialBlockSize * Math.Max(minLevelSize, (float)Math.Pow(LevelSizeModifier, Level - 1)), 1f, InitialBlockSize * Math.Max(minLevelSize, (float)Math.Pow(LevelSizeModifier, Level - 1)));
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
            // handle changing top of stack with powerups
            foreach (Powerup powerup in powerups)
            {
                print("Deactivating powerups while switching stack top");
                powerup.DeactivatePowerup();
            }
            _stackTopCollider = value;
            foreach (Powerup powerup in powerups)
            {
                print("Reactivating powerups after switching stack top");
                powerup.ActivatePowerup();
            }
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
                DontDestroyOnLoad(_Instance.gameObject);
            }
            return _Instance;
        }
    }

    public static void Awake()
    {
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

    public static void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Time.timeScale = 1f;
            ReactivatePowerUps();
        }
        else
        {
            Time.timeScale = 0;
        }
    }

    public static void AdvanceLevel()
    {
        // Update level, reset score and block counter
        Instance.Level++;
        Instance.LevelCount = 0;
        Instance.Score += Instance.LevelBonus;
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
        if (Instance.Level > levelNames.Length)
            return levelNames.Last() + " x " + (Instance.Level - levelNames.Length + 1);
        return levelNames[Instance.Level - 1];
    }
}