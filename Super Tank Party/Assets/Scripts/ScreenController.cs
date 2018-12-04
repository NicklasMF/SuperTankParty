using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenController : MonoBehaviour {

    [SerializeField] string startingLevel;
    [SerializeField] string[] levels;
    [SerializeField] string mainMenu;
    public int currentLevelIndex = -1;

    public MenuUIController menuUIController;
    public UIController uiController;

    AsyncOperation asyncLoadLevel;

    void Start() {
        if (GetComponent<GameController>().isDebugging) {
            SetupDebugging();
        }
    }

    public void StartPractice() {
        GetComponent<GameController>().gameStarted = false;
        SetupPreGame();
        StartCoroutine(LoadLevel(startingLevel));
    }

    public void StartGame() {
        GetComponent<GameController>().gameStarted = true;
        GoToNextLevel();
    }

    void SetupDebugging() {
        for (int i = 0; i < GetComponent<GameController>().playerCount; i++) {
            GameObject player = Instantiate(GetComponent<GameController>().playerPrefab);
            player.GetComponent<Player>().Setup(i, GetComponent<GameController>().playerColors[i]);
            player.GetComponent<PlayerController>().SetupKeys(GetComponent<GameController>().keys[i * 2], GetComponent<GameController>().keys[i * 2 + 1]);
            GetComponent<GameController>().players.Add(player);
        }
        SetupGame();
    }

    void SetupPreGame() {
        GameController controller = GetComponent<GameController>();
        GetComponent<GameController>().players.Clear();
        foreach(Transform child in menuUIController.playerWrapper) {
            MenuPlayerSelection menuPlayer = child.GetComponent<MenuPlayerSelection>();
            if (menuPlayer.isSelected) {
                GameObject player = Instantiate(controller.playerPrefab);
                int index = child.GetSiblingIndex();
                player.GetComponent<Player>().Setup(index, controller.playerColors[index]);
                player.GetComponent<PlayerController>().SetupKeys(controller.keys[index * 2], controller.keys[index * 2 + 1]);
                GetComponent<GameController>().players.Add(player);
            }
        }
    }

    IEnumerator LoadLevel(string _scenename) {
        asyncLoadLevel = SceneManager.LoadSceneAsync(_scenename, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone)
        {
            yield return null;
        }
        SetupGame();
    }

    void SetupGame() {
        uiController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
        uiController.GetComponent<UIController>().SetupControls(GetComponent<GameController>().players);
        GetComponent<GameController>().StartRound();
    }

    public void GoToNextLevel() {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Length) {
            currentLevelIndex = 0;
        }
        StartCoroutine(LoadLevel(levels[currentLevelIndex]));
    }

    public void EndGame() {
        SceneManager.LoadScene(mainMenu);
    }

}
