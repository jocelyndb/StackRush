using System;
using System.Collections;
using System.Collections.Generic;
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
    public double LevelSizeModifier = 0.8;
    public float InitialBlockSize = 5f;

    public Vector3 BlockSize
    {
        get
        {
            return new Vector3(InitialBlockSize * (float)Math.Pow(LevelSizeModifier, Level - 1), 1f, InitialBlockSize * (float)Math.Pow(LevelSizeModifier, Level - 1));
        }
    }

    private Rigidbody _stackTop;
    public Rigidbody StackTop
    {
        get
        {
            if (!_stackTop)
            {
                _stackTop = GameObject.Find("StackBottom").GetComponent<Rigidbody>();
            }
            return _stackTop;
        }
        set
        {
            _stackTop = value;
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

    public static void Start()
    {
        Instance.Score = 0;
    }

    public static void Update()
    {
        // Debug.Log(Instance.Score);
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
                if (obj.name != "StackBottom")
                {
                    Destroy(obj);
                }
                else
                {
                    obj.GetComponent<Rigidbody>().detectCollisions = true;
                }

            }
        }
    }
}