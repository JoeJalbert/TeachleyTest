using UnityEngine;
using System.Collections;

public class FollowBall : MonoBehaviour {

    public Transform Ball;
    public float followSpeed;

    public float topSpeed;
    public float midSpeed;
    public float lowSpeed;

    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, Ball.position, followSpeed * Time.deltaTime);
        Vector3 tempPos = transform.position;
        tempPos.z = -10;
        transform.position = tempPos;

        float distance = Vector2.Distance(transform.position, Ball.position);
        // Increase the speed if the ball is getting too far away.
        if (distance > .8f)
        {
            followSpeed = Mathf.Lerp(followSpeed, topSpeed, 3f * Time.deltaTime);
        }
        else if(distance > .2f)
        {
            followSpeed = Mathf.Lerp(followSpeed, midSpeed, 3f * Time.deltaTime);
        }
        else
        {
            followSpeed = lowSpeed;
        }
    }

}
