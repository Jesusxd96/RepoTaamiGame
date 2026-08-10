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

    public CharacterController valuesToPass;//Los valores que se le enviaran al character controller principal.
    public CharacterController tempValues;//Valores que se guardan del Child para pasarlo al Parent

    private void Start()//Siempre se iniciara con la forma base
    {
        currentForm = taamiForms[0];//La 0 siempre siempre sera la forma humanoide
        //tempValues = currentForm.GetComponentInChildren<CharacterController>();//Se consiguen los valores del hijo.
        //valuesToPass = tempValues;
        //this.gameObject.GetComponent<CharacterController>(CharacterController).Equals valuesToPass;//Pasarle los valores del otro al 
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ChangeForm();
        }
    }

    void ChangeForm()//Con este se podra cambiar de forma
    {
        if (currentForm==taamiForms[0])
        {
            taamiForms[0].SetActive(false);
            taamiForms[1].SetActive(true);
            currentForm = taamiForms[1];
            CopyCharacterController(taamiForms[1].GetComponent<CharacterController>(),this.gameObject.GetComponent<CharacterController>());
        }
        else if(currentForm== taamiForms[1])
        {
            taamiForms[1].SetActive(false);
            taamiForms[0].SetActive(true);
            currentForm = taamiForms[0];
            CopyCharacterController(taamiForms[0].GetComponent<CharacterController>(), this.gameObject.GetComponent<CharacterController>());
        }
    }
    void CopyCharacterController(CharacterController source, CharacterController destination)//Copia los 
    {
        destination.radius = source.radius;
        destination.height = source.height;
        destination.center = source.center;
        destination.slopeLimit = source.slopeLimit;
        destination.stepOffset = source.stepOffset;
        destination.skinWidth = source.skinWidth;
        destination.minMoveDistance = source.minMoveDistance;
        destination.detectCollisions = source.detectCollisions;
        destination.enableOverlapRecovery = source.enableOverlapRecovery;
    }
}
