using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "New InteractableItem")]
public class InteractableItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;

    [Header("Use")]
    public bool Use;
    public float InteractionPower;
    public float CoolDown;
}
