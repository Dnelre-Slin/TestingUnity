using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if (ENABLE_INPUT_SYSTEM)
using UnityEngine.InputSystem;
#endif

[RequireComponent(typeof(BaseInstigator))]
public class InstigatorControllerSimplified : MonoBehaviour
{
    #if (ENABLE_LEGACY_INPUT_MANAGER)
    [SerializeField]
    private string inputInteract = "f";
    #elif (ENABLE_INPUT_SYSTEM)
    [SerializeField]
    private InputActionAsset actionAsset = null;
    [SerializeField]
    private string actionMap = "Player";
    [SerializeField]
    private string inputMove = "Interact";
    #endif

    private BaseInstigator instigator = null;

    void Start()
    {
        this.instigator = this.GetComponent<BaseInstigator>();

        #if (ENABLE_INPUT_SYSTEM)
        InputActionMap inputActionMap = this.actionAsset.FindActionMap(this.actionMap);
        inputActionMap.Enable();
        InputAction inputActionMove = inputActionMap.FindAction(inputMove);
        inputActionMove.performed += OnInteract;
        #endif
    }

    #if (ENABLE_LEGACY_INPUT_MANAGER)
    void Update()
    {
        if (this.inputInteract != "")
        {
            if (Input.GetKeyDown(this.inputInteract))
            {
                this.instigator.Instigate();
            }
        }
    }
    #endif

    #if (ENABLE_INPUT_SYSTEM)
    void OnInteract(InputAction.CallbackContext context)
    {
        this.instigator.Instigate();
    }
    #endif
}
