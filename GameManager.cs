using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager manager = null;
    public GameObject boardPrefab;
    public int highScore;
    string savePath;
    public GameObject audioObj;
    public AudioManager audioManager;
    public bool bgLightsMade;
    private bool loaded;
    public bool newHighScore;
    public bool audioLoaded;
    public bool managerLoaded;
    // Start is called before the first frame update
    private void Awake()
    {
        managerLoaded = false;
        newHighScore = false;
        loaded = false;
        audioManager = audioObj.GetComponent<AudioManager>();
        bgLightsMade = false;
        if (manager != null && manager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            manager = this;
        }
        DontDestroyOnLoad(gameObject);
        Debug.Log("done");
        managerLoaded = true;
    }
    void Start()
    {
        savePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "savedata.tetris";
        highScore = Load();
        loaded = true;
    }
    public void NewHighScore(int amount)
    {
        if(!newHighScore)
        {
            audioManager.Play(6);
            newHighScore = true;
        }
        highScore = amount;
    }
    private void OnApplicationQuit()
    {
        Save();
    }
    private void OnApplicationPause(bool pause)
    {
        if (loaded)
        {
            Save();
        }
    }
    private void Save()
    {
        if(savePath == null)
        {
            savePath = Application.persistentDataPath + Path.DirectorySeparatorChar + "savedata.tetris";
        }
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(savePath, FileMode.Create);
        Debug.Log(highScore);
        formatter.Serialize(stream, new SaveData(highScore));
        stream.Close();
    }
    private int Load()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(savePath))
        {
            FileStream stream = new FileStream(savePath, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;
            stream.Close();
            Debug.Log(data.score);
            return data.score;
        }
        else
        {
            Debug.Log("no file");
            FileStream stream = new FileStream(savePath, FileMode.Create);
            formatter.Serialize(stream, new SaveData(0));
            stream.Close();
            return 0;
        }
    }
    public void Reset()
    {
        highScore = 0;
        Save();
    }
    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }
}
