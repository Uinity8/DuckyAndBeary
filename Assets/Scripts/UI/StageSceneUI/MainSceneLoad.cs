using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneLoad : MonoBehaviour
{
    [SerializeField] private Button maintButton;

    private void Awake()
    {
        maintButton.onClick.AddListener(LoadMainScene);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene("StartScene");
    }
}
