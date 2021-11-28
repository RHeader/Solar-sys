using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Simulate : MonoBehaviour
{

    readonly float G = 6.7f;
    public float a = 10;
    GameObject[] celestials;
    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestial");
        InitialVelocity();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    void FixedUpdate()
    {
        Gravity();
    }
    void Gravity()
    {
        foreach (GameObject first in celestials)
        {
            foreach (GameObject second in celestials)
            {
                if (!first.Equals(second))
                {
                    float m1 = first.GetComponent<Rigidbody>().mass;
                    float m2 = second.GetComponent<Rigidbody>().mass;
                    float r = Vector3.Distance(first.transform.position, second.transform.position);

                    first.GetComponent<Rigidbody>().AddForce((second.transform.position - first.transform.position).normalized * (G * (m1 * m2) / (r * r)));
                    Debug.Log($" FirstObj={first.name} Second = {second.name} Object Radius = {r}, AddForce X = {first.transform.position.x}, AddForce Y = {first.transform.position.y}");
                }
            }
        }
    }
    void InitialVelocity()
    {
        foreach (GameObject first in celestials)
        {
            foreach (GameObject second in celestials)
            {
                if (!first.Equals(second))
                {
                    float m2 = second.GetComponent <Rigidbody>().mass;
                    float r = Vector3.Distance(first.transform.position, second.transform.position);
                    first.transform.LookAt(second.transform);
                   
                    first.GetComponent<Rigidbody>().velocity += first.transform.right * Mathf.Sqrt((G * m2) / r);
                    Debug.Log($"Object = {first.name} ");
                }
            }
        }

    }
}
