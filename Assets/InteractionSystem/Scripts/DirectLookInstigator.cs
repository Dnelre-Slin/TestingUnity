using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLookInstigator : BaseInstigator
{
    [SerializeField]
    private float interactDistance = 2f;
    private Transform lookDirection;

    new void Start()
    {
        base.Start();
        Camera playerCam = this.gameObject.GetComponentInChildren<Camera>();
        if (playerCam != null)
        {
            lookDirection = playerCam.transform;
        }
        else
        {
            Debug.LogError("No player camera found. Will result in unwanted behaviour");
        }
    }

    void Update()
    {
        LookForInteractable();
    }

    override protected void LookForInteractable()
    {
        RaycastHit hit;
        int layerMask = Physics.DefaultRaycastLayers;
        if (Physics.Raycast(this.lookDirection.position, this.lookDirection.forward, out hit, this.interactDistance, layerMask))
        {
            this.SetCurrentInteractable(hit.collider.transform.gameObject.GetComponent<BaseInteractable>());
            if (this.currentInteractable != null)
            {
                this.UpdateText(this.currentInteractable.GetDescription(), true);
                return;
            }
        }
        // If no interactable in sight:
        this.SetCurrentInteractable(null);
        this.UpdateText("", false);
    }
}
