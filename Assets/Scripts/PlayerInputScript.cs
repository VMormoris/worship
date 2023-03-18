using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    Camera mCam;

    void Start()
    {
        GameContext.Instance.PlayerInput = this;
        mCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 coords = Input.mousePosition;
        if (Input.GetMouseButtonUp(1))
        {
            Vector3 target = mCam.ScreenToWorldPoint(coords);
            target.z = 0.0f;
            PlayerMovement script = GetComponent<PlayerMovement>();
            script.MoveTo(target);
        }
        else if(Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = mCam.ScreenPointToRay(coords);
            if(Physics.Raycast(ray, out hit))
            {
                InterlocutorScript scirpt = hit.transform.GetComponent<InterlocutorScript>();
                scirpt.StartConversation();
            }
        }
    }
}
