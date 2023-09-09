using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance_;
    public static T Instance
    {
        get
        {
            if(instance_ == null)
            {
                GameObject obj = new GameObject();
                obj.AddComponent<T>();
                obj.name = typeof(T).ToString();
                instance_ = obj.GetComponent<T>();
            }    

            return instance_;
        }
    }

    private void Awake()
    {
        instance_ = gameObject.GetComponent<T>();
    }

    private void Reset()
    {
        gameObject.name = typeof(T).ToString();
    }
}
