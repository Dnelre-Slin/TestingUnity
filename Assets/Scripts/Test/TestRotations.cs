using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRotations : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform barrel;
    [SerializeField]
    private float minAngle = -170f;
    [SerializeField]
    private float maxAngle = 170f;
    [SerializeField]
    private float deltaAngle = 0.1f;
    [SerializeField]
    private float minAngleBarrel = -20f;
    [SerializeField]
    private float maxAngleBarrel = 90f;
    [SerializeField]
    private float deltaAngleBarrel = 0.1f;
    [SerializeField]
    private bool idleOnOutOfBounds = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.parent.position - 0.5f * this.transform.parent.up, this.transform.parent.forward * 3f, Color.blue);
        Debug.DrawRay(this.transform.position, this.transform.forward * 3f, Color.magenta);

        if (this.target != null)
        {
            Vector3 vecToTarget = target.position - this.transform.position;
            Quaternion qautToTarget = Quaternion.FromToRotation(Vector3.forward, Quaternion.Inverse(this.barrel.transform.rotation) * vecToTarget);
            Quaternion quatBase = Quaternion.AngleAxis(qautToTarget.eulerAngles.y, this.transform.parent.up);
            Quaternion quatBarrel = Quaternion.AngleAxis(qautToTarget.eulerAngles.x, quatBase * this.transform.right);

            this.transform.rotation = quatBase * this.transform.rotation;
            this.barrel.transform.rotation = quatBarrel * this.barrel.transform.rotation;

            Debug.DrawRay(this.transform.position, this.barrel.transform.forward * 10f, Color.red);
        }

        // if (this.target != null)
        // {
        //     Vector3 vecToTarget = target.position - this.transform.position;
        //     Quaternion qautToTarget = Quaternion.FromToRotation(this.transform.parent.forward, vecToTarget);
        //     float curAngle = this.transform.rotation.eulerAngles.y;
        //     float newAngle = qautToTarget.eulerAngles.y;
        //     float curAngleBarrel = this.barrel.transform.rotation.eulerAngles.x;
        //     float newAngleBarrel = qautToTarget.eulerAngles.x;
        //     if (curAngle > 180f)
        //     {
        //         curAngle -= 360f;
        //     }
        //     if (newAngle > 180f)
        //     {
        //         newAngle -= 360f;
        //     }
        //     if (newAngle < this.minAngle || newAngle > this.maxAngle)
        //     {
        //         if (idleOnOutOfBounds)
        //         {
        //             newAngle = 0;
        //         }
        //         else
        //         {
        //             newAngle = Mathf.Clamp(newAngle, this.minAngle, this.maxAngle);
        //         }
        //     }
        //     if (curAngleBarrel > 180f)
        //     {
        //         curAngleBarrel -= 360f;
        //     }
        //     if (newAngleBarrel > 180f)
        //     {
        //         newAngleBarrel -= 360f;
        //     }
        //     if (newAngleBarrel < this.minAngleBarrel || newAngleBarrel > this.maxAngleBarrel)
        //     {
        //         if (idleOnOutOfBounds)
        //         {
        //             newAngleBarrel = 0;
        //         }
        //         else
        //         {
        //             newAngleBarrel = Mathf.Clamp(newAngleBarrel, this.minAngleBarrel, this.maxAngleBarrel);
        //         }
        //     }
        //     float dAngle = this.deltaAngle * Time.deltaTime;
        //     float diffAngle = Mathf.Clamp(newAngle - curAngle, -dAngle, dAngle);

        //     float dAngleBarrel = this.deltaAngleBarrel * Time.deltaTime;
        //     float diffAngleBarrel = Mathf.Clamp(newAngleBarrel - curAngleBarrel, -dAngleBarrel, dAngleBarrel);

        //     this.transform.rotation = Quaternion.AngleAxis(diffAngle, this.transform.parent.up) * this.transform.rotation;
        //     this.barrel.transform.rotation = Quaternion.AngleAxis(diffAngleBarrel, this.transform.right) * this.barrel.transform.rotation;

        //     Debug.DrawRay(this.barrel.transform.position, this.barrel.transform.forward * 10f, Color.red);
        // }
    }
}
