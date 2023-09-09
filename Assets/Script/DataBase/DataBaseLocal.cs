using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.Events;

public class DataEventTrigger : UnityEvent<object>
{

}
public static class DataTrigger
{
    public static Dictionary<string, DataEventTrigger> dicOnValueChange = new Dictionary<string, DataEventTrigger>();
    public static void RegisterValueChange(string s, UnityAction<object> delegateDataChange)
    {
        if (dicOnValueChange.ContainsKey(s))
        {
            dicOnValueChange[s].AddListener(delegateDataChange);
        }
        else
        {
            dicOnValueChange.Add(s, new DataEventTrigger());
            dicOnValueChange[s].AddListener(delegateDataChange);
        }

    }
    public static void UnRegisterValueChange(string s, UnityAction<object> delegateDataChange)
    {
        if (dicOnValueChange.ContainsKey(s))
        {
            dicOnValueChange[s].RemoveListener(delegateDataChange);
        }


    }
    //extention method 
    public static void TriggerEventData(this object data, string path)
    {
        if (dicOnValueChange.ContainsKey(path))
            dicOnValueChange[path].Invoke(data);
    }
}
public class DataBaseLocal : MonoBehaviour
{
    private PlayerData dataPlayer;
    public bool LoadData()
    {
        if (PlayerPrefs.HasKey("DATA"))
        {

            GetData();
            return true;
        }
        else
        {
            return false;
        }
    }
    public void CreateNewData(PlayerData data)
    {
        dataPlayer = data;
        SaveData();
    }
    public T Read<T>(string path)
    {
        object data = null;

        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);

        ReadDataBypath(paths, dataPlayer, out data);

        return (T)data;
    }
    public T Read<T>(string path, object key)
    {
        object data = null;

        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);

        ReadDataBypath(paths, dataPlayer, out data);
        Dictionary<string, T> newDic = (Dictionary<string, T>)data;
        T outData;
        newDic.TryGetValue(key.ToKey(), out outData);
        return outData;

    }
    private void ReadDataBypath(List<string> paths, object data, out object dataOut)
    {
        string p = paths[0];

        Type t = data.GetType();

        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            dataOut = field.GetValue(data);
        }
        else
        {
            paths.RemoveAt(0);
            ReadDataBypath(paths, field.GetValue(data), out dataOut);
        }

    }
    public void UpdateData(string path, object dataNew, Action callback)
    {

        string[] s = path.Split('/');
         
        List<string> paths = new List<string>();
        paths.AddRange(s);
        List<object> dataChanged = new List<object>();
        UpdateDataBypath(paths, dataPlayer, dataNew, ref dataChanged, callback);
        SaveData();
        string spath = string.Empty;
        paths.Clear();
        paths.AddRange(s);
        for (int i = 0; i < paths.Count; i++)
        {
            if (i == 0)
            {
                spath = paths[i];
            }
            else
                spath = spath + "/" + paths[i];
           
            dataChanged[i].TriggerEventData(spath);
        }
    }
    private void UpdateDataBypath(List<string> paths, object data, object datanew, ref List<object> dataChanged, Action callback)
    {
       
        string p = paths[0];
        Type t = data.GetType();
        FieldInfo field = t.GetField(p);
        if (paths.Count == 1)
        {
            dataChanged.Add(datanew);
            field.SetValue(data, datanew);
            if (callback != null)
            {
                callback();
            }
        }
        else
        {
            object dataAdd = field.GetValue(data);
            dataChanged.Add(dataAdd);

            paths.RemoveAt(0);
            UpdateDataBypath(paths, dataAdd, datanew, ref dataChanged, callback);
        }

    }
    public void UpdateData<TValue>(string path, object key, TValue dataNew, Action callback)
    {

        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        object dicObject = UpdateDataDicBypath(paths, dataPlayer, key, dataNew, callback);
        SaveData();
        dicObject.TriggerEventData(path);
        dataNew.TriggerEventData(path + "/" + key.ToKey());
    }
    private object UpdateDataDicBypath<TValue>(List<string> paths, object data, object key, TValue dataNew, Action callback)
    {
        string p = paths[0];

        Type t = data.GetType();

        FieldInfo field = t.GetField(p);

        object objectReturn = null;
        if (paths.Count == 1)
        {

            object dic = field.GetValue(data);

            Dictionary<string, TValue> newDic = (Dictionary<string, TValue>)dic;
            newDic[key.ToKey()] = dataNew;
            objectReturn = newDic;
            field.SetValue(data, newDic);
            if (callback != null)
            {
                callback();
            }

        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataDicBypath(paths, field.GetValue(data), key, dataNew, callback);
        }
        return objectReturn;
    }

    public void UpdateData2<TValue>(string path, object key, TValue dataNew, Action callback)
    {

        string[] s = path.Split('/');
        List<string> paths = new List<string>();
        paths.AddRange(s);
        object dicObject = UpdateDataDicBypath2(paths, dataPlayer, key, dataNew, callback);
        SaveData();
        dicObject.TriggerEventData(path);
        dataNew.TriggerEventData(path + "/" + key);
    }
    private object UpdateDataDicBypath2<TValue>(List<string> paths, object data, object key, TValue dataNew, Action callback)
    {
        string p = paths[0];

        Type t = data.GetType();

        FieldInfo field = t.GetField(p);

        object objectReturn = null;
        if (paths.Count == 1)
        {

            object dic = field.GetValue(data);

            Dictionary<string, TValue> newDic = (Dictionary<string, TValue>)dic;
            newDic[key.ToString()] = dataNew;
            objectReturn = newDic;
            field.SetValue(data, newDic);
            if (callback != null)
            {
                callback();
            }

        }
        else
        {
            paths.RemoveAt(0);
            UpdateDataDicBypath(paths, field.GetValue(data), key, dataNew, callback);
        }
        return objectReturn;
    }

    public void Delete()
    {

    }

    private void SaveData()
    {
        string s = JsonUtility.ToJson(dataPlayer);
        PlayerPrefs.SetString("DATA", s);
    }
    private void GetData()
    {
        string s = PlayerPrefs.GetString("DATA");
        dataPlayer = JsonUtility.FromJson<PlayerData>(s);
    }
}
