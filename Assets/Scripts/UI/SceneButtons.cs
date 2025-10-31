using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    public void ReloadMainScene() => SceneManager.LoadScene("MainScene");
    public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");
}
