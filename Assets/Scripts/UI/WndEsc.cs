using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WndEsc : WndBase
{
    public override void BeforeActive(bool active, MsgParam param)
    {
        Time.timeScale = active ? 0 : 1;
    }

    IEnumerator LoadMainScene()
    {
        Time.timeScale = 1;
        GameMgr.Singleton.Clear();

        yield return new WaitForEndOfFrame();

        //SceneManager.LoadScene(0);
         SceneManager.LoadScene(2);
    }

    public void OnClickMainMenu()
    {
        StartCoroutine(LoadMainScene());
    }
}
