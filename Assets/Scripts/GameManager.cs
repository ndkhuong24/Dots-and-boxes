using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GridGenerator gridGenerator;

    public int rows = 5;
    public int cols = 5;
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
}