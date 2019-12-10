using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class compartido : MonoBehaviour
{
    public int vidas;
    public enum tipo { personaje, enemigo };

    public tipo mitipo;

    public Slider barra_vida_personaje;
    public TextMesh barra_vida_enemigo;

    public bool muerto;

    public dano rango;

    public GameObject mipersonaje;
    void Start()
    {
        muerto = false;

        if (mitipo == tipo.enemigo)
        {
            barra_vida_enemigo.text = vidas.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void golpear()
    {
        //Debug.LogError("golpeado");
        rango.golpear_todos();
    }


    public int disminuir_vida(int dano)
    {
        vidas = vidas - dano;

        if (!muerto)
        {

            if (mitipo == tipo.personaje)
            {
                barra_vida_personaje.value = vidas;
            }
            else if (mitipo == tipo.enemigo)
            {
                barra_vida_enemigo.text = vidas.ToString();
            }

            if (vidas < 1)
            {
                muerto = true;
            }
        }

        return vidas;
    }

    public void muerte()
    {
        if (mitipo == tipo.enemigo)
        {
            Invoke("destruir_enemigo", 1.5f);
        }
        else if (mitipo == tipo.personaje)
        {
            Invoke("destruir_personaje", 1.5f);
        }
    }

    public void destruir_enemigo()
    {
        Destroy(this.gameObject);
    }

    public void destruir_personaje()
    {
        var vec = new Vector3(329, 14, 213);

        mipersonaje.transform.position = vec;
        barra_vida_personaje.value = 100;
        vidas = 100;
        muerto = false;
    }
}
