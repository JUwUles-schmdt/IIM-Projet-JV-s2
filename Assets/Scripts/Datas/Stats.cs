using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Stats
{
    public int life;
    public int def;
    public int damage;
    public float speedMove;
    public float forceJump;

    public Stats(int life, int def, int damage, float speedMove, float forceJump)
    {
        this.life = life;
        this.def = def;
        this.damage = damage;
        this.speedMove = speedMove;
        this.forceJump = forceJump;
    }

    public Stats(int level)
    {
        life = level;
        def = level;
        damage = level;
        speedMove = level;
        forceJump = level;
    }
}