using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float offsetY;

    private void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + offsetY, transform.position.z);
    }
}
