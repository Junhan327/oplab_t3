using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void RestartGame()
    {
        Time.timeScale = 1;  // 恢复时间流动
        SceneManager.LoadScene("Scene1");  // 重新加载场景
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
