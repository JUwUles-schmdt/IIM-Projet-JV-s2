using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyData
{
    public enum MOVE_DIR
    {
        HORIZONTAL,
        VERTICAL
    }

    public string label;

    [Header("SETUP")]
    public float scaleCoef;
    public Sprite sprite;
    public Color color;

    [Header("STATS")]
    public int pv;
    public float speed;
    public int damage;

    [Header("DURATION")]
    public float durationIDLE;
    public float durationWALK;
}
