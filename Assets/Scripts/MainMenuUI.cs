using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameSetUpUI gameSetUpUI;
    public HowToPlayUI howToPlayUI;

    public Button howToPlayButton;
    public Button startGameButton;

    private void Start()
    {
        startGameButton.onClick.AddListener(StartGameClick);

        howToPlayButton.onClick.AddListener(HowToPlayClick);
    }

    private void HowToPlayClick()
    {
        howToPlayUI.gameObject.SetActive(true);
    }

    private void StartGameClick()
    {
        gameObject.SetActive(false);
        gameSetUpUI.gameObject.SetActive(true);
    }
}