using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PortalCamera : MonoBehaviour
{
    public string playerTag;
    private GameObject player;
    public Transform portal1;
    public Transform portal2;
    private bool white;
    
    private Transform playerCam;

    void Start(){
        player = null;
        playerCam = null;
        white = PhotonNetwork.IsMasterClient;
    }

    // Update is called once per frame
    void Update()
    {
        if (white ^ playerTag == "playerWhite")
        {
            Debug.Log("Desactivated");
            //transform.parent.gameObject.SetActive(false);
            return;
        }
        if (playerCam is null)
        {
            player = GameObject.FindGameObjectWithTag(playerTag);
            if (player is not null)
                playerCam = player.transform.GetChild(0).GetChild(0);
        }
        else
        {
            Debug.Log(playerCam);
            Debug.Log(white);
            Debug.Log(playerTag);
            transform.position = playerCam.position;
            transform.rotation = playerCam.rotation;

        //float angle = -Quaternion.Angle(portal1.rotation, portal2.rotation);
            float angle = portal2.transform.eulerAngles.y - portal1.transform.eulerAngles.y;

            transform.RotateAround(portal1.position, Vector3.up, angle);
            Vector3 offset = portal2.position - portal1.position;

            transform.position += offset; 
        }   
    }
}
