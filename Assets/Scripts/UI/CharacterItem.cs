using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterItem : MonoBehaviour
{
    public GameObject mItemPrafab;
    public GameObject mCharacterPrefab;
    public WndSelCharacter mWnd;

    Button mClickBtn;

    private void Awake()
    {
        mClickBtn = GetComponent<Button>();
    }

    private void OnEnable()
    {
        if (mClickBtn)
        mClickBtn.interactable = mItemPrafab != null;
    }

    public void OnClick()
    {
        mWnd.OnClickItem(this);
    }
}
