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

        LineHover randomLine = availableLines[Random.Range(0, availableLines.Count)];

        randomLine.OnClick();
    }
}