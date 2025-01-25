using System.Collections;
using UnityEngine;


public class TriggerSubsuelo : MonoBehaviour
{
    [Header("Objeto a Desactivar")]
    public GameObject objectToDisable; // Objeto externo que será desactivado

    [Header("Tiempo de Desactivación")]
    public float disableDuration = 15f; // Tiempo en segundos

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && objectToDisable != null) // Asegura que el objeto a desactivar esté asignado
        {
            StartCoroutine(DisableTemporarily());
        }
    }

    private IEnumerator DisableTemporarily()
    {
        objectToDisable.SetActive(false); // Desactiva el objeto
        yield return new WaitForSeconds(disableDuration); // Espera el tiempo configurado
        objectToDisable.SetActive(true); // Reactiva el objeto
    }
}

