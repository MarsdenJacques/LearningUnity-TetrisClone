using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameBoardLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMainMenu()
    {
        StartCoroutine(LoadMenu());
    }
    IEnumerator LoadMenu()
    {
        SceneManager.LoadSceneAsync(1);
        yield return null;
    }
}
