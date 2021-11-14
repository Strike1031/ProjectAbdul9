using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBase : MonoBehaviour
{
    static ScreenBase mInstance;

    public static T GetInstance<T>() where T : ScreenBase
    {
        if (mInstance == null)
            mInstance = FindObjectOfType<ScreenBase>();
        return (T)mInstance;
    }

    protected virtual void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    protected virtual void Update()
    {

    }
}
