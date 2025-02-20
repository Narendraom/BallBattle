using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    public enum GameState { Attack, Defense }
    public GameState currentState = GameState.Attack;

   
   

    public int playerWins = 0;
    public int enemyWins = 0;
    public int totalMatches = 5;
    private int currentMatch = 1;

   
    private bool isPenaltyGame = false;



    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartMatch();
    }

   

    void StartMatch()
    {
        if (currentMatch > totalMatches)
        {
            EndGame();
            return;
        }


        Debug.Log("Match " + currentMatch + " Started: " + currentState);
    }

   
    void EndGame()
    {
        string result = playerWins > enemyWins ? "YOU WIN!" : "GAME OVER!";
        Debug.Log(result);
        // Implement UI or scene transition for game end.
    }

    public void BallReachedGate()
    {
        Debug.Log("Attacker Wins this match!");
        playerWins++;
        EndMatch(true);
    }

    public void EndMatch(bool attackerWon)
    {
        if (!attackerWon)
        {
            Debug.Log("Defender Wins this match!");
            enemyWins++;
        }

        currentMatch++;

        if (currentMatch > totalMatches)
        {
            CheckGameOver();
        }
        else
        {
            RestartMatch();
        }
    }

    private void CheckGameOver()
    {
        Debug.Log("Checking final results...");

        if (playerWins > enemyWins)
        {
            Debug.Log("Player is the Winner!");
            ShowWinScreen();
        }
        else if (playerWins < enemyWins)
        {
            Debug.Log("Game Over! Enemy Wins.");
            ShowLoseScreen();
        }
        else
        {
            Debug.Log("It's a tie! Starting Penalty Game.");
            StartPenaltyGame();
        }
    }

    private void RestartMatch()
    {
        Debug.Log($"Starting Match {currentMatch}...");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartPenaltyGame()
    {
        isPenaltyGame = true;
        Debug.Log("Penaly Games");
        //SceneManager.LoadScene("PenaltyGameScene"); // Ensure you have a scene for this
    }

    private void ShowWinScreen()
    {
        Debug.Log("Win");
        //SceneManager.LoadScene("WinScreen"); // Add a win scene
    }

    private void ShowLoseScreen()
    {
        Debug.Log("Loss");
        //SceneManager.LoadScene("LoseScreen"); // Add a lose scene
    }
}
