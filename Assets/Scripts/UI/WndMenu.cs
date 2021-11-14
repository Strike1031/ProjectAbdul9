using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WndMenu : WndBase
{
    public Button mPvsCBtn;
    public Button mPvsPBtn;

    // Start is called before the first frame update
    protected override void Start()
    {
        mPvsPBtn.interactable = false;
    }

    public void OnClickPvsC()
    {
        WndMgr.Singleton.CloseWnd(WndTypes.WndMenu.ToString());
        WndMgr.Singleton.OpenCloseWnd(WndTypes.WndSelCharacter.ToString());
        GameMgr.Singleton.mGameMode = GameMode.Single;
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }
}
