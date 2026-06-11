using UnityEngine;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayUI;
    [SerializeField] private GameObject CreditsUI;
    [SerializeField] private GameObject QuitUI;

    private void Awake()
    {
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        HowToPlayUI.SetActive(true);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(false);
    }

    public void OpenCredits()
    {
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(true);
        QuitUI.SetActive(false);
    }

    public void OpenQuit()
    {
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(true);
    }

    public void CloseAll()
    {
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}