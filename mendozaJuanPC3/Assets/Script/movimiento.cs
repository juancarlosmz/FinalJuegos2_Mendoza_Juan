using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movimiento : MonoBehaviour
{
    public Rigidbody rb;
    public Animator anim;
    public Collider collider;
    public GameObject modelo;

    public float velocidad;
    public float salto;

    public bool pisando_tierra;

    private float distancia_raycast = 0.8f;

    public enum movimiento_Horizontal { A, D, Ninguno }
    public enum movimiento_Vertical { W, S, Ninguno }


    public movimiento_Horizontal mover_player_horizontal = movimiento_Horizontal.Ninguno;
    public movimiento_Vertical mover_player_vertical = movimiento_Vertical.Ninguno;
    void Start()
    {
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        float dt = Time.deltaTime;

        var center = collider.bounds.center;
        var extends = collider.bounds.extents;

        var x = center.x;
        var y = (center.y - extends.y) + distancia_raycast;
        var z = center.z;

        Ray ray = new Ray(new Vector3(x, y, z), Vector3.down);

        //int layerMask =  LayerMask.NameToLayer("Terreno");


        Debug.DrawRay(ray.origin, ray.direction, Color.red);

        RaycastHit hit;

        if (Physics.Raycast(ray.origin, ray.direction, out hit, 1))
        {
            pisando_tierra = true;
            // Debug.LogWarning(hit.collider.gameObject.name);
        }
        else
        {
            pisando_tierra = false;
            //Debug.DrawRay(ray.origin, ray.direction * 1000, Color.red);
            //Debug.LogWarning("Did not Hit");
        }


        teclas_presionada();
        tecla_soltada();

        if (pisando_tierra)
        {
            if (mover_player_horizontal == movimiento_Horizontal.Ninguno && mover_player_vertical == movimiento_Vertical.Ninguno)
            {
                anim.SetBool("correr", false);
            }
            else
            {
                anim.SetBool("correr", true);
            }
        }




    }

    void FixedUpdate()
    {
        float dt = Time.deltaTime;



        if (mover_player_vertical == movimiento_Vertical.W)
        {
            var vec = new Vector3(0, 0, 1);
            rb.AddForce(vec * velocidad * rb.mass * dt);

            modelo.transform.eulerAngles = new Vector3(0, 0, 0);
            // rb.MoveRotation(Quaternion.Euler(0, 0, 0));

        }
        else if (mover_player_vertical == movimiento_Vertical.S)
        {
            var vec = new Vector3(0, 0, -1);
            rb.AddForce(vec * velocidad * rb.mass * dt);

            modelo.transform.eulerAngles = new Vector3(0, 180, 0);
            // rb.MoveRotation(Quaternion.Euler(0, 180, 0));

        }

        if (mover_player_horizontal == movimiento_Horizontal.A)
        {
            var vec = new Vector3(-1, 0, 0);
            rb.AddForce(vec * velocidad * rb.mass * dt);

            modelo.transform.eulerAngles = new Vector3(0, -90, 0);
            //rb.MoveRotation(Quaternion.Euler(0, -90, 0));

        }
        else if (mover_player_horizontal == movimiento_Horizontal.D)
        {
            var vec = new Vector3(1, 0, 0);
            rb.AddForce(vec * velocidad * rb.mass * dt);

            modelo.transform.eulerAngles = new Vector3(0, 90, 0);
            // rb.MoveRotation(Quaternion.Euler(0, 90, 0));

        }


        anim.SetBool("Pisando_tierra", pisando_tierra);

    }

    private void teclas_presionada()
    {


        if (Input.GetKeyDown(KeyCode.A))
        {

            mover_player_horizontal = movimiento_Horizontal.A;

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {

            mover_player_horizontal = movimiento_Horizontal.D;

        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            mover_player_vertical = movimiento_Vertical.W;


        }
        else if (Input.GetKeyDown(KeyCode.S))
        {

            mover_player_vertical = movimiento_Vertical.S;
        }

        if (Input.GetKeyDown(KeyCode.Space) && pisando_tierra && mover_player_horizontal == movimiento_Horizontal.Ninguno && mover_player_vertical == movimiento_Vertical.Ninguno)
        {
            rb.AddForce(Vector3.up * salto * rb.mass);

            pisando_tierra = false;


            anim.SetTrigger("saltar");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            anim.SetTrigger("atacar");
        }

    }
    private void tecla_soltada()
    {

        if (Input.GetKeyUp(KeyCode.A))
        {

            mover_player_horizontal = movimiento_Horizontal.Ninguno;


        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            mover_player_horizontal = movimiento_Horizontal.Ninguno;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            mover_player_vertical = movimiento_Vertical.Ninguno;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            mover_player_vertical = movimiento_Vertical.Ninguno;
        }
    }
}
