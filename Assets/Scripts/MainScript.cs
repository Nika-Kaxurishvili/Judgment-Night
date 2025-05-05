using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainScript : MonoBehaviour
{
    [Header("-----GameObject-----")]
    public GameObject[] Tasks;
    public GameObject Menu;


    [Header("-----Text-----")]
    public Text timeText; // ტექსტი

    [Header("-----Float-----")]
    public float timeSpeed = 30f; // რამდენჯერ სწრაფად მიდის დრო რეალურ დროზე
    private float currentTimeInMinutes;

    [Header("-----Int-----")]
    public int startHour = 14; // დაწყების დრო
    public int startMinute = 0;

    [Header("-----Bool-----")]
    public bool inMenu;
    void Start()
    {
        currentTimeInMinutes = startHour * 60 + startMinute;
    }
    void Update()
    {
        // დროის მატება
        currentTimeInMinutes += Time.deltaTime * (timeSpeed / 60f);

        int hours = Mathf.FloorToInt(currentTimeInMinutes / 60) % 24;
        int minutes = Mathf.FloorToInt(currentTimeInMinutes) % 60;

        string ampm = hours >= 12 ? "PM" : "AM";
        int displayHour = hours % 12;
        if (displayHour == 0) displayHour = 12;

        timeText.text = $"{displayHour:D2}:{minutes:D2} {ampm}";
        // ESC ღილაკზე დაჭერა
        if (Input.GetKeyDown(KeyCode.Escape) & !inMenu)
        {
            GetComponent<FirstPersonController>().enabled = false;
            inMenu = true;
            Time.timeScale = 0;
            Menu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if (Input.GetKeyDown(KeyCode.Escape) & inMenu)
        {
            GetComponent<FirstPersonController>().enabled = true;
            inMenu = false;
                Time.timeScale = 1;
                Menu.SetActive(false);
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
        }
    }
}
