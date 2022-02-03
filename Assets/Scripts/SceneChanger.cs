using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// SceneManagerの位置をVisualStudioCodeに教える
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // 切り替え先のシーン名
    [SerializeField]
    string nextSceneName = null;


    // シーンを切り替える
    public void ChangeScene()
    {
        // シーンを切り替える
        SceneManager.LoadScene(nextSceneName);
    }
}
