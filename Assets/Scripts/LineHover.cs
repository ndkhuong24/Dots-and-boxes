﻿using System;
using System.Collections;
using UnityEngine;

public class LineHover : MonoBehaviour
{
    private SpriteRenderer sr;
    private bool isFilled = false;

    // Lượt chơi: true = người chơi, false = AI
    private static bool isPlayerTurn = true;

    public BoxController boxA;
    public int edgeA;

    public BoxController boxB;
    public int edgeB;

    public BoxController boxC;
    public int edgeC;

    public BoxController boxD;
    public int edgeD;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetAlpha(0f);
    }

    void OnMouseDown()
    {
        if (isFilled) return;

        isFilled = true;

        sr.color = isPlayerTurn ? new Color32(0x00, 0xD2, 0xFE, 0xFF) : new Color32(0xFE, 0x72, 0x72, 0xFF);

        SetAlpha(1f); // Hiện rõ

        // Xác định người chơi hiện tại
        int player = isPlayerTurn ? 1 : 2;

        // Cập nhật trạng thái cạnh cho các box liên quan
        if (boxA != null) boxA.SetEdge(edgeA, player);
        if (boxB != null) boxB.SetEdge(edgeB, player);
        if (boxC != null) boxC.SetEdge(edgeC, player);
        if (boxD != null) boxD.SetEdge(edgeD, player);

        // Chuyển lượt
        isPlayerTurn = !isPlayerTurn;

        StartCoroutine(ResetColorAfterDelay(1f));
    }

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