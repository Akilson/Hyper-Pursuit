using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    private int inst = 0; 
    private Vector3 spawnPoint = new Vector3(20, 13, 30);
    void Awake()
    {
        PV =GetComponent<PhotonView>();
    }
    
    void Start()
    {
        if(PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        if (PV.Controller.IsMasterClient)
        {
            Debug.Log("instantiated White player controller");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerController"), spawnPoint, Quaternion.identity);
            inst++;
        }
        else
        {
            Debug.Log("instantiated Black player controller");
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerControllerB"), spawnPoint + new Vector3(0, 100, 0), Quaternion.identity);
        }
    }
}
