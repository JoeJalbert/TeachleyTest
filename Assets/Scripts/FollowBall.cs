using UnityEngine;
using System.Collections;

public class FollowBall : MonoBehaviour {

    public Transform Ball;
    public float followSpeed;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Ball.position, followSpeed / Time.deltaTime);
        Vector3 tempPos = transform.position;
        tempPos.z = -10;
        transform.position = tempPos;

        float distance = Vector2.Distance(transform.position, Ball.position);
        // Increase the speed if the ball is getting too far away.
        if (distance > 1)
        {
            followSpeed = Mathf.Lerp(followSpeed, .002f, .003f / Time.deltaTime);
        }
        else if(distance > .2f)
        {
            followSpeed = Mathf.Lerp(followSpeed, .0004f, .003f / Time.deltaTime);
        }
        else
        {
            followSpeed = .0001f;
        }
    }

}
