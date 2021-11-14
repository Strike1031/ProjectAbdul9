using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenStage : ScreenBase
{
    public Transform mStartLPos;
    public Transform mStartRPos;
    public HealthBar mUserHealthBar;
    public HealthBar mAIHealthBar;

    public Text mUserScore;
    public Text mAIScore;
    public Text mUserName;
    public Text mAIName;
    public Text mRoundTime;
    public Text mRoundText;

    protected override void Start()
    {
        mRoundText.gameObject.SetActive(false);
        WndMgr.Singleton.Clear();

        GameMgr.Singleton.StartGame();

        FindObjectOfType<AudioMgr>().UpdateMusic();
    }

    protected override void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            WndMgr.Singleton.OpenCloseWnd(WndTypes.WndEsc.ToString(), null, null, true);
        }
    }

    public void UpdateRoundTime(int roundTime)
    {
        mRoundTime.text = roundTime.ToString();
    }

    public void ShowScore(int userScore, int otherScore)
    {
        mUserScore.text = userScore.ToString();
        mAIScore.text = otherScore.ToString();
    }

    public void ShowRoundUI(int round)
    {
        mRoundText.text = string.Format("  ROUND   {0}  ", round);
        mRoundText.gameObject.SetActive(true);

        AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.AudioUI, "Round");
    }

    public void HideRoundUI()
    {
        mRoundText.gameObject.SetActive(false);
    }

    public void ShowEndGameUI(string resultText)
    {
        mRoundText.text = resultText;
        mRoundText.gameObject.SetActive(true);
    }

    public void ShowPlayerInfo(Player userPlayer, Player aiPlayer)
    {
        userPlayer.SetHealthBar(mUserHealthBar);
        aiPlayer.SetHealthBar(mAIHealthBar);

        mUserName.text = userPlayer.name;
        mAIName.text = aiPlayer.name;
    }

    public void DisplayTrackTime(int trackTime)
    {
        mRoundTime.text = trackTime.ToString();
    }
}
