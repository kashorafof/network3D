using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cullingMask : MonoBehaviour
{
    public LayerMask layers;
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.cullingMask = layers;


    }
}
