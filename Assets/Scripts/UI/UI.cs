using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject MobileControlUI;
    [SerializeField] private TextMeshProUGUI timeStatsText;
    [SerializeField] private TextMeshProUGUI killStatsText;
    [Space]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI killCountText;

    [SerializeField] AudioSource GameplayBGM;
    [SerializeField] AudioSource GameOverBGM;
    [SerializeField] AudioSource ButtonClickSFX;
    [SerializeField] AudioSource PausedSFX;

    private int killCount = 0;
    public bool gameOver = false;
    public bool paused = false;
    private bool howToPlayActive = false;
    private bool firstLoad;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        killCountText.text = killCount.ToString();
        timeText.text = 0.00f.ToString("F2") + "s";
        gameOver = false;
        paused = false;
        firstLoad = true;
        EntityMoveAndJump(true);
    }

    private void Start()
    {
        GameOverBGM.Stop();
        GameplayBGM.Play();
    }

    private void Update()
    {
        HandleInput();
        HandleMobileControls();
        if (!gameOver)
            timeText.text = Time.timeSinceLevelLoad.ToString("F2") + "s";
    }


    public void GameOver()
    {
        if (!gameOver)
        {
            GameplayBGM.Stop();
            GameOverBGM.Play();
            gameOver = true;
            EntityMoveAndJump(false);
            gameOverUI.SetActive(true);
            ResumeGame();
            Time.timeScale = 0.2f;
            timeStatsText.text = Time.timeSinceLevelLoad.ToString("F2") + " Seconds";
            killStatsText.text = killCount.ToString() + " Kills";
        }
    }

    public void TryAgain()
    {
        GameOverBGM.Stop();
        ButtonClickSFX.Play();
        gameOver = false;
        EntityMoveAndJump(true);
        killCount = 0;
        timeText.text = "0.00s";
        gameOverUI.SetActive(false);
        ResumeGame();
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount() // This method can be called from other scripts to update the kill count
    {
        killCount++; // killCount = killCount + 1;
        killCountText.text = killCount.ToString();
    }

    public void PauseGame()
    {
        if (!gameOver && !howToPlayActive)
        {
            GameplayBGM.Pause();
            PausedSFX.Play();
            paused = true;
            Time.timeScale = 0;
            EntityMoveAndJump(false);
            pauseMenuUI.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        ButtonClickSFX.Play();
        paused = false;
        Time.timeScale = 1;
        EntityMoveAndJump(true);
        pauseMenuUI.SetActive(false);
        GameplayBGM.UnPause();
    }

    private void HandleInput()
    { 
        if (Input.GetKeyDown(KeyCode.Escape) && !paused)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && paused)
        {
            ResumeGame();
        }
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        howToPlayActive = false;
        firstLoad = false;
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus && !gameOver)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    private void HandleMobileControls()
    {
        if (Application.isMobilePlatform && !gameOver && !paused)
        {
            MobileControlUI.SetActive(true);
        }
        else
        {
            MobileControlUI.SetActive(false);
        }
    }

    private void EntityMoveAndJump(bool enable)
    {
        //if (enable)
        //{
        //    Player.instance.ResetMoveAndJump();
        //    Enemy.instance.ResetMove();
        //}
        //else
        //{
        //    Player.instance.StopMoveAndJump();
        //    Enemy.instance.StopMove();
        //}
    }

    public void MainMenu()
    {
        ButtonClickSFX.Play();
        Time.timeScale = 0;
        SceneManager.LoadScene(0);
    }
}
