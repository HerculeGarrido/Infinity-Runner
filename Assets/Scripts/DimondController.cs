using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DimondController : MonoBehaviour
{
    private Rigidbody2D _diamondRB2;

    private GameController _gameController;
    // Start is called before the first frame update
    void Start()
    {
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;
        _diamondRB2 = GetComponent<Rigidbody2D>();
        _diamondRB2.velocity = new Vector2(-6 - _gameController._coinVelocidade, 0);

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _gameController._fxGame.PlayOneShot(_gameController._fxMoedaColetada);
            Debug.Log("Pegou o diamante!");
            Destroy(this.gameObject);
            TeleportarParaOutraCena();
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
        Debug.Log("O Diamante Foi destruido!");

    }

    private void TeleportarParaOutraCena()
    {
        // Lógica para teleportar para outra cena
        SceneManager.LoadScene("SpBox"); // Substitua "NomeDaOutraCena" pelo nome da cena para onde deseja teleportar
    }
}
