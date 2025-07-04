using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public bool IsPlayerOneTurn { get; internal set; }

    [SerializeField] private GameMode gameMode;
    [SerializeField] private GridGenerator gridGenerator;
    [SerializeField] private SimpleAI ai;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    private int playerScore = 0;
    private int aiScore = 0;

    public int rows;
    public int cols;

    public GameMode mode;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Setup(int r, int c, GameMode m)
    {
        rows = r;
        cols = c;
        mode = m;

        IsPlayerOneTurn = true;

        gridGenerator.Generate(rows, cols);

        if (m == GameMode.PlayerVsAI)
        {
            ai.Init(gridGenerator);
        }

        if (m == GameMode.PlayerVsAI)
        {
            UIManager uiManager = FindFirstObjectByType<UIManager>();
            if (uiManager != null)
            {
                uiManager.playerName2.text = "Bot";
            }
            else
            {
                Debug.LogError("UIManager instance not found in the scene.");
            }
        }
        else if (m == GameMode.PlayerVsPlayer)
        {
            UIManager uiManager = FindFirstObjectByType<UIManager>();
            if (uiManager != null)
            {
                uiManager.playerName2.text = "Player 2";
            }
            else
            {
                Debug.LogError("UIManager instance not found in the scene.");
            }
        }
    }

    internal void AddScore(int player, int amount)
    {
        if (player == 1)
        {
            playerScore += amount;
            playerScoreText.text = playerScore.ToString();
        }
        else
        {
            aiScore += amount;
            aiScoreText.text = aiScore.ToString();
        }
    }

    public void CheckGameEnd()
    {
        foreach (var box in gridGenerator.GetAllBoxes())
        {
            if (box.owner == 0)
            {
                return;
            }
        }

        if (playerScore > aiScore)
            Debug.Log("Player thắng!");
        else if (playerScore < aiScore)
            Debug.Log("Bot thắng!");
        else
            Debug.Log("Hòa!");
    }

    public void EndTurn()
    {
        IsPlayerOneTurn = !IsPlayerOneTurn;

        if (mode == GameMode.PlayerVsAI && !IsPlayerOneTurn)
        {
            Invoke(nameof(CallAI), 0.5f);
        }
    }

    private void CallAI()
    {
        ai.MakeMove();
    }

    public void ResetGame()
    {
        playerScore = 0;
        aiScore = 0;
        playerScoreText.text = "0";
        aiScoreText.text = "0";
    }
}