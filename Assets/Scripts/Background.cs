using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 3f;    

    // Update is called once per frame
    void Update()
    {
        //Time.deltaTime : 성능이 다른 컴퓨터에서도 동일한 속도로 이동 할 수 있게 해줌
        transform.position += Vector3.down * moveSpeed * Time.deltaTime;

        if(transform.position.y < -10){
            transform.position += new Vector3(0, 20f, 0);
        }
    }
}
