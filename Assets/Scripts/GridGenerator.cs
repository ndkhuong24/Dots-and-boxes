using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject linePrefab;

    public int rows = 4;   // số hàng Dot
    public int cols = 4;   // số cột Dot
    public float spacing = 1.0f;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector2 gridCenter = Vector2.zero; // gốc thế giới là trung tâm
        Vector2 startPos = gridCenter - new Vector2((cols - 1) * spacing / 2f, (rows - 1) * spacing / 2f);


        GameObject[,] dots = new GameObject[rows, cols];

        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector2 pos = startPos + new Vector2(x * spacing, y * spacing);
                dots[y, x] = Instantiate(dotPrefab, pos, Quaternion.identity, transform);
            }
        }

        // Sinh line ngang
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols - 1; x++)
            {
                Vector2 pos = (dots[y, x].transform.position + dots[y, x + 1].transform.position) / 2f;
                GameObject line = Instantiate(linePrefab, pos, Quaternion.identity, transform);
                line.transform.localScale = new Vector3(spacing * 0.9f, 0.05f, 1);
            }
        }

        // Sinh line dọc
        for (int y = 0; y < rows - 1; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector2 pos = (dots[y, x].transform.position + dots[y + 1, x].transform.position) / 2f;
                GameObject line = Instantiate(linePrefab, pos, Quaternion.identity, transform);
                line.transform.localScale = new Vector3(0.05f, spacing * 0.9f, 1);
            }
        }
    }
}