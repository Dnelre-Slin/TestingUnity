using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuat2 : MonoBehaviour
{
    [SerializeField]
    private Transform quat1 = null;
    [SerializeField]
    private Transform quat2 = null;
    [SerializeField]
    private Vector3 vec1 = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 dirRed = quat1.rotation * vec1;
        // Vector3 dirBlue = (quat2.rotation * quat1.rotation) * vec1;
        // Vector3 dirYellow = (Quaternion.Inverse(quat2.rotation) * (quat2.rotation * quat1.rotation)) * vec1;

        // Debug.DrawRay(this.transform.position, vec1.normalized * 3f, Color.white);
        // Debug.DrawRay(this.transform.position, dirRed.normalized * 3f, Color.red);
        // Debug.DrawRay(this.transform.position, dirBlue.normalized * 3f, Color.blue);
        // Debug.DrawRay(this.transform.position, dirYellow.normalized * 2f, Color.yellow);

        Vector3 dirRed = quat1.rotation * vec1;
        Vector3 dirBlue = quat2.rotation * vec1;

        Quaternion quat3 = quat2.rotation * Quaternion.Inverse(quat1.rotation);

        // Vector3 dirGreen = quat3 * vec1;
        Vector3 dirYellow = (quat3 * quat1.rotation) * vec1;

        Debug.DrawRay(this.transform.position, vec1.normalized * 3f, Color.white);
        Debug.DrawRay(this.transform.position, dirRed.normalized * 3f, Color.red);
        // Debug.DrawRay(this.transform.position, dirGreen.normalized * 3f, Color.green);
        Debug.DrawRay(this.transform.position, dirBlue.normalized * 3f, Color.blue);
        Debug.DrawRay(this.transform.position, dirYellow.normalized * 2f, Color.yellow);
    }
}
