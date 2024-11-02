using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore : MonoBehaviour
{
    public static int cherry_score=0;
   
    public Text cherry;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        cherry_score ++;
        cherry.text = "cherry:" + cherry_score.ToString();
    }
}
