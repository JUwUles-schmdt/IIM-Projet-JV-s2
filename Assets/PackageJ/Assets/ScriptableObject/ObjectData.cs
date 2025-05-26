using UnityEngine;

[CreateAssetMenu(fileName = "ObjectData", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    public bool canJump;
    public bool canMove;
    public bool isImmuned;
}

public enum ItemType
{
    TV,
    Box,
    Vase,
}
