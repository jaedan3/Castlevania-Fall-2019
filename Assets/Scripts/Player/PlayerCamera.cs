using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    public string xAxis = "Horizontal";
    public string yAxis = "Vertical";

    public float bounds = 0.5f;

    private float lastX = 1;
    private float lastY;

    private float easeTimeX = 0;
    private float easeTimeY = 0;
    private Camera camera;
    private Vector2 easeFrom;
    private Vector2 easeTo;

    // Start is called before the first frame update
    void Start()
    {
        camera = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = SignWithZero(Input.GetAxisRaw(xAxis));
        if (x != 0 && x != lastX)
        {
            lastX = x;
            easeTimeX = 0;
            easeFrom.Set(transform.localPosition.x, easeFrom.y);
        }

        float y = SignWithZero(Input.GetAxisRaw(yAxis));
        if (y != lastY)
        {
            lastY = y;
            easeTimeY = 0;
            easeFrom.Set(easeFrom.x, transform.localPosition.y);
        }

        easeTimeX += Time.deltaTime;
        easeTimeY += Time.deltaTime;

        easeTo = Vector2.right * (lastX * camera.orthographicSize * 16 / 9 * bounds);
        easeTo += Vector2.up * (lastY * camera.orthographicSize * bounds);

        transform.localPosition = Vector3.back * 10;
        transform.localPosition += Vector3.right * Mathf.SmoothStep(easeFrom.x, easeTo.x, easeTimeX / 2);
        transform.localPosition += Vector3.up * Mathf.SmoothStep(easeFrom.y, easeTo.y, easeTimeY / 2);
    }

    int SignWithZero(float x)
    {
        if (x == 0) { return 0; }
        return (x > 0) ? 1 : -1;
    }
}
