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
        operation.allowSceneActivation = false;//������ɳ�����������ʾ���ó�����ʾΪfalse

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            slider.value = progress;
            changeNum.text = (progress * 100f).ToString("F0") + "%";
            changeNum.text = "�����������";
            if (Input.anyKey)
            {
                operation.allowSceneActivation = true;//��ʾ����
            }
            yield return null;
        }
    }

}
