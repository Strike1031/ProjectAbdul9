using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class AudioChannel
{
    [NonSerialized]
    public Transform mChannel;
    [NonSerialized]
    public List<AudioSource> mAudioSources = new List<AudioSource>();
    public bool mMute = false;
    public bool mLoop = false;

    [Range(0f, 1f)]
    public float mVolumn = 1f;
}

public class AudioMgr : MonoBehaviour
{
    public enum AudioChannelType
    {
        AudioBg,
        AudioUI,
        Audio3D,
        AudioAll,
    }

    public static AudioMgr Singleton
    {
        get { return Manager.mAudioMgr; }
    }

    private AudioListener mListener;

    public AudioListener Listener
    {
        get
        {
            mListener = FindObjectOfType<AudioListener>();
            if (mListener == null)
                mListener = Camera.main.gameObject.AddComponent<AudioListener>();
            return mListener;
        }
        set
        {
            mListener = value;
        }
    }

    public Dictionary<AudioChannelType, AudioChannel> mChannelData = new Dictionary<AudioChannelType, AudioChannel>();

    private AudioSource mCurrentBgAudio;

    private void Awake()
    {
        LoadChannelData();

        UpdateMusic();
    }

    public void UpdateMusic()
    {

        int childNumber;

        if (SceneManager.GetActiveScene().name == "main")
        {
            childNumber = 0;
        } else
        {
            childNumber = 0;
        }

        if (transform.childCount != 0)
        {
            if (PlayerPrefs.HasKey("Music"))
            {
                if (PlayerPrefs.GetString("Music") == "Off")
                {
                    for (var i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
                    {
                        gameObject.transform.GetChild(childNumber).GetChild(i).GetComponent<AudioSource>().volume = 0;
                    }
                }
                else if (PlayerPrefs.GetString("Music") == "On")
                {
                    for (var i = 0; i < gameObject.transform.GetChild(0).childCount; i++)
                    {
                        gameObject.transform.GetChild(childNumber).GetChild(i).GetComponent<AudioSource>().volume = 1;
                    }
                }

            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        Dictionary<AudioChannelType, AudioChannel>.Enumerator enumer = mChannelData.GetEnumerator();
        while (enumer.MoveNext())
        {
            string strType = enumer.Current.Key.ToString();
            GameObject obj = new GameObject(strType);
            obj.transform.SetParent(transform);

            enumer.Current.Value.mChannel = obj.transform;

            AudioClip[] clips = ResourceMgr.Singleton.GetAudioClips(strType);
            for (int i = 0; i < clips.Length; i++)
            {
                AddAudioSource(enumer.Current.Value, clips[i]);
            }
        }

        UpdateMusic();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadChannelData()
    {
        string audioChannelDataPath = Path.Combine(Application.persistentDataPath, "AudioData");
        if (File.Exists(audioChannelDataPath))
        {
            using (FileStream fs = File.Open(audioChannelDataPath, FileMode.Open))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    mChannelData = (Dictionary<AudioChannelType, AudioChannel>)formatter.Deserialize(fs);
                }
                catch (SerializationException e)
                {
                    Debug.Log("Failed to deserialize. Reason: " + e.Message);
                    return;
                }
                finally
                {
                    fs.Close();
                }
            }
        }

        if (!mChannelData.ContainsKey(AudioChannelType.AudioBg))
        {
            mChannelData.Add(AudioChannelType.AudioBg, new AudioChannel());
        }

        if (!mChannelData.ContainsKey(AudioChannelType.AudioUI))
        {
            mChannelData.Add(AudioChannelType.AudioUI, new AudioChannel());
        }

        if (!mChannelData.ContainsKey(AudioChannelType.Audio3D))
        {
            mChannelData.Add(AudioChannelType.Audio3D, new AudioChannel());
        }

        UpdateMusic();
    }

    void SaveChannelData()
    {
        string audioChannelDataPath = Path.Combine(Application.persistentDataPath, "AudioData");
        using (FileStream fs = File.OpenWrite(audioChannelDataPath))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, mChannelData);
            }
            catch (SerializationException e)
            {
                Debug.Log("Failed to serialize. Reason: " + e.Message);
                return;
            }
            finally
            {
                fs.Close();
            }
        }

        UpdateMusic();
    }

    void AddAudioSource(AudioChannel channel, AudioClip clip)
    {
        GameObject obj = new GameObject(clip.name);
        AudioSource source = obj.AddComponent<AudioSource>();
        source.clip = clip;
        source.mute = channel.mMute;
        source.volume = channel.mVolumn;
        source.loop = channel.mLoop;
        source.playOnAwake = false;
        obj.transform.SetParent(channel.mChannel);

        channel.mAudioSources.Add(source);

        UpdateMusic();
    }

    public void PlaySound(AudioChannelType type, string audioName, Transform attach = null)
    {

        UpdateMusic();

        AudioSource source = null;
        AudioChannel channel;
        if (mChannelData.TryGetValue(type, out channel))
            source = channel.mAudioSources.Find(audio => audio.name == audioName);
        
        if (source == null)
            return;

        if (!attach)
            attach = Listener.transform;

        source.transform.position = attach.position;
        source.transform.rotation = attach.rotation;

        source.Play();

        if (type == AudioChannelType.AudioBg)
        {
            if (mCurrentBgAudio)
                mCurrentBgAudio.Stop();
            mCurrentBgAudio = source;
        }

        UpdateMusic();
    }

    public void StopBgSound()
    {
        if (mCurrentBgAudio)
            mCurrentBgAudio.Stop();
    }

    public void StopSound(AudioChannelType type, string audioName)
    {
        AudioSource source = null;
        AudioChannel channel;
        if (mChannelData.TryGetValue(type, out channel))
            source = channel.mAudioSources.Find(audio => audio.name == audioName);

        if (source == null)
            return;

        source.Stop();

        UpdateMusic();
    }

    public void OnEnable()
    {
        UpdateMusic();
    }
}
