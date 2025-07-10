using System;
using UnityEngine;
using UnityEngine.UI;

public class HowToPlayUI : MonoBehaviour
{
    public Button closeButton;

    private void Start()
    {
        closeButton.onClick.AddListener(CloseHowToPlay);
    }

    private void CloseHowToPlay()
    {
        gameObject.SetActive(false);
    }
}
