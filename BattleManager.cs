using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static ScrBaseEvent;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private int levelCurType, levelCurHistory, levelMaxHistory, levelDungeon, stage;

    [SerializeField, Header("—прайты")]
    private Sprite sprBase;

    [SerializeField, Header("—сылки")]
    private ScrCrossScene scrCrossScene;

    [SerializeField]
    private StagesHelperUI stagesHelperUI;

    [SerializeField]
    private EventsList eventsList;

    [SerializeField]
    private ScrptblForSaving scrptblForSaving;

    [SerializeField]
    private ScrListOfLevels scrListOfLevels;

    [SerializeField]
    private ScrptblHeroStat scrptblHeroStat;

    [SerializeField]
    private BattleUIElements battleUIElements;

    private void Start()
    {
        levelCurType = scrptblForSaving.lvlCurType;
        levelCurHistory = scrptblForSaving.lvlCurHistoryNum;
        levelDungeon = 0;
        stage = 0;

        scrptblHeroStat.SetCurHpToMax();
        battleUIElements.SetImageEnemy_1(sprBase);

        Debug.Log($"Cur event type - {levelCurType}");
        Debug.Log($"Cur history/dungeon level - {levelCurHistory + 1}");
        Debug.Log($"Cur level stage- {stage}");
    }

    public void ReturnToHub()
    {
        SceneManager.LoadScene("HubScene");
    }

    public void TurnNextLevelStage()
    {
        switch (levelCurType)
        {
            case 0:
                StartHistoryLevelStage();
                break;
            case 1:
                StartDungeonLevelStage();
                break;
            default: break;
        }
    }

    private void StartHistoryLevelStage()
    {
        if (stage >= scrListOfLevels.listHistoryLevels[levelCurHistory].stages.Count)
        {
            levelCurHistory++;
            scrptblForSaving.SetlvlCurHistoryNum(levelCurHistory);

            if (scrptblForSaving.lvlHistoryMaxProgress <= levelCurHistory)
            {
                scrptblForSaving.SetlvlHistoryMaxProgress(levelCurHistory);
            }

            stage = 0;
            SceneManager.LoadScene("Map");
        }

        StagesEnum numEvent = scrListOfLevels.listHistoryLevels[levelCurHistory].stages[stage].stage;
        eventsList.SelectEvent(numEvent);

        Debug.Log($"Cur history level - {levelCurHistory + 1}");
        Debug.Log($"Cur level stage - {stage}");
    }

    private void StartDungeonLevelStage()
    {
        if (stage >= scrListOfLevels.listDungeonLevels[levelDungeon].stages.Count)
        {
            stage = 0;
            SceneManager.LoadScene("Map");
        }

        StagesEnum numEvent = scrListOfLevels.listDungeonLevels[levelDungeon].stages[stage].stage;
        eventsList.SelectEvent(numEvent);
    }

    public void StageUp(int num)
    {
        stage += num;
        stagesHelperUI.nextStage(stage);
    }

    public int getStage()
    {
        return stage;
    }

    public int getStagesCount()
    {
        return scrListOfLevels.listHistoryLevels[levelCurHistory].stages.Count;
    }
}
