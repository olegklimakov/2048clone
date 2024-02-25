using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPosition : MonoBehaviour
{

    public float speed = 10f;
    public Vector3 targetPosition;

    private Boolean _needToMove = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (!_needToMove)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        
        if (transform.position == targetPosition)
        {
            _needToMove = false;
        }
    }

    void SetTarget(Vector3 target)
    {
        _needToMove = true;
        targetPosition = target;
    }
}
