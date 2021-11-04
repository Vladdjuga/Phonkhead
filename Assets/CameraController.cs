using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Range(0, 1)]
    public float smoothTime;

    public Transform player_transform;
    private void Start()
    {
    }

    void Update()
    {
        Vector3 pos = GetComponent<Transform>().position;
        pos.x = Mathf.Lerp(pos.x, player_transform.position.x,smoothTime);
        pos.y = player_transform.position.y;
        pos.z = transform.position.z;
        GetComponent<Transform>().position = pos;
    }
}
