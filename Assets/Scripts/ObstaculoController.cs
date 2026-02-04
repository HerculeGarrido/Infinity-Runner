using System.Collections;
using UnityEngine;

public class ObstaculoController : MonoBehaviour
{
    private Rigidbody2D ObstaculoRB;

    private GameController _GameController;
    private CameraShaker _CameraShaker;

    private PlayerController _playerController;
    public int _VidaInimigo = 10;

    // Tempo de espera após a animação de dano
    public float waitTimeAfterDamage = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        ObstaculoRB = GetComponent<Rigidbody2D>();
        _GameController = FindObjectOfType(typeof(GameController)) as GameController;
        _CameraShaker = FindObjectOfType(typeof(CameraShaker)) as CameraShaker;
        _playerController = FindObjectOfType(typeof(PlayerController)) as PlayerController;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveObjeto();
    }

    void MoveObjeto()
    {
        transform.Translate(Vector2.left * _GameController._obstaculoVelocidade * Time.smoothDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            _GameController._vidasPlayer--;
            if (_GameController._vidasPlayer <= 0)
            {
                Debug.Log("Fim do Jogo");
                _playerController.anim.SetInteger("transition", 1);
                _GameController.GameOver(); // Chama a função GameOver do GameController
                _GameController._fxGame.PlayOneShot(_GameController._fxGameOver);

            }
            else
            {
                _GameController._txtVidas.text = _GameController._vidasPlayer.ToString();
                Debug.Log("Tocou no obstáculo!");
                _CameraShaker.ShakeIt();
                _playerController.anim.SetInteger("transition", 3);
                _GameController._fxGame.PlayOneShot(_GameController._fxDamage);

                // Inicia a Coroutine para esperar um tempo antes de voltar à animação de corrida
                StartCoroutine(ReturnToRunningAnimation());
            }
        }
    }

    // Coroutine para esperar um tempo antes de voltar à animação de corrida
    IEnumerator ReturnToRunningAnimation()
    {
        yield return new WaitForSeconds(waitTimeAfterDamage);

        // Volta à animação de corrida
        _playerController.anim.SetInteger("transition", 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
