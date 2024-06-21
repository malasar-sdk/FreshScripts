using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScrBaseEvent", menuName = "Events/ScrBaseEvent")]
public class ScrBaseEvent : ScriptableObject
{
    public StagesEnum stage;

    public enum StagesEnum
    {
        random = 0,
        battleSimple = 1,
        battleBoss = 2,
        lootChest = 3,
        meeting = 4
    };
}
