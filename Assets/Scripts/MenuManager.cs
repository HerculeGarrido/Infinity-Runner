using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;


[Serializable]

public class MenuManager : MonoBehaviour
{
    [SerializeField] private string nomeDaCena;
    public List<GameObject> telas = new List<GameObject>();
    private int indiceCenaAtual;
    private GameObject telaAtual;
    private bool isLogin;


    public void Iniciar()
    {
        SceneManager.LoadScene(nomeDaCena);
    }
    void Start()
    {
        // Certifique-se de que há pelo menos uma tela
        if (telas.Count > 0)
        {
            // Inicialmente, exibe a primeira tela
            MostrarTela(telas[0]);
        }
        else
        {
            Debug.LogError("Adicione pelo menos uma tela ao MenuManager.");
        }
        PlayerPrefs.SetInt("Perdeu", 0);
        PlayerPrefs.SetInt("metros", 0);
        PlayerPrefs.SetInt("score", 0);
        Debug.Log(PlayerPrefs.GetInt("metros"));
        Debug.Log(PlayerPrefs.GetInt("score"));
        Debug.Log(PlayerPrefs.GetInt("Perdeu"));

    }

    // Método para trocar para uma tela específica
    public void MostrarTela(GameObject tela)
    {
        // Oculta a tela atual
        if (telaAtual != null)
        {
            telaAtual.SetActive(false);
        }

        // Exibe a nova tela
        tela.SetActive(true);
        telaAtual = tela;
        if (telas.IndexOf(tela) == 0)
        {
            isLogin = true;
            Debug.Log("Is true");
        }
        else if(telas.IndexOf(tela) == 1)
        {
            isLogin = false;
            Debug.Log("Is false");
        }
    }

    // Método para voltar para a tela anterior
    public void VoltarParaTelaAnterior()
    {
        if (isLogin)
        {
            MostrarTela(telas[0]);
        }
        else
        {
            MostrarTela(telas[1]);
        }
    }

    public void FecharJogo()
    {
        // Fecha o jogo
        Application.Quit();
    }
}
