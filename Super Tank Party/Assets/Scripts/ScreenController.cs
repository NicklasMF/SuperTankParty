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

    public void StartGame() {
        SetupPreGame();
        StartCoroutine(LoadLevel(startingLevel));
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
        PositionPlayers();
    }

    void PositionPlayers() {
        Transform positionParent = GameObject.FindGameObjectWithTag("StartingPositions").transform;
        foreach (GameObject player in GetComponent<GameController>().players) {
            player.transform.position = positionParent.GetChild(player.GetComponent<Player>().index).position;
        }
    }

    public void GoToNextLevel() {
        currentLevelIndex++;
        if (levels.Length < currentLevelIndex) {
            StartCoroutine(LoadLevel(levels[currentLevelIndex]));
            SetupGame();
        } else {
            SceneManager.LoadScene(mainMenu);
        }
    }
    
}
