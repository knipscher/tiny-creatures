using UnityEngine;

[System.Serializable]
public class VectorPid
{
    private Vector3 integral;
    private Vector3 lastDifference;
    
    public Vector3 Update(Vector3 currentDifference, float timeFrame, VectorPidConstants constants)
    {
        integral += currentDifference * timeFrame;
        var deriv = (currentDifference - lastDifference) / timeFrame;
        lastDifference = currentDifference;
        return currentDifference * constants.pCoefficient
               + integral * constants.iCoefficient
               + deriv * constants.dCoefficient;
    }
}