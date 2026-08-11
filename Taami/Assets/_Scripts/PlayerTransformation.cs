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
            CopyCharacterValues(taamiForms[1].GetComponent<FormProperties>(), this.gameObject.GetComponent<PlayerController>());
            CopyCharacterController(taamiForms[1].GetComponent<CharacterController>(),this.gameObject.GetComponent<CharacterController>());
        }
        else if(currentForm== taamiForms[1])
        {
            taamiForms[1].SetActive(false);
            taamiForms[0].SetActive(true);
            currentForm = taamiForms[0];
            CopyCharacterController(taamiForms[0].GetComponent<CharacterController>(), this.gameObject.GetComponent<CharacterController>());
            CopyCharacterValues(taamiForms[0].GetComponent<FormProperties>(), this.gameObject.GetComponent<PlayerController>());
        }
    }
    void CopyCharacterController(CharacterController fuente, CharacterController destino)//Copia los character controller de las otras formas
    {
        destino.radius = fuente.radius;
        destino.height = fuente.height;
        destino.center = fuente.center;
        destino.slopeLimit = fuente.slopeLimit;
        destino.stepOffset = fuente.stepOffset;
        destino.skinWidth = fuente.skinWidth;
        destino.minMoveDistance = fuente.minMoveDistance;
        destino.detectCollisions = fuente.detectCollisions;
        destino.enableOverlapRecovery = fuente.enableOverlapRecovery;
    }
    void CopyCharacterValues(FormProperties valores,PlayerController jugador)//Copia las variables de velocidad, salto, etc del modelo a desplegar.
    {
        jugador.SetSpeed(valores.speed);
        jugador.SetRotationSpeed(valores.rotationSpeed);
        jugador.SetJumpPower(valores.jumpPower);
        jugador.SetMaxNumberOfJumps(valores.maxNumberOfJumps);
        jugador.SetRotationSpeed(valores.rotationSpeed);
        jugador.SetGravityMultiplier(valores.gravityMultiplier);
    }
}