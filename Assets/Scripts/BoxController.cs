using System;
using TMPro;
using UnityEngine;

public class BoxController : MonoBehaviour
{
    public bool[] edges = new bool[4]; // 0: top, 1: right, 2: bottom, 3: left  
    public int owner = 0; // 0: no owner, 1: player 1, 2: player 2 (AI)  

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetEdge(int edgeIndex, int player)
    {
        edges[edgeIndex] = true;

        if (IsCompleted() && owner == 0)
        {
            owner = player;
            spriteRenderer.color = (player == 1) ? new Color32(21, 193, 239, 255) : new Color32(255, 94, 91, 255);

            //Gọi cập nhật điểm ở GameManager
            GameManager.Instance.AddScore(player, 1);

            //Check End Game
            GameManager.Instance.CheckGameEnd();

            //GameManager.Instance.EndTurn();
        }
    }

    public bool IsCompleted()
    {
        return edges[0] && edges[1] && edges[2] && edges[3];
    }
}