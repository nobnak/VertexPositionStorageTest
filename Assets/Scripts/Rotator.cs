using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {
    public const float ROTATION_DEG = 360f;
    public Vector3 noiseDirectionalAngles = new Vector3 (0f, 90f, 180f);
    public float timeScale = 0.1f;
    public float dtScale = 1f;

    Vector2[] dirs;

    void OnEnable() {
        dirs = new Vector2[3];
        for (var i = 0; i < dirs.Length; i++) {
            var angle = Mathf.Cos (noiseDirectionalAngles [i] * Mathf.Deg2Rad);
            dirs [i] = new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle));
        }
    }
	void Update () {
        var t =timeScale * Time.timeSinceLevelLoad;
        var dt = dtScale * Time.deltaTime;
        transform.localRotation *= Quaternion.Euler (
            dt * Noise (t * dirs [0]) * ROTATION_DEG, 
            dt * Noise (t * dirs [1]) * ROTATION_DEG,
            dt * Noise (t * dirs [2]) * ROTATION_DEG);
	}

    float Noise(float x, float y) {
        return 2f * Mathf.PerlinNoise (x, y) - 1f;
    }
    float Noise(Vector2 v) {
        return Noise (v.x, v.y);
    }
}
