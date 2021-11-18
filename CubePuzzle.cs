using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzle : MonoBehaviour
{
    [SerializeField] public GameObject cubeObject;
    [SerializeField] GameObject cubeParent;
    
    private Quaternion startRot;
    private Quaternion cubeRot;

    private float x;
    private float y;
    private float z;
    private float startx;
    private float starty;
    private float startz;
    private bool rotating = false;
    private bool calculated = false;

    

    // Start is called before the first frame update
    void Start()
    {
        startRot = cubeObject.transform.rotation;
        cubeRot = startRot;
        x = startRot.eulerAngles.x;
        y = startRot.eulerAngles.y;
        z = startRot.eulerAngles.z;
        startx = startRot.eulerAngles.x;
        starty = startRot.eulerAngles.y;
        startz = startRot.eulerAngles.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(rotating)
        {
            rotateCube();
        }
    }

    public void raycast(RaycastHit objectName)
    {
        if(rotating)
        {
            return;
        }

        if (objectName.transform.name == "UpButton")
        {
            if (Mathf.Abs(y) == startRot.eulerAngles.y || Mathf.Abs(y - startRot.eulerAngles.y) % 360 < 0.001f)
            {
                x += 90;
            }
            else if(Mathf.Abs(y - startRot.eulerAngles.y) % 180 < 0.001f)
            {
                x -= 90;
            }
            else if (Mathf.Abs(y - startRot.eulerAngles.y) % 270 < 0.001f)
            {
                if (y < startRot.eulerAngles.y)
                {
                    z += 90;
                }
                else
                {
                    z -= 90;
                }
            }
            else if (Mathf.Abs(y - startRot.eulerAngles.y) % 90 < 0.001f)
            {
                if (y < startRot.eulerAngles.y)
                {
                    z -= 90;
                }
                else
                {
                    z += 90;
                }
            }
            rotating = true;
        }
        else if (objectName.transform.name == "DownButton")
        {
            if (Mathf.Abs(y) == startRot.eulerAngles.y || Mathf.Abs(y - startRot.eulerAngles.y) % 360 < 0.001f)
            {
                x -= 90;
            }
            else if (Mathf.Abs(y - startRot.eulerAngles.y) % 180 < 0.001f)
            {
                x += 90;
            }
            else if (Mathf.Abs(y - startRot.eulerAngles.y) % 270 < 0.001f)
            {
                if (y < startRot.eulerAngles.y)
                {
                    z -= 90;
                }
                else
                {
                    z += 90;
                }
            }
            else if (Mathf.Abs(y - startRot.eulerAngles.y) % 90 < 0.001f)
            {
                if (y < startRot.eulerAngles.y)
                {
                    z += 90;
                }
                else
                {
                    z -= 90;
                }
            }
            rotating = true;
        }
        else if (objectName.transform.name == "LeftButton")
        {
            y += 90;
            rotating = true;
        }
        else if (objectName.transform.name == "RightButton")
        {
            y -= 90;
            rotating = true;
        }
        else if (objectName.transform.name == "ResetButton")
        {
            x = startx;
            y = starty;
            z = startz;
            rotating = true;
        }
    }

    void rotateCube()
    {
        if(!calculated)
        {
            calculateRot();
        }
        cubeObject.transform.rotation = Quaternion.Slerp(cubeObject.transform.rotation, cubeRot, Time.deltaTime * 2.5f);
        if (Quaternion.Angle(cubeObject.transform.rotation, cubeRot) < 0.01f)
        {
            rotating = false;
            calculated = false;
        }
    }

    void calculateRot()
    {
        cubeRot = Quaternion.Euler(x, y, z);
        calculated = true;
    }

    public string panelRay(RaycastHit panelObj)
    {
        return "";
    }
}

