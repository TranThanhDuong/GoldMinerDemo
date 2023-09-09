using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BootLoader : Singleton<BootLoader>
{
    // Start is called before the first frame update
    void Start()
    {
        ConfigManager.Instance.InitConfig(() =>
        {
            InitConfigDone();
        });
    }

    private void InitConfigDone()
    {
        DataAPIControler.Instance.OnInit(() =>
        {
            SoundManager.Instance.Setup();
            LoadSceneManager.Instance.LoadSceneByIndex(1, () => {

                ViewManager.Instance.OnSwitchView(ViewIndex.HomeView);
            });
        });
     
    }
}
