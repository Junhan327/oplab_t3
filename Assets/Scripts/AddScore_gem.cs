using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddScore_gem : MonoBehaviour
{
     public static int gem_score=0;
     public Text gem;
     private void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
        gem_score ++;
        gem.text = "gem:" + gem_score.ToString();
    }
}
