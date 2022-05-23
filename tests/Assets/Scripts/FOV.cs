using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
    public float viewRadius;
    [Range (0,360)]
    public float viewAngle;

    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public GameObject parent;

    public List<GameObject> visibleTarget= new List<GameObject>();

    private void Start()
    {
    }

    private void FixedUpdate()
    {
        FindVisibleTargets();
    }

    void FindVisibleTargets()
    {
        visibleTarget.Clear();

        Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
        
        for (int i = 0; i < targetsInViewRadius.Length; i++)
        {
            Transform target = targetsInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToTarget);
            if (angle*2 < viewAngle){
                float distance = Vector3.Distance(transform.position,target.position);
                if (!Physics.Raycast(transform.position, dirToTarget, distance, obstacleMask) && target != transform.parent)
                {
                    if (distance-0.5f <= viewRadius)
                    {
                        visibleTarget.Add(target.gameObject);
                    }
                }
            }
        }
        transform.GetComponentInParent<PlayerManagerAdd>().nearPlayers = visibleTarget;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
}
