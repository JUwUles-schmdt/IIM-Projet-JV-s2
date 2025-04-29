using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace A1_24_25
{
    [Serializable]
    public class BaseData
    {
        public string label;

        [Header("MAIN")]
        public Sprite sprite;
        public Color color;
        public float scaleCoef;
    }
}
