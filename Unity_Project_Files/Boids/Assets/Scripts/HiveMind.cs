using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiveMind : MonoBehaviour
{
    [SerializeField] private Boid boidPrefab;
    [SerializeField] private int numberOfBoids = 100;
    [SerializeField] private Vector3 size = new Vector3(25, 25, 25);
    private List<Boid> boids;
    [SerializeField] private float accCohesion = 5f;
    [SerializeField] private float accRepulsion = 20f;
    [SerializeField] private float accAlignment = 0.1f;
    [SerializeField] private float personalSpace = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        boids = new List<Boid>();
        for (int i = 0; i < numberOfBoids; i++)
        {
            var boid = Instantiate(boidPrefab);
            boid.transform.position = new Vector3(Random.Range(-size.x / 2, size.x / 2),
                                                  Random.Range(-size.y / 2, size.y / 2),
                                                  Random.Range(-size.z / 2, size.z / 2));
            boids.Add(boid);
        }

        FindObjectOfType<Camera>().GetComponent<CameraController>().followThatBoid = boids[0];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCohesionVelocity();
        UpdateRepulsionVelocity();
        UpdateAlignmentVelocity();
    }

    void UpdateAlignmentVelocity()
    {
        Vector3 groupVelocity = new Vector3();
        foreach(var b in boids)
        {
            groupVelocity += b.velocity;
        }
        groupVelocity /= numberOfBoids;

        foreach(var b in boids)
        {
            b.velocity += (groupVelocity) * accAlignment * Time.deltaTime;
        }
    }

    void UpdateCohesionVelocity()
    {
        Vector3 com = GetCentreOfMass();

        foreach (var b in boids)
        {
            b.velocity += (com - b.transform.position).normalized * Time.deltaTime * accCohesion;
        }
    }

    void UpdateRepulsionVelocity()
    {
        foreach(var boid in boids)
        {
            foreach(var boid2 in boids)
            {
                if(boid == boid2)
                {
                    continue;
                }

                var r = boid2.transform.position - boid.transform.position;
                if(r.magnitude < personalSpace)
                {
                    boid.velocity += Vector3.Cross(boid.velocity, r).normalized * accRepulsion * Time.deltaTime;// / (r.magnitude);
                }
            }
        }
    }

    public Vector3 GetCentreOfMass()
    {
        Vector3 com = new Vector3();
        foreach (var b in boids)
        {
            com += b.transform.position;
        }
        com /= numberOfBoids;
        return com;
    }
}
