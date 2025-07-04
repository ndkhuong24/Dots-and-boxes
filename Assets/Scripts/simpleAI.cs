using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour
{
    private GridGenerator gridGenerator;

    public void Init(GridGenerator grid)
    {
        gridGenerator = grid;
    }

    public void MakeMove()
    {
        List<LineHover> availableLines = gridGenerator.GetAvailableLines();

        if (availableLines.Count == 0) return;

        // Ưu tiên chọn đường hoàn thành box
        foreach (var line in availableLines)
        {
            if (CreatesBox(line))
            {
                line.OnMouseDown();
                return;
            }
        }

        // Loại bỏ đường tạo cơ hội (tức là làm box có 3 cạnh)
        List<LineHover> safeMoves = new List<LineHover>();
        foreach (var line in availableLines)
        {
            if (!LeavesThreeSides(line))
            {
                safeMoves.Add(line);
            }
        }

        // Nếu còn nước đi an toàn → chọn ngẫu nhiên trong đó
        if (safeMoves.Count > 0)
        {
            LineHover line = safeMoves[Random.Range(0, safeMoves.Count)];
            line.OnMouseDown();
            return;
        }

        // Nếu không còn nước đi an toàn → chơi đại
        LineHover fallback = availableLines[Random.Range(0, availableLines.Count)];
        fallback.OnMouseDown();
    }

    private bool CreatesBox(LineHover line)
    {
        return (line.boxA != null && line.boxA.MissingEdges() == 1) ||
               (line.boxB != null && line.boxB.MissingEdges() == 1) ||
               (line.boxC != null && line.boxC.MissingEdges() == 1) ||
               (line.boxD != null && line.boxD.MissingEdges() == 1);
    }

    private bool LeavesThreeSides(LineHover line)
    {
        return (line.boxA != null && line.boxA.MissingEdges() == 2) ||
               (line.boxB != null && line.boxB.MissingEdges() == 2) ||
               (line.boxC != null && line.boxC.MissingEdges() == 2) ||
               (line.boxD != null && line.boxD.MissingEdges() == 2);
    }
}