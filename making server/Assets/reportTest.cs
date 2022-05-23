using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reportTest : MonoBehaviour
{
    float timer;
    private void OnEnable()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;  
    }

    private void OnDisable()
    {
        Debug.Log(timer);
    }
}
