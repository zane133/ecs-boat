using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseOrbitImproved : MonoBehaviour {

    public Transform target;
    public float distance = 10f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;

    public float yMinLimit = -60f;
    public float yMaxLimit = 60f;

    public float distanceMin = 2f;
    public float distanceMax = 15f;

    //private Rigidbody rigidbody;

    float x = 0.0f;
    float y = 0.0f;

    // Use this for initialization
    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //rigidbody = GetComponent<Rigidbody>();

        //// Make the rigid body not change rotation
        //if (rigidbody != null)
        //{
        //    rigidbody.freezeRotation = true;
        //}
    }

    void LateUpdate()
    {
        if (target)
        {
            if (Input.GetMouseButton(0))
            {
                x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
                y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            }

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            // 围绕着这根轴进行偏移
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            // 先旋转再平移 (rotation * negDistance 返回的是旋转之后的位置)
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;

            Debug.DrawLine(transform.position, target.position, Color.red);

        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
    
}
