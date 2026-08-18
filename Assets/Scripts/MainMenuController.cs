using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [Header("UI References")]
    public Button continueButton;

    void Start()
    {
        if (PlayerPrefs.HasKey("MonthlyStipend"))
        {
            continueButton.interactable = true;
        }
        else
        {
            continueButton.interactable = false;
        }
    }
    public void LoadSavedGame()
    {
        SceneManager.LoadScene("Days");
    }
    public void StartNewMonth()
    {
        PlayerPrefs.DeleteKey("SavedDay");
        PlayerPrefs.DeleteKey("MonthlyStipend");

        SceneManager.LoadScene("New_Month");
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}