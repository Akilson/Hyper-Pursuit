using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCamera : MonoBehaviour
{
    public Transform player;
    public Transform portal1;
    public Transform portal2;

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = player.position - portal1.position;
        transform.position = portal2.position + offset;
        float angleOffset = Quaternion.Angle(portal2.rotation, portal1.rotation);
        Quaternion rotation = Quaternion.AngleAxis(angleOffset, Vector3.up);
        Vector3 direction = rotation * player.forward;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }   
}
