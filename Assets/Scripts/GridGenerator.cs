using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public GameObject dotPrefab;
    public GameObject linePrefab;
    public GameObject lineUnderPrefab;
    public GameObject boxPrefab;

    public int rows = 5;
    public int cols = 5;

    private float gridSize = 9f;
    private float spacing;

    public float dotScale = 0.35f;

    private BoxController[,] boxes;

    void Start()
    {
        spacing = gridSize / (Mathf.Max(rows, cols) - 1);
        GenerateGrid();
    }

    void GenerateGrid()
    {
        Vector2 gridCenter = Vector2.zero; // gốc thế giới là trung tâm
        Vector2 startPos = gridCenter - new Vector2((cols - 1) * spacing / 2f, (rows - 1) * spacing / 2f);

        GameObject[,] dots = new GameObject[rows, cols];

        // Sinh dot
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector2 pos = startPos + new Vector2(x * spacing, y * spacing);
                dots[y, x] = Instantiate(dotPrefab, pos, Quaternion.identity, transform);
                dots[y, x].transform.localScale = new Vector3(dotScale, dotScale, 1);
            }
        }

        // Khởi tạo và gán giá trị cho boxes trước khi sinh line
        boxes = new BoxController[rows - 1, cols - 1];
        SpriteRenderer boxSpriteRenderer = lineUnderPrefab.GetComponent<SpriteRenderer>();
        float boxLineWidth = boxSpriteRenderer.bounds.size.x;
        for (int y = 0; y < rows - 1; y++)
        {
            for (int x = 0; x < cols - 1; x++)
            {
                Vector2 topLeft = dots[y, x].transform.position;
                Vector2 topRight = dots[y, x + 1].transform.position;
                Vector2 bottomLeft = dots[y + 1, x].transform.position;
                Vector2 bottomRight = dots[y + 1, x + 1].transform.position;
                Vector2 center = (topLeft + topRight + bottomLeft + bottomRight) / 4f;
                GameObject box = Instantiate(boxPrefab, center, Quaternion.identity, transform);
                float boxSize = spacing - boxLineWidth / 2;
                box.transform.localScale = new Vector3(boxSize, boxSize, 1);
                var sr = box.GetComponent<SpriteRenderer>();
                if (sr != null) sr.sortingOrder = 0;
                boxes[y, x] = box.GetComponent<BoxController>();
            }
        }

        // Sinh line ngang
        for (int y = 0; y < rows; y++)
        {
            SpriteRenderer spriteRenderer = linePrefab.GetComponent<SpriteRenderer>();
            float lineWidth = spriteRenderer.bounds.size.x; // Lấy chiều rộng của linePrefab

            for (int x = 0; x < cols - 1; x++)
            {
                Vector2 pos = (dots[y, x].transform.position + dots[y, x + 1].transform.position) / 2f;
                GameObject line = Instantiate(linePrefab, pos, Quaternion.identity, transform);
                line.transform.localScale = new Vector3(spacing, lineWidth / 2, 1);

                // Gán box và edge cho line
                LineHover hover = line.GetComponent<LineHover>();
                if (hover != null)
                {
                    if (y > 0) { hover.boxA = boxes[y - 1, x]; hover.edgeA = 2; } // box phía trên, cạnh dưới
                    if (y < rows - 1) { hover.boxB = boxes[y, x]; hover.edgeB = 0; } // box phía dưới, cạnh trên
                }
            }
        }

        // Sinh line dọc
        for (int y = 0; y < rows - 1; y++)
        {
            SpriteRenderer spriteRenderer = linePrefab.GetComponent<SpriteRenderer>();
            float lineWidth = spriteRenderer.bounds.size.x; // Lấy chiều rộng của linePrefab

            for (int x = 0; x < cols; x++)
            {
                Vector2 pos = (dots[y, x].transform.position + dots[y + 1, x].transform.position) / 2f;
                GameObject line = Instantiate(linePrefab, pos, Quaternion.identity, transform);
                line.transform.localScale = new Vector3(lineWidth / 2, spacing, 1);

                // Gán box và edge cho line dọc
                LineHover hover = line.GetComponent<LineHover>();
                if (hover != null)
                {
                    if (x > 0) { hover.boxA = boxes[y, x - 1]; hover.edgeA = 1; } // box bên trái, cạnh phải
                    if (x < cols - 1) { hover.boxB = boxes[y, x]; hover.edgeB = 3; } // box bên phải, cạnh trái
                }
            }
        }

        // Sinh line under ngang
        for (int y = 0; y < rows; y++)
        {
            SpriteRenderer spriteRenderer = lineUnderPrefab.GetComponent<SpriteRenderer>();
            float lineWidth = spriteRenderer.bounds.size.x; // Lấy chiều rộng của linePrefab

            for (int x = 0; x < cols - 1; x++)
            {
                Vector2 pos = (dots[y, x].transform.position + dots[y, x + 1].transform.position) / 2f;
                GameObject line = Instantiate(lineUnderPrefab, pos, Quaternion.identity, transform);
                line.transform.localScale = new Vector3(spacing, lineWidth / 2, 1);
            }
        }

        // Sinh line under dọc
        for (int y = 0; y < rows - 1; y++)
        {
            SpriteRenderer spriteRenderer = lineUnderPrefab.GetComponent<SpriteRenderer>();
            float lineWidth = spriteRenderer.bounds.size.x; // Lấy chiều rộng của linePrefab

            for (int x = 0; x < cols; x++)
            {
                Vector2 pos = (dots[y, x].transform.position + dots[y + 1, x].transform.position) / 2f;
                GameObject line = Instantiate(lineUnderPrefab, pos, Quaternion.identity, transform);
                line.transform.localScale = new Vector3(lineWidth / 2, spacing, 1);
            }
        }
    }
}