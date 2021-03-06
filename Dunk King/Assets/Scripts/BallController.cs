using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float Power = 5f;

    private Rigidbody2D _rb;
    private LineRenderer _lr;

    private Vector2 _dragStartPos;
    private float _maxVelocity = 9f;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _lr = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _dragStartPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(0))
        {
            Vector2 dragEndPos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = (dragEndPos - (Vector2)transform.position) * Power * -1f;
            velocity = ClampVelocity(velocity, _maxVelocity);

            Vector2[] trajectory = Plot(_rb, (Vector2) transform.position, velocity, 500);
            _lr.positionCount = trajectory.Length;

            Vector3[] positions = new Vector3[trajectory.Length];
            for (int i = 0; i < trajectory.Length; i++)
            {
                positions[i] = (Vector3)trajectory[i];
            }

            _lr.material.mainTextureScale = new Vector2(1f / _lr.startWidth, 1f);
            _lr.SetPositions(positions);
        }

        if (Input.GetMouseButtonUp(0))
        {
            _lr.positionCount = 0;
            Vector2 dragEndPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 velocity = (dragEndPos - (Vector2)transform.position) * Power * -1f;
            velocity = ClampVelocity(velocity, _maxVelocity);

            _rb.velocity = velocity;
        }

    }

    public Vector2[] Plot(Rigidbody2D rigidbody, Vector2 pos, Vector2 velocity, int steps)
    {
        Vector2[] results = new Vector2[steps / 2]; // steps / 2 not to show full trajectory to make it harder

        float timestep = Time.fixedDeltaTime / Physics2D.velocityIterations;
        Vector2 gravityAccel = Physics2D.gravity * rigidbody.gravityScale * timestep * timestep;
        float drag = 1f - timestep * rigidbody.drag;
        Vector2 moveStep = velocity * timestep;

        for (int i = 0; i < results.Length; ++i)
        {
            moveStep += gravityAccel;
            moveStep *= drag;
            pos += moveStep;
            results[i] = pos;
        }

        return results;
    }

    private static Vector2 ClampVelocity(Vector2 velocity, float maxVelocity)
    {
        return velocity.magnitude > maxVelocity ? velocity.normalized * maxVelocity : velocity; 
    }
}
