using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gist;

public class VertexPositionCapture : System.IDisposable {
    SkinnedMeshRenderer skin;
    GPUArray<Vector3> gpuVertexPositions;
    Mesh animatedMesh;

    public VertexPositionCapture(SkinnedMeshRenderer skin) {
        this.skin = skin;
        this.animatedMesh = new Mesh ();
        this.gpuVertexPositions = new GPUArray<Vector3> (skin.sharedMesh.vertexCount);
    }

    #region IDisposable implementation
    public void Dispose () {
        gpuVertexPositions.Dispose ();
        Release (ref animatedMesh);
    }
    #endregion

    public ComputeBuffer GPUBuffer {
        get { return gpuVertexPositions.GPUBuffer; }
    }
    public void Capture() {
        skin.BakeMesh (animatedMesh);
        var vertices = animatedMesh.vertices;
        System.Array.Copy (vertices, gpuVertexPositions.CPUBuffer, vertices.Length);
        gpuVertexPositions.Upload ();
    }

    void Release<T>(ref T obj) where T : Object {
        if (Application.isPlaying)
            Object.Destroy (obj);
        else
            Object.DestroyImmediate (obj);
        obj = null;
    }
}
