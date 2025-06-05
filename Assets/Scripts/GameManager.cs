using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GridGenerator gridGenerator;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;
    private int playerScore = 0;
    private int aiScore = 0;

    public int rows;
    public int cols;

    public GameMode mode = GameMode.PlayerVsAI;

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

        gridGenerator.Generate(rows, cols);
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
}