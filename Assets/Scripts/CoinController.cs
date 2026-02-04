using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private Rigidbody2D _moedasRB2D;

    private GameController _gameController;

    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _moedasRB2D = GetComponent<Rigidbody2D>();
        _moedasRB2D.velocity = new Vector2(-6 - _gameController._coinVelocidade, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            _gameController._fxGame.PlayOneShot(_gameController._fxMoedaColetada);
            _gameController.Pontos(10);
            Debug.Log("Pegou a moeda!" + _gameController._pontosPlayer);
            Destroy(this.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("A moeda Foi destruida!");
        
    }
}
