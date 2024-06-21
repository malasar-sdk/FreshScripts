using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightManager : MonoBehaviour
{
    [SerializeField]
    private int curNumQueue;

    [SerializeField]
    private ScrMapEnemy activeEnemy;

    [SerializeField]
    private List<int> fightQueueList;

    [SerializeField, Header("События")]
    private UnityEvent playerDeth;

    [SerializeField]
    private UnityEvent enemyDeth;

    [SerializeField, Header("Ссылки")]
    private ScrptblEnemysList scrptblEnemysList;

    [SerializeField]
    private ScrptblHeroStat scrptblHeroStat;

    [SerializeField]
    private List<ScrMapEnemy> scrMapEnemyList;

    [SerializeField]
    private BattleUIElements battleUIElements;

    public void StartFightingEnemy()
    {
        GenerateEnemys(1, 1);
        activeEnemy = scrMapEnemyList[0];
        SetFightQueue();
        FightingQueue();

        Debug.Log("Battle enemy event");
    }

    public void StartFightingBoss()
    {
        GenerateEnemys(1, 2);
        activeEnemy = scrMapEnemyList[0];
        SetFightQueue();
        FightingQueue();

        Debug.Log("Battle boss event");
    }


    #region Enemy creation
    private void GenerateEnemys(int numEnemy, int dificult)
    {
        for (int i = 0; i < numEnemy; i++)
        {
            ScrBaseEnemy enemy = CreateRandomEnemy();
            scrMapEnemyList[i].GenerateEnemy(enemy, dificult);
            SetEnemyInitiative(i);
        }

        battleUIElements.SetImageEnemy_1(scrMapEnemyList[0].sprEnemy);
    }

    private ScrBaseEnemy CreateRandomEnemy()
    {
        int num = Random.Range(0, scrptblEnemysList.enemysFromLocation1.Count);
        ScrBaseEnemy enemyObj = scrptblEnemysList.enemysFromLocation1[num];
        return enemyObj;
    }

    private void SetEnemyInitiative(int init)
    {
        scrMapEnemyList[init].SetInitiativeEnemy();
    }
    #endregion


    #region Fighting
    private void SetFightQueue()
    {
        fightQueueList = new List<int> { 3, 0 };
        curNumQueue = 0;

        battleUIElements.SetHeroHelth(scrptblHeroStat.hpCur, scrptblHeroStat.hpMax,0,false);
        battleUIElements.SetEnemyHelth_1(activeEnemy.hpEnemyCur, activeEnemy.hpEnemyMax,0,false);
    }

    private void FightingQueue()
    {
        if (fightQueueList.Count > 1)
        {
            if (activeEnemy.hpEnemyCur > 0)
            {
                if (fightQueueList[curNumQueue] == 3)
                {
                    battleUIElements.btnMainAction.interactable = true;
                    battleUIElements.btnBonusAction.interactable = true;

                    curNumQueue++;
                    if (curNumQueue >= fightQueueList.Count)
                        curNumQueue = 0;
                }
                else
                {
                    battleUIElements.btnMainAction.interactable = false;
                    battleUIElements.btnBonusAction.interactable = false;

                    curNumQueue++;
                    if (curNumQueue >= fightQueueList.Count)
                        curNumQueue = 0;

                    EnemyMainAttack(0);
                }
            }
        }
    }

    public void PlayerMainAttack()
    {
        int attack = Calculations.DiceD20();
        int enemyArmor = activeEnemy.armorEnemy;

        if (attack >= enemyArmor)
        {
            int damage = Calculations.DiceThrow(2, 4);
            int modificator = scrptblHeroStat.strengthBonus;
            int totalDamage = Calculations.Damage(damage, modificator);

            activeEnemy.SetDamage(totalDamage);
            battleUIElements.SetEnemyHelth_1(activeEnemy.hpEnemyCur, activeEnemy.hpEnemyMax, totalDamage,true);

            Debug.Log($"Player damage - {totalDamage}");

            if (activeEnemy.hpEnemyCur <= 0)
                enemyDeth?.Invoke();
        }
        else
        {
            Debug.Log("Player missed!");
        }

        FightingQueue();
    }

    public void EnemyMainAttack(int numEnemy)
    {
        int attack = Calculations.DiceD20();
        int playerArmor = scrptblHeroStat.armor;

        if (attack >= playerArmor)
        {
            int diceNum = scrMapEnemyList[numEnemy].diceCountEnemy;
            int diceSize = scrMapEnemyList[numEnemy].diceDamageEnemy;

            int damage = Calculations.DiceThrow(diceNum, diceSize);
            int modificator = scrMapEnemyList[numEnemy].bonusEnemy;
            int totalDamage = Calculations.Damage(damage, modificator);

            scrptblHeroStat.DamageHero(totalDamage);
            battleUIElements.SetHeroHelth(scrptblHeroStat.hpCur, scrptblHeroStat.hpMax, totalDamage,true);

            Debug.Log($"Enemy damage - {totalDamage}");

            if (scrptblHeroStat.hpCur <= 0)
                playerDeth?.Invoke();
        }
        else
        {
            Debug.Log("Enemy missed!");
        }

        FightingQueue();
    }
    #endregion
}
