using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Newtonsoft.Json;

public class StartMenuManager : MonoBehaviour
{
    public static StartMenuManager Instance;

    public TextMeshProUGUI menuText;
    public static string username;
    public static int bestScore = 0;

    public static Dictionary<string, int> userdata = new Dictionary<string, int>();

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadData();
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }


    public void ReadNameInput(string name)
    {
        if (name != "")
        {
            username = name;

            if (userdata.ContainsKey(username))
            {
                bestScore = userdata[username];
            }
            else
            {
                userdata.Add(username, 0);
                bestScore = userdata[username];
                Debug.Log("New user. Adding to dictionary.");
            }

            menuText.text = $"Welcome, {name}! High score: {bestScore}";
        }
        else
        {
            menuText.text = "Enter your name:";
        }

    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/userdata.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            userdata = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
            Debug.Log("File found. Loading data");
        }
        else
        {
            Debug.Log("No file found. Using new dictionary.");
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
            Application.Quit();
#endif
    }
}
