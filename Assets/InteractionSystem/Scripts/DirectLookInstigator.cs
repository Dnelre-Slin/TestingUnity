using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLookInstigator : BaseInstigator
{
    [SerializeField]
    private float interactDistance = 2f;
    private Transform lookDirection;

    protected void Start()
    {
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

    protected void LookForInteractable()
    {
        RaycastHit hit;
        int layerMask = Physics.DefaultRaycastLayers;
        if (Physics.Raycast(this.lookDirection.position, this.lookDirection.forward, out hit, this.interactDistance, layerMask))
        {
            this.SetCurrentInteractable(hit.collider.transform.gameObject.GetComponent<BaseInteractable>());
            if (this.currentInteractable != null)
            {
                this.Refresh();
                return;
            }
        }
        // If no interactable in sight:
        this.Clear();
        this.SetCurrentInteractable(null);
    }

}
