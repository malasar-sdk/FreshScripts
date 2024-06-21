using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "ScrBaseLEvel", menuName = "Events/ScrBaseLEvel")]
public class ScrBaseLEvel : ScriptableObject
{
    public List<ScrBaseEvent> stages;
}
