using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;
    public PlayerTransform playerTransform;
    public float minX, minY, maxX, maxY;

    void Start()
    {
        Vector3 randomPosition = new Vector3(53.6f,104.6f,-2);
        GameObject pl= PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
        playerTransform.player = pl;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
