using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //유니티에서 직접 값 조절 가능
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private GameObject[] weapons;
    private int weaponIndex = 0;

    [SerializeField]
    private Transform shootTransform;

    [SerializeField]
    private float shootInterval = 0.05f;
    private float lastShootTime = 0f;


    

    // Update is called once per frame
    void Update()
    {
        //키보드 제어방법 1
    //     //가로 기준
    //     float horizontalInput = Input.GetAxisRaw("Horizontal");
    //     //상하 기준
    //     float verticalInput = Input.GetAxisRaw("Vertical");

    //     //어디로 이동할지 정의
    //     Vector3 moveTo = new Vector3(horizontalInput, 0f, 0f);
    //     transform.position += moveTo * moveSpeed * Time.deltaTime;

        // // 키보드 제어 방법 2
        // Vector3 moveTo = new Vector3(moveSpeed * Time.deltaTime, 0f, 0f);
        // if(Input.GetKey(KeyCode.LeftArrow)) {
        //     transform.position -= moveTo;
        // } else if(Input.GetKey(KeyCode.RightArrow)){
        //     transform.position += moveTo;
        // }

        //충돌영역 적용 안됨
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //최솟값, 최댓값 지정해서, 이 값 이상 혹은 이하가 되면, 기존 값으로 지정됨
        float toX = Mathf.Clamp(mousePos.x, -2.35f, 2.35f);
        transform.position = new Vector3(toX, transform.position.y, transform.position.z);

        //무기 발사
        if(GameManager.instance.isGameOver == false){
            Shoot();
        }
        

    }

    void Shoot(){
        if(Time.time - lastShootTime > shootInterval){
            Instantiate(weapons[weaponIndex], shootTransform.position, Quaternion.identity);
            lastShootTime = Time.time;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss"){
            GameManager.instance.SetGameOver();
            Destroy(gameObject);
            
        } else if(other.gameObject.tag == "Coin"){
            GameManager.instance.IncreaseCoin();
            Destroy(other.gameObject);
        }
    }

    public void Upgrade(){
        weaponIndex++;
        if(weaponIndex >= weapons.Length){
            weaponIndex = weapons.Length - 1;
        }
    }

}
