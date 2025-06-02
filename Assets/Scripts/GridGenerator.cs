using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject dotPrefab;
    public GameObject linePrefab;
    public GameObject lineUnderPrefab;
    public GameObject boxPrefab;

    [Header("Settings")]
    public float dotScale = 0.35f;
    public float gridSize = 9f;

    private float spacing;
    private BoxController[,] boxes;

    public void Generate(int rows, int cols)
    {
        // Xóa grid cũ nếu có
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        spacing = gridSize / (Mathf.Max(rows, cols) - 1);

        float camHeight = 2f * Camera.main.orthographicSize;
        float camWidth = camHeight * Camera.main.aspect;
        Vector2 camCenter = Camera.main.transform.position;

        float centerX = camCenter.x - camWidth / 2f + camWidth * 0.68f;
        float centerY = camCenter.y;
        Vector2 gridCenter = new Vector2(centerX, centerY);

        Vector2 size = new Vector2((cols - 1) * spacing, (rows - 1) * spacing);
        Vector2 startPos = gridCenter - size / 2f;

        GameObject[,] dots = new GameObject[rows, cols];

        // Spawn Dots
        for (int y = 0; y < rows; y++)
        {
            for (int x = 0; x < cols; x++)
            {
                Vector2 pos = startPos + new Vector2(x * spacing, y * spacing);
                GameObject dot = Instantiate(dotPrefab, pos, Quaternion.identity, transform);
                dot.transform.localScale = Vector3.one * dotScale;
                dots[y, x] = dot;
            }
        }

        // Boxes
        boxes = new BoxController[rows - 1, cols - 1];
        float boxLineWidth = lineUnderPrefab.GetComponent<SpriteRenderer>().bounds.size.x;
        for (int y = 0; y < rows - 1; y++)
        {
            for (int x = 0; x < cols - 1; x++)
            {
                Vector2 center = AveragePos(dots[y, x], dots[y, x + 1], dots[y + 1, x], dots[y + 1, x + 1]);
                GameObject box = Instantiate(boxPrefab, center, Quaternion.identity, transform);
                float boxSize = spacing - boxLineWidth / 2;
                box.transform.localScale = Vector3.one * boxSize;
                boxes[y, x] = box.GetComponent<BoxController>();
            }
        }

        // Horizontal lines
        CreateLines(rows, cols, dots, true, linePrefab, true);
        // Vertical lines
        CreateLines(rows, cols, dots, false, linePrefab, true);
        // Underlines
        CreateLines(rows, cols, dots, true, lineUnderPrefab, false);
        CreateLines(rows, cols, dots, false, lineUnderPrefab, false);
    }

    //private void CreateLines(int rows, int cols, GameObject[,] dots, bool horizontal, GameObject prefab, bool assignBoxes)
    //{
    //    SpriteRenderer sr = prefab.GetComponent<SpriteRenderer>();
    //    float width = sr.bounds.size.x;

    //    int loopA = horizontal ? rows : rows - 1;
    //    int loopB = horizontal ? cols - 1 : cols;

    //    for (int a = 0; a < loopA; a++)
    //    {
    //        for (int b = 0; b < loopB; b++)
    //        {
    //            GameObject dot1 = horizontal ? dots[a, b] : dots[b, a];
    //            GameObject dot2 = horizontal ? dots[a, b + 1] : dots[b + 1, a];
    //            Vector2 pos = (dot1.transform.position + dot2.transform.position) / 2f;

    //            GameObject line = Instantiate(prefab, pos, Quaternion.identity, transform);

    //            if (horizontal)
    //                line.transform.localScale = new Vector3(spacing, width / 2, 1);
    //            else
    //                line.transform.localScale = new Vector3(width / 2, spacing, 1);

    //            if (assignBoxes)
    //            {
    //                LineHover hover = line.GetComponent<LineHover>();
    //                if (hover != null)
    //                {
    //                    if (horizontal)
    //                    {
    //                        if (a > 0) { hover.boxA = boxes[a - 1, b]; hover.edgeA = 2; }
    //                        if (a < rows - 1) { hover.boxB = boxes[a, b]; hover.edgeB = 0; }
    //                    }
    //                    else
    //                    {
    //                        if (b > 0) { hover.boxA = boxes[b, a - 1]; hover.edgeA = 1; }
    //                        if (b < cols - 1) { hover.boxB = boxes[b, a]; hover.edgeB = 3; }
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}

    private void CreateLines(int rows, int cols, GameObject[,] dots, bool horizontal, GameObject prefab, bool assignBoxes)
    {
        SpriteRenderer sr = prefab.GetComponent<SpriteRenderer>();
        float width = sr.bounds.size.x;

        if (horizontal)
        {
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols - 1; x++)
                {
                    GameObject dot1 = dots[y, x];
                    GameObject dot2 = dots[y, x + 1];
                    Vector2 pos = (dot1.transform.position + dot2.transform.position) / 2f;

                    GameObject line = Instantiate(prefab, pos, Quaternion.identity, transform);

                    line.transform.localScale = new Vector3(spacing, width / 2, 1);

                    if (assignBoxes)
                    {
                        LineHover hover = line.GetComponent<LineHover>();
                        if (hover != null)
                        {
                            if (y > 0) { hover.boxA = boxes[y - 1, x]; hover.edgeA = 2; }
                            if (y < rows - 1) { hover.boxB = boxes[y, x]; hover.edgeB = 0; }
                        }
                    }
                }
            }
        }
        else
        {
            for (int x = 0; x < cols; x++)
            {
                for (int y = 0; y < rows - 1; y++)
                {
                    GameObject dot1 = dots[y, x];
                    GameObject dot2 = dots[y + 1, x];
                    Vector2 pos = (dot1.transform.position + dot2.transform.position) / 2f;

                    GameObject line = Instantiate(prefab, pos, Quaternion.identity, transform);

                    line.transform.localScale = new Vector3(width / 2, spacing, 1);

                    if (assignBoxes)
                    {
                        LineHover hover = line.GetComponent<LineHover>();
                        if (hover != null)
                        {
                            if (x > 0) { hover.boxA = boxes[y, x - 1]; hover.edgeA = 1; }
                            if (x < cols - 1) { hover.boxB = boxes[y, x]; hover.edgeB = 3; }
                        }
                    }
                }
            }
        }
    }

    private Vector2 AveragePos(params GameObject[] objs)
    {
        Vector2 sum = Vector2.zero;
        foreach (var o in objs) sum += (Vector2)o.transform.position;
        return sum / objs.Length;
    }

    void Start()
    {
        Generate(5, 5); // Test thử
    }
}