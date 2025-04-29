using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Database/Item", order = 1)]

public class ItemDatabase : ScriptableObject
{
    public enum CATEGORY
    {
        NONE,
        COIN,
        LIFE,
        //POWER,
    }
    public CATEGORY category;

    [SerializeField] private List<ItemData> datas = new();

    public ItemData GetData(int id, bool random = false)
    {
        if (random && (id < 0 || id >= datas.Count))
            id = Random.Range(0, datas.Count);
        else
            id = Mathf.Clamp(id, 0, datas.Count - 1);

        return datas[id];
    }
}