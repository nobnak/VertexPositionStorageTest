using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionStorageTest : MonoBehaviour {
    public string propVertexPosition = "_VertesPositions";
    public GameObject referenceObject;

    VertexPositionCapture capture;
    Renderer attachedRenderer;

    #region Unity
    void OnEnable() {
        capture = new VertexPositionCapture (referenceObject.GetComponentInChildren<SkinnedMeshRenderer> ());
        attachedRenderer = GetComponentInChildren<Renderer> ();
    }
    void Update() {
        capture.Capture ();
        attachedRenderer.sharedMaterial.SetBuffer (propVertexPosition, capture.GPUBuffer);
    }
    void OnDisable() {
        capture.Dispose ();
    }
    #endregion
}
