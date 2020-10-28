using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingSceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadMenu());
    }
    IEnumerator LoadMenu()
    {
        yield return new WaitUntil(() => GameManager.manager.audioLoaded);// && GameManager.manager.bgLightsMade && GameManager.manager.managerLoaded);
        GameManager.manager.audioManager.Play(0);
        SceneManager.LoadSceneAsync(1);
    }
}
