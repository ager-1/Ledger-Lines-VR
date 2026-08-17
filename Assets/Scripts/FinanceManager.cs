using UnityEngine;
using TMPro;

public class FinanceManager : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text laptopDisplayText;

    private float currentStipend;
    private int totalDays = 5;

    void Start()
    {
        currentStipend = PlayerPrefs.GetFloat("MonthlyStipend", 300f);
        UpdateDisplay();
    }
    public void DeductFunds(float amount)
    {
        currentStipend -= amount;
        PlayerPrefs.SetFloat("MonthlyStipend", currentStipend);
        PlayerPrefs.Save();

        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        int currentDayIndex = PlayerPrefs.GetInt("SavedDay", 0);
        int remainingDays = totalDays - currentDayIndex;
        if (remainingDays < 1)
        {
            remainingDays = 1;
        }
        float dailyAllowance = currentStipend / remainingDays;
        laptopDisplayText.text =
            "Total Funds: $" + currentStipend.ToString("F2") +
            "\n\nRemaining Days: " + remainingDays +
            "\n\nSafe Daily Spend: $" + dailyAllowance.ToString("F2");
    }
}