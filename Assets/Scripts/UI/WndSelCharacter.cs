using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WndSelCharacter : WndBase
{
    public Text mTimeText;
    public GameObject mItemsPanel;
    public Transform mCharacterPanel;
    public Transform mLeft;
    public Transform mRight;


    int expireTime = 55;

    // Start is called before the first frame update
    protected override void Start()
    {
        StartCoroutine(TrackTime());

        mItemsPanel.SetActive(true);
        mLeft.DetachChildren();
        mRight.DetachChildren();
    }

    IEnumerator TrackTime()
    {
        while (expireTime >= 0)
        {
            mTimeText.text = string.Format("Time : <color=orange>{0}</color>", expireTime--);
            yield return new WaitForSeconds(1f);
        }
    }

    IEnumerator LoadScene()
    {
        mItemsPanel.SetActive(false);

        yield return new WaitForSeconds(3f);

        AudioMgr.Singleton.StopBgSound();
        SceneManager.LoadScene(1);
    }

    public void OnClickItem(CharacterItem item)
    {
        if (mLeft.childCount == 0)
        {
            GameObject obj = Instantiate(item.mItemPrafab);
            obj.transform.SetParent(mLeft, false);
            GameMgr.Singleton.SetUserPlayer(item.mCharacterPrefab, item.name)
                .transform.localPosition = Vector3.zero;
        }
        else if (mRight.childCount == 0)
        {
            GameObject obj = Instantiate(item.mItemPrafab);
            obj.transform.SetParent(mRight, false);
            GameMgr.Singleton.SetAIPlayer(item.mCharacterPrefab, item.name)
                .transform.localPosition = Vector3.one * 5f;

            StartCoroutine(LoadScene());
        }
    }
}
