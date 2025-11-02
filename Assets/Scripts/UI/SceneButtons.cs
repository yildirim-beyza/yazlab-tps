using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour
{
    //public void ReloadMainScene() => SceneManager.LoadScene("MainScene");
    //public void LoadMainMenu() => SceneManager.LoadScene("MainMenu");

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReloadMainScene()
    {
        SceneManager.LoadScene("MainScene");
        if (GameManager.Instance != null)
            GameManager.Instance.StartGame();
    }

    // 🔹 YENİ: Retry tuşu için
    public void RetryGame()
    {
        SceneManager.LoadScene("MainScene");
        if (GameManager.Instance != null)
        {
            GameManager.Instance.state = GameState.Playing;
            GameManager.Instance.StartGame();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Oyun kapatıldı."); // Editörde test için
    }
}