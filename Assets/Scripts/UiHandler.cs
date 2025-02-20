using System.IO;
using TMPro;
using UnityEngine;
public class UiHandler : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    public GameObject attackUI;
    public GameObject defenseUI;

    public float matchTime = 140f;
    private float timer;


    void Start()
    {
        timer = matchTime;
        UpdateTimerUI();
    }


    void StartMatch()
    {

        if (GameManager.Instance.currentState == GameManager.GameState.Attack)
        {
            attackUI.SetActive(true);
            defenseUI.SetActive(false);
        }
        else
        {
            attackUI.SetActive(false);
            defenseUI.SetActive(true);
        }

       
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.CeilToInt(timer) + "s";
    }


    void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            UpdateTimerUI();
        }
        else
        {
            GameManager.Instance.EndMatch(false);
        }
    }
}
