using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";

    public float maxVelocity = 5;
    public float camAccelerationX = 2;
    public float camAccelerationY = 4;
    public float bounds = 0.5f;

    private float lastX = 1;
    private float lastY;
    
    private Camera camera;
    private Vector2 vel;
    private Vector2 easeTo;
    private Vector2 truePos;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = SignWithZero(Input.GetAxisRaw(xAxis));
        if (x != 0 && x != lastX)
        {
            lastX = x;
        }
        float y = SignWithZero(Input.GetAxisRaw(yAxis));
        if (y != lastY)
        {
            lastY = y;
        }

        easeTo = Vector2.right * (lastX * camera.orthographicSize * 16 / 9 * bounds);
        easeTo += Vector2.up * (lastY * camera.orthographicSize * bounds);

        // scary math ahead
        vel = new Vector2(accelerateMe(easeTo.x - truePos.x, vel.x, camAccelerationX), accelerateMe(easeTo.y - truePos.y, vel.y, camAccelerationY));
        Vector2 lastPos = truePos;
        truePos += vel * Time.fixedDeltaTime;
        if (vel.x == 0 || (lastPos.x - easeTo.x) * (truePos.x - easeTo.x) <= 0)
        {
            truePos.Set(easeTo.x, truePos.y);
        }
        if (vel.y == 0 || (lastPos.y - easeTo.y) * (truePos.y - easeTo.y) <= 0)
        {
            truePos.Set(truePos.x, easeTo.y);
        }

        transform.localPosition = new Vector3(Mathf.Round(truePos.x * 16) / 16, Mathf.Round(truePos.y * 16) / 16, -10);
    }

    int SignWithZero(float x)
    {
        if (x == 0) { return 0; }
        return (x > 0) ? 1 : -1;
    }
    
    float accelerateMe(float deltaPos, float vel, float acceleration)
    {
        if (Mathf.Abs(deltaPos) < 0.01)
        {
            return 0;
        }
        else if (Mathf.Abs(deltaPos) < vel * vel / 2 / acceleration)
        {
            //print("acc away");
            float unclampedVel = vel - acceleration * SignWithZero(deltaPos) * Time.fixedDeltaTime;
            return Mathf.Clamp(unclampedVel, -maxVelocity, maxVelocity);
        }
        else if ((deltaPos) * vel > 0)
        {
            //print("acc twrds");
            float unclampedVel = vel + acceleration * SignWithZero(deltaPos) * Time.fixedDeltaTime;
            return Mathf.Clamp(unclampedVel, -maxVelocity, maxVelocity);
        }
        else
        {
            //print("acc twrds HARD");
            float unclampedVel = vel + acceleration * SignWithZero(deltaPos) * Time.fixedDeltaTime * 2;
            return Mathf.Clamp(unclampedVel, -maxVelocity, maxVelocity);
        }
    }
}
