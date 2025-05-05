using UnityEngine;
using UnityEngine.UI;

public class AdminCommandListener : MonoBehaviour
{
    public GameObject adminPanel;  // ადმინ პანელი
    public InputField chatInput;   // ჩასაწერი Input
    public AdminFlyContoler playerFlyController; // სკრიპტი

    private bool isPanelActive = false; // პანელის შემოწმება

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            ToggleAdminPanel();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if (isPanelActive && Input.GetKeyDown(KeyCode.Return)) // თუ პანელი აქტიურია და Enter დააწვა
        {
            string command = chatInput.text.Trim();
            ProcessCommand(command);
            chatInput.text = ""; // გაწმინდოს ტექსტი გაგზავნის შემდეგ
        }
    }

    void ToggleAdminPanel()
    {
        isPanelActive = !isPanelActive;
        adminPanel.SetActive(isPanelActive);

        if (isPanelActive)
        {
            chatInput.ActivateInputField(); // რომ ავტომატურად ჩეთზე გადავიდეს ფოკუსი
        }
    }
    // Commands
    void ProcessCommand(string command)
    {
        if (command.Equals("FLY:ME_AROUND_THE_WORLD", System.StringComparison.OrdinalIgnoreCase))
        {
            playerFlyController.ActivateFlyMode();
        }
        else if (command.Equals("STOP:FLYING", System.StringComparison.OrdinalIgnoreCase))
        {
            playerFlyController.DeactivateFlyMode();
        }
        else
        {
            Debug.Log("Unknown command: " + command);
        }
    }
}
