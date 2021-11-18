using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSlider : MonoBehaviour
{
    [SerializeField] Vector3 endPos;
    [SerializeField] float speed = 1.0f;

    private bool moving = false;
    private bool opening = true;
    private bool closing = false;
    private Vector3 startPos;

    public bool Moving
    {
        get { return moving; }
        set { moving = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if(moving)
        {
            if(opening)
            {
                MoveObject(endPos);
            }
            if(closing)
            {
                MoveObject(startPos);
            }
        }
    }

    void MoveObject(Vector3 goalPos)
    {
        //Debug.Log("In moving function");
        float dist = Vector3.Distance(transform.position, goalPos);
        if(dist > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, goalPos, speed * Time.deltaTime);
        }
        else
        {
            moving = false;
            ChangeState();
        }
    }

    void ChangeState()
    {
        
        if(opening)
        {
            opening = false;
            closing = true;
        }
        else if(closing)
        {
            opening = true;
            closing = false;
        }
    }
}
