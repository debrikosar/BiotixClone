using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject levelTracker;

    private GameObject activeLevelTracker;

    [SerializeField]
    private GameObject[] levels;

    private void Start()
    {
        if (GameObject.FindGameObjectsWithTag("LevelTracker").Length == 0)
            Instantiate(levelTracker);

        activeLevelTracker = GameObject.FindGameObjectWithTag("LevelTracker");

        int activeLevel = activeLevelTracker.GetComponent<LevelTracker>().ActiveLevel;
        if (activeLevel >= levels.Length)
            activeLevel = 0;
        levels[activeLevel].SetActive(true);
    }
}
