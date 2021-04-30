using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectLookInstigator : BaseInstigator
{
    [SerializeField]
    private float interactDistance = 2f;
    private Transform lookDirection;
    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        LookForInteractable();
    }

    override protected void LookForInteractable()
    {
        RaycastHit hit;
        // int layerMask = 1 << 9;
        // int layerMask = LayerMask.GetMask("Interaction");
        int layerMask = Physics.DefaultRaycastLayers;
        if (Physics.Raycast(this.lookDirection.position, this.lookDirection.forward, out hit, this.interactDistance, layerMask))
        {
            this.currentInteractable = hit.collider.transform.gameObject.GetComponent<BaseInteractable>();
            // this.interactText.text = this.currentInteractable.GetDescription();
            // this.interactText.enabled = true;
            if (this.currentInteractable != null)
            {
                this.UpdateText(this.currentInteractable.GetDescription(), true);
                return;
            }
        }
        // else
        {
            this.currentInteractable = null;
            // this.interactText.enabled = false;
            this.UpdateText("", false);
        }
    }
}
