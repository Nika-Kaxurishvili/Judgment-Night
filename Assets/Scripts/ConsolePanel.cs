using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConsolePanel : MonoBehaviour
{
    // კონსოლის გაკეთებას ვცდილობდი მაგრამ Warning - ებმა შემიშალა ხელი ამიტომ თავი გავანებე. :D
    public GameObject panel;
    public Text logText;
    private List<string> logs = new List<string>();

    void Awake()
    {
        Application.logMessageReceived += HandleLog;
        panel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            panel.SetActive(!panel.activeSelf);
            RefreshLogs();
        }
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        logs.Add(logString);
        if (logs.Count > 100)
        {
            logs.RemoveAt(0);
        }
    }

    void RefreshLogs()
    {
        logText.text = "";
        foreach (string log in logs)
        {
            logText.text += log + "\n";
        }
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }
}
