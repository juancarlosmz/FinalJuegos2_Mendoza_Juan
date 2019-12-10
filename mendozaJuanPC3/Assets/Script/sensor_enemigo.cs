using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensor_enemigo : MonoBehaviour
{
    public Moverenemigo mover;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Personaje"))
        {
            mover.nuevo_usuario(collision.gameObject.GetComponent<centro>());
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Personaje"))
        {
            mover.eliminar_usuario(collision.gameObject.GetComponent<centro>());
        }
    }

}
