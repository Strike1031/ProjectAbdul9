using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenHall : ScreenBase
{
    protected override void Awake()
    {
        if (Manager.mInstance == null)
        {
            GameObject managerInstance = new GameObject("Manager");
            managerInstance.AddComponent<Manager>();
        }
    }

    // Start is called before the first frame update
    protected override void Start()
    {
        WndMgr.Singleton.OpenCloseWnd(WndTypes.WndMenu.ToString());

        StartCoroutine(PlayBackgroundSound());
    }

    // Update is called once per frame
    protected override void Update()
    {
        
    }

    // In order to start from Start parallel
    IEnumerator PlayBackgroundSound()
    {
        yield return new WaitForEndOfFrame();

        AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.AudioBg, "Fantasy_Adventure");
    }
}
