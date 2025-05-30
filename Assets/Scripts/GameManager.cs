using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }


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
}
