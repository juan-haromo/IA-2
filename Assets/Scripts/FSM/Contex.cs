using UnityEngine;

[System.Serializable]
public class FloatContext
{
    public FloatContext()
    {
        key = string.Empty;
        value = 0;
    }
    public FloatContext (string key, float value)
    {
        this.key = key;
        this.value = value;
    }
    public string key;
    public float value;
}

[System.Serializable]
public class TransformContext
{
    public TransformContext()
    {
        key = string.Empty;
        value = null;
    }
    public TransformContext (string key, Transform value)
    {
        this.key = key;
        this.value = value;
    }
    public string key;
    public Transform value;
}