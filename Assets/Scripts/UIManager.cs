using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TMP_Dropdown rowsDropdown;
    public TMP_Dropdown colsDropdown;
    public TMP_Dropdown modesDropdown;

    public void StartGame()
    {
        // Lấy số hàng/cột từ dropdown
        int rows = rowsDropdown.value + 5; // Vì index 0 = 5
        int cols = colsDropdown.value + 5;

        // Lấy chế độ chơi
        string mode = modesDropdown.options[modesDropdown.value].text;

        // In ra để kiểm tra
        Debug.Log($"Rows: {rows}, Cols: {cols}, Mode: {mode}");

        // Gửi thông tin đến GameManager (hoặc xử lý khởi tạo tại đây)
        // GameManager.Instance.Setup(rows, cols, mode);
    }

    private void Start()
    {
        StartGame(); // Gọi hàm StartGame khi bắt đầu trò chơi
    }
}
