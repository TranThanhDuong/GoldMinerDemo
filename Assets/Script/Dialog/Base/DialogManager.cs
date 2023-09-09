using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DialogManager : Singleton<DialogManager>
{
    public event Action<BaseDialog> OnSwitchNewView;
  
    private Dictionary<DialogIndex, BaseDialog> dicView = new Dictionary<DialogIndex, BaseDialog>();
    private List<BaseDialog> dialogs = new List<BaseDialog>();
    public RectTransform anchorParent;
    // Start is called before the first frame update
    void Start()
    {
        foreach (DialogIndex e in DialogConfig.dialogIndexs)
        {
            string nameDialog = e.ToString();

            GameObject goView = Instantiate(Resources.Load("Dialog/" + nameDialog, typeof(GameObject))) as GameObject;

            goView.transform.SetParent(anchorParent, false);
            BaseDialog dialog = goView.GetComponent<BaseDialog>();
            dicView[e] = dialog;
            dialog.gameObject.SetActive(false);
        }
       
    }

    public void ShowDialog(DialogIndex index, DialogParam param = null, Action callBack = null)
    {

        BaseDialog dialog = dicView[index];
        dialog.gameObject.SetActive(true);
        dialog.GetComponent<RectTransform>().SetAsLastSibling();
        dialog.OnSetup(param);
        dialog.OnShow(callBack);
        dialogs.Add(dialog);
    }

    public void HideDialog(DialogIndex index,  Action callBack = null)
    {
        BaseDialog dialog = dicView[index];
        dialog.OnHide(callBack);
        dialogs.Remove(dialog);
        dialog.gameObject.SetActive(false);

    }
    public void HideAllDialog(DialogIndex index, Action callBack = null)
    {

        foreach(BaseDialog e in dialogs)
        {
            e.OnHide(callBack);
            e.gameObject.SetActive(false);
        }
        dialogs.Clear();
    }
}
