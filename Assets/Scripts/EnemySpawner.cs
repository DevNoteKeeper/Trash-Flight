using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] enemies;

    [SerializeField]
    private GameObject boss;
    private float[] arrPosX = {-2.2f, -1.1f, 0f, 1.1f, 2.2f};

    [SerializeField]
    private float spawnInterval = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        StartEnemyRoutine();
    }

    void StartEnemyRoutine(){
        StartCoroutine("EnemyRoutine");
        
    }
    public void StopEnemyRoutine(){
        StopCoroutine("EnemyRoutine");
    }
    IEnumerator EnemyRoutine(){
        yield return new WaitForSeconds(3f);

        int spawnCount = 0;
        int enemyIndex = 0;
        float moveSpeed = 5f;

        while(true){
            foreach(float posX in arrPosX){
            // int index = Random.Range(0, enemies.Length);
            spawnEnemy(posX, enemyIndex, moveSpeed);
            }

            spawnCount++;

            if(spawnCount % 10 == 0){
                enemyIndex++;
                moveSpeed += 2; 
            }

            if(enemyIndex >= enemies.Length){
                spawnBoss();
                enemyIndex = 0;
                moveSpeed = 5f;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
        
        
    }
    void spawnEnemy(float posX, int index, float moveSpeed){
        Vector3 spawnPos = new Vector3(posX, transform.position.y, 0f);

        if(Random.Range(0, 5) == 0){
            index++;
        }

        if(index >= enemies.Length){
            index = enemies.Length-1;
        }

        GameObject enemyObject = Instantiate(enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed);
    }

    void spawnBoss(){
        Instantiate(boss, transform.position, Quaternion.identity);

    }

}
