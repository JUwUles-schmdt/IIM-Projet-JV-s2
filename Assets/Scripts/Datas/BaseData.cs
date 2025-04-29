using System;
using UnityEngine;

[Serializable]
public class BaseData
{
    public string label;

    [Header("MAIN")]
    public Sprite sprite;
    public Color color;
    public float scaleCoef;
}
