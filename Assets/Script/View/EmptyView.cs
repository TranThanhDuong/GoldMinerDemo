using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyView : BaseView
{
    public override void OnSetup(ViewParam param)
    {
        base.OnSetup(param);
        //ViewManager.instances.OnSwitchView(ViewIndex.HomeView);
    }
}
