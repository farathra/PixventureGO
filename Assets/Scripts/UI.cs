using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public static UI instance;

    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI killCountText;
    private int killCount;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        timeText.text = Time.time.ToString("F2") + "s";
    }

    public void AddKillCount() // This method can be called from other scripts to update the kill count
    { 
        killCount++; // killCount = killCount + 1;
        killCountText.text = killCount.ToString();
    }
}
