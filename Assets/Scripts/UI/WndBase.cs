using UnityEngine;
using UnityEngine.EventSystems;

/**
 * The meaning of active is opening or closing.
 */
public class WndBase : UIBehaviour
{
    /**
     * Called before opening as well as before closing.
     * @param 
     *      active: true before opening, false before closing.
     *      param: nullable.
     */
    public virtual void BeforeActive(bool active, MsgParam param)
    {

    }

    public virtual void AfterActive(bool active, MsgParam param)
    {

    }

    public virtual void CloseWnd()
    {
        WndMgr.Singleton.CloseWnd(this.GetType().Name);
    }

    public void PlaySound()
    {
        AudioMgr.Singleton.PlaySound(AudioMgr.AudioChannelType.AudioUI, "Click");
    }
}
