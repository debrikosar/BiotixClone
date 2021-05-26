using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class EndMenuScript : MonoBehaviour
{
    private GameObject activeLevelTracker;

    public void RestartPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinuePressed()
    {
        activeLevelTracker = GameObject.FindGameObjectWithTag("LevelTracker");
        activeLevelTracker.GetComponent<LevelTracker>().ActiveLevel++;
        SceneManager.LoadScene(0);
    }
}
