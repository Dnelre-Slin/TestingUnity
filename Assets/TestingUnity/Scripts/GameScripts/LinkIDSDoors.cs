using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkIDSDoors : MonoBehaviour
{
    [SerializeField]
    private GameObject idsGameObject = null;
    // Start is called before the first frame update
    void Start()
    {
        if (idsGameObject != null)
        {
            float precision = 0.1f;
            IDSExternalFancy idsExternal = idsGameObject.GetComponentInChildren<IDSExternalFancy>();
            if (idsExternal != null)
            {
                GameObject interiorObject = idsExternal.idsInternal.gameObject;

                TwoStageAnimatorLinkedReactable[] exteriorDoors = idsGameObject.GetComponentsInChildren<TwoStageAnimatorLinkedReactable>();
                TwoStageAnimatorLinkedReactable[] interiorDoors = interiorObject.GetComponentsInChildren<TwoStageAnimatorLinkedReactable>();

                foreach (var exteriorDoor in exteriorDoors)
                {
                    Vector3 extPos = Quaternion.Inverse(idsGameObject.transform.rotation) *  (exteriorDoor.transform.position - idsGameObject.transform.position);
                    foreach (var interiorDoor in interiorDoors)
                    {
                        Vector3 intPos = interiorDoor.transform.position - interiorObject.transform.position;
                        if (Vector3.Distance(extPos, intPos) < precision)
                        {
                            exteriorDoor.SetPartner(interiorDoor);
                            interiorDoor.SetPartner(exteriorDoor);
                            break;
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("idsGameObject not sat on LinkIDSDoors. This will result in unwanted behaviour.");
        }
    }
}
