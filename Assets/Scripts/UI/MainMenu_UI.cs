using UnityEngine;

public class MainMenu_UI : MonoBehaviour
{
    [SerializeField] private GameObject HowToPlayUI;
    [SerializeField] private GameObject CreditsUI;
    [SerializeField] private GameObject QuitUI;

    [SerializeField] private AudioSource buttonClickSFX;

    private void Awake()
    {
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(false);
    }

    public void OpenHowToPlay()
    {
        buttonClickSFX.Play();
        HowToPlayUI.SetActive(true);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(false);
    }

    public void OpenCredits()
    {
        buttonClickSFX.Play();
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(true);
        QuitUI.SetActive(false);
    }

    public void OpenQuit()
    {
        buttonClickSFX.Play();
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(true);
    }

    public void CloseAll()
    {
        buttonClickSFX.Play();
        HowToPlayUI.SetActive(false);
        CreditsUI.SetActive(false);
        QuitUI.SetActive(false);
    }

    public void QuitGame()
    {
        buttonClickSFX.Play();
        Application.Quit();
    }

    public void LoadGame()
    {
        buttonClickSFX.Play();
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
}