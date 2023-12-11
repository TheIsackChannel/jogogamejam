using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms;
using static Unity.Burst.Intrinsics.X86;
using UnityEngine.UI;
using Unity.VisualScripting;

public class Protag : MonoBehaviour
{
    public Rigidbody2D Corpo;
    public Animator Animador;

    bool noChao = false;

    // CONFIGS DE PULO
    private Rigidbody2D rb;
    private float lastJumpTime;
    private float jumpForce = 5f;
    private float jumpCooldown = 5f;
    public float timer = 0.0f;
    public double quinze = 5.15; // Dá pra alterar quando quiser
    public float alturapulo = 255; // Dá pra alterar quando quiser
    public bool faliceu = false;

    // TAMANHO PERSONAGENS
    public double xycord = 0.35;
    public double zcord = 0.175;
    public float velocidadeX = 5;
    public float velocidadeXa = 5;

    public bool correndo = false;

    //Ataque Distancia
    public GameObject Bala;
    private float meuTempoTiro = 0;
    private float pode_atirar = 0.1f;
    public GameObject PontoDeOrigem;
    public bool mirandocima = false;
    public bool atirourecente = false;
    public float timerT = 0.0f;
    public float timerS = 0.0f;

    public bool travaS = false;
    public bool travaW = false;

    //Quantidade de Sangue
    public double hp = 140;
    private double hpv;
    private double hpa;
    public float tiroC = 0.08f;

    public double itemhp = 55;

    private Image BarraHPV;
    private Image BarraHPA;

    //Coletáveis
    public int moedas = 0;
    private Text MoedaTexto; // Moeda_txt

    // MARCOS MOEDAS
    private bool moedas_07 = false;
    private bool moedas_17 = false;
    private bool moedas_27 = false;
    private bool moedas_37 = false;
    private bool moedas_47 = false;

    public bool coletavelDano = false;

    public Vector3 posInicial;

    public SpriteRenderer edigeSR;

    public float timerD = 0.0f;


    //SONS
    //Musicas
    public float timerMsc1 = 0.0f;
    public float timerMsc2 = 0.0f;
    public float timerMsc3 = 0.0f;
    public float timerMsc4 = 0.0f;
    private float secondsMsc4=0;

    public float timerFX1 = 0.0f;
    public float timerFX2 = 0.0f;

    public AudioSource audioMusicas;
    public AudioSource audioEfeitosI;
    public AudioSource audioEfeitosII;

    public GameObject MusicaPadrao;
    public bool musicaReproduzindo = true;
    public bool musicaReproduzindoII = false;
    public GameObject MusicaBoss;

    //Efeios Sonoros
    //Drones
    public GameObject detectorDrone;
    public GameObject detectorDrone01;
    public GameObject detectorDrone02;
    public GameObject detectorDrone03;
    public GameObject detectorDrone04;
    public GameObject detectorDrone05;
    public GameObject detectorDrone06;
    public GameObject detectorDrone07;
    public GameObject detectorDrone08;
    public GameObject detectorDrone09;
    public GameObject detectorDrone10;
    public GameObject detectorDrone11;
    public bool efeitoDrone = false;
    public GameObject FXDrone;

    public GameObject detectorToretta;
    public bool efeitoToretta = false;
    public GameObject FXToretta;
    public GameObject detectorBoss;
    public bool efeitoBoss = false;
    public GameObject FXBoss;

    // Inicia - Roda a primeira Vez e Só
    void Start()
    {
        audioMusicas = GetComponent<AudioSource>();
        audioEfeitosI = GetComponent<AudioSource>();
        audioEfeitosII = GetComponent<AudioSource>();

        edigeSR = GetComponent<SpriteRenderer>();

        hpv = hp / 2;
        hpa = hp/2;

        Corpo = GetComponent<Rigidbody2D>();
        rb = GetComponent<Rigidbody2D>();
        lastJumpTime = -jumpCooldown; // Inicializa a variável para permitir o primeiro salto imediatamente
        Animador = GetComponent<Animator>();

        BarraHPV = GameObject.FindGameObjectWithTag("VidaV").GetComponent<Image>();
        BarraHPA = GameObject.FindGameObjectWithTag("VidaA").GetComponent<Image>();

        MoedaTexto = GameObject.FindGameObjectWithTag("Moeda_tag").GetComponent<Text>();

        posInicial = new Vector3(-40.5f, 13f, 0f);
        transform.position = posInicial;

    }
    void Update()
    {
        // SONS
        //Musica principal

        /*timerMsc1 += Time.deltaTime;
        float secondsMsc1 = timerMsc1 % 155;

        if (Vector3.Distance(detectorBoss.transform.position, transform.position) < 20 && efeitoBoss == false)
        {
            efeitoBoss = true;
            musicaReproduzindo= true;
            GameObject Musica = Instantiate(MusicaBoss);
            Musica.GetComponent<ContrlBala>().DirectionBullet(1f);
            Destroy(Musica,150f);
            if (secondsMsc1 > 0)
            {
                secondsMsc1 = 0;
                timerMsc1 = 0;
            }
        }
        if (efeitoBoss == true)
        {
            

            if (secondsMsc1 > 149)
            {
                efeitoBoss = false;
                timerMsc1 = 0;
                secondsMsc1 = 0;
            }
        }

        timerMsc4 += Time.deltaTime;
        secondsMsc4 = timerMsc4 % 155;

        if (secondsMsc4>150f)
        {
            musicaReproduzindo = false;
            timerMsc4 = 0;
            secondsMsc4 = 0;
        }

        if (musicaReproduzindo == true)
        {
            timerMsc4 += Time.deltaTime;
            secondsMsc4 = timerMsc4 % 60;

            if (secondsMsc4 > 1f)
            {
                timerMsc4 = 0;
                secondsMsc4 = 0;

            }
        }
        if (musicaReproduzindoII == true)
        {
            timerMsc3 += Time.deltaTime;
            float secondsMsc3 = timerMsc3 % 60;

            if (secondsMsc3 > 0.1f)
            {
                musicaReproduzindoII = false;
                musicaReproduzindo = false;
                timerMsc3 = 0;
                secondsMsc3 = 0;
            }
        }*/

        // EFEITOS SONOROS
        //DRONES

        if (detectorDrone01 != null)
        {
            detectorDrone = detectorDrone01;
        }else if (detectorDrone02 != null)
        {
            detectorDrone = detectorDrone02;
        }else if (detectorDrone03 != null)
        {
            detectorDrone = detectorDrone03;
        }
        else if (detectorDrone04 != null)
        {
            detectorDrone = detectorDrone04;
        }
        else if (detectorDrone05 != null)
        {
            detectorDrone = detectorDrone05;
        }
        else if (detectorDrone06 != null)
        {
            detectorDrone = detectorDrone06;
        }
        else if (detectorDrone07 != null)
        {
            detectorDrone = detectorDrone07;
        }
        else if (detectorDrone08 != null)
        {
            detectorDrone = detectorDrone08;
        }
        else if (detectorDrone09 != null)
        {
            detectorDrone = detectorDrone09;
        }
        else if (detectorDrone10 != null)
        {
            detectorDrone = detectorDrone10;
        }
        else if (detectorDrone11!= null)
        {
            detectorDrone = detectorDrone11;
        }else
        {
            detectorDrone=null;
        }

        if (detectorDrone!= null)
        {
           /* if (Vector3.Distance(detectorDrone.transform.position, transform.position) < 10 && efeitoDrone == false)
            {
                efeitoDrone = true;
                GameObject DroneFX = Instantiate(FXDrone);
                DroneFX.GetComponent<ContrlBala>().DirectionBullet(1f);
                Destroy(DroneFX, 17f);
            }
            if (efeitoDrone == true)
            {
                timerMsc1 += Time.deltaTime;
                float secondsMsc1 = timerMsc1 % 60;
                if (secondsMsc1 > 17)
                {
                    efeitoDrone = false;
                    timerMsc1 = 0;
                    secondsMsc1 = 0;
                }
            }*/
        }
        

        // MOEDA +VIDA
        if (moedas == 7 && moedas_07==false)
        {
            hp = hp + 5;
            moedas_07=true;
        }
        if (moedas == 17 && moedas_17 == false)
        {
            hp = hp + 10;
            moedas_17 = true;
        }
        if (moedas == 27 && moedas_27 == false)
        {
            hp = hp + 15;
            moedas_27 = true;
        }
        if (moedas == 37 && moedas_37 == false)
        {
            hp = hp + 25;
            moedas_37 = true;
        }
        if (moedas == 47 && moedas_47 == false)
        {
            hp = hp + 50;
            moedas_47 = true;
        }

        // DEMAIS
        if (faliceu == true)
        {
            timer += Time.deltaTime;
            float seconds = timer % 60;

            if (seconds > 5)
            {
                if (moedas > 6)
                {
                    faliceu = false;

                    hp = hp + 75;

                    transform.position = posInicial;

                    /*pode_atirar = pode_atirar / 3f;
                    coletavelDano = true;*/

                    moedas = moedas - 10;
                    MoedaTexto.text = moedas.ToString();

                    StartCoroutine("CorRegen");

                    Animador.SetBool("Morreu", false);

                    hpa = hp/2;
                }
                else
                {
                    Morrer();
                }
                
            }
        }

        if (atirourecente== true)
        {
            timerS += Time.deltaTime;
            float secondsS = timerS % 60;

            if (secondsS > pode_atirar)
            {
                atirourecente=false;

                timerS= 0;
                secondsS= 0;

            }
        }
        if (hp > 0)
        {
            if (mirandocima == false)
            {
                float xy = (float)xycord;
                float z = Convert.ToSingle(zcord);

                // MOVIMENTO
                float VelX = Input.GetAxis("Horizontal") * velocidadeXa;
                float VelY = Corpo.velocity.y;
                Corpo.velocity = new Vector2(VelX, VelY);


                if (VelX < 0)
                {
                    transform.localScale = new Vector3(-xy, xy, z);
                    Animador.SetBool("CorrePers", true);
                    Animador.SetBool("DanoPers", false);
                    tiroC = -0.08f;
                    correndo = true;
                }
                else if (VelX > 0)
                {
                    transform.localScale = new Vector3(xy, xy, z);
                    Animador.SetBool("CorrePers", true);
                    Animador.SetBool("DanoPers", false);
                    tiroC = 0.08f;
                    correndo = true;
                }
                else
                {
                    Animador.SetBool("CorrePers", false);
                    correndo = false;

                }
            } else
            {
                float VelX = Input.GetAxis("Horizontal") * 0;
                float VelY = Corpo.velocity.y;
                Corpo.velocity = new Vector2(VelX, VelY);
            }
            
            if (correndo==true)
            {
                SomCorrendo(true);
            }
            else{
                SomCorrendo(false);
            }
            // TIROS
            if (atirourecente== false)
            {
                //TIRO S
                if (Input.GetKey(KeyCode.S) && travaS==false)
                {
                    DisparoLateral();
                    TempoTiro();
                    velocidadeXa=velocidadeX/2;
                    travaW = true;
                }

                //TIRO W
                if (Input.GetKey(KeyCode.W) && travaW==false)
                {
                    DisparoCima();
                    mirandocima = true;
                    TempoTiro();
                    travaS = true;
                }
                
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                mirandocima = false;
                Animador.SetBool("Tiro", false);
                travaS = false;

            }
            if (Input.GetKeyUp(KeyCode.S)) 
            {
                Animador.SetBool("Tiro", false);
                velocidadeXa= velocidadeX;
                travaW = false;
            }
        }

        /* PULO
             Verifica se o jogador pressionou a tecla de salto (por exemplo, barra de espaço) e se o tempo de espera passou */

        if (hp > 0)
        {
            /*if (Input.GetKeyDown(KeyCode.UpArrow) && noChao)  // double quinze = Tempo de espera entre os pulos (15) 
            {
                Animador.SetBool("PuloPers", true);
                Animador.SetBool("DanoPers", false);
                Pular();

            }

            if (Input.GetKeyDown(KeyCode.W) && noChao)  // double quinze = Tempo de espera entre os pulos (15) 
            {
                Animador.SetBool("PuloPers", true);
                Animador.SetBool("DanoPers", false);
                Pular();

            }*/



            if (Input.GetButtonDown("Jump") && Time.time - lastJumpTime >= jumpCooldown / quinze)  // double quinze = Tempo de espera entre os pulos (15) 
            {
                Animador.SetBool("PuloPers", true);
                Animador.SetBool("DanoPers", false);
                Pular();

            }
            if (Time.time - lastJumpTime >= jumpCooldown / 15)
            {
                //Animador.SetBool("PuloPers", false);
            }
        }

    }


    public void Pular()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // Zera a velocidade vertical antes do pulo para evitar pulos duplos
        Corpo.AddForce(Vector3.up * alturapulo);
        lastJumpTime = Time.time; // Registra o tempo do último salto
    }

    public void DisparoLateral()
    {
        Animador.SetBool("Tiro", true);

        //if ()

        /*GameObject Tiro = Instantiate(Bala, PontoDeOrigem.transform.position, Quaternion.identity);
        Destroy(Tiro, 3f);*/

        Vector3 pontoDisparo = new Vector3(transform.position.x + 0.3f, transform.position.y - 0.15f, transform.position.z);
        GameObject BalaDisparada = Instantiate(Bala, pontoDisparo, Quaternion.identity);
        //BalaDisparada.GetComponent<ContrlBala>().DirectionBullet(tiroC);
        Destroy(BalaDisparada, 3.55f);


        /*if (transform.localScale.x == 1)
        {
            //Não Preciso Fazer Nada
            BalaDisparada.GetComponent<ContrlBala>().DirectionBullet(0.8f);
        }
        if (transform.localScale.x == -1)
        {
            //Tiro.GetComponent<AtaqueDistancia>().MudaVelocidade(-5);
            BalaDisparada.GetComponent<ContrlBala>().DirectionBullet(-0.8f);
        }*/
    }

    public void DisparoCima()
    {
        Animador.SetTrigger("AtiraCima");

        //if ()

        /*GameObject Tiro = Instantiate(Bala, PontoDeOrigem.transform.position, Quaternion.identity);
        Destroy(Tiro, 3f);
        if(transform.localScale.x == 1)
        {
            //Não Preciso Fazer Nada
        }
        if (transform.localScale.x == -1)
        {
            //Tiro.GetComponent<AtaqueDistancia>().MudaVelocidade(-5);
        }*/

        Vector3 pontoDisparo = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);
        GameObject BalaDisparada = Instantiate(Bala, pontoDisparo, Quaternion.identity);
        //BalaDisparada.GetComponent<ControleBala>().DirectionBullet(0.08f);
        Destroy(BalaDisparada, 1f);

    }

    public void Dano()
    {
        if (faliceu==false)
        {
            Animador.SetTrigger("DanoPers");
            StartCoroutine("CorDanoII");
            hp--;
            hpv--;
            if (hp < 0.0001)
            {
                Morte();
            }

            if (hp>hpa)
            {
                PerderHPV();
            }
            else
            {
                PerderHPA();
            }
        }
        
        if (coletavelDano==true)
        {
            pode_atirar = pode_atirar *3f;

            coletavelDano = false;
        }

    }

    public void Morte()
    {
        Animador.SetBool("Morreu",true);
        faliceu = true;
    }

    public void Morrer()
    {
        SceneManager.LoadScene("Derrota");
    }

    // DANO 1 e 2
    private void OnCollisionStay2D(Collision2D colisao)
    {
        //Debug.Log(colisao.gameObject.name);
        if (colisao.gameObject.tag == "Inimigo")
        {
            Dano();
        }
        if (colisao.gameObject.tag == "HitKill")
        {
            hp = 0;
            hpv = 0;
            PerderHPV();
            PerderHPA();
            Morte();
        }

        if (colisao.gameObject.tag == "Portaboss" && Input.GetKeyDown(KeyCode.F))
        {
            Vector2 pontoTP = new Vector2(47.95f, -34f);
            transform.position = pontoTP;

            GameObject Musica = Instantiate(MusicaBoss);
            //Musica.GetComponent<ContrlBala>().DirectionBullet(1f);
            efeitoBoss = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D tocar)
    {

        if (tocar.gameObject.tag == "AtkHit")
        {
            Dano();
        }

        if (tocar.gameObject.tag == "chao") 
        {
            noChao = true;
            Animador.SetBool("PuloPers", false);
            transform.parent = tocar.transform;
        }

        if (tocar.gameObject.tag == "Moeda")
        {
            Destroy(tocar.gameObject);
            moedas++;
            MoedaTexto.text = moedas.ToString();
        }

        if (tocar.gameObject.tag == "Regen")
        {
            Destroy(tocar.gameObject);
            hp=hp+itemhp;

            hpa = hp/1.5f;
        }

        if (tocar.gameObject.tag == "ColetavelDano")
        {
            Destroy(tocar.gameObject);

            pode_atirar = pode_atirar/3f;

            coletavelDano = true;

            StartCoroutine("CorDano");
        }

        if (tocar.gameObject.tag == "PontoDeControle")
        {
            posInicial = tocar.gameObject.transform.position;
            Destroy(tocar.gameObject);
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("chao"))
        {
            // Remove o objeto do personagem como filho da plataforma
            transform.parent = null;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "chao")
        {
            noChao = false;
        }
    }

    public void TempoTiro()
    {
        timerT += Time.deltaTime;
        float secondsT = timerT % 60;

        if (secondsT > 0.01f)
        {
            atirourecente = true;

            timerT = 0;
            secondsT = 0;
        }
    }

    public void PerderHPV()
    {
        int vida_parabarra_x = Convert.ToInt16(hpv) * 10;
        int vida_parabarra = vida_parabarra_x /7;
        BarraHPV.rectTransform.sizeDelta = new Vector2(vida_parabarra, 30);
    }

    public void PerderHPA()
    {
        int vida_parabarra_x = Convert.ToInt16(hp) * 10;
        int vida_parabarra = vida_parabarra_x /7;
        BarraHPA.rectTransform.sizeDelta = new Vector2(vida_parabarra, 30);
    }

    IEnumerator CorRegen()
    {
        edigeSR.color = Color.green;
        yield return new WaitForSeconds(3f);
        edigeSR.color = Color.white;
    }

    IEnumerator CorDano()
    {
        edigeSR.color = Color.magenta;
        yield return new WaitForSeconds(3f);
        edigeSR.color = Color.white;
    }

    IEnumerator CorDanoII()
    {
        edigeSR.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        edigeSR.color = Color.white;
    }

    public void SomCorrendo(bool ligar)
    {
        if (ligar == true)
        {
            audioEfeitosI.volume = 1;
        }
        else
        {
            audioEfeitosI.volume = 0;
        }



    }
}
