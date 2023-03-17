using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float Velocity = 10.0f;

    Vector3 mTarget;
    public bool mCanMove = true;

    // Start is called before the first frame update
    void Start()
    {
        mTarget = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!mCanMove) return;

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        Vector2 dir = (mTarget - transform.position).normalized;
        rb.position += Velocity * Time.fixedDeltaTime * dir;

        const float desired = 0.2f * 0.2f;
        if(Math.SquareDistance(transform.position, mTarget) <= desired)
            transform.position = mTarget;
    }


    public void MoveTo(Vector3 target) { mTarget = target; }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        mCanMove = false;
    
        mTarget = transform.position;
    }
    
    private void OnCollisionExit2D(Collision2D collision)
    {
        mCanMove = true;
        mTarget = transform.position;
    }
}
