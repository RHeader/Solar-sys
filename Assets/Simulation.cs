using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulation : MonoBehaviour
{


    public float gravity = 6.73f;
    public float sunMass = 100; // maybe too much?
    public float sphereMassScale = 2; // depends on actual scale of spheres
    public float initForceScale = 100;
    public List<GameObject> spheres;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject s in spheres)
        {
            s.GetComponent<Rigidbody>().AddForce(Random.insideUnitSphere * initForceScale, ForceMode.Acceleration);
        }
    }

    void FixedUpdate()
    {
        // Loop through each sphere totaling up and applying gravitational forces
        foreach (GameObject s in spheres)
        {
            Vector3 totalForce = new Vector3(0, 0, 0), forceDirection;
            float forceAmount;
            float sphereMass = s.transform.localScale.x * sphereMassScale;

            // first calculate sun's gravity by multiplying direction and force amount
            forceDirection = (transform.position - s.transform.position).normalized;
            forceAmount = getForce(sphereMass, sunMass, Vector3.Distance(transform.position, s.transform.position));
            totalForce += forceDirection * forceAmount;

            // this is optional, use if you want sphere gravity to affect each other (moons)
            foreach (GameObject p in spheres)
            {
                if (!p.Equals(s))
                { // don't check itself
                    forceDirection = (p.transform.position - s.transform.position).normalized;
                    forceAmount = getForce(sphereMass, p.transform.localScale.x * sphereMassScale, Vector3.Distance(p.transform.position, s.transform.position));
                    totalForce += forceDirection * forceAmount;
                }
            }
           // s.GetComponent<Rigidbody>().AddForce(totalForce, ForceMode.Acceleration);
        }
    }
    float getForce(float mass1, float mass2, float distance)
    {
        // return F=(Gm2m2)/d^2
        return (gravity * mass1 * mass2) / Mathf.Pow(distance, 2);
    }       // Update is called once per frame
    void Update()
    {
        
    }

}
