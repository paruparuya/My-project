using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text winOrLoseText;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private EnemyController enemyController;
    private bool gameResult = false;


    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); 
        }

        StartCoroutine(StartCountdown());
    }


    void Update()
    {

    }

    public void WinText()
    {
        if (!gameResult)
        {
            winOrLoseText.text = "Win";
            gameResult = true;
        }
    }

    public void LoseText()
    {
        if (!gameResult)
        {
            winOrLoseText.text = "Lose";
            gameResult = true;
        }
    }

    public void GameStrat()
    {
        SceneManager.LoadScene("MainMode");
    }

    private IEnumerator StartCountdown()
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1f);

        countdownText.text = "2";
        yield return new WaitForSeconds(1f);

        countdownText.text = "1";
        yield return new WaitForSeconds(1f);

        countdownText.text = "Start!";
        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);
        playerController.canControl = true;
        enemyController.canControl = true;

    }
}
