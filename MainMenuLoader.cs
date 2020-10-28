using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuLoader : MonoBehaviour
{
    public TMP_Text highScoreTxt;

    void Start()
    {
        highScoreTxt.SetText("" + GameManager.manager.highScore);
    }
    public void Reset()
    {
        GameManager.manager.Reset();
        highScoreTxt.SetText("" + GameManager.manager.highScore);
    }
    public void LoadGameBoard()
    {
        StartCoroutine(LoadBoard());
    }
    IEnumerator LoadBoard()
    {
        SceneManager.LoadSceneAsync(2);
        yield return null;
    }
    public void Quit()
    {
        GameManager.manager.Quit();
    }
}
