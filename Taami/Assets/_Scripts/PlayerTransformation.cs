using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerTransformation : MonoBehaviour
{
    /*0 Forma base, 1 Zorro*/
    //No se si sea mejor el usar gameobjects directo o una lista sencilla y que de ahi
    #region Transformaciones
    public List<GameObject> taamiForms = new List<GameObject>();//Lista para las transformaciones en formato GameObject 
    private GameObject previousForm;
    private GameObject currentForm;
    private GameObject newForm;
    #endregion

    private void Start()//Siempre se iniciara con la forma base
    {
        //currentForm = player;
    }

    void ChangeForm(string forma)//Con este se podra cambiar de forma
    {//Recibe un string que interpreta para hacer el cambio
        //newForm = FindObjectOfType<GameObject>().tag(forma);
    }
}
