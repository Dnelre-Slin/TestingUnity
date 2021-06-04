using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct SourceObjectData
{
    public IDSInternal idsInternal;
    // public GameObject sourceObject;
    public IDSProjection idsProjection;
    public List<CameraInfo> cameraInfos;

    public SourceObjectData(IDSInternal idsInternal, IDSProjection idsProjection, List<CameraInfo> cameraInfos)
    {
        this.idsInternal = idsInternal;
        // this.sourceObject = sourceObject;
        this.idsProjection = idsProjection;
        this.cameraInfos = cameraInfos;
    }
}

public class IDSLevelScript : MonoBehaviour
{
    private Dictionary<GameObject, SourceObjectData> sourceObjects = null;
    private List<GameObject> positionIndexes = null;
    // private bool inited = false;

    // public void SetupInit()
    // {
    //     if (!this.inited)
    //     {
    //         this.sourceObjects = new Dictionary<GameObject, SourceObjectData>(10);
    //         this.positionIndexes = new List<GameObject>(10);
    //         this.inited = true;
    //     }
    // }

    void Awake()
    {
        // if (!this.inited)
        // {
        this.sourceObjects = new Dictionary<GameObject, SourceObjectData>(10);
        this.positionIndexes = new List<GameObject>(10);
            // this.inited = true;
        // }
    }

    public void AddSourceObjectData(GameObject sourceObject, SourceObjectData sourceObjectData)
    {
        this.sourceObjects[sourceObject] = sourceObjectData;
    }

    public void ModifySourceObjectData(GameObject sourceObject, SourceObjectData newSourceObjectData)
    {
        this.sourceObjects[sourceObject] = newSourceObjectData;
    }

    public SourceObjectData? GetSourceObjectData(GameObject sourceObject)
    {
        if (this.sourceObjects.ContainsKey(sourceObject))
        {
            return this.sourceObjects[sourceObject];
        }
        return null;
    }

    public SourceObjectData? RemoveSourceObjectData(GameObject sourceObject)
    {
        if (this.sourceObjects.ContainsKey(sourceObject))
        {
            SourceObjectData sObj = this.sourceObjects[sourceObject];
            this.sourceObjects.Remove(sourceObject);
            return sObj;
        }
        return null;
    }

    public Vector3 GetPositionFromIndex(int index)
    {
        float z = 100f * index - 1000f;
        return new Vector3(-2000f, 0f, z);
    }

    public int GetPositionIndex(GameObject gameObject)
    {
        Debug.Log(this.positionIndexes);
        for (int i = 0; i < this.positionIndexes.Count; i++)
        {
            if (this.positionIndexes[i] == null)
            {
                this.positionIndexes[i] = gameObject;
                return i;
            }
        }
        this.positionIndexes.Add(gameObject);
        return this.positionIndexes.Count - 1;
    }

    public void ClearPositionIndex(int index)
    {
        if (index < this.positionIndexes.Count)
        {
            this.positionIndexes[index] = null;
        }
    }
}
