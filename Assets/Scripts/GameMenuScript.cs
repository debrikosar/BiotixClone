using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameMenu;

    public void SummonMenu() => gameMenu.SetActive(gameMenu.activeSelf ? false : true);

    public void RestartLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
