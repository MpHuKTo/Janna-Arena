using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;


public class RestartButton : MonoBehaviour
{
    private PlayerController playerController;
    [SerializeField] public TMP_Text KilledText;
    [SerializeField] public TMP_Text TimeLived;


    private float TimeLivedLast = 0;
    private float TimeLive = 0;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();

        TimeLived.text = Convert.ToString(Time.realtimeSinceStartup);
        KilledText.text = Convert.ToString(playerController.killedEnemies);



    }

    public void OnMenuClick()
    {

        SceneManager.LoadScene("Menu");

    }


    public void OnRestartClick()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
        Time.timeScale = 1.0f;

    }
}
