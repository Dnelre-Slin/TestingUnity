using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IControllable))]
public class PossessVehicleReactable : BaseReactable
{
    private IControllable controllable;

    void Start()
    {
        this.controllable = GetComponent<IControllable>();
    }

    override public void TriggerReaction()
    {
        this.controllable.AquirePossession();
    }
}
