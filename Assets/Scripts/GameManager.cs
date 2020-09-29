using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject spawnMenu;
    [SerializeField] private GameObject[] prefabSlime;

    [SerializeField] private int[] populationSlime;

    [SerializeField] private TMP_Text textSlimeA;
    [SerializeField] private TMP_Text textSlimeB;
    [SerializeField] private TMP_Text textSlimeC;
    [SerializeField] private TMP_Text textSlimeTotal;

    private float playTime; // Essentially score, how long player survived
    [SerializeField] private TMP_Text timerText;

    private void Awake()
    {
        spawnMenu.SetActive(false);
    }

    private void Start()
    {
        populationSlime = new int[3];
        populationSlime[0] = 0;
        populationSlime[1] = 0;
        populationSlime[2] = 0;
        playTime = 0.0f;
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
        playTime += Time.deltaTime;
        timerText.text = playTime.ToString("F2"); // 2 d.p.
    }

    public void SpawnSlime(int num)
    {
        Vector3 currMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Instantiate(prefabSlime[num], new Vector3(currMousePos.x, currMousePos.y, 0f), Quaternion.identity);
        spawnMenu.SetActive(false);

        populationSlime[num]++;
        UpdateHUD();
    }

    void UpdateHUD()
    {
        textSlimeA.text = "Slime A: " + populationSlime[0].ToString();
        textSlimeB.text = "Slime B: " + populationSlime[1].ToString();
        textSlimeC.text = "Slime C: " + populationSlime[2].ToString();
        textSlimeTotal.text = "Total     : " + (populationSlime[0] + populationSlime[1] + populationSlime[2]).ToString();
    }

}
