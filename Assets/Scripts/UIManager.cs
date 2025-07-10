using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    public MainMenuUI mainMenuUI;
    //public  gameSetupPanel;
    //public GameObject gameUIPanel;
    //public GameObject gameOverPanel;
    //public GameObject gameplayRoot;


    //public TMP_Dropdown rowsDropdown;
    //public TMP_Dropdown colsDropdown;
    //public TMP_Dropdown modesDropdown;

    //public TextMeshProUGUI playerScoreText;
    //public TextMeshProUGUI aiScoreText;

    //public TextMeshProUGUI playerName2;
    //public TextMeshProUGUI playerName1;

    //public Button startGameButton;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ShowMainMenu();

        // 👇 UI Panel setup
        //mainMenuPanel.SetActive(true);
        //gameSetupPanel.SetActive(false);
        //gameUIPanel.SetActive(false);
        //gameOverPanel.SetActive(false);
        //gameplayRoot.SetActive(false);


        //rowsDropdown.value = 0;
        //colsDropdown.value = 0;
        //modesDropdown.value = 0;

        //rowsDropdown.RefreshShownValue();
        //colsDropdown.RefreshShownValue();
        //modesDropdown.RefreshShownValue();

        //startButton.onClick.AddListener(StartGame);
    }

    private void ShowMainMenu()
    {
        mainMenuUI.gameObject.SetActive(true);
    }

    //public void StartGame()
    //{
    //    int rows = rowsDropdown.value + 5;
    //    int cols = colsDropdown.value + 5;

    //    GameManager.Instance.ResetGame();

    //    string modeText = modesDropdown.options[modesDropdown.value].text;
    //    GameMode mode = modeText == "Player vs Player" ? GameMode.PlayerVsPlayer : GameMode.PlayerVsAI;

    //    GameManager.Instance.Setup(rows, cols, mode);
    //}
}