using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Slime_Boss : MonoBehaviour
{
    public GameObject prefabBalaBoss;
    public int hp = 30;
    public Transform originOlhoDireito;
    public Transform originOlhoEsquerdo;
    public float delayBala;
    public float speedBala;
    bool podeAtirar = true;
    public bool Ativado = false;
    public Animator anim;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Ativado) { 
            if (podeAtirar) 
            {
                Atirar();
            }
        }
    }

    void Atirar()
    {
        StartCoroutine("DelayBala");
        float rand = Random.Range(0f, 1f);
        if (rand >= 0.5)
        {
            GameObject bala = Instantiate(prefabBalaBoss, originOlhoDireito.position, originOlhoDireito.rotation);
            bala.GetComponent<BalaBoss>().SetSpeed(speedBala);
        }
        else 
        {
            GameObject bala = Instantiate(prefabBalaBoss, originOlhoEsquerdo.position, originOlhoEsquerdo.rotation);
            bala.GetComponent<BalaBoss>().SetSpeed(speedBala);
        }
    }

    void OnTriggerEnter2D(Collider2D colisao)
    {

        if (colisao.gameObject.tag == "Fogo")
        {


            hp--;

           
            
                anim.SetTrigger("Dano_Esquerdo");
            

          


            if (hp <= 0)
            {
                

                Destroy(this.gameObject);


            }
            
        }

        if (colisao.gameObject.tag == "fogoforte")
        {


            hp = hp - 2;

            anim.SetTrigger("Dano_Esquerdo");

            if (hp <= 0)

            {
                //anima��o do Boss morre

                anim.SetTrigger("SlAmorte");
                
                GetComponent<Rigidbody2D>().gravityScale = 0;
                GetComponent<BoxCollider2D>().enabled = false;

                Destroy(this.gameObject);


            }




        }


     

    }
    IEnumerator DelayBala()
    {
        podeAtirar = false;
        yield return new WaitForSeconds(delayBala);
        podeAtirar = true;
    }
}



   


