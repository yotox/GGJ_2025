using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    private float puntos;
    public TextMeshProUGUI textMesh;
    // Start is called before the first frame update
    private void Start()
    {
        puntos = 0f;
        textMesh.text = "Destruye las burbujas!";
    }

    // Update is called once per frame
    private void Update()
    {
        if(puntos != 0){
        textMesh.text = "Score: " + puntos.ToString();
        } 
    }
    
    public void EscenaInicio()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void EscenaJugar()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void SalirJuego()
    {
        Application.Quit();
        Debug.Log("Salio");
    }

    public void SumarPuntos(float puntosEntrantes){
        puntos += puntosEntrantes;
    }
}
