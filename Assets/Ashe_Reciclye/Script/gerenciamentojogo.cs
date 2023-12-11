using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gerenciamentojogo : MonoBehaviour
{


    public bool GameLigado = false;
    // Start is called before the first frame update
    void Start()
    {
        GameLigado = false;
        Time.timeScale = 0;
    }

    public GameObject TelaGameOver;
    

    

    // Update is called once per frame
    void Update()
    {
        
    }



    public bool EstadoDoJogo()
    {
        return GameLigado;
    }

    public void LigarJogo()
    {
        GameLigado = true;
        Time.timeScale = 1;
    }

    public void PersonagemMorreu()
    {
        TelaGameOver.SetActive(true);
        GameLigado = false;
        Time.timeScale = 0;
    }

   public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }

    public void SairDoJogo()
    {
        Debug.Log("Saiu do jogo");
        Application.Quit();
    }

}
