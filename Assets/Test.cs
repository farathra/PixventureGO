using System;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public string playerName = "Player1";

    private void Start()
    {
        Debug.Log($"{playerName} has join the game");
    }
}
