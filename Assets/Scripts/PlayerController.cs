using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 0f;
    public bool isGrounded = true;
    public bool isRolling = false;
    public float jumpForce = 650f;

    public Animator anim;
    private Rigidbody2D rig;

    public LayerMask LayerGround;
    public Transform checkGround;
    public string isGroundBool = "eChao";

    private GameController _gameController;

    private bool rollInput = false;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        _gameController = FindObjectOfType(typeof(GameController)) as GameController;

        MovimentaPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) //Tecla para cima pressionada
        {
            Jump();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) // Tecla para baixo pressionada
        {
            rollInput = true;
            if (isGrounded)
            {
                _gameController._fxGame.PlayOneShot(_gameController._fxRoll);
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow)) // Tecla para baixo solta
        {
            rollInput = false;
        }
    }

    private void MovimentaPlayer()
    {
        transform.Translate(new Vector3(speed, 0, 0));
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(speed, 0, 0));

        if (Physics2D.OverlapCircle(checkGround.transform.position, 0.2f, LayerGround))
        {
            anim.SetBool(isGroundBool, true);
            isGrounded = true;
        }
        else
        {
            anim.SetBool(isGroundBool, false);
            isGrounded = false;
        }

        if (rollInput)
        {
            StartRoll();
        }
        else
        {
            StopRoll();
        }

        if (_gameController._pontosPlayer == 100)
        {
            _gameController._pontosPlayer = 0;
            

        }

    }

    public void Jump()
    {
        if (isGrounded)
        {
            _gameController._fxGame.PlayOneShot(_gameController._fxJump);
            rig.velocity = Vector2.zero;
            rig.AddForce(new Vector2(0, jumpForce));
        }
    }

    void StartRoll()
    {
        if (!isRolling)
        {
            isRolling = true;
            anim.SetInteger("transition", 4); // Ative a animação de agachar
            DiminuirTamanho(); // Ajuste o tamanho do Collider
        }
    }

    void DiminuirTamanho()
    {
        transform.GetComponent<BoxCollider2D>().size = new Vector2(0.13f, 0.22f);
    }

    void StopRoll()
    {
        if (isRolling)
        {
            isRolling = false;
            StartCoroutine(DelayedStopRoll());
        }
    }

    IEnumerator DelayedStopRoll()
    {
        yield return new WaitForSeconds(0.3f); // Tempo de espera após soltar o botão
        anim.SetInteger("transition", 0);
        AjustarTamanho(); // Ajuste o tamanho do Collider
    }

    void AjustarTamanho()
    {
        transform.GetComponent<BoxCollider2D>().size = new Vector2(0.13f, 0.3f);
    }


}

