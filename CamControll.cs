using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamControll : MonoBehaviour {

    public float x;
    public float y;
    public float SpeedRotation;
    public Transform Player;

    void Update()
    {
        x += Input.GetAxis("Mouse X") * SpeedRotation * Time.deltaTime;
        y += Input.GetAxis("Mouse Y") * SpeedRotation * Time.deltaTime;
        y = Mathf.Clamp(y, -50, 50);
        Quaternion CameTarget = Quaternion.Euler(-y, -13.5f, 0);
        Quaternion CharterTarget = Quaternion.Euler(0, x, 0);
        transform.localRotation = CameTarget;
        Player.localRotation = CharterTarget;
    }
}
