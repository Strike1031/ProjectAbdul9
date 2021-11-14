using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement : MonoBehaviour
{
    public AttackUniversal[] attackPoints;

    protected virtual void Awake()
    {
        enabled = false;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }

    public void SetAttackLayer(LayerMask mask)
    {
        for (int i = 0; i < attackPoints.Length; i++)
        {
            attackPoints[i].collisionLayer = mask;
        }
    }
}
