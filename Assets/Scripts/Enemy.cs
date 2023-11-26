using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private float moveSpeed = 10f;

    private float minY = -7f;

    [SerializeField]
    private float hp = 1f;

    public void SetMoveSpeed(float moveSpeed){
        //this.moveSpeed는 private로 정의한 moveSpeed
        //이름이 동일할 경우 this 이용할 것
        this.moveSpeed = moveSpeed;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;
        if(transform.position.y < minY){
            Destroy(gameObject);
        }
    }

    //isTrigger가 선택되어있다면 OnTriggerEnter2D
    //선택되어있지 않다면 OnCollisionEnter2D 사용

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Weapon"){
            Weapon weapon = other.gameObject.GetComponent<Weapon>();
            hp -= weapon.damage;
             if(hp <= 0){
                if(gameObject.tag == "Boss"){
                    GameManager.instance.SetGameOver();
                }
                Destroy(gameObject);
                Instantiate(coin, transform.position, Quaternion.identity);
             }
             Destroy(other.gameObject);
        }
    }
}
