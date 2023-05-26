using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 offset = new Vector3(0.0f, 1.0f, 0.0f);
    [SerializeField] float distance = 5.0f;
    public Boid followThatBoid;
    private bool followToggle = false;
    [SerializeField] float RotationSpeed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(followThatBoid.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (followToggle)
        {
            FollowBoid();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            followToggle = !followToggle;
            if (!followToggle)
            {
                ResetCamera();
            }
        }

        if (!followToggle)
        {
            ApplyCameraControls();
        }
    }

    private void ApplyCameraControls()
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            RotateBy(new Vector3(-RotationSpeed, 0.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            RotateBy(new Vector3(RotationSpeed, 0.0f, 0.0f));
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateBy(new Vector3(0.0f, -RotationSpeed, 0.0f));
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateBy(new Vector3(0.0f, RotationSpeed, 0.0f));
        }
        RotateBy(new Vector3(0.0f, 0.0f, RotationSpeed) * Input.mouseScrollDelta.y * 10);
    }

    private void RotateBy(Vector3 rot)
    {
        transform.Rotate(rot * Time.deltaTime);
    }

    private void ResetCamera()
    {
        transform.position = new Vector3(0f, 0f, 0f);
        var com = FindObjectOfType<HiveMind>().GetCentreOfMass();
        transform.LookAt(com);
    }

    private void FollowBoid()
    {
        transform.position = followThatBoid.transform.position - distance * followThatBoid.velocity.normalized;
        transform.LookAt(followThatBoid.transform.position);
    }
}
