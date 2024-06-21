using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ScrHeroStat", menuName = "Scriptable/ScrHeroStat")]
public class ScrptblHeroStat : ScriptableObject
{
    //initiative = 1d20 + dexterityBonus; spabrosok = 1d20 + statBonus; Perception = 10 + wisdomBonus
    [field: SerializeField, Header("Переменные описания")] public string nameH { get; private set; }
    [field: SerializeField] public string raceH { get; private set; }
    [field: SerializeField] public string classH { get; private set; }

    [field: SerializeField, Header("Переменные информации")] public int level { get; private set; }
    [field: SerializeField] public int experience { get; private set; }
    [field: SerializeField] public int skillBonus { get; private set; }
    [field: SerializeField] public int hpMax { get; private set; }
    [field: SerializeField] public int hpCur { get; private set; }
    [field: SerializeField] public int armor { get; private set; } // armor class = armor + dexterityBonus + bonus armor + bonus shield

    [field: SerializeField, Header("Переменные скиллов")] public int strength { get; private set; } // bonus = (strength - 10) / 2
    [field: SerializeField] public int strengthBonus { get; private set; }
    [field: SerializeField] public int agility { get; private set; }
    [field: SerializeField] public int agilityBonus { get; private set; }
    [field: SerializeField] public int intelligence { get; private set; }
    [field: SerializeField] public int intelligenceBonus { get; private set; }
    [field: SerializeField] public int wisdom { get; private set; }
    [field: SerializeField] public int wisdomBonus { get; private set; }

    [field: SerializeField, Header("Переменные навыков")] public bool isDisciplineOpened { get; private set; } // From strength
    [field: SerializeField] public int disciplineBonus { get; private set; }
    [field: SerializeField] public bool isConvictionOpened { get; private set; } // From strength
    [field: SerializeField] public int convictionBonus { get; private set; }
    [field: SerializeField] public bool isReactionOpened { get; private set; } // From agility
    [field: SerializeField] public int reactionBonus { get; private set; }
    [field: SerializeField] public bool isDexterityOpened { get; private set; } // From agility
    [field: SerializeField] public int dexterityBonus { get; private set; }
    [field: SerializeField] public bool isTheologyOpened { get; private set; } // From intelligence
    [field: SerializeField] public int theologyBonus { get; private set; }
    [field: SerializeField] public bool isHistoryOpened { get; private set; } // From intelligence
    [field: SerializeField] public int historyBonus { get; private set; }
    [field: SerializeField] public bool isMedicineOpened { get; private set; } // From wisdom
    [field: SerializeField] public int medicineBonus { get; private set; }
    [field: SerializeField] public bool isPerceptionOpened { get; private set; } // From wisdom
    [field: SerializeField] public int perceptionBonus { get; private set; }

    #region Setters hero
    public void SetHeroName(string name)
    {
        nameH = name;
    }

    public void SetHeroRace(string race)
    {
        raceH = race;
    }

    public void SetHeroClass(string clas)
    {
        classH = clas;
    }
    #endregion

    #region Setters info
    public void SetCurHpToMax()
    {
        hpCur = hpMax;
    }

    public void AddHeroLevel(int lvl)
    {
        level += lvl;
        skillBonus = Calculations.SkillBonus(level);
    }

    public void AddHeroExperience(int ex)
    {
        experience += ex;
    }

    public void AddHeroMaxHP(int mHP)
    {
        hpMax += mHP;

        if (hpMax < 1)
            hpMax = 1;

        if (hpCur > hpMax)
            hpCur = hpMax;
    }

    public void AddHeroCurHP(int cHP)
    {
        hpCur += cHP;

        if (hpCur > hpMax)
            hpCur = hpMax;
        else if (hpCur < 0)
            hpCur = 0;
    }

    public void DamageHero(int cHP)
    {
        hpCur -= cHP;

        if (hpCur < 0)
            hpCur = 0;
    }
    #endregion

    #region Setters stats
    public void AddHeroStrength(int str)
    {
        strength += str;
        strengthBonus = Calculations.CharacteristicModifier(strength);

        if (isDisciplineOpened)
            disciplineBonus = strengthBonus + skillBonus;
        else
            disciplineBonus = strengthBonus;

        if (isConvictionOpened)
            convictionBonus = strengthBonus + skillBonus;
        else
            convictionBonus = strengthBonus;
    }

    public void AddHeroAgility(int agi)
    {
        agility += agi;
        agilityBonus = Calculations.CharacteristicModifier(agility);
        armor = 10 + Calculations.CharacteristicModifier(agility);

        if (isReactionOpened)
            reactionBonus = agilityBonus + skillBonus;
        else
            reactionBonus = agilityBonus;

        if (isDexterityOpened)
            dexterityBonus = agilityBonus + skillBonus;
        else
            dexterityBonus = agilityBonus;
    }

    public void AddHeroIntelligence(int inte)
    {
        intelligence += inte;
        intelligenceBonus = Calculations.CharacteristicModifier(intelligence);

        if (isTheologyOpened)
            theologyBonus = intelligenceBonus + skillBonus;
        else
            theologyBonus = intelligenceBonus;

        if (isHistoryOpened)
            historyBonus = intelligenceBonus + skillBonus;
        else
            historyBonus = intelligenceBonus;
    }

    public void AddHeroWisdom(int wsd)
    {
        wisdom += wsd;
        wisdomBonus = Calculations.CharacteristicModifier(wisdom);

        if (isMedicineOpened)
            medicineBonus = wisdomBonus + skillBonus;
        else
            medicineBonus = wisdomBonus;

        if (isPerceptionOpened)
            perceptionBonus = wisdomBonus + skillBonus;
        else
            perceptionBonus = wisdomBonus;
    }
    #endregion

    public void SetParametersWithMoreDependence(int lvl, int str, int agi, int inte, int wsd)
    {
        AddHeroLevel(lvl);
        AddHeroStrength(str);
        AddHeroAgility(agi);
        AddHeroIntelligence(inte);
        AddHeroWisdom(wsd);
    }
}
