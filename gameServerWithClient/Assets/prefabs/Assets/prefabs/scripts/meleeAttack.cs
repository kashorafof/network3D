using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeAttack : MonoBehaviour
{
    float enemyHP;
    Animator animator;

    [SerializeField] Transform righthand , leftHand, rightLeg, leftLeg ;
    [SerializeField] LayerMask enemyLayer;

    Transform[] handAttackPoints , legAttackPoints , allAttackPoints;
    
    [SerializeField] float legDmg , handDmg, radius;
    private void Awake()
    {
        handAttackPoints =  new Transform[2] { righthand, leftHand};
        legAttackPoints = new Transform[2] {rightLeg, leftLeg };
        animator = GetComponent<Animator>();
        timer = 0f;
    }

    public float timer, timeColdown = 0.5f;
    [SerializeField] float idleTime;

    // Update is called once per frame
    void Update()
    {
        if (timer > timeColdown)
        {
            if (Input.GetMouseButtonDown(0))
            {
                animator.SetTrigger("handAttack");
                attackBox();
                timer = 0;
            }
            else if (Input.GetMouseButtonDown(1))
            {
                animator.SetTrigger("legAttack");
                timer = 0;

                attackKick();
            }
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > idleTime)
            {

                animator.SetTrigger("idle");
            }
        }

    }

    private void attackBox()
    {
        animator.SetFloat("hand Box", animator.GetFloat("hand Box") + 1);
        if (animator.GetFloat("hand Box") > 2)
        {
            animator.SetFloat("hand Box", 0);
        }
        foreach (var point in handAttackPoints)
        {
            armHits(point);

        }

    }

    private void armHits(Transform atkPoint)
    {

        Collider[] enemies = Physics.OverlapSphere(atkPoint.position, radius, enemyLayer);
        int i = 0;
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<HP>().getDamage(handDmg);
            Debug.Log(i);
        }
    }


    private void attackKick()
    {
        animator.SetFloat("kicks", animator.GetFloat("kicks") + 1);
        if (animator.GetFloat("kicks") > 2)
        {
            animator.SetFloat("kicks", 0);
        }
        foreach (var point in legAttackPoints)
        {
            legHit(point);

        }
    }

    private void legHit(Transform atkPoint)
    {

        Collider[] enemies = Physics.OverlapSphere(atkPoint.position, radius, enemyLayer);
        int i = 0;
        foreach (var enemy in enemies)
        {
            enemy.GetComponent<HP>().getDamage(legDmg);
            Debug.Log(i);
        }
    }

    private void OnDrawGizmosSelected()
    {

        allAttackPoints = new Transform[4] { righthand, leftHand, rightLeg, leftLeg };


        foreach (var point in allAttackPoints)
        {

            Gizmos.DrawWireSphere(point.position, radius);
        }
    }
}
