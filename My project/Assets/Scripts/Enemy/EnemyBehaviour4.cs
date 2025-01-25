using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour4 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    //public Animator anim;
    public Quaternion angulo;
    public float grado;
    public float speed;
    public GameObject target;

    public NavMeshAgent agente;
    public float distancia_ataque;
    public float radio_vision;
 
 [SerializeField] private GameManager puntaje;
 [SerializeField] private float cantidadPuntos;
    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
        target = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Comportamiento_Enemigo();
    }

    public void Comportamiento_Enemigo(){
        if(Vector3.Distance(transform.position, target.transform.position) >radio_vision){
            agente.enabled = false;
        //anim.SetBool("run",false);
        cronometro +=1 * Time.deltaTime;
        if(cronometro >= 4){
            rutina = Random.Range(0,2);
            cronometro = 1;
        }
        switch (rutina){
            case 0:
            //anim.SetBool("walk",false);
            break;

            case 1:
            grado = Random.Range(0,360);
            angulo = Quaternion.Euler(0,grado,0);
            rutina++;
            break;

            case 2: 
            transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
            transform.Translate(Vector3.forward*speed*Time.deltaTime);
            //anim.SetBool("walk",true);
            break;
            }
        }
        else{
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);
            agente.enabled = true;
            agente.SetDestination(target.transform.position);

        }
    }

    private void OnTriggerEnter(Collider other){

        if(other.tag == "Player")
        {
            puntaje.SumarPuntos(cantidadPuntos);
            Destroy(this.gameObject);
        }
        if (other.tag == "Bullet4")
        {
            Destroy(this.gameObject);
        }
    }
}
