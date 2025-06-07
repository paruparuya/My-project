using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private TMP_Text winOrLoseText;
    [SerializeField] private PlayerController playerController;
    //[SerializeField] private EnemyController[] enemies;  //複数の敵を動かすときのリスト
    [SerializeField] private Transform spawnPoint;
    [Header("敵プレハブリスト")]
    [SerializeField] private List<GameObject> enemyPrefabs = new List<GameObject>();
    private EnemyController enemyController;  
    private int currentEnemyIndex = 0;
    private bool gameResult = false;  //勝敗表示を更新しないように
    


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
            winOrLoseText.gameObject.SetActive(true);
            winOrLoseText.text = "Win";
            gameResult = true;
        }
    }

    public void LoseText()
    {
        if (!gameResult)
        {
            winOrLoseText.gameObject.SetActive(true);
            winOrLoseText.text = "Lose";
            gameResult = true;
        }
    }

    public void GameStrat()
    {
        SceneManager.LoadScene("MainMode");
    }

    private void SpawnEnemy()
    {
        if (currentEnemyIndex >= enemyPrefabs.Count)
        {
            SceneManager.LoadScene("Finish");
            Debug.Log("すべての敵が登場しました");
            return;
        }

        GameObject enemyObj = Instantiate(enemyPrefabs[currentEnemyIndex], spawnPoint.position, Quaternion.identity);
        enemyController = enemyObj.GetComponent<EnemyController>();
        currentEnemyIndex++;
        gameResult = false;
        playerController.canControl = false;
    }

    private IEnumerator StartCountdown()
    {
        SpawnEnemy();
        winOrLoseText.gameObject.SetActive(false); 
        countdownText.gameObject.SetActive(true);


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
        countdownText.gameObject.SetActive(false);

        /*foreach (EnemyController enemy in enemies)  
        {
            enemy.canControl = true;
        }*/

    }

    public void OnEnemyDefeated()
    {
        StartCoroutine(NextEnemy());
    }

    private IEnumerator NextEnemy()
    {
        yield return new WaitForSeconds(5f);
        StartCoroutine(StartCountdown());
    }
}
