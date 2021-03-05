using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuat : MonoBehaviour
{
    private Vector3 dir = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        // Quaternion q = new Quaternion(0, 0, 0, 1);
        // q *= Quaternion.AngleAxis(90, Vector3.forward);
        // q *= Quaternion.AngleAxis(45, Vector3.up);
        // q *= Quaternion.AngleAxis(45, this.transform.up);
        // this.transform.rotation *= q;

        // Quaternion q1 = new Quaternion(1f/2f, 0, 0, Mathf.Sqrt(3f) / 2f).normalized;
        // Quaternion q2 = new Quaternion(1, 0, 0, 1).normalized;

        // float angle = Quaternion.Angle(q1, q2);
        // Debug.Log("Angle : " + angle);

        // Vector3 point = new Vector3(3,6,1);
        // Vector3 point2 = new Vector3(5,8,2);
        // // Debug.DrawRay(this.transform.position, point*5, Color.red, 10);
        // // Debug.DrawRay(this.transform.position, point2*5, Color.blue, 10);
        // Vector3 cross = Vector3.Cross(point, point2);
        // float angle = Vector3.SignedAngle(point, point2, cross);
        // Quaternion q1 = Quaternion.AngleAxis(angle, cross);
        // Quaternion q2 = Quaternion.FromToRotation(point, point2);
        // Debug.Log("corss : " + cross);
        // Debug.Log("angel : " + angle);
        // Debug.Log("Q1 : x: " + q1.x + ", y: " + q1.y + ", z: " + q1.z + ", w: " + q1.w);
        // Debug.Log("Q2 : x: " + q2.x + ", y: " + q2.y + ", z: " + q2.z + ", w: " + q2.w);

        // float ang = 30f;
        // Vector3 axis = new Vector3(2,4,5);
        // Quaternion q3 = Quaternion.AngleAxis(ang, axis);
        // float s = Mathf.Sin((ang/2f)*Mathf.Deg2Rad);
        // float w = Mathf.Cos((ang/2f)*Mathf.Deg2Rad);
        // Vector3 xyz = axis.normalized * s;
        // Debug.Log(s);
        // // xyz = xyz.normalized;
        // Quaternion q4 = new Quaternion(xyz.x, xyz.y, xyz.z, w);
        // Debug.Log("Q3 : " + q3);
        // Debug.Log("Q4 : " + q4);

        // Vector3 v1 = new Vector3(1,3,7);
        // Vector3 v2 = new Vector3(8,4,8);
        // Debug.DrawRay(this.transform.position, v1.normalized * 10, Color.red, 10);
        // Debug.DrawRay(this.transform.position, v2.normalized * 10, Color.blue, 10);
        // Quaternion q3 = Quaternion.FromToRotation(v1.normalized, v2.normalized);
        // // Debug.DrawRay(this.transform.position, q3*v1.normalized * 15, Color.green, 10);
        // // Vector3 vcn = Vector3.Cross(v1.normalized, v2.normalized);
        // // Debug.Log(vcn.magnitude);
        // // float w = 1f + Vector3.Dot(v1.normalized, v2.normalized);
        // // Debug.Log(w);
        // // Quaternion q4 = new Quaternion(vcn.x, vcn.z, vcn.z, w).normalized;
        // // Vector3 half = (v1.normalized + v2.normalized).normalized;
        // Vector3 cr = Vector3.Cross(v1.normalized, v2.normalized);
        // Quaternion q4 = new Quaternion(cr.x, cr.y, cr.z, Vector3.Dot(v1.normalized, v2.normalized));
        // q4.w += Mathf.Sqrt(q4.x*q4.x + q4.y*q4.y + q4.z*q4.z + q4.w*q4.w);
        // q4 = q4.normalized;
        // Debug.DrawRay(this.transform.position, q4*v1.normalized * 7, Color.green, 10);
        // Debug.Log("Q3 : " + q3);
        // Debug.Log("Q3 : " + q3.normalized);
        // Debug.Log("Q4 : " + q4);
        // Debug.Log("Q4 : " + q4.normalized);

        // Vector3 v1 = new Vector3(2,5,7);
        // Vector3 v2 = new Vector3(2,5,8);
        // Quaternion q3 = Quaternion.FromToRotation(v1, v1);
        // Debug.Log(q3.w);
        // Quaternion q3 = new Quaternion(2,44,2,1).normalized;
        // Debug.Log("Before : " + this.transform.up);
        // this.transform.rotation *= q3;
        // Debug.Log("After : " + this.transform.up);

        // dir = new Vector3(7,3,12).normalized;
        // dir = new Vector3(0,0,1).normalized;
        // Debug.DrawRay(this.transform.position, dir * 10, Color.red, 20);

        // Debug.Log("point : " + this.transform.TransformPoint(point));
        // Debug.Log("vec? : " + this.transform.TransformVector(point));
        // Debug.Log("nils? : " + (this.transform.position + this.transform.rotation * point));

        // Quaternion q1 = new Quaternion(0,1,0,1).normalized;
        // Quaternion q2 = new Quaternion(0,0,1,1).normalized;
        // Debug.Log("Q1 : " + q1);
        // Debug.Log("Q2 : " + q2);
        // Quaternion q3 = q1 * q2;
        // Quaternion q4 = q2 * q1;
        // Debug.Log("Q3 : " + q3);
        // Debug.Log("Q4 : " + q4);

        // this.transform.rotation = q2;
        // Debug.Log("tra : " + this.transform.rotation);

        // Debug.Log("i : " + this.transform.right);
        // Quaternion q1 = new Quaternion(0, 1, 0, 1).normalized;
        // this.transform.rotation *= q1;
        // Debug.Log("i after : " + this.transform.right);

        // Debug.Log("Right : " + this.transform.right);
        // Debug.Log("Right calc : " + Vector3.Cross(this.transform.up, this.transform.forward));

        // Quaternion q = Quaternion.AngleAxis(45, new Vector3(0,0,0));
        // Debug.Log("Q : " + q);

        // Debug.Log("Hmm : " + Vector3.SignedAngle(new Vector3(1,2,3), this.transform.up, Vector3.zero));

        // Debug.Log("Quat : " + this.transform.rotation);
        // Quaternion qbase = Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.forward);
        dir = new Vector3(-78, 175, -34).normalized;
        Debug.DrawRay(this.transform.position, dir * 15, Color.yellow, 20);
        // Debug.DrawRay(this.transform.position, qbase * dir * 15, Color.cyan, 20);
        // Debug.DrawRay(this.transform.position, qbase * Vector3.forward * 15, Color.blue, 20);
        // Quaternion q00 = Quaternion.FromToRotation(Vector3.forward, dir) * qbase;
        // Debug.Log(q00.ToString("F8"));
        Quaternion q0 = Quaternion.FromToRotation(Vector3.forward, dir);
        Debug.Log(q0.eulerAngles.ToString("F8"));
        Quaternion q1 = Quaternion.AngleAxis(q0.eulerAngles.y, Vector3.up);
        Quaternion q2 = Quaternion.AngleAxis(q0.eulerAngles.x, q1 * Vector3.right);
        Debug.Log(q2.ToString("F8"));
        Debug.DrawRay(this.transform.position, (q2*q1) * Vector3.forward * 10, Color.magenta, 20);
        // Debug.Log((Quaternion.Inverse(qbase)*q0).eulerAngles.ToString("F8"));
        // Quaternion q1 = Quaternion.AngleAxis(q0.eulerAngles.z, Vector3.up);
        // this.transform.rotation = q1;
        // Debug.Log((qbase*q0).eulerAngles.ToString("F8"));
        // Quaternion q = Quaternion.Euler(0, 90, 315);
        // Quaternion q = Quaternion.Euler(-45, 90, -45);
        // Debug.DrawRay(this.transform.position, q * Vector3.forward * 10, Color.magenta, 20);
        // Debug.Log(q.eulerAngles.ToString("F8"));
        // Debug.DrawRay(this.transform.position, Vector3.ProjectOnPlane(this.transform.forward, dir).normalized * 10, Color.cyan);
        // Debug.DrawRay(this.transform.position, Vector3.ProjectOnPlane(this.transform.right, dir).normalized * 10, Color.magenta);
        // Quaternion qbase = Quaternion.AngleAxis(90, Vector3.up) * Quaternion.AngleAxis(90, Vector3.forward);
        // Debug.DrawRay(this.transform.position, qbase * Vector3.up * 5, Color.green, 20);
        // Debug.DrawRay(this.transform.position, qbase * Vector3.forward * 5, Color.blue, 20);
        // Debug.DrawRay(this.transform.position, qbase * Vector3.right * 5, Color.red, 20);

        // Debug.DrawRay(this.transform.position, qbase * dir * 5, Color.magenta, 20);

        // this.transform.up = dir;
        // Quaternion q = Quaternion.FromToRotation(qbase * Vector3.forward, qbase * dir);
        // Vector3 v = q.eulerAngles;
        // Debug.Log(v.ToString("F8"));
        // Debug.DrawRay(this.transform.position, q * qbase * Vector3.forward * 4, Color.cyan, 20);
        // Vector3 v2 = (qbase*q).eulerAngles;
        // Debug.Log(v2.ToString("F8"));
        // Debug.DrawRay(this.transform.position, (qbase*q) * Vector3.forward * 4, Color.black, 20);
        // Quaternion q2 = Quaternion.AngleAxis(v.y, Vector3.up);
        // Quaternion q3 = Quaternion.AngleAxis(v.x, Vector3.right);
        // Quaternion q4 = q2 * q3;
        // Debug.DrawRay(this.transform.position, q2 * Vector3.forward * 10, Color.blue, 20);
        // Debug.DrawRay(this.transform.position, q3 * Vector3.forward * 8, Color.red, 20);
        // Debug.DrawRay(this.transform.position, q4 * Vector3.forward * 7, Color.magenta, 20);
        // this.transform.rotation = q2 * this.transform.rotation;
        // Debug.DrawRay(this.transform.position, q3 * this.transform.forward * 5, Color.green, 20);
    }

    // Update is called once per frame
    void Update()
    {
        // // Debug.Log("Quat : " + this.transform.rotation);
        // dir = new Vector3(1, 1, 1).normalized;
        // Debug.DrawRay(this.transform.position, dir * 10, Color.yellow);
        // // Debug.DrawRay(this.transform.position, dir * 10, Color.yellow);
        // // Debug.DrawRay(this.transform.position, Vector3.ProjectOnPlane(this.transform.forward, dir).normalized * 10, Color.cyan);
        // // Debug.DrawRay(this.transform.position, Vector3.ProjectOnPlane(this.transform.right, dir).normalized * 10, Color.magenta);

        // // this.transform.up = dir;
        // Quaternion q = Quaternion.FromToRotation(this.transform.forward, dir);
        // Vector3 v = q.eulerAngles;
        // Debug.Log(v.ToString("F8"));
        // Quaternion q2 = Quaternion.AngleAxis(v.y, Vector3.up);
        // Quaternion q3 = Quaternion.AngleAxis(v.x, Vector3.right);
        // Quaternion q4 = q2 * q3;
        // Debug.DrawRay(this.transform.position, q2 * Vector3.forward * 5, Color.blue);
        // Debug.DrawRay(this.transform.position, q3 * Vector3.forward * 6, Color.red);
        // Debug.DrawRay(this.transform.position, q2 * this.transform.forward * 5, Color.green);
        // Debug.DrawRay(this.transform.position, q4 * Vector3.forward * 5, Color.magenta);
        // this.transform.rotation = q3 * this.transform.rotation;
        // Debug.Log(q.ToString("F8"));
        // Debug.Log(q.eulerAngles.ToString("F8"));
        // Quaternion q2 = Quaternion.Inverse(this.transform.rotation) * q;
        // Vector3 v1 = q2.eulerAngles;
        // // Debug.Log(q2.ToString("F8"));
        // // Debug.Log(v1.ToString("F8"));
        // Quaternion q3 = Quaternion.AngleAxis(v1.y, this.transform.up) * Quaternion.AngleAxis(v1.x, this.transform.right) * Quaternion.AngleAxis(v1.z, this.transform.forward);
        // Quaternion q = Quaternion.FromToRotation(Vector3.up, Vector3.right);
        // Quaternion q2 = Quaternion.FromToRotation(Vector3.right, Vector3.forward);
        // q2 = q * q2;
        // this.transform.rotation *= q;
        // Debug.DrawRay(this.transform.position, q2 * this.transform.up * 5, Color.green);
        // Debug.DrawRay(this.transform.position, q2 * this.transform.forward * 5, Color.blue);
        // Debug.DrawRay(this.transform.position, q2 * this.transform.right * 5, Color.red);
        // this.transform.rotation = q2;
        // this.transform.rotation = Quaternion.LookRotation(Vector3.zero, Vector3.forward);
    }
}
