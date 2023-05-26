using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += velocity * Time.deltaTime;
        transform.LookAt(transform.position + velocity);
    }
}
