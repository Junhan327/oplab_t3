using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float offset = 2f;
    private void Update() {
        transform.position = new Vector3(target.position.x + offset, target.position.y,-10f);
    }
}
