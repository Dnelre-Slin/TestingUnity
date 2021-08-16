using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideInteractionTextOnUnpossess : MonoBehaviour
{
    [SerializeField]
    Controllable controllable = null;
    [SerializeField]
    IInstigatorWithUiText instigator = null;

    void Awake()
    {
        this.controllable = this.GetComponent<Controllable>();
        this.instigator = this.GetComponent<IInstigatorWithUiText>();
    }

    void OnEnable()
    {
        Controllable.OnNewControlled += UpdateText;
    }
    void OnDisable()
    {
        Controllable.OnNewControlled -= UpdateText;
    }

    void UpdateText(Controllable newPossessedControllable)
    {
        if (this.instigator != null && this.controllable != null)
        {
            this.instigator.SetShowText(newPossessedControllable == this.controllable); // Show text if the new possesed controllable, is the controllable in 'this'.
        }
    }
}
