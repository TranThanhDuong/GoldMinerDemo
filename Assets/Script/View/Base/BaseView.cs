using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BaseView : MonoBehaviour
{
    public ViewIndex index;
    public string nameView;
    [SerializeField]
    protected BaseViewAnimation baseViewAnimation;

    private void Awake()
    {
        baseViewAnimation = gameObject.GetComponentInChildren<BaseViewAnimation>();
        OnAwake();
    }
    public virtual void OnAwake()
    {

    }
    public virtual void OnSetup(ViewParam param)
    { }
    public void OnShow(Action callBack)
    {
        baseViewAnimation.OnShowAnim(() =>
        {
            OnShowView();
            callBack?.Invoke();
        });
    }
    public void OnHide(Action callBack)
    {
        baseViewAnimation.OnHideAnim(() =>
        {
            OnHideView();
            callBack?.Invoke();
        });
    }
    public virtual void OnShowView()
    { }
    public virtual void OnHideView()
    { }


}
