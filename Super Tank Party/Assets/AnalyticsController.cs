using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class AnalyticsController : MonoBehaviour {

    public float timeSinceAppOpened;
    public float timeForRound;

    void Start() {
        timeSinceAppOpened = 0;
    }

    void Update() {
        timeSinceAppOpened += Time.deltaTime;

        if (GetComponent<GameController>().gameStarted) {
            timeForRound += Time.deltaTime;
        }
    }

    public void StartGame() {
        AnalyticsEvent.Custom("GameStarted", new Dictionary<string, object> {
            {"PlayerCount", GetComponent<GameController>().players.Count},
            {"TimeSinceAppOpened", timeSinceAppOpened}
        });
    }

    public void EndRound() {
        AnalyticsEvent.Custom("EndRound", new Dictionary<string, object> {
            {"TimeRound", timeForRound}
        });
        timeForRound = 0;
    }
}
