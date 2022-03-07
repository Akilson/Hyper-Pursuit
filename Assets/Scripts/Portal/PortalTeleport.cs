using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    public Transform player;
    public Transform receiver;

    private bool overlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (overlapping) {
            Vector3 portalToPlayer = player.position - transform.position;
            float dotProduct = Vector3.Dot(transform.up, portalToPlayer);

            //if (dotProduct < 0f) {
                float rotation = receiver.eulerAngles.y - transform.eulerAngles.y;
                //float rotation = Quaternion.Angle(transform.rotation, receiver.rotation);
                //rotation += 180f;
                player.Rotate(Vector3.up, rotation);

                Vector3 offset = Quaternion.Euler(0f, rotation, 0f) * portalToPlayer;
                player.position = receiver.position + offset;

                overlapping = false;
            //}
        }
    }

    void OnTriggerEnter (Collider other) {
        if (other.tag == "Player") {
            overlapping = true;
        }
    }

    void OnTriggerExit (Collider other) {
        if (other.tag == "Player") {
            overlapping = false;
        }
    }
}