using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenController : MonoBehaviour {

    [SerializeField] string[] levels;
    [SerializeField] string mainMenu;
    [HideInInspector] public int currentLevelIndex = -1;

    [HideInInspector] public CameraController cameraController;
    [HideInInspector] public MenuController menuController;
    [HideInInspector] public UIController uiController;

    AsyncOperation asyncLoadLevel;

    void Start() {
        if (GetComponent<GameController>().isDebugging && SceneManager.GetActiveScene().name != "MainMenu") {
            SetupDebugging();
        }
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

    #region Before Game
    void SetupPreGame() {
        GameController controller = GetComponent<GameController>();
        controller.players.Clear();
        foreach (Transform child in menuController.menuPlayerSelection.GetComponent<MenuPlayerSelection>().playerWrapper) {
            MenuPlayerSelectionPlayer menuPlayer = child.GetComponent<MenuPlayerSelectionPlayer>();
            if (menuPlayer.isSelected) {
                GameObject player = Instantiate(controller.playerPrefab);
                int index = child.GetSiblingIndex();
                player.GetComponent<Player>().Setup(index, controller.playerColors[index]);
                player.GetComponent<PlayerController>().SetupKeys(controller.keys[index * 2], controller.keys[index * 2 + 1]);
                GetComponent<GameController>().players.Add(player);
            }
        }
    }

    void SetupGame() {
        cameraController = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>();
        uiController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIController>();
        uiController.showIntro = GetComponent<GameController>().showIntro;
        uiController.GetComponent<UIController>().SetupControls(GetComponent<GameController>().players);
        GetComponent<GameController>().PrepareNewRound();
    }

    public void StartGame() {
        GoToFirstLevel();
    }

    void GoToFirstLevel() {
        GetComponent<GameController>().gameStarted = true;
        if (GetComponent<GameController>().players.Count == 0) {
            SetupPreGame();
        }
        StartCoroutine(LoadLevel());
    }

    #endregion


    #region Before Round


    #endregion


    #region In Round


    #endregion


    #region Between Rounds
    public void ShowResults() {
        uiController.ShowResults(GetComponent<GameController>().players);
    }

    public void GoToNextLevel() {
        currentLevelIndex++;
        if (currentLevelIndex >= levels.Length) {
            currentLevelIndex = 0;
        }
        StartCoroutine(LoadLevel(levels[currentLevelIndex]));
    }

    IEnumerator LoadLevel(string _scenename = null) {
        if (_scenename == null) {
            if (currentLevelIndex < 0) {
                currentLevelIndex = 0;
            }
            _scenename = levels[currentLevelIndex];
        }
        asyncLoadLevel = SceneManager.LoadSceneAsync(_scenename, LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone) {
            yield return null;
        }
        SetupGame();
    }

    IEnumerator GoToMainMenu() {
        asyncLoadLevel = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Single);
        while (!asyncLoadLevel.isDone) {
            yield return null;
        }
        Destroy(cameraController.gameObject);
        menuController = GameObject.FindGameObjectWithTag("Canvas").GetComponent<MenuController>();
    }

    #endregion


    public void EndGame() {
        GetComponent<GameController>().players.ForEach((GameObject obj) => Destroy(obj));
        GetComponent<GameController>().players = new List<GameObject>();
        Destroy(uiController.gameObject);

        StartCoroutine(GoToMainMenu());
    }

}
