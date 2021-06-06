using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllableManagment : MonoBehaviour
{
    [SerializeField]
    private Controllable startControllable = null;
    private Controllable currentControlledControllable = null;

    void OnEnable()
    {
        Controllable.OnNewControlled += NewControlled;
    }

    void OnDisable()
    {
        Controllable.OnNewControlled -= NewControlled;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (startControllable != null)
        {
            startControllable.InitControll();
        }
    }

    private void NewControlled(Controllable newControlledControllable)
    {
        this.currentControlledControllable = newControlledControllable;
    }

    public Controllable GetCurrentControlledControllable()
    {
        return this.currentControlledControllable;
    }
}
