using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MemoryController : MonoBehaviour
{
    public const int columns = 4;
    public const int rows = 2;

    public const float Xspace = 4f;
    public const float Yspace = 5f;
    public AudioSource  fxGame;
    public AudioClip    fxAcerto;
    public AudioClip    fxErro;


    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;

    private int[] Randomiser(int[] locations)
    {
        int[] array = locations.Clone() as int[];
        for (int i = 0; i < array.Length; i++)
        {
            int newArray = array[i];
            int j = Random.Range(i, array.Length);
            array[i] = array[j];
            array[j] = newArray;
        }
        return array;
    }

    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3 };
        locations = Randomiser(locations);


        Vector3 startPosition = startObject.transform.position;

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                MainImageScript gameImage;
                if (i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else
                {
                    gameImage = Instantiate(startObject) as MainImageScript;
                }

                int index = j * columns + i;
                int id = locations[index];
                gameImage.ChangeSprite(id, images[id]);

                float positionX = (Xspace * i) + startPosition.x;
                float positionY = startPosition.y - (Yspace * j); // Ajuste aqui

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
            }
        }
    }


    private MainImageScript firstOpen;
    private MainImageScript secondOpen;

    private int score = 0;
    private int attemps = 0;

    [SerializeField] private TextMesh scoreText;
    [SerializeField] private TextMesh attemptsText;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    public void imageOpened(MainImageScript startObject)
    {
        if(firstOpen == null)
        {
            firstOpen = startObject;
        }
        else
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        if (firstOpen.spriteId == secondOpen.spriteId)
        {
            score++;
            scoreText.text = "Acertos: " + score + "    +50";
            fxGame.PlayOneShot(fxAcerto);

        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstOpen.Close();
            secondOpen.Close();
            fxGame.PlayOneShot(fxErro);
        }

        
        attemps++;
        attemptsText.text = "Tentativas: " + attemps + "/8 GameOver";
        Debug.Log(firstOpen);
        firstOpen = null;
        secondOpen = null;
        LoadScene();
        Debug.Log(firstOpen);
    }

    public void LoadScene()
    {
        if (score >= 4)
        {
            Debug.Log("Score 4");
            SceneManager.LoadScene("SampleScene");
            PlayerPrefs.SetInt("Perdeu", 4); //Por não ter Prefs do tipo bool, vou controlar true e false com 4(false) e 8(true)
            Debug.Log(PlayerPrefs.GetInt("Perdeu"));
            
        }
        else if (attemps >= 8 || (attemps - score < 0))
        {
            Debug.Log("Attemps 8");
            PlayerPrefs.SetInt("Perdeu", 8); //Por não ter Prefs do tipo bool, vou controlar true e false com 4(false) e 8(true)
            SceneManager.LoadScene("SampleScene");
            Debug.Log(PlayerPrefs.GetInt("Perdeu"));

        }
       
    }
}


