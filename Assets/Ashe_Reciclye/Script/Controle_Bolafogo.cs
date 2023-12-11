using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controle_Bolafogo : MonoBehaviour
{
    private float velocidade_fogo = 0;



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MoverFogo();
    }

    void MoverFogo()
    {
        //movimenta��o
        transform.position = new Vector3(transform.position.x + velocidade_fogo, transform.position.y, transform.position.z);

    }
    //Dire��o do fogo

    public void direcaofogo(float direcao)
    {
        velocidade_fogo = direcao;
    }

    private void OnTriggerEnter2D(Collider2D colisao)
    {
        if (colisao.gameObject.tag == "Dano_Azul_tag")
        {
            Destroy(this.gameObject);
        }

        if (colisao.gameObject.tag == "Dano_Roxo_tag")
        {
            Destroy(this.gameObject);
        }

        if (colisao.gameObject.tag == "Dano_Bombado_tag")
        {
            Destroy(this.gameObject);
        }

        if (colisao.gameObject.tag == "Dano_Morcego_tag")
        {
            Destroy(this.gameObject);
        }

        if (colisao.gameObject.tag == "Boss")
        {
            Destroy(this.gameObject);
        }


        if (colisao.gameObject.tag == "ch�o")
        {
            Destroy(this.gameObject);
        }

    }
    





}
