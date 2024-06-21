using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrBaseEnemy", menuName = "Scriptable/ScrBaseEnemy")]
public class ScrBaseEnemy : ScriptableObject
{
    public string nameEnemy, classEnemy, raceEnemy;

    public int hpEnemyMax0, hpEnemyMax1, armorEnemy0, armorEnemy1, bonusEnemy0, bonusEnemy1, diceCountEnemy, diceDamageEnemy0, diceDamageEnemy1, initiativeEnemy;

    public Sprite sprEnemy;
}
