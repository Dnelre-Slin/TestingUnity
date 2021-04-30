using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAddToInputSection : MonoBehaviour
{
    [SerializeField]
    private TestRenderInput testRenderInput = null;
    [SerializeField]
    private GameObject addGameObject = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (addGameObject != null)
        {
            this.testRenderInput.AddGameObject(this.addGameObject);
            this.addGameObject = null;
        }
    }
}
