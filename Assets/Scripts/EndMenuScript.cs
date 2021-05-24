using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenuScript : MonoBehaviour
{    
    public void RestartPressed()
    {
        SceneManager.LoadScene(0);
    }

    public void ContinuePressed()
    {
        SceneManager.LoadScene(0);
    }
}
