using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    [SerializeField] int maxHP;
    float currentHP;
    Animator animator;
    public bool die;
    // Start is called before the first frame update
    void Start()
    {
        currentHP = maxHP;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void getDamage(float dmg)
    {
        currentHP -= dmg;
        if (currentHP < 0)
            Die();
    }

    void Die()
    {
        animator.SetBool("defeat", true);
        Destroy(gameObject, 2f);
    }
}
