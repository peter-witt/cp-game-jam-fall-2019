using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        var newPos = new Vector3(player.position.x, player.position.y + 2, -10);
        transform.position = Vector3.Lerp(transform.position, newPos, 5 * Time.deltaTime);
    }
}
