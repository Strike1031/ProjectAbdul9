using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ResourceMgr : MonoBehaviour
{
    public static ResourceMgr Singleton
    {
        get
        {
            return Manager.mResourceMgr;
        }
    }

    Dictionary<string, GameObject> mLoadedPrefabs = new Dictionary<string, GameObject>();
    Dictionary<string, AudioClip> mLoadedSounds = new Dictionary<string, AudioClip>();

    public GameObject GetUI(string wndName)
    {
        return GetPrefab(Path.Combine("UI", wndName));
    }

    public GameObject GetPrefab(string absName)
    {
        string parsedAbsName = Util.ParsePath(absName);

        GameObject obj;
        if (mLoadedPrefabs.TryGetValue(parsedAbsName, out obj))
            return obj;

        obj = Resources.Load<GameObject>(absName);
        mLoadedPrefabs.Add(parsedAbsName, obj);
        return obj;
    }

    public AudioClip GetAudioClip(string absName)
    {
        string parsedAbsName = Util.ParsePath(absName);

        AudioClip clip;
        if (mLoadedSounds.TryGetValue(parsedAbsName, out clip))
            return clip;

        clip = Resources.Load<AudioClip>(absName);
        mLoadedSounds.Add(parsedAbsName, clip);
        return clip;
    }

    public AudioClip[] GetAudioClips(string channelName)
    {
        string channelPath = Path.Combine("Audio", channelName);
        AudioClip[] clips = Resources.LoadAll<AudioClip>(channelPath);
        for (int i = 0; i < clips.Length; i++)
        {
            AudioClip clip = clips[i];
            mLoadedSounds[Util.ParsePath(Path.Combine(channelPath, clip.name))] = clip;
        }

        FindObjectOfType<AudioMgr>().UpdateMusic();
        return clips;

        
    }
}
