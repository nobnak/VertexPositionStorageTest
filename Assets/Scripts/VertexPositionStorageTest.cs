using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertexPositionStorageTest : MonoBehaviour {
    public string propVertexPosition = "_VertesPositions";
    public GameObject referenceObject;

    VertexPositionStorage capture;
    Renderer attachedRenderer;

    #region Unity
    void OnEnable() {
        capture = new VertexPositionStorage (referenceObject.GetComponentInChildren<SkinnedMeshRenderer> ());
        attachedRenderer = GetComponentInChildren<Renderer> ();
    }
    void Update() {
        capture.Capture (referenceObject.transform.localToWorldMatrix);
        attachedRenderer.sharedMaterial.SetBuffer (propVertexPosition, capture.GPUBuffer);
    }
    void OnDisable() {
        capture.Dispose ();
    }
    #endregion
}
