using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public static UI instance;

    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI timeStatsText;
    [SerializeField] private TextMeshProUGUI killStatsText;
    [Space]
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI killCountText;
    private int killCount = 0;
    bool gameOver = false;


    private void Awake()
    {
        instance = this;
        Time.timeScale = 1;
        killCountText.text = killCount.ToString();
        timeText.text = 0.00f.ToString("F2") + "s";
        gameOver = false;
        Player.instance.EnableMove(true);
    }

    private void Update()
    {
        if (!gameOver)
            timeText.text = Time.timeSinceLevelLoad.ToString("F2") + "s";
    }

    public void GameOver()
    {
        gameOver = true;
        Player.instance.EnableMove(false);
        gameOverUI.SetActive(true);
        Time.timeScale = 0.2f;
        timeStatsText.text = Time.timeSinceLevelLoad.ToString("F2") + " Seconds";
        killStatsText.text = killCount.ToString() + " Kills";
    }

    public void TryAgain()
    {
        gameOver = false;
        Player.instance.EnableMove(true);
        killCount = 0;
        timeText.text = "0.00s";
        gameOverUI.SetActive(false);
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
    }

    public void AddKillCount() // This method can be called from other scripts to update the kill count
    { 
        killCount++; // killCount = killCount + 1;
        killCountText.text = killCount.ToString();
    }
}
