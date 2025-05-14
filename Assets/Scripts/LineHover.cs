using UnityEngine;

public class LineHover : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFilled = false;

    // Lượt chơi: true = người chơi, false = AI
    private static bool isPlayerTurn = true;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAlpha(0f); // ẩn vạch ban đầu
    }

    void OnMouseEnter()
    {
        if (!isFilled) SetAlpha(0.3f); // Hover mờ mờ
    }

    void OnMouseExit()
    {
        if (!isFilled) SetAlpha(0f); // Rời chuột thì ẩn lại
    }

    void OnMouseDown()
    {
        if (isFilled) return;

        isFilled = true;

        // Đặt màu tùy lượt
        sr.color = isPlayerTurn ? Color.green : Color.red;

        SetAlpha(1f); // Hiện rõ

        // Chuyển lượt
        isPlayerTurn = !isPlayerTurn;
    }

    private void SetAlpha(float alpha)
    {
        if (sr == null) return;

        Color color = sr.color;
        color.a = alpha;
        sr.color = color;
    }
}