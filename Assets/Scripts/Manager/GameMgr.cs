using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Single,
    Multiple
}

public class GameMgr : MonoBehaviour
{
    public static GameMgr Singleton
    {
        get
        {
            return Manager.mGameMgr;
        }
    }

    public GameMode mGameMode;
    public FollowCamera mFollowCamera;
    private Player mUserPlayer;
    private Player mAIPlayer;
    private int mRound = 0;
    private int mRoundTime = 90;
    private int mUserScore = 0;
    private int mAIScore = 0;
    private Player mLoster;

    public GameObject SetUserPlayer(GameObject prefab, string name)
    {
        GameObject obj = Instantiate(prefab);
        obj.name = name;
        obj.transform.SetParent(transform);
        mUserPlayer = obj.GetComponent<Player>();
        mUserPlayer.IsPlayer = true;
        return obj;
    }

    public GameObject SetAIPlayer(GameObject prefab, string name)
    {
        GameObject obj = Instantiate(prefab);
        obj.name = name;
        obj.transform.SetParent(transform);
        mAIPlayer = obj.GetComponent<Player>();
        mAIPlayer.IsPlayer = false;
        return obj;
    }

    public void Clear()
    {
        Destroy(mUserPlayer.gameObject);
        Destroy(mAIPlayer.gameObject);
        StopAllCoroutines();
        mRound = 0;
    }

    IEnumerator TrackTime(ScreenStage screen)
    {
        while (mRoundTime-- > 0 && mLoster == null)
        {
            yield return new WaitForSeconds(1f);
            screen.DisplayTrackTime(mRoundTime);
        }
        EndRound(mRoundTime <= 0);
    }

    IEnumerator StartRoundStepByStep()
    {
        yield return new WaitForSeconds(0.5f);

        ScreenStage screen = ScreenBase.GetInstance<ScreenStage>();
        screen.ShowRoundUI(mRound);

        if (mRound == 1)
            AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.AudioBg, "IronForge");

        yield return new WaitForSeconds(2f);

        screen.HideRoundUI();

        StartCoroutine(TrackTime(screen));

        mUserPlayer.EnablePlayer(true);
        mAIPlayer.EnablePlayer(true);
    }

    IEnumerator ReStartRoundStepByStep()
    {
        yield return new WaitForSeconds(2f);

        InitPlayers();

        yield return new WaitForSeconds(1f);

        StartRound();
    }

    IEnumerator EndGameStepByStep()
    {
        ScreenStage screen = ScreenBase.GetInstance<ScreenStage>();

        string uiText = "";
        if (mRound == 6)
            uiText = string.Format("  Drawed!!  ");
        else
        {
            string winnerName = mUserPlayer.name;
            if (mAIScore == 3)
                winnerName = mAIPlayer.name;

            uiText = string.Format("  {0} Wins!  ", winnerName);
        }
        screen.ShowEndGameUI(uiText);

        yield return new WaitForSeconds(2f);

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(0);
        Clear();
    }

    void StartRound()
    {
        mRound += 1;
        mRoundTime = 90;
        mLoster = null;
        StartCoroutine(StartRoundStepByStep());
        FindObjectOfType<AudioMgr>().UpdateMusic();
    }

    void EndRound(bool isTimeout)
    {
        mUserPlayer.EnablePlayer(false);
        mAIPlayer.EnablePlayer(false);
        if (isTimeout)
        {
            if (mUserPlayer.GetHealth() > mAIPlayer.GetHealth())
            {
                mUserScore += 1;
                mUserPlayer.SetIdle();
            }
            else if (mUserPlayer.GetHealth() > mAIPlayer.GetHealth())
            {
                mAIScore += 1;
                mAIPlayer.SetIdle();
            }
        }
        else
        {
            if (mLoster == mUserPlayer)
            {
                mAIPlayer.SetIdle();
                mAIScore += 1;
            }
            else
            {
                mUserPlayer.SetIdle();
                mUserScore += 1;
            }
        }

        ScreenStage screen = ScreenBase.GetInstance<ScreenStage>();
        screen.ShowScore(mUserScore, mAIScore);

        if (mUserScore == 3 || mAIScore == 3 || mRound == 6)
            EndGame();
        else
            StartCoroutine(ReStartRoundStepByStep());
    }

    public void EndRound(Player loster)
    {
        mUserPlayer.EnablePlayer(false);
        mAIPlayer.EnablePlayer(false);

        mLoster = loster;
    }

    public void StartGame()
    {
        mRound = 0;
        mUserScore = 0;
        mAIScore = 0;

        mUserPlayer.enemy = mAIPlayer;
        mAIPlayer.enemy = mUserPlayer;

        mFollowCamera = Camera.main.GetComponent<FollowCamera>();
        if (mFollowCamera.targets == null)
            mFollowCamera.targets = new List<GameObject>();
        mFollowCamera.targets.Add(mUserPlayer.gameObject);
        mFollowCamera.targets.Add(mAIPlayer.gameObject);

        ScreenStage screen = ScreenBase.GetInstance<ScreenStage>();
        screen.ShowScore(0, 0);
        screen.ShowPlayerInfo(mUserPlayer, mAIPlayer);

        InitPlayers();
        StartRound();

        FindObjectOfType<AudioMgr>().UpdateMusic();
    }

    void EndGame()
    {
        StartCoroutine(EndGameStepByStep());
    }

    void InitPlayers()
    {
        ScreenStage screen = ScreenBase.GetInstance<ScreenStage>();

        mUserPlayer.transform.position = screen.mStartLPos.position;
        mUserPlayer.transform.rotation = screen.mStartLPos.rotation;
        mAIPlayer.transform.position = screen.mStartRPos.position;
        mAIPlayer.transform.rotation = screen.mStartRPos.rotation;
        mUserPlayer.Reset();
        mAIPlayer.Reset();

        mFollowCamera.offset.x = Mathf.Abs(mFollowCamera.offset.x);

        FindObjectOfType<AudioMgr>().UpdateMusic();
    }
}
