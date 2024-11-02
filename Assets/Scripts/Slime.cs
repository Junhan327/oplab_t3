using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    public Vector3 pointA; 
    public Vector3 pointB; 
    public float speed = 2f; // 移动速度
    private Vector3 targetPosition;

    private void Start()
    {
        targetPosition = pointA;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
 
            if(targetPosition == pointA)
            {
                targetPosition = pointB;
                transform.localRotation = Quaternion.Euler(0,180,0);
            }
            else
            {
                targetPosition = pointA;
                transform.localRotation = Quaternion.Euler(0,0,0);
            }
        }
    }
}
