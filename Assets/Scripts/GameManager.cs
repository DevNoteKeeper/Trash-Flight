using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private TextMeshProUGUI finalText;
    private int coin = 0;
    private int finalCoin = 0;

    [HideInInspector]
    public bool isGameOver = false;

    [SerializeField]
    private GameObject gameOverPanel;

    void Awake() {
        if(instance == null){
            instance = this;
        }
    }

    public void IncreaseCoin(){
        coin++;
        text.SetText(coin.ToString());

        if(coin % 10 == 0){
            Player player = FindAnyObjectByType<Player>();
            if(player != null){
                player.Upgrade();
            }
        }
    }

    public void SetGameOver(){
        isGameOver = true;
        EnemySpawner enemySpawner = FindAnyObjectByType<EnemySpawner>();
        if(enemySpawner != null){
            finalCoin = coin;
            finalText.SetText(finalCoin.ToString());
            enemySpawner.StopEnemyRoutine();
        }
        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel(){
        gameOverPanel.SetActive(true);

    }

    public void PlayAgain(){
        SceneManager.LoadScene("SampleScene");
    }
}
