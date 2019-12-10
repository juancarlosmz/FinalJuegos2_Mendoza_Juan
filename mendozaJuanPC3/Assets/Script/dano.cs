using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dano : MonoBehaviour
{
    public List<compartido> lista_objetivos;

    public string layer_receptor;
    public int dano1;

    void Start()
    {
        lista_objetivos = new List<compartido>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer_receptor))
        {
            nuevo_receptor(other.gameObject.GetComponent<compartido>());
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layer_receptor))
        {
            eliminar_receptor(other.gameObject.GetComponent<compartido>());
        }
    }

    public void nuevo_receptor(compartido obj)
    {
        if (lista_objetivos.Count == 0)
        {
            lista_objetivos.Add(obj);

        }
        else
        {
            foreach (var player in lista_objetivos)
            {
                if (player == obj)
                {
                    return;
                }
            }

            lista_objetivos.Add(obj);
        }
    }

    public void eliminar_receptor(compartido obj)
    {
        if (lista_objetivos.Count != 0)
        {
            lista_objetivos.Remove(obj);
        }
    }

    public void golpear_todos()
    {
        for (var i = lista_objetivos.Count - 1; i >= 0; i--)
        {
            try
            {
                var pj = lista_objetivos[i];

                var script = pj.gameObject.GetComponent<compartido>();

                int vida = script.disminuir_vida(dano1);

                if (vida < 1)
                {

                    script.muerte();

                    lista_objetivos.Remove(pj);

                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                lista_objetivos.Remove(lista_objetivos[i]);
            }

        }

    }
}
