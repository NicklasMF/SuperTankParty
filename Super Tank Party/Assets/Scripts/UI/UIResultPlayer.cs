using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResultPlayer : MonoBehaviour {

    [SerializeField] Image sprite;
    [SerializeField] Text points;

    public void Setup(Color _color, int _points) {
        sprite.color = _color;
        points.text = _points.ToString();
    }

}
