using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenceChange : MonoBehaviour
{
    public GameObject ScenceChangeBG;
    public Slider slider;
    public Text changeNum;

    public void LoadScence()
    {   
        gameObject.SetActive(true);
        StartCoroutine(LoadSceneAsync());
    }
    private IEnumerator LoadSceneAsync()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("levol 1");
        operation.allowSceneActivation = false;//加载完成场景不马上显示，让场景显示为false

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            changeNum.text = (progress * 100f).ToString("F0") + "%";
            changeNum.text = "按任意键继续";
            if (Input.anyKey)
            {
                operation.allowSceneActivation = true;//显示场景
            }
            yield return null;
        }
    }

}
