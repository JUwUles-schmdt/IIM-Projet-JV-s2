using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyData : BaseData
{
    public enum MOVE_DIR
    {
        HORIZONTAL,
        VERTICAL
    }


    [Header("STATS")]
    public int pv;
    public float speed;
    public int damage;

    [Header("DURATION")]
    public float durationIDLE;
    public float durationWALK;
}
