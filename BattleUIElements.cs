using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleUIElements : MonoBehaviour
{
    [SerializeField, Header("—сылки на геро€")]
    private TMP_Text txtHeroHelth;
    [SerializeField]
    private Slider imgHeroHp;

    [SerializeField, Header("—сылки на врагов")]
    private TMP_Text txtEnemyHelth_1;
    [SerializeField]
    private Slider imgEnemyHp_1;
    [SerializeField]
    private TMP_Text txtEnemyHelth_2;
    [SerializeField]
    private Slider imgEnemyHp_2;
    [SerializeField]
    private TMP_Text txtEnemyHelth_3;
    [SerializeField]
    private Slider imgEnemyHp_3;

    [SerializeField, Header("—сылки на кубик")]
    private TMP_Text txtDifficultClass;
    [SerializeField]
    private GameObject dice;

    [SerializeField]
    private TMP_Text txtSkillNum, txtSkillName, txtModNum, txtModName, txtDiceNum, txtResultEvent;

    [SerializeField, Header("—сылки на объекты")]
    private GameObject btnNextStage;

    [SerializeField]
    private GameObject btnDice, pnlPlayerDeth, autoBtnDice, logsBtnDice, pnlSucsessFailed;

    [SerializeField]
    private GameObject pnlEnemys, pnlEventBattle, pnlEventLoot, pnlEventMeeting;

    public Button btnMainAction, btnBonusAction;

    [SerializeField]
    private GameObject lootVariants, meetingVariants;

    [SerializeField, Header("—сылки на картинки")]
    private Image imgLoot;

    [SerializeField]
    private Image imgMeeting, imgEnemy_1, imgEnemy_2, imgEnemy_3;

    private void Start()
    {
        OpenCloseDethPanel(false);
        OpenCloseNextStageBtn(false);
        OpenCloseDiceBtn(false);
        CloseAllEventPanels();
    }

    public void SetHeroHelth(int curHP, int maxHP, int damage, bool isLoad)
    {
        txtHeroHelth.text = $"{curHP}/{maxHP}";

        if(isLoad) 
        {
            imgHeroHp.GetComponentInParent<loseHpHelper>().loseHp(damage);

            imgHeroHp.value = curHP * 1.0f / maxHP;
        }
       
        //Debug.Log(curHP * 1.0f / maxHP);
    }

    public void SetEnemyHelth_1(int curHP, int maxHP, int damage, bool isLoad)
    {
        if(isLoad)
        {
            imgEnemyHp_1.GetComponentInParent<loseHpHelper>().loseHp(damage);
            
        }
        imgEnemyHp_1.value = curHP * 1.0f / maxHP;
        txtEnemyHelth_1.text = $"{curHP}/{maxHP}";
      
        //Debug.Log(curHP*1.0f / maxHP);
    }

    public void SetEnemyHelth_2(int curHP, int maxHP, int damage, bool isLoad)
    {
        txtEnemyHelth_2.text = $"{curHP}/{maxHP}";
        imgEnemyHp_2.value = curHP * 1.0f / maxHP;
    }

    public void SetEnemyHelth_3(int curHP, int maxHP, int damage, bool isLoad)
    {
        txtEnemyHelth_3.text = $"{curHP}/{maxHP}";
        imgEnemyHp_3.value = curHP * 1.0f / maxHP;
    }

    public void SetDiceObjectStats(int numDifficultClass, int numSkill, int numMod, string nameSkill, string nameMod)
    {
        txtDifficultClass.text = $"+{numDifficultClass}";
        txtSkillNum.text = $"+{numSkill}";
        txtModNum.text = $"+{numMod}";
        txtSkillName.text = nameSkill;
        txtModName.text = nameMod;
    }

    public void setDiceNum(int numDice)
    {
        dice.GetComponent<rollDice>().rollDiceAnim();
        dice.GetComponent<Image>().enabled = true;
        txtDiceNum.text = $"{numDice}";
        txtDiceNum.DOFade(1f, 0.5f).SetDelay(1.2f).SetEase(Ease.InOutQuart);
    }

    public void closeDice()
    {
        dice.GetComponent<Image>().enabled = false;
        txtDiceNum.text = "";
    }

    public void SetImageLoot(Sprite sprLoot)
    {
        imgLoot.sprite = sprLoot;
    }

    public void SetImageMeeting(Sprite sprMeeting)
    {
        imgMeeting.sprite = sprMeeting;
    }

    public void SetImageEnemy_1(Sprite sprEnemy)
    {
        Debug.Log("working");
        imgEnemy_1.sprite = sprEnemy;
    }

    public void SetImageEnemy_2(Sprite sprEnemy)
    {
        imgEnemy_2.sprite = sprEnemy;
    }

    public void SetImageEnemy_3(Sprite sprEnemy)
    {
        imgEnemy_3.sprite = sprEnemy;
    }


    public void OpenCloseDethPanel(bool dethPanel)
    {
        pnlPlayerDeth.SetActive(dethPanel);
    }

    public void OpenCloseEnemys(bool enemyPanel)
    {
        pnlEnemys.SetActive(enemyPanel);
    }

    public void OpenCloseNextStageBtn(bool neaxtStageBtn)
    {
        btnNextStage.SetActive(neaxtStageBtn);
    }

    public void OpenCloseDiceBtn(bool diceBtn)
    {
        btnDice.SetActive(diceBtn);
        autoBtnDice.SetActive(!diceBtn);
        logsBtnDice.SetActive(!diceBtn);
    }

    public void OpenCloseEventBattlePanel(bool battlePanel)
    {
        pnlEventBattle.SetActive(battlePanel);

        if (battlePanel == true)
        {
            OpenCloseNextStageBtn(false);
        }
        else
        {
            OpenCloseNextStageBtn(true);
        }
    }

    public void OpenCloseEventLootPanel(bool lootPanel)
    {
        pnlEventLoot.SetActive(lootPanel);
        lootVariants.SetActive(lootPanel);

        if (lootPanel == true)
        {
            OpenCloseNextStageBtn(false);
        }
        else
        {
            OpenCloseNextStageBtn(true);
        }
    }

    public void OpenCloseEventMeetingPanel(bool meetingPanel)
    {
        pnlEventMeeting.SetActive(meetingPanel);
        meetingVariants.SetActive(meetingPanel);

        if (meetingPanel == true)
        {
            OpenCloseNextStageBtn(false);
        }
        else
        {
            OpenCloseNextStageBtn(true);
        }
    }

    public void OpenCloseResultPanel(bool resultPanel, bool isLoot, string txt, Color colorTxt)
    {
        if (resultPanel && isLoot)
        {
            lootVariants.SetActive(false);
        }
        else if(resultPanel && !isLoot)
        {
            meetingVariants.SetActive(false);
        }
        pnlSucsessFailed.SetActive(resultPanel);
        txtResultEvent.text = txt;
        txtResultEvent.color = colorTxt;
    }


    public void OpenNextButtonWiyhDelay(float delay)
    {
        Invoke("NextBtnInvokeer", delay);
    }

    private void NextBtnInvokeer()
    {
        btnNextStage.SetActive(true);
    }

    public void CloseAllEventPanels()
    {
        //OpenCloseEnemys(false);
        OpenCloseEventBattlePanel(false);
        OpenCloseEventLootPanel(false);
        OpenCloseEventMeetingPanel(false);

        //SetImageEnemy_1(null);
    }
}
