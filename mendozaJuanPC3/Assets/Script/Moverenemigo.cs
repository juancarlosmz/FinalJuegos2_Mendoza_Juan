using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moverenemigo : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public Collider collider;

    public float velocidad;

    public List<centro> lista_usuarios;

    void Start()
    {
        rb.freezeRotation = true;

        lista_usuarios = new List<centro>();
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;
     
        if (lista_usuarios.Count != 0)
        {

            direccionar_angulo(limitar_distancia(), dt);
        }
    }
    void FixedUpdate()
    {
        float dt = Time.deltaTime;
 
        if (lista_usuarios.Count != 0 && anim.GetCurrentAnimatorStateInfo(0).IsName("Correr"))
        {
            rb.AddForce(transform.forward * velocidad * rb.mass * dt);
        }
    }

    public void nuevo_usuario(centro obj)
    {
        if (lista_usuarios.Count == 0)
        {
            lista_usuarios.Add(obj);

            anim.SetBool("correr", true);

        }
        else
        {
            foreach (var player in lista_usuarios)
            {
                if (player == obj)
                {
                    return;
                }
            }

            lista_usuarios.Add(obj);
        }


    }

    public void eliminar_usuario(centro obj)
    {
        if (lista_usuarios.Count != 0)
        {
            lista_usuarios.Remove(obj);

            if (lista_usuarios.Count == 0)
            {
                anim.SetBool("correr", false);
            }

        }
    }
  
    public Vector3 limitar_distancia()
    {
        Vector3 direccion = new Vector3(0, 0);

        var center = collider.bounds.center;

        double distancia_min = 9999f;


        
        foreach (var player in lista_usuarios)
        {
            var vector = player.get_center_position();

            var distance = Vector3.Distance(collider.bounds.center, vector);

            if (distance < distancia_min)
            {
                distancia_min = distance;

                direccion = vector;

            }

        }
      

        return direccion;
    }

    public void direccionar_angulo(Vector3 vector, float dt)
    {
        Vector3 direction = (vector - collider.bounds.center).normalized;
        Quaternion rotate = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, dt * 5f);
        rb.MoveRotation(transform.rotation);
    }

    public void OnCollisionStay(Collision collision)
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Pegar") && collision.gameObject.layer == LayerMask.NameToLayer("Personaje"))
        {
            anim.SetTrigger("pegar");
        }
    }
}
