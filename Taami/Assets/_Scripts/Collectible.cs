using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [SerializeField] private int collectibleId;//El id de cada objeto, debe de ser distinto para poder ayudar al Level Manager.
    [SerializeField] private int collectibleType;//Tipo de colleccionable
    [SerializeField] bool canRespawn; //Al recagar el nivel
    private bool hasBeenGrabbed;//Si en otra partida ya habia sido obtenida
    //0 Estrella, 1 Estrella ya obtenida,  vida
    private void Awake()
    {
        //Verificar el hasBeenGrabbed del objeto preguntandole a un "LevelManager"
        if (hasBeenGrabbed) collectibleType = 1;
    }
    private void DetectType(int type)
    {//Se puede usar un switch en vez de un if.
        if (type == 0)
        {
            Debug.Log("Tomaste una Estrella NUEVA");
            //PlayerPrefs Estrellas + 1
            //Cambia hasBeenGrabbed a true
            //Deja de existir
        }
        if (type == 1)
        {
            Debug.Log("Tomaste una Estrella VIEJA");
            //PlayerPrefs Estrellas + 1
            //Cambia hasBeenGrabbed a true
            //Deja de existir
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DetectType(this.collectibleType);
        }
    }
}
