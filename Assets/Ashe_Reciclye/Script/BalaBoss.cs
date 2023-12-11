using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaBoss : MonoBehaviour
{
    Rigidbody2D rigidBody;
    Player player;
    Vector3 direction;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        rigidBody = GetComponent<Rigidbody2D>();
        direction = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        rigidBody.velocity = direction * speed;
    }

    public void SetSpeed(float velocidade)
    {
        speed = velocidade;
    }
    private void OnTriggerEnter2D(Collider2D colisao)
    {



        if (colisao.gameObject.tag == "Jogador_Principal")
        {
            
            //Destroy(this.gameObject);
        }


        if (colisao.gameObject.tag == "chao")
        {
            Destroy(this.gameObject);
        }

    }
}
