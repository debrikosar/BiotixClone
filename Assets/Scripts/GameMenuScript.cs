using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenuScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameMenu;

    public void SummonMenu() => gameMenu.SetActive(gameMenu.activeSelf ? false : true);
}
