using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ballColliderScript : MonoBehaviour
{
    public float speed;   // ボールの速度
    float s = 5;

    public int Atk = 1;//攻撃力

    Vector3 direction = new Vector3(0, 1, 0);  // 前方向に直進する
    void Start()
    {
        speed = s;
    }
    void Update()
    {
        Move(direction);
    }
    public void TypesChange(Vector3 typeVector3)
    {
        //ボールほ進む方向をセットする
        direction = typeVector3;
    }
    void Move(Vector3 direction)
    {
        // ボールを指定した方向と速度で移動させる
        transform.position += direction * speed * Time.deltaTime;
    }
}
