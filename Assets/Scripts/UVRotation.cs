using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UVRotation : MonoBehaviour
{
    // Attach to an object that has a Renderer component,
    // and use material with the shader below.
    public float rotateSpeed = 30f;
  
    public GameObject myobj;

    bool rotate = false;    

    public void Update()
    {

        if (rotate == true)
        {
            // Construct a rotation matrix and set it for the shader
            Quaternion rot = Quaternion.Euler(0, 0, rotateSpeed * Time.time);
            Matrix4x4 m = Matrix4x4.TRS(Vector3.zero, rot, Vector3.one);
            myobj.GetComponent<Renderer>().material.SetMatrix("_TextureRotation", m);
        }

        
    }

    public void ButtonPress()
    {
        if (rotate == false)
        {
            rotate = true;
        }else
        {
            rotate = false;
        }
    }
}
