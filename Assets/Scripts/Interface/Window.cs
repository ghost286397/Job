using UnityEngine;
using System.Collections;

public abstract class Window : MonoBehaviour
{
    public UnityEngine.Events.UnityAction OnShow;
    public UnityEngine.Events.UnityAction OnHide;

    protected bool _Active;
    protected CanvasGroup myCanvasGroup;

    public int Priority;

    public bool Active
    {
        get { return _Active; }
    }
    public virtual void Init()
    {
        myCanvasGroup = GetComponent<CanvasGroup>();
        if (myCanvasGroup == null)
        {
            gameObject.AddComponent<CanvasGroup>();
            myCanvasGroup = GetComponent<CanvasGroup>();
        }
        myCanvasGroup.alpha = 0;
        gameObject.SetActive(false);
    }

    public virtual void Show()
    {
        _Active = true;
        gameObject.SetActive(true);
        StopCoroutine("HideCoroutine");
        StartCoroutine("ShowCoroutine");
        if (OnShow != null) OnShow();
    }

    public virtual void Hide()
    {
        _Active = false;
        if (OnHide != null) OnHide();
        StopCoroutine("ShowCoroutine");
        if(gameObject.activeInHierarchy)
            StartCoroutine("HideCoroutine");
        // FSBanner.ShowFSBanner();
    }
    private IEnumerator ShowCoroutine()
    {
        while (myCanvasGroup.alpha < 1)
        {
            myCanvasGroup.alpha += 0.1f;
            yield return new WaitForSeconds(0.02f);
        }
    }
    private IEnumerator HideCoroutine()
    {
        while (myCanvasGroup.alpha > 0)
        {
            myCanvasGroup.alpha -= 0.2f;
            yield return new WaitForSeconds(0.02f);
        }
        gameObject.SetActive(false);
    }
}
