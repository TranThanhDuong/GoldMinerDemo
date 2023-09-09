using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager :Singleton<ConfigManager> 
{
    [HideInInspector]
    public ConfigMission configMission;
    [HideInInspector]
    public ConfigObject configObject;
    [HideInInspector]
    public ConfigDataObjects configDataObjects;

    // Start is called before the first frame update
    public void InitConfig(Action callback)
    {
        StartCoroutine(LoadConfig(callback));
    }
    IEnumerator LoadConfig(Action callback)
    {
        configMission = Resources.Load("DataTable/ConfigMission", typeof(ScriptableObject)) as ConfigMission;
        yield return new WaitUntil(() => configMission != null);
        configDataObjects = Resources.Load("DataTable/ConfigDataObjects", typeof(ScriptableObject)) as ConfigDataObjects;
        yield return new WaitUntil(() => configDataObjects != null);
        configObject = Resources.Load("DataTable/ConfigObject", typeof(ScriptableObject)) as ConfigObject;
        yield return new WaitUntil(() => configObject != null);
        callback?.Invoke();
    }
}
