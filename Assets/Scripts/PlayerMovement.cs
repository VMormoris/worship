using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Velocity = 10.0f;
    public bool CanMove = true;

    Vector3 mTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        mTarget = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 dir = (mTarget - transform.position).normalized;
        Vector2 pos = rb.position;
        rb.position += Velocity * Time.fixedDeltaTime * dir;
        if (rb.IsTouching(GetComponent<BoxCollider2D>()))
        {
            rb.position = pos;
            mTarget = rb.position;
        }

        const float desired = 0.2f * 0.2f;
        if (Utils.SquareDistance(transform.position, mTarget) <= desired)
            transform.position = mTarget;
    }


    public void MoveTo(Vector3 target) { mTarget = target; }

}
