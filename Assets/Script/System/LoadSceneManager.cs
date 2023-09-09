using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;
using TMPro;

public class LoadSceneManager : Singleton<LoadSceneManager>
{
    public GameObject loadProgess;
    public Image imageProgess;
    public TextMeshProUGUI lbPregess;
    public void LoadSceneByName(string name, Action callBack)
    {
        StopAllCoroutines();
        StartCoroutine(CoLoadSceneByName(name, callBack));
    }
    IEnumerator CoLoadSceneByName(string scenName, Action callBack)
    {
        loadProgess.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scenName, LoadSceneMode.Single);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            lbPregess.text = Mathf.RoundToInt(asyncLoad.progress * 100).ToString();
            imageProgess.fillAmount = asyncLoad.progress;
            yield return null;
        }
        loadProgess.SetActive(false);
        callBack?.Invoke();
    }
    public void LoadSceneByIndex(int index, Action callBack)
    {
        StopAllCoroutines();
        StartCoroutine(CoLoadSceneByIndex(index, callBack));
    }
    IEnumerator CoLoadSceneByIndex(int index, Action callBack)
    {
        loadProgess.SetActive(true);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(index,LoadSceneMode.Single);
        
        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            //lbPregess.text = Mathf.RoundToInt(asyncLoad.progress * 100).ToString();
            //imageProgess.fillAmount = asyncLoad.progress;
            yield return null;
        }
        float tiemCount = 0;
        while (tiemCount<1)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            tiemCount += Time.deltaTime;
            
            lbPregess.text = Mathf.RoundToInt(tiemCount * 100).ToString()+"%";
            imageProgess.fillAmount = tiemCount;
            yield return null;
        }
      
        loadProgess.SetActive(false);
        callBack?.Invoke();
    }
}
