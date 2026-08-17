using UnityEngine;

[CreateAssetMenu(fileName = "NewScenario", menuName = "LedgerLines/Scenario")]
public class ScenarioData : ScriptableObject
{
    [TextArea(3, 5)]
    public string scenarioDescription;
}