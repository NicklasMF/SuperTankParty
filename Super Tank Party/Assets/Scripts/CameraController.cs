using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Transform camTransform;

    float shakeDuration = 0f;
    float shakeAmount = 0.5f;
    float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake() {
        if (GameObject.FindGameObjectsWithTag("MainCamera").Length > 1) {
            Destroy(gameObject);
        } else {
            DontDestroyOnLoad(gameObject);
        }
        if (camTransform == null) {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable() {
        originalPos = camTransform.localPosition;
    }

    void Update() {
        if (shakeDuration > 0) {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        } else {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }
    }

    public void Screenshake(ScreenshakeType screenshake) {
        shakeDuration = 0.1f * (int) screenshake;
    }
}

public enum ScreenshakeType {
    Low = 1,
    Medium = 2,
    Hard = 3
}
