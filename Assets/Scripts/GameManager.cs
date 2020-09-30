using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnMenu;
    [SerializeField] private GameObject[] prefabSlime;

    [SerializeField] private int[] populationSlime;
    [SerializeField] private int totalSlimes;

    [SerializeField] private TMP_Text textSlimeA;
    [SerializeField] private TMP_Text textSlimeB;
    [SerializeField] private TMP_Text textSlimeC;
    [SerializeField] private TMP_Text textSlimeTotal;

    [Space]
    [SerializeField] private GameObject panelLostGame;
    [SerializeField] private TMP_Text gameLoseText;

    [Space]
    private float playTime; // Essentially score, how long player survived
    [SerializeField] private TMP_Text timerText;

    private bool gameStop;

    private void Awake()
    {
        populationSlime = new int[3];
        populationSlime[0] = 0;
        populationSlime[1] = 0;
        populationSlime[2] = 0;
        SpawnStartingSlimes();
    }

    private void Start()
    {
        playTime = 0.0f;
        gameStop = false;
        panelLostGame.SetActive(false); 
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Right-click call up spawn menu

            Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            spawnMenu.transform.position = new Vector3(currMousePos.x, currMousePos.y, 0f);
            spawnMenu.SetActive(true);
        }
        else if (Input.GetMouseButtonDown(0)) 
        {
            // Left-click cancels spawn menu if we're NOT clicking in menu

            Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if ((currMousePos.x > spawnMenu.transform.position.x + 2f) ||
                (currMousePos.x < spawnMenu.transform.position.x) ||
                (currMousePos.y > spawnMenu.transform.position.y) ||
                (currMousePos.y < spawnMenu.transform.position.y - 2f))
            {
                spawnMenu.SetActive(false);
            }
        }

        // Update play timer
        if (!gameStop)
        {
            playTime += Time.deltaTime;
            timerText.text = playTime.ToString("F2"); // 2 d.p.
        }
        

        // Calculate total slimes here
        totalSlimes = populationSlime[0] + populationSlime[1] + populationSlime[2];

        UpdateHUD();

        // Check win/lose condition
        if (totalSlimes < 10 || totalSlimes > 30)
        {
            Debug.Log("Lose condition");
            gameStop = true;
            LostGame();
        }
    }

    public void IncrementCount(int index)
    {
        populationSlime[index]++;
        UpdateHUD();
    }

    public void DecrementCount(int index)
    {
        populationSlime[index]--;
        UpdateHUD();
    }

    public void SpawnSlime(int num)
    {
        Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefabSlime[num], new Vector3(currMousePos.x, currMousePos.y, 0f), Quaternion.identity);
        spawnMenu.SetActive(false);
        
        UpdateHUD();
    }

    void UpdateHUD()
    {
        if (gameStop) // Don't update if game stopped
        {
            return;
        }

        textSlimeA.text = "Slime A: " + populationSlime[0].ToString();
        textSlimeB.text = "Slime B: " + populationSlime[1].ToString();
        textSlimeC.text = "Slime C: " + populationSlime[2].ToString();
        textSlimeTotal.text = "Total     : " + (populationSlime[0] + populationSlime[1] + populationSlime[2]).ToString();

        if (totalSlimes > 30 || totalSlimes < 15)
        {
            textSlimeTotal.color = new Color32(255, 0, 0, 255);
        }
        else
        {
            textSlimeTotal.color = new Color32(0, 0, 0, 255);
        }
    }

    // Small issue: OnStart does not work if you drag slimes in, or spawn them in like this either apparently...
    void SpawnStartingSlimes()
    {
        float x, y;

        // Top right island:
        // 9 green slimes, 1 red slimes
        StartCoroutine(SpawnGreenSlimes()); // See function below
        for (int k = 0; k < 1; k++)
        {
            x = Random.Range(0.1f, 8.5f);
            y = Random.Range(1f, 4.5f);
            Instantiate(prefabSlime[1], new Vector3(x, y, 0f), Quaternion.identity);
        }
        
        // Bottom right island:
        // 5 blue slimes, 1 red slime
        for (int j = 0; j < 5; j++)
        {
            x = Random.Range(0.45f, 8.5f);
            y = Random.Range(-4.2f, -0.15f);

            Instantiate(prefabSlime[2], new Vector3(x, y, 0f), Quaternion.identity);
        }
        x = Random.Range(0.45f, 8.5f);
        y = Random.Range(-4.2f, -0.15f);
        Instantiate(prefabSlime[1], new Vector3(x, y, 0f), Quaternion.identity);

        // Bottom left island:
        // 1 red slime
        x = Random.Range(-8.5f, -1f);
        y = Random.Range(-4.2f, -0.6f);
        Instantiate(prefabSlime[1], new Vector3(x, y, 0f), Quaternion.identity);

        // Top left island
        // 2 red slimes, 1 blue slime
        for (int k = 0; k < 2; k++)
        {
            x = Random.Range(-8.5f, -1f);
            y = Random.Range(1f, 4.5f);
            Instantiate(prefabSlime[1], new Vector3(x, y, 0f), Quaternion.identity);
        }
        x = Random.Range(-8.5f, -1f);
        y = Random.Range(1f, 4.5f);
        Instantiate(prefabSlime[2], new Vector3(x, y, 0f), Quaternion.identity);

        UpdateHUD();
    }

    // Due to the way the mating code works, need to stagger their spawning, can't be bothered changing mating code
    IEnumerator SpawnGreenSlimes()
    {
        float x, y;
        for (int i = 0; i < 9; i++)
        {
            x = Random.Range(0.1f, 8.5f);
            y = Random.Range(1f, 4.5f);

            Instantiate(prefabSlime[0], new Vector3(x, y, 0f), Quaternion.identity);
            yield return new WaitForSeconds(0.08f);
        }
    }

    void LostGame()
    {
        panelLostGame.SetActive(true);

        if (totalSlimes >= 25)
        {
            gameLoseText.text = "You lost due to overpopulation!";
        }
        else
        {
            gameLoseText.text = "You lost due to near extinction!";
        }
    }

}
