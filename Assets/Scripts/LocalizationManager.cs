using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class LocalizationManager : MonoBehaviour
{

    public static LocalizationManager instance;
    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            SetLanguage(PlayerPrefs.GetString("lang", "localizedText_en.json"));
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLocalizedText(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }
        }
        else
        {
            Debug.LogError("Cannot find file! named" + fileName);
        }
        isReady = true;
        SceneManager.LoadScene("StartScreenMenu");
    }

    IEnumerator LoadLocalizedTextOnAndroid(string fileName)
    {
        localizedText = new Dictionary<string, string>();
        string filePath;
        filePath = Path.Combine(Application.streamingAssetsPath + "/", fileName);
        string dataAsJson;
        if (filePath.Contains("://") || filePath.Contains(":///"))
        {
            UnityEngine.Networking.UnityWebRequest www = UnityEngine.Networking.UnityWebRequest.Get(filePath);
            yield return www.SendWebRequest();
            dataAsJson = www.downloadHandler.text;
        }
        else
        {
            dataAsJson = File.ReadAllText(filePath);
        }
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }
        isReady = true;
        SceneManager.LoadScene("StartScreenMenu");
    }

    public void SetLanguage(string fileName)
    {
        if (Application.platform == RuntimePlatform.WindowsEditor)
            LoadLocalizedText(fileName);
        else if (Application.platform == RuntimePlatform.OSXEditor)
            LoadLocalizedText(fileName);
        else if (Application.platform == RuntimePlatform.Android)
            StartCoroutine("LoadLocalizedTextOnAndroid", fileName);
        PlayerPrefs.SetString("lang", fileName);        
    }

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}