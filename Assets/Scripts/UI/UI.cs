using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject HowToPlayUI;
    [SerializeField] private TextMeshProUGUI timeStatsText;
    [SerializeField] private TextMeshProUGUI killStatsText;
    [Space]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI killCountText;
    private int killCount = 0;
    public bool gameOver = false;
    private bool paused = false;
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
        HowToPlay();
    }

    private void Update()
    {
        HandleInput();
        if (!gameOver)
            timeText.text = Time.timeSinceLevelLoad.ToString("F2") + "s";
    }

    public void GameOver()
    {
        if (!gameOver)
        {
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
            paused = true;
            Time.timeScale = 0;
            EntityMoveAndJump(false);
            pauseMenuUI.SetActive(true);
        }
    }

    public void ResumeGame()
    {
        paused = false;
        Time.timeScale = 1;
        EntityMoveAndJump(true);
        pauseMenuUI.SetActive(false);
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

    private void HowToPlay()
    {
        //if (firstLoad)
        //{
        //    Time.timeScale = 0;
        //    HowToPlayUI.SetActive(true);
        //    howToPlayActive = true;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //    HowToPlayUI.SetActive(false);
        //    howToPlayActive = false;
        //}
    }

    public void PlayGame()
    {
        HowToPlayUI.SetActive(false);
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
}
