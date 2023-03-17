using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    Camera mCam;

    void Start()
    {
        mCam = Camera.main;    
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Vector3 target = mCam.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0.0f;
            PlayerMovement script = GetComponent<PlayerMovement>();
            script.MoveTo(target);
        }
    }
}
