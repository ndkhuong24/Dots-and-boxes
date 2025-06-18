using System;
using System.Collections;
using UnityEngine;

public class LineHover : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFilled = false;

    private static bool isPlayerTurn = true;

    public BoxController boxA;
    public int edgeA;

    public BoxController boxB;
    public int edgeB;

    public BoxController boxC;
    public int edgeC;

    public BoxController boxD;
    public int edgeD;

    public bool IsSelected { get; private set; } = false;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAlpha(0f);
    }

    public void OnClick()
    {
        if (isFilled) return;
        HandleClick();
    }

    public void OnMouseDown()
    {
        if (isFilled) return;
        HandleClick();
    }

    private void HandleClick()
    {
        isFilled = true;
        IsSelected = true;

        sr.color = isPlayerTurn ? new Color32(0x00, 0xD2, 0xFE, 0xFF) : new Color32(0xFE, 0x72, 0x72, 0xFF);
        SetAlpha(1f);

        int player = isPlayerTurn ? 1 : 2;

        // Lưu trạng thái trước khi cập nhật
        bool boxACompletedBefore = boxA != null && boxA.IsCompleted();
        bool boxBCompletedBefore = boxB != null && boxB.IsCompleted();
        bool boxCCompletedBefore = boxC != null && boxC.IsCompleted();
        bool boxDCompletedBefore = boxD != null && boxD.IsCompleted();

        // Cập nhật các cạnh của box
        if (boxA != null) boxA.SetEdge(edgeA, player);
        if (boxB != null) boxB.SetEdge(edgeB, player);
        if (boxC != null) boxC.SetEdge(edgeC, player);
        if (boxD != null) boxD.SetEdge(edgeD, player);

        // Kiểm tra box mới hoàn thành
        bool justCompleted =
            (boxA != null && !boxACompletedBefore && boxA.IsCompleted()) ||
            (boxB != null && !boxBCompletedBefore && boxB.IsCompleted()) ||
            (boxC != null && !boxCCompletedBefore && boxC.IsCompleted()) ||
            (boxD != null && !boxDCompletedBefore && boxD.IsCompleted());

        if (!justCompleted)
        {
            isPlayerTurn = !isPlayerTurn;
            GameManager.Instance.EndTurn();
        }

        StartCoroutine(ResetColorAfterDelay(1f));
    }

    //public void OnClick()
    //{
    //    if (IsSelected) return;

    //    IsSelected = true;
    //}

    //void OnMouseDown()
    //{
    //    if (isFilled) return;

    //    isFilled = true;

    //    sr.color = isPlayerTurn ? new Color32(0x00, 0xD2, 0xFE, 0xFF) : new Color32(0xFE, 0x72, 0x72, 0xFF);

    //    SetAlpha(1f); // Hiện rõ

    //    int player = isPlayerTurn ? 1 : 2;

    //    // Lưu trạng thái hoàn thành trước khi cập nhật
    //    bool boxACompletedBefore = boxA != null && boxA.IsCompleted();
    //    bool boxBCompletedBefore = boxB != null && boxB.IsCompleted();
    //    bool boxCCompletedBefore = boxC != null && boxC.IsCompleted();
    //    bool boxDCompletedBefore = boxD != null && boxD.IsCompleted();

    //    // Cập nhật trạng thái cạnh cho các box liên quan
    //    if (boxA != null) boxA.SetEdge(edgeA, player);
    //    if (boxB != null) boxB.SetEdge(edgeB, player);
    //    if (boxC != null) boxC.SetEdge(edgeC, player);
    //    if (boxD != null) boxD.SetEdge(edgeD, player);

    //    // Kiểm tra nếu có box nào vừa hoàn thành thì không chuyển lượt
    //    bool justCompleted =
    //        (boxA != null && !boxACompletedBefore && boxA.IsCompleted()) ||
    //        (boxB != null && !boxBCompletedBefore && boxB.IsCompleted()) ||
    //        (boxC != null && !boxCCompletedBefore && boxC.IsCompleted()) ||
    //        (boxD != null && !boxDCompletedBefore && boxD.IsCompleted());

    //    if (!justCompleted)
    //    {
    //        isPlayerTurn = !isPlayerTurn;

    //        GameManager.Instance.EndTurn();
    //    }

    //    StartCoroutine(ResetColorAfterDelay(1f));
    //}

    private IEnumerator ResetColorAfterDelay(float v)
    {
        yield return new WaitForSeconds(v);

        Color color;
        if (ColorUtility.TryParseHtmlString("#424042", out color))
        {
            sr.color = color;
        }
    }


    private void SetAlpha(float alpha)
    {
        if (sr == null) return;

        Color color = sr.color;
        color.a = alpha;
        sr.color = color;
    }
}