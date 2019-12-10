using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class centro : MonoBehaviour
{
    public Collider collider;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector3 get_center_position()
    {
        return collider.bounds.center;
    }
}
