using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public int index;
    public Color color;
    public int points;

    void Start() {
        DontDestroyOnLoad(gameObject);
    }

    public void Setup(int _index, Color _color) {
        index = _index;
        color = _color;
        points = 0;
    }

}
