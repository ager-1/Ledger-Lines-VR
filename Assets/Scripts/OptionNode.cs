using UnityEngine;
using TMPro;

public class OptionNode : MonoBehaviour
{
    [Header("References")]
    public LineRenderer guideLine;
    public LineRenderer playerLine;
    public TMP_Text optionDisplayText;
    public Transform xrHandBrush;
    public Transform bankOriginPoint;

    private ScenarioOption currentOptionData;
    private Vector3[] pathPoints;
    private int currentPointIndex = 0;
    private bool isTracing = false;

    public void SetupNode(ScenarioOption data)
    {
        currentOptionData = data;
        optionDisplayText.text = data.optionText + "\n$" + data.cost.ToString("F2");

        GenerateTableBoundPath(data.difficultyLevel);
    }

    private void GenerateTableBoundPath(int difficulty)
    {
        int numPoints = 1 + (difficulty * 2);
        if (difficulty == 3) numPoints = 9;

        pathPoints = new Vector3[numPoints];

        Vector3 startPos = bankOriginPoint.position;
        Vector3 endPos = transform.position;
        Vector3 direction = (endPos - startPos).normalized;
        Vector3 rightDir = Vector3.Cross(direction, Vector3.up).normalized;

        for (int i = 0; i < numPoints; i++)
        {
            float progress = (float)i / (numPoints - 1);
            Vector3 basePoint = Vector3.Lerp(startPos, endPos, progress);

            float variance = 0f;
            if (i > 0 && i < numPoints - 1)
            {
                float sign = (Random.value > 0.5f) ? 1f : -1f;
                variance = sign * Random.Range(0.03f, 0.05f * difficulty);
            }

            pathPoints[i] = basePoint + (rightDir * variance);
        }

        guideLine.positionCount = numPoints;
        guideLine.SetPositions(pathPoints);

        playerLine.positionCount = 0;
        currentPointIndex = 0;
    }

    void Update()
    {
        CheckTraceProgress();
    }

    private void CheckTraceProgress()
    {
        if (currentPointIndex >= pathPoints.Length) return;

        float distanceToNextPoint = Vector3.Distance(xrHandBrush.position, pathPoints[currentPointIndex]);
        if (!isTracing && currentPointIndex == 0)
        {
            if (distanceToNextPoint < 0.04f)
            {
                isTracing = true;
            }
        }

        if (isTracing)
        {
            if (distanceToNextPoint < 0.15f)
            {
                playerLine.positionCount = currentPointIndex + 1;
                playerLine.SetPosition(currentPointIndex, pathPoints[currentPointIndex]);
                currentPointIndex++;

                if (currentPointIndex >= pathPoints.Length)
                {
                    FinishTrace();
                }
            }
        }
    }

    public void ResetTrace()
    {
        isTracing = false;
        currentPointIndex = 0;
        playerLine.positionCount = 0;
    }

    private void FinishTrace()
    {
        isTracing = false;
        Object.FindAnyObjectByType<FinanceManager>().DeductFunds(currentOptionData.cost);
        Object.FindAnyObjectByType<DayProgressionManager>().OnTraceCompleted();
    }
}