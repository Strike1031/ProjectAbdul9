using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WndMgr : MonoBehaviour
{
    public static WndMgr Singleton
    {
        get
        {
            return Manager.mWndMgr;
        }
    }

    private Dictionary<string, GameObject> mInstantiatedWndMap = new Dictionary<string, GameObject>();

    public GameObject OpenCloseWnd(string wndName, MsgParam param = null, Transform parent = null, bool bToggle = false)
    {
        GameObject obj;
        if (mInstantiatedWndMap.TryGetValue(wndName, out obj))
        {
            WndBase wnd = obj.GetComponent<WndBase>();
            if (bToggle)
            {
                wnd.BeforeActive(!obj.activeInHierarchy, param);

                obj.SetActive(!obj.activeInHierarchy);
                if (obj.activeInHierarchy)
                    obj.transform.SetAsLastSibling();
                else
                    obj.transform.SetAsFirstSibling();

                wnd.AfterActive(!obj.activeInHierarchy, param);
            }
            else
            {
                wnd.BeforeActive(true, param);
                obj.SetActive(true);
                obj.transform.SetAsLastSibling();
                wnd.AfterActive(true, param);
            }
        }
        else
        {
            obj = ResourceMgr.Singleton.GetUI(wndName);
            OpenWnd(wndName, obj, param, parent);
        }

        return obj;
    }

    public GameObject OpenWnd(string wndName, GameObject prefab, MsgParam param, Transform parent)
    {
        if (!parent)
            parent = FindObjectOfType<ScreenBase>().gameObject.transform;

        GameObject obj = Instantiate(prefab);
        obj.transform.SetParent(parent, false);
        obj.tag = "Window";

        WndBase wnd = obj.GetComponent<WndBase>();
        wnd.BeforeActive(true, param);
        obj.SetActive(true);
        obj.transform.SetAsLastSibling();
        wnd.AfterActive(true, param);

        mInstantiatedWndMap.Add(wndName, obj);

        return obj;
    }

    public void CloseWnd(string wndName, MsgParam param = null)
    {
        GameObject obj;
        if (!mInstantiatedWndMap.TryGetValue(wndName, out obj))
            return;

        WndBase wnd = obj.GetComponent<WndBase>();
        wnd.BeforeActive(false, param);
        obj.SetActive(false);
        obj.transform.SetAsFirstSibling();
        wnd.AfterActive(false, param);
    }

    public void Clear()
    {
        Dictionary<string, GameObject>.Enumerator enumer =  mInstantiatedWndMap.GetEnumerator();
        while(enumer.MoveNext())
        {
            Destroy(enumer.Current.Value);
        }
        mInstantiatedWndMap.Clear();
    }
}
