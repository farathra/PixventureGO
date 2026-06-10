using System;
using UnityEngine;

public class HealthBar_UI : MonoBehaviour
{
    //private Player player;
    //private ObjectToProtect objectToProtect;
    //private UI ui;


    [Header("Player Health Bar")]
    [SerializeField] private GameObject playerHealthBar4;
    [SerializeField] private GameObject playerHealthBar3;
    [SerializeField] private GameObject playerHealthBar2;
    [SerializeField] private GameObject playerHealthBar1;
    [SerializeField] private GameObject playerHealthBar0;

    [Header("Object to Protect Health Bar")]
    [SerializeField] private GameObject objectToProtectHealthBar3;
    [SerializeField] private GameObject objectToProtectHealthBar2;
    [SerializeField] private GameObject objectToProtectHealthBar1;
    [SerializeField] private GameObject objectToProtectHealthBar0;



    private void Awake()
    {
        playerHealthBar4.SetActive(false);
        playerHealthBar3.SetActive(false);
        playerHealthBar2.SetActive(false);
        playerHealthBar1.SetActive(false);
        playerHealthBar0.SetActive(false);

        objectToProtectHealthBar3.SetActive(true);
        objectToProtectHealthBar2.SetActive(false);
        objectToProtectHealthBar1.SetActive(false);
        objectToProtectHealthBar0.SetActive(false);
    }

    private void Update()
    {
        if (UI.instance.gameOver == false)
        {
            UpdatePlayerHealth();
            UpdateObjectToProtectHealth();
        }
    }

    private void UpdatePlayerHealth()
    {
        if (Player.instance != null)
        {
            int playerHealth = Player.instance.SendHealthInfo();

            if (playerHealth == 5)
            {
                playerHealthBar4.SetActive(false);
                playerHealthBar3.SetActive(false);
                playerHealthBar2.SetActive(false);
                playerHealthBar1.SetActive(false);
                playerHealthBar0.SetActive(false);
            }
            else if (playerHealth == 4)
            {
                playerHealthBar4.SetActive(true);
                playerHealthBar3.SetActive(false);
                playerHealthBar2.SetActive(false);
                playerHealthBar1.SetActive(false);
                playerHealthBar0.SetActive(false);
            }
            else if (playerHealth == 3)
            {
                playerHealthBar4.SetActive(false);
                playerHealthBar3.SetActive(true);
                playerHealthBar2.SetActive(false);
                playerHealthBar1.SetActive(false);
                playerHealthBar0.SetActive(false);
            }
            else if (playerHealth == 2)
            {
                playerHealthBar4.SetActive(false);
                playerHealthBar3.SetActive(false);
                playerHealthBar2.SetActive(true);
                playerHealthBar1.SetActive(false);
                playerHealthBar0.SetActive(false);
            }
            else if (playerHealth == 1)
            {
                playerHealthBar4.SetActive(false);
                playerHealthBar3.SetActive(false);
                playerHealthBar2.SetActive(false);
                playerHealthBar1.SetActive(true);
                playerHealthBar0.SetActive(false);
            }
            else if (playerHealth <= 0)
            {
                playerHealthBar3.SetActive(false);
                playerHealthBar2.SetActive(false);
                playerHealthBar1.SetActive(false);
                playerHealthBar0.SetActive(true);
            }
        }
    }

    private void UpdateObjectToProtectHealth()
    {
        if (ObjectToProtect.instance != null)
        {
            int objectHealth = ObjectToProtect.instance.SendHealthInfo();

            if (objectHealth == 3)
            {
                objectToProtectHealthBar3.SetActive(true);
                objectToProtectHealthBar2.SetActive(false);
                objectToProtectHealthBar1.SetActive(false);
                objectToProtectHealthBar0.SetActive(false);
            }
            else if (objectHealth == 2)
            {
                objectToProtectHealthBar3.SetActive(false);
                objectToProtectHealthBar2.SetActive(true);
                objectToProtectHealthBar1.SetActive(false);
                objectToProtectHealthBar0.SetActive(false);
            }
            else if (objectHealth == 1)
            {
                objectToProtectHealthBar3.SetActive(false);
                objectToProtectHealthBar2.SetActive(false);
                objectToProtectHealthBar1.SetActive(true);
                objectToProtectHealthBar0.SetActive(false);
            }
            else if (objectHealth <= 0)
            {
                objectToProtectHealthBar3.SetActive(false);
                objectToProtectHealthBar2.SetActive(false);
                objectToProtectHealthBar1.SetActive(false);
                objectToProtectHealthBar0.SetActive(true);
            }
        }
    }
}
