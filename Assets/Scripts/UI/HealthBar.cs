using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public RectTransform mTotalBar;
    public RectTransform mMaskBar;

    const float cTotal = 100f;
    private float mDamage;
    private float mTotalLength;

    public bool IsMinuse
    {
        get
        {
            return mDamage >= cTotal;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        mTotalLength = mTotalBar.rect.width;
        mMaskBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damage)
    {
        mDamage += damage;
        StartCoroutine(UpdateDamageUI(mDamage));
    }

    IEnumerator UpdateDamageUI(float originalDamage)
    {
        yield return new WaitForEndOfFrame();

        while (originalDamage - mDamage < 2f)
        {
            originalDamage = Mathf.Lerp(originalDamage, mDamage, 0.2f);
            float hSize = originalDamage / cTotal * mTotalLength;

            mMaskBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, hSize);

            yield return new WaitForEndOfFrame();
        }

        mDamage = cTotal;
        mMaskBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, mTotalLength);
    }

    public void Reset()
    {
        mDamage = 0f;

        StopAllCoroutines();
        mMaskBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
    }
}
