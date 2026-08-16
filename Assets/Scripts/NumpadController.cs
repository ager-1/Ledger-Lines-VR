using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class NumpadController : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text stipendDisplay;

    private string currentInput = "";
    public void AddDigit(string digit)
    {
        if (currentInput.Length < 6)
        {
            currentInput += digit;
            stipendDisplay.text = "$" + currentInput;
        }
    }
    public void ClearInput()
    {
        currentInput = "";
        stipendDisplay.text = "$0";
    }
    public void SubmitStipend()
    {
        if (float.TryParse(currentInput, out float stipendValue))
        {
            PlayerPrefs.SetFloat("MonthlyStipend", stipendValue);
            PlayerPrefs.Save();

            Debug.Log("Stipend Saved: " + stipendValue);
            SceneManager.LoadScene("Days");
        }
        else
        {
            Debug.LogWarning("Invalid input. Cannot parse to float.");
        }
    }
}