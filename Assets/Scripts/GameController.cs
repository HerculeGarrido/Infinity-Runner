using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Propriedades do chão
    [Header("Configuração do chão")]
    public float _chaoDestruido;
    public float _chaoTamanho;
    public float _chaoVelocidade;
    public GameObject _chaoPrefab;

    [Header("Configuração do Obstáculo")]
    public float _obstaculoTempo;
    public List<GameObject> _obstaculoPrefabs; // Lista de Prefabs de obstáculos
    public float _obstaculoVelocidade;
    public int _numeroDeObstaculos = 10;

    [Header("Configuração do Coin - Moeda")]
    public float _coinTempo;
    public GameObject _coinPrefab;
    public float _coinVelocidade;

    [Header("Configuração do Diamante")]
    public GameObject _diamondPrefab;
    public float _diamondTempo;

    [Header("Configuração do UI")]
    public int _pontosPlayer;
    public Text _txtPontos;
    public int _vidasPlayer;
    public Text _txtVidas;
    public Text _txtMetros;
    public Text _SaveMetros;
    public Text _SavePontos;
    public Text _Score;
    public GameObject _porDoSol;
    private int _isNight = 1;

    [Header("Controle de Distância")]
    public int _metrosPercorridos = 0;

    [Header("Sons e Efeitos")]
    public AudioSource _fxGame;
    public AudioClip _fxMoedaColetada;
    public AudioClip _fxJump;
    public AudioClip _fxWinTask;
    public AudioClip _fxGameOver;
    public AudioClip _fxRoll;
    public AudioClip _fxDamage;

    [Header("Lógica Geral")] 
    public GameObject _pauseObj;
    public GameObject _gameOverobj;
    private bool _isPaused;
    private bool _isOver = false;
    private int  _score = 0;






    // Start is called before the first frame update
    void Start()
    {
        RecuperaValor();
        StartCoroutine("SpawnObstaculo");
        StartCoroutine("SpawnCoin");
        StartCoroutine("SpawnDiamond");
        InvokeRepeating("DistanciaPercorrida", 0f, 0.2f);
        Debug.Log(PlayerPrefs.GetInt("score"));
        Debug.Log(PlayerPrefs.GetInt("metros"));
    }

    // Update is called once per frame
    void Update()
    {
        if (_isOver == false)
        {
            PauseGame();
        }
    }
    IEnumerator SpawnObstaculo()
    {
        yield return new WaitForSeconds(_obstaculoTempo);

        GameObject objetoObstaculoTemp = Instantiate(_obstaculoPrefabs[Random.Range(0, _obstaculoPrefabs.Count)]);

        // Ajuste a posição X para ser aleatória entre 11 e 15
        float randomX = Random.Range(11f, 15f);
        objetoObstaculoTemp.transform.position = new Vector3(randomX, objetoObstaculoTemp.transform.position.y, 0f);

        StartCoroutine("SpawnObstaculo");
        yield return new WaitForSeconds(1.5f);
        StartCoroutine("SpawnCoin");
    }

    IEnumerator SpawnCoin()
    {
        int moedasaleatorias = Random.Range(1, 5);
        for (int contagem = 1; contagem <= moedasaleatorias; contagem++)
        {
            yield return new WaitForSeconds(_coinTempo);
            GameObject _objetoSpawn = Instantiate(_coinPrefab);
            _objetoSpawn.transform.position = new Vector3(_objetoSpawn.transform.position.x, _objetoSpawn.transform.position.y, 0);
        }
    }


    IEnumerator SpawnDiamond()
    {
        yield return new WaitForSeconds(_diamondTempo);

        GameObject diamond = Instantiate(_diamondPrefab);
        diamond.transform.position = new Vector3(diamond.transform.position.x, diamond.transform.position.y, 0);

        StartCoroutine("SpawnDiamond");
    }

    public void Pontos(int _qtdPontos)
    {
        _pontosPlayer += _qtdPontos;
        
        if(_pontosPlayer != 100)
        {
         _txtPontos.text = _pontosPlayer.ToString();
        }
        else
        {
         _pontosPlayer = 101;
         _txtPontos.text = _pontosPlayer.ToString();
        }
        PlayerPrefs.SetInt("score", _pontosPlayer);
        
    }

    void DistanciaPercorrida()
    {
        _metrosPercorridos++;
        _txtMetros.text = _metrosPercorridos.ToString() + " M";
        PlayerPrefs.SetInt("metros", _metrosPercorridos);

        if ((_metrosPercorridos % 50) == 0)
        {
            if (_chaoVelocidade < 16) {
                _chaoVelocidade += 0.5f;
            }
            if (_obstaculoTempo > 1)
            {
                _obstaculoTempo -= 0.15f;
            }
            if (_obstaculoVelocidade < 16)
            {
                _coinVelocidade += 0.5f;
                _obstaculoVelocidade += 0.5f;
            }

        }
    }
    public void GameOver()
    {
        _isOver = true;
        _gameOverobj.SetActive(true);
        Time.timeScale = 0f;
        Score();
        
    }

    public void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            _isPaused = !_isPaused;
            _pauseObj.SetActive(_isPaused);
        }
        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void Bpause()
    {
        _isPaused = !_isPaused;
        _pauseObj.SetActive(_isPaused);
        if (_isPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }
    public void FecharJogo()
    {
        // Fecha o jogo
        Application.Quit();
    }

    public void Score()
    {
        _SaveMetros.text = _metrosPercorridos.ToString() + " M";
        _SavePontos.text = _pontosPlayer.ToString();

        _score = (_metrosPercorridos/100) * _pontosPlayer ;
        _Score.text = "Score: " + _score.ToString();
        PlayerPrefs.SetInt("SaveScore", _score);


    }
    

    public void AtivarImagem()
    {
        _isNight = PlayerPrefs.GetInt("isNight");

        if (_isNight == 1)
        {
            _porDoSol.SetActive(true);
            Debug.Log("Ativou o bg");
            _isNight = 2;
        }
        else
        {
          _porDoSol.SetActive(false);
          Debug.Log("Desativou o bg");
            _isNight = 1;
        }

        PlayerPrefs.SetInt("isNight", _isNight);


    }


void RecuperaValor()
    {
        _metrosPercorridos = _metrosPercorridos + PlayerPrefs.GetInt("metros");
        if(PlayerPrefs.GetInt("Perdeu") == 4)
        {
            _fxGame.PlayOneShot(_fxWinTask);
            _pontosPlayer = _pontosPlayer + PlayerPrefs.GetInt("score")+50;
            _txtPontos.text = _pontosPlayer.ToString();
            AtivarImagem();
        }
        else if (PlayerPrefs.GetInt("Perdeu") == 8)
        {
            GameOver();
        }
       


    }

    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
