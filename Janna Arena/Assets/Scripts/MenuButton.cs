using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButton : MonoBehaviour
{
    public GameObject panelSettings; //Насстройки

    private void Start()
    {
        if (panelSettings != null)
            panelSettings.SetActive(false); // Изначально закрыта
    }

    public void Play()
    {
        SceneManager.LoadScene("Gameplay"); //Загрузка Игры
        Time.timeScale = 1.0f;
    }

    public void Back()
    {
        SceneManager.LoadScene("Menu"); //Загрузка Меню
    }


    public void Settings()
    {
        if(panelSettings.activeSelf == false)
        {
            panelSettings.SetActive(true);
        }
        else if(panelSettings.activeSelf == true)
        {
            panelSettings.SetActive(false);
        }
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Exit");
    }

}
