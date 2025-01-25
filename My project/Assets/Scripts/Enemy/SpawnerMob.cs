using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerMob : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject obj;
    public float distancia; 
    public float tiempo;
    public EnemyBehaviour enemyScript;
    public GameObject target;

    void Start()
    {
        distancia = Vector3.Distance(transform.position, target.transform.position);
        target = GameObject.Find("Player");
        tiempo = 0f;        
    }

    // Update is called once per frame
    void Update()
    {     
        if(enemyScript.persiguiendo == true && distancia > 4){
            tiempo += Time.deltaTime;
            
        }else if (enemyScript.persiguiendo == false && tiempo >0){
                tiempo -= Time.deltaTime;
            }
        Vector3 posicionActual = transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.left, out hit, 10.0f)){
            Debug.Log(tiempo);
            if(hit.distance >= 6f && hit.distance<10f && tiempo > 15f){
                Vector3 nuevaPosicion = posicionActual + new Vector3(posicionActual.x-(hit.distance/2), 0f, 0f);
                GameObject nuevoObjeto = Instantiate(obj, nuevaPosicion, transform.rotation);
                tiempo = 0f;
            }
        }else if (Physics.Raycast(transform.position, Vector3.right, out hit, 10.0f)){
            Debug.Log(tiempo);
            if(hit.distance >= 6f && hit.distance<10f && tiempo > 15f){
                Vector3 nuevaPosicion = posicionActual + new Vector3(posicionActual.x+(hit.distance/2), 0f, 0f);
                Instantiate(obj, nuevaPosicion, transform.rotation);
                tiempo = 0f;
            }
        } else  if (Physics.Raycast(transform.position, Vector3.forward, out hit, 10.0f)){
            Debug.Log(tiempo);
            if(hit.distance >= 6f && hit.distance<10f && tiempo > 15f){
                Vector3 nuevaPosicion = posicionActual + new Vector3(0f, 0f, posicionActual.z+(hit.distance/2));
                Instantiate(obj, nuevaPosicion, transform.rotation);
                tiempo = 0f;
            }
        }
    }
}
