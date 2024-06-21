using UnityEngine;

public static class Calculations
{
    public static int DiceThrow(int diceNum, int diceSize)
    {
        int num = 0;

        for (int i = 0; i < diceNum; i++)
        {
            num += Random.Range(1, diceSize + 1);
        }

        Debug.Log($"Dice {diceNum}d{diceSize} = {num}");

        return num;
    }

    public static int DiceD20()
    {
        int num = Random.Range(1, 21);
        Debug.Log($"Dice d20 = {num}");

        return num;
    }

    public static int SkillBonus(int lvl)
    {
        int num = (lvl / 4) + 1;

        return num;
    }

    public static int CharacteristicModifier(int charMod)
    {
        int num = (charMod - 10) / 2;

        return num;
    }

    public static int Initiative(int mod)
    {
        int num = DiceD20() + mod;

        return num;
    }

    public static int ArmorBonus(int armBase, int armor, int shield)
    {
        int num = armBase + armor + shield;

        return num;
    }

    public static int Attack(int skill, int mod)
    {
        int num = DiceD20() + skill + mod;

        return num;
    }

    public static int Damage(int dmgW, int mod)
    {
        int num = dmgW + mod;

        return num;
    }

    public static int ComplexityOfSaveThrow(int modSpell, int skill)
    {
        int num = 8 + modSpell + skill;

        return num;
    }

    public static int SaveThrow(int mod, int skill)
    {
        int num = DiceD20() + mod + skill;

        return num;
    }

    public static float Difficulty(int num)
    {
        float numDif = 0;

        switch (num)
        {
            case 1:
                numDif = 1f;
                break;
            case 2:
                numDif = 1.5f;
                break;
            case 3:
                numDif = 2f;
                break;
            case 4:
                numDif = 3f;
                break;
            default:
                numDif = 1f; 
                break;
        }

        return numDif;
    }

    public static bool RandoChecker(float randProcent)
    {
        bool yn = false;
        float rand = Random.Range(0, 101);

        if (rand <= randProcent)
        {
            yn = true;
        }

        return yn;
    }
}
