[System.Serializable]
public class VectorPidConstants
{
    public float pCoefficient;
    public float iCoefficient;
    public float dCoefficient;

    public VectorPidConstants(float p, float i, float d)
    {
        pCoefficient = p;
        iCoefficient = i;
        dCoefficient = d;
    }
}
