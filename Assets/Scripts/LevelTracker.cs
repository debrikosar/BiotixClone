using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTracker : MonoBehaviour
{
    public static LevelTracker instance = null;

    private int activeLevel;

    public int ActiveLevel
    {
        get => activeLevel;
        set => activeLevel = value;
    }

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            activeLevel = 0;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
}
