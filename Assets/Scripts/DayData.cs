using UnityEngine;

[CreateAssetMenu(fileName = "NewDay", menuName = "LedgerLines/Day")]
public class DayData : ScriptableObject
{
    public ScenarioData[] scenarios = new ScenarioData[3];
}