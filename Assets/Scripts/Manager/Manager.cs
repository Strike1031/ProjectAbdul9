using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager mInstance;

    public static WndMgr mWndMgr;
    public static ResourceMgr mResourceMgr;
    public static GameMgr mGameMgr;
    public static AudioMgr mAudioMgr;

    private void Awake()
    {
        mInstance = this;

        mWndMgr = CreateManager<WndMgr>();
        mResourceMgr = CreateManager<ResourceMgr>();
        mGameMgr = CreateManager<GameMgr>();
        mAudioMgr = CreateManager<AudioMgr>();

        DontDestroyOnLoad(mInstance);

        FindObjectOfType<AudioMgr>().UpdateMusic();
    }

    T CreateManager<T>() where T : Component
    {
        GameObject obj = new GameObject(typeof(T).Name);
        T manager = obj.AddComponent<T>();
        obj.transform.SetParent(transform);
        return manager;
    }
}
