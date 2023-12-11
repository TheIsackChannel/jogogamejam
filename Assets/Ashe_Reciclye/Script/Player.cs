using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //sprite geral a receber comandos
    public Rigidbody2D Corpo;


    //velocidade de movimentos
    public float Velocidade;


    //pega o componente SpriteRenderer
    public SpriteRenderer Spritejogador;


    //qntdd de pulos que meu personagem realizou
    public int qtdpulo = 0;


    //controlar quando posso pular novamente
    private float meuTempoPulo = 0;
    //boleana que me diz se POSSO pular
    public bool pode_pular = true;

    //VIDA DO PERSONAGEM
    public int vida = 10;
    int vidamax;
    private float meuTempoDano = 0;
    private bool pode_dano = true;


    //BARRA DE HP

    private Image BarraDeHP;


    //Animações recebidas ao comandar cada ação do personagem
    public Animator anim;


    //castada de fogo
    public GameObject fogo;
    public GameObject fogo2;

    public GameObject fogoF;
    public GameObject fogoF2;

    private float tempofogo = 0;
    private bool cooldown = true;


    //Mecanica de moedas
    public int Orbs = 0;

    //Chance de Jogo
    private int chances = 3;
    private Text Chances_Texto;

    //Variavel com a posição inicial
    public Vector3 posInicial;


    private gerenciamentojogo GJ;

    public bool ataque_forteativado=false;

   


    //A primeira ação vista ao começar o jogo
    void Start()
    {
        //Determino a posição no inicio do jogo

        //Mudo a posição do personagem

        vidamax = vida;
     
        
        anim = GetComponent<Animator>();
       
    }



    //ações inseridas durante o jogo
    void Update()
    {

        
        {
            mover();
            virar();
            pular();
            castfogo();        
            TemporarizadorPulo();
            Dano();
            TemporarizadorFogo();
        }
        
    }



    //comando de movimentação (andar)
    void mover()
    {
        Velocidade = Input.GetAxis("Horizontal") * 3;
        Corpo.velocity = new Vector2(Velocidade, Corpo.velocity.y);

        if (Input.GetAxis("Horizontal") != 0)
        {
            anim.SetBool("Plrun", true);
        }
        else
        {
            anim.SetBool("Plrun", false);
        }

    }




    //mudança de direção do sprite do corpo
    void virar()
    {
        if (Velocidade > 0)
        {

            Spritejogador.flipX = false;

        }

        else if (Velocidade < 0)
        {

            Spritejogador.flipX = true;

        }
    }





    //COMANDOS DE PULAR
    void pular()
    {
        if (Input.GetKeyDown(KeyCode.Space) && pode_pular == true)
        {
            pode_pular = false;
            qtdpulo++;
            if (qtdpulo <= 1)
            {
                acaopular();
                anim.SetBool("Pljump", true);
            }

        }
        if (pode_pular == false)
        {
            TemporarizadorPulo();
        }


    }

    //Controla o TEMPO para Pular Novamente
    void TemporarizadorPulo()
    {
        meuTempoPulo += Time.deltaTime;
        if (meuTempoPulo > 0.5f)
        {
            pode_pular = true;
            meuTempoPulo = 0;
        }
    }


    //Força dada ao ser impulsionado para cima
    void acaopular()
    {
        //Zera velocidade de queda para o pulo
        Corpo.velocity = new Vector2(Velocidade, 0);
        Corpo.AddForce(transform.up * 245f);
    }


    //Trigger dado ao tocar no chão
    void OnTriggerEnter2D(Collider2D gatilho)
    {
        if (gatilho.gameObject.tag == "chao")
        {
            qtdpulo = 0;
            pode_pular = true;
            meuTempoPulo = 0;


            anim.SetBool("Pljump", false);

        }

        if (gatilho.gameObject.tag == "cajado")
        {
            Destroy(gatilho.gameObject);
            
            ataque_forteativado = true;
            CastadaForte();

        }
    

        if (gatilho.gameObject.tag == "morte_imediata")
        {
            if (pode_dano == true)
            {
                pode_dano = false;
                //tirar toda a vida
                vida = vida - 30;
                PerderHp();
                Morrer();
            }
        }

        if (gatilho.gameObject.tag == "Orb")
        {
            Destroy(gatilho.gameObject);
            Orbs++;
        }
       
        
        
        if (gatilho.gameObject.tag == "potion")
        {
            Destroy(gatilho.gameObject);
            if (vida < vidamax)
            {
                
                vida++;
                if (vida > vidamax) 
                {
                    vida = vidamax;
                }
            }
            PerderHp();

        }

        if (gatilho.gameObject.tag == "bala")
        {
            TomarDano(1);
            Destroy(gatilho.gameObject);
        }

    }


    // Lançar a Bola de fogo nos inimigos
    void castfogo()
    {
        if (cooldown == true)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                cooldown = false;
                
                
                if (ataque_forteativado == true)
                {
                    CastadaForte();
                    anim.SetBool("Platackforte", true);

                }
                else
                {
                    castada();
                    anim.SetBool("Platack", true);

                }

                
            }
            else
            {
                TemporarizadorFogo();
                anim.SetBool("Platack", false);
                anim.SetBool("Platackforte", false);
            }

        }



    }

    void castada()
    {

        if (Spritejogador.flipX == false)
        {
            // direcao ------>
            //posição que o fogo sai
            Vector3 pontodeCast = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.1f, transform.position.z);
            GameObject castfogo = Instantiate(fogo, pontodeCast, Quaternion.identity);
            castfogo.GetComponent<Controle_Bolafogo>().direcaofogo(0.03f);

            //destruir fogo
            Destroy(castfogo, 1f);
        }

        if (Spritejogador.flipX == true)
        {
            // direcao <------
            //posição que o fogo sai
            Vector3 pontodeCast = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.1f, transform.position.z);
            GameObject castfogo = Instantiate(fogo2, pontodeCast, Quaternion.identity);
            castfogo.GetComponent<Controle_Bolafogo>().direcaofogo(-0.03f);

            //destruir fogo
            Destroy(castfogo, 1f);
        }
        
    }

    void CastadaForte()
    {

        if (Spritejogador.flipX == false)
        {
            // direcao ------>
            //posição que o fogo sai
            Vector3 pontodeCast = new Vector3(transform.position.x + 0.5f, transform.position.y + 0.1f, transform.position.z);
            GameObject castfogo = Instantiate(fogoF, pontodeCast, Quaternion.identity);
            castfogo.GetComponent<Controle_Bolafogo>().direcaofogo(0.03f);

            //destruir fogo
            Destroy(castfogo, 1f);
        }

        if (Spritejogador.flipX == true)
        {
            // direcao <------
            //posição que o fogo sai
            Vector3 pontodeCast = new Vector3(transform.position.x - 0.5f, transform.position.y + 0.1f, transform.position.z);
            GameObject castfogo = Instantiate(fogoF2, pontodeCast, Quaternion.identity);
            castfogo.GetComponent<Controle_Bolafogo>().direcaofogo(-0.03f);

            //destruir fogo
            Destroy(castfogo, 1f);
        }

    }



    void TemporarizadorFogo()
    {
        tempofogo += Time.deltaTime;
        if (tempofogo > 0.5f)
        {
            cooldown = true;
            tempofogo = 0;
        }
    }


    ///Danos

    void Dano()
    {
        if (pode_dano == false)
        {
            TemporarizadorDano();
        }
    }


    // Personagem MORRER
    void TemporarizadorDano()
    {
        meuTempoDano += Time.deltaTime;
        if (meuTempoDano > 1f)
        {
            pode_dano = true;
            meuTempoDano = 0;
            Spritejogador.color = UnityEngine.Color.white;
        }
    }



    private void OnCollisionStay2D(Collision2D colisao)
    {

        //JOGADOR TOMA DANO DO SLIME AZUL
        if (colisao.gameObject.tag == "Dano_Azul_tag")
        {
            TomarDano(1);
        }


        //JOGADOR TOMA DANO DO SLIME ROXO
        if (colisao.gameObject.tag == "Dano_Roxo_tag")
        {
            TomarDano(2);
        }


        //JOGADOR TOMA DANO DO SLIME MORCEGO
        if (colisao.gameObject.tag == "Dano_Morcego_tag")
        {
            TomarDano(1);
        }

        if (colisao.gameObject.tag == "Dano_Bombado_tag")
        {
            TomarDano(3);
        }

        if (colisao.gameObject.tag == "bala")
        {
            TomarDano(1);
            Destroy(colisao.gameObject);
        }





        //JOGADOR PEGA AS BANDEIRAS DE CHECKPOINT
        if (colisao.gameObject.tag == "checkpoint")
        {
            posInicial = colisao.gameObject.transform.position;
            Destroy(colisao.gameObject);
        }
    


    }

   


    void TomarDano(int dano)
    {
        if (pode_dano == true)
        {

            
            vida = vida - dano;
            PerderHp();
            pode_dano = false;
            Spritejogador.color = UnityEngine.Color.red;
        }



        //Só morro se minha vida for menor ou igual
        if (vida <= 0)
        {
            anim.SetBool("Pmorte", true);
        }
        else
        {
            anim.SetBool("Pmorte", false);
        }
        
            
        if (vida <= 0)
        {
           
            Morrer();
        }
    }

    // PERSONAGEM PERDE A VIDA

    void PerderHp()
    {
        BarraDeHP.fillAmount = (float)vida / vidamax;
        //int vida_parabarra = vida * 10;
        //BarraDeHP.rectTransform.sizeDelta = new Vector2(vida_parabarra, 15);
    }

    //PERSONAGEM VAI DE BASE

    void Morrer()

    {
        chances--;
        Chances_Texto.text = ": " + chances.ToString();

        //só reinicia se acabar as chances
        if (chances <= 0)
        {
            GJ.PersonagemMorreu();
        }
        else
        {
            Inicializar();
        }

    }

    void Inicializar()
    {
        //Mudo a posição do personagem
        transform.position = posInicial;
        
        //recuperar Vida
        vida = vidamax;
        //int vida_parabarra = vida * 6;
        //BarraDeHP.rectTransform.sizeDelta = new Vector2(vida_parabarra, 15);
        BarraDeHP.fillAmount = 1;
        anim.SetBool("Pmorte", false);
        anim.SetTrigger("Previve");
    }

    //Reinicia o jogo

   
    
    
        

          
    



}