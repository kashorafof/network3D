using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour
{
    Animator animator;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetBool("attack", true);
            timer = 0;
        }
        else
        {
            animator.SetBool("attack", false);
            timer += Time.deltaTime;
        }

        if(timer >= 5)
        {
            animator.SetBool("idle", true);

        }

    }
}
