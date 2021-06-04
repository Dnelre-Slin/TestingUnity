using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controllable))]
public class PossessVehicleReactable : BaseReactable
{
    private ControllableManagment controllableManagment;
    private Controllable controllable;
    // Start is called before the first frame update
    void Start()
    {
        this.controllableManagment = GameObject.FindObjectOfType<ControllableManagment>();
        this.controllable = GetComponent<Controllable>();
    }

    override public void TriggerReaction()
    {
        Controllable currentControllable = controllableManagment.GetCurrentControlledControllable();
        currentControllable.Possess(this.controllable);
    }
}
