using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] InputField playerName;

    void Start()
    {
        if (SaveInstanseManager.Instance.currentPlayerName != "")
        {
            playerName.text = SaveInstanseManager.Instance.currentPlayerName;
        }
        Debug.Log(Application.persistentDataPath);
    }

    public void StartNew()
    {
        SaveInstanseManager.Instance.currentPlayerName = playerName.text;
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }

}
