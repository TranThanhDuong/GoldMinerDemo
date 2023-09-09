using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewManager : Singleton<ViewManager>
{
    public event Action<BaseView> OnSwitchNewView;
    public ViewIndex currentViewIndex;
    public BaseView PreviousView;
    public BaseView currentView;
    private Dictionary<ViewIndex, BaseView> dicView = new Dictionary<ViewIndex, BaseView>();
    public RectTransform anchorParent;
    // Start is called before the first frame update
    void Start()
    {
        foreach (ViewIndex e in ViewConfig.viewIndices)
        {
            string nameView = e.ToString();
            GameObject goView = Instantiate(Resources.Load("View/" + nameView, typeof(GameObject))) as GameObject;
            goView.transform.SetParent(anchorParent, false);
            BaseView view= goView.GetComponent<BaseView>();
            dicView[e] = view;
            view.gameObject.SetActive(false);
        }
        OnSwitchView(currentViewIndex);
    } 

    public void OnSwitchView(ViewIndex index, ViewParam param=null, Action callBack=null)
    {
       
        if(currentView!=null)
        {
            if (currentView.index == index)
                return;

            PreviousView = currentView;
            PreviousView.OnHide(()=> {
                PreviousView.gameObject.SetActive(false);

                currentView = dicView[index];
                OnSwitchNewView?.Invoke(currentView);
            
                currentView.gameObject.SetActive(true);
                currentView.OnSetup(param);
                currentView.OnShow(callBack);

            });
        }
        else
        {

            currentView = dicView[index];
            OnSwitchNewView?.Invoke(currentView);
            currentView.gameObject.SetActive(true);
            currentView.OnSetup(param);
            currentView.OnShow(callBack);

        }
    }

    public void OnPreviouView(ViewParam param = null, Action callBack = null)
    
        {
        BaseView temp = PreviousView;
        PreviousView = currentView;
        PreviousView.OnHide(() => {
            PreviousView.gameObject.SetActive(false);

            currentView = temp;
            OnSwitchNewView?.Invoke(currentView);

            currentView.gameObject.SetActive(true);
            currentView.OnSetup(param);
            currentView.OnShow(callBack);

        });
    }
}
