using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public enum GameState { Menu, Playing, Paused, Win, Lose }

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("State")]
    public GameState state = GameState.Menu;

    [Header("Refs")]
    public GameObject player;
    public List<GameObject> enemies = new List<GameObject>();

    [Header("UI Panels (optional)")]
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainScene")
        {
            enemies.Clear();
            winConditionArmed = false;   // sahne baþýnda kapalý
            StartGame();
        }
        else if (scene.name == "MainMenu")
        {
            Time.timeScale = 1f;
            state = GameState.Menu;
            HideAllPanels();
        }
    }


    public bool winConditionArmed = false; // en az bir düþman kaydolunca true


    void Awake()
    {
        if (Instance == null) { Instance = this; DontDestroyOnLoad(gameObject); }
        else { Destroy(gameObject); }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            TogglePause();
    }

    public void StartGame()
    {
        state = GameState.Playing;
        Time.timeScale = 1f;
        HideAllPanels();
    }

     public void TogglePause()
    {
        if (state == GameState.Paused)
        {
            state = GameState.Playing;
            Time.timeScale = 1f;
            if (pausePanel) pausePanel.SetActive(false);
        }
        else if (state == GameState.Playing)
        {
            state = GameState.Paused;
            Time.timeScale = 0f;
            if (pausePanel) pausePanel.SetActive(true);
        }
    }

    public void RegisterEnemy(GameObject e)
    {
        if (!enemies.Contains(e)) enemies.Add(e);
        if (enemies.Count > 0) winConditionArmed = true;
    }

    void LateUpdate()
    {
        if (state != GameState.Playing) return;

        enemies.RemoveAll(e => e == null || !e.activeInHierarchy);

        // YALNIZCA en az bir düþman kaydedildiyse kazanma kontrolü yap
        if (winConditionArmed && enemies.Count == 0) OnWin();

        if (player == null || !player.activeInHierarchy) OnLose();
    }


    void OnWin()
    {
        state = GameState.Win;
        Time.timeScale = 0f;
        if (winPanel) winPanel.SetActive(true);
    }

    void OnLose()
    {
        state = GameState.Lose;
        Time.timeScale = 0f;
        if (losePanel) losePanel.SetActive(true);
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        state = GameState.Menu;
        SceneManager.LoadScene("MainMenu");
    }

    void HideAllPanels()
    {
        if (pausePanel) pausePanel.SetActive(false);
        if (winPanel) winPanel.SetActive(false);
        if (losePanel) losePanel.SetActive(false);
    }
}
