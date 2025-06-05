using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TMP_Dropdown rowsDropdown;
    public TMP_Dropdown colsDropdown;
    public TMP_Dropdown modesDropdown;

    public Button startButton;

    void Start()
    {
        rowsDropdown.value = 0;
        colsDropdown.value = 0;
        modesDropdown.value = 0;

        rowsDropdown.RefreshShownValue();
        colsDropdown.RefreshShownValue();
        modesDropdown.RefreshShownValue();

        startButton.onClick.AddListener(StartGame);
    }

    public void StartGame()
    {
        int rows = rowsDropdown.value + 5;
        int cols = colsDropdown.value + 5;

        string modeText = modesDropdown.options[modesDropdown.value].text;
        GameMode mode = modeText == "Player vs Player" ? GameMode.PlayerVsPlayer : GameMode.PlayerVsAI;

        GameManager.Instance.Setup(rows, cols, mode);
    }
}