using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowFps : MonoBehaviour
{
    public float fps = 0f;
    private float dtime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(QualitySettings.vSyncCount);
    }

    // Update is called once per frame
    void Update()
    {
        dtime += (Time.unscaledDeltaTime - dtime) * 0.1f;
        fps = 1.0f / dtime;
    }
}
