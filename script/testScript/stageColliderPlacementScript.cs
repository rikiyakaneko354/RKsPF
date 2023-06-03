using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stageColliderPlacementScript : MonoBehaviour
{
    //画面ぎりぎりに壁を作るスクリプト
    void Start()
    {
        // カメラを取得
        Camera mainCamera = Camera.main;

        // 画面の左下と右上のワールド座標を計算
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // ステージの左端にオブジェクトを設置
        Vector3 leftBoundaryPosition = new Vector3(bottomLeft.x - 2.5f, (bottomLeft.y + topRight.y) / 2f, 0f);
        GameObject leftBoundaryObject = new GameObject("LeftBoundary");
        leftBoundaryObject.transform.position = leftBoundaryPosition;
        leftBoundaryObject.transform.localScale = new Vector3(5f, topRight.y - bottomLeft.y+5, 5f);
        leftBoundaryObject.AddComponent<BoxCollider>();

        // ステージの右端にオブジェクトを設置
        Vector3 rightBoundaryPosition = new Vector3(topRight.x + 2.5f, (bottomLeft.y + topRight.y) / 2f, 0f);
        GameObject rightBoundaryObject = new GameObject("RightBoundary");
        rightBoundaryObject.transform.position = rightBoundaryPosition;
        rightBoundaryObject.transform.localScale = new Vector3(5f, topRight.y - bottomLeft.y+5, 5f);
        rightBoundaryObject.AddComponent<BoxCollider>();
        // ステージの上壁を作成
        Vector3 topBoundaryPosition = new Vector3((bottomLeft.x + topRight.x) / 2f, topRight.y + 2.5f, 0f);
        GameObject topBoundaryObject = new GameObject("TopBoundary");
        topBoundaryObject.transform.position = topBoundaryPosition;
        topBoundaryObject.transform.localScale = new Vector3(topRight.x - bottomLeft.x + 5f, 5f, 5f);
        topBoundaryObject.AddComponent<BoxCollider>();
        // ステージの下壁を作成
        Vector3 bottomBoundaryPosition = new Vector3((bottomLeft.x + topRight.x) / 2f, bottomLeft.y - 2.5f, 0f);
        GameObject bottomBoundaryObject = new GameObject("BottomBoundary");
        bottomBoundaryObject.transform.position = bottomBoundaryPosition;
        bottomBoundaryObject.transform.localScale = new Vector3(topRight.x - bottomLeft.x + 5f, 5f, 5f);
        bottomBoundaryObject.AddComponent<BoxCollider>();

    }
    //カメラが動いた時に画面にあわせる
    void MoveAdjust(){
        // カメラを取得
        Camera mainCamera = Camera.main;

        // 画面の左下と右上のワールド座標を計算
        Vector3 bottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 topRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // ステージの左端の位置を更新
        Vector3 leftBoundaryPosition = new Vector3(bottomLeft.x - 2.5f, (bottomLeft.y + topRight.y) / 2f, 0f);
        GameObject leftBoundaryObject = GameObject.Find("LeftBoundary");
        leftBoundaryObject.transform.position = leftBoundaryPosition;
        leftBoundaryObject.transform.localScale = new Vector3(5f, topRight.y - bottomLeft.y+5, 5f);

        // ステージの右端の位置を更新
        Vector3 rightBoundaryPosition = new Vector3(topRight.x + 2.5f, (bottomLeft.y + topRight.y) / 2f, 0f);
        GameObject rightBoundaryObject = GameObject.Find("RightBoundary");
        rightBoundaryObject.transform.position = rightBoundaryPosition;
        rightBoundaryObject.transform.localScale = new Vector3(5f, topRight.y - bottomLeft.y +5, 5f);
        // ステージの上壁の位置を更新
        Vector3 topBoundaryPosition = new Vector3((bottomLeft.x + topRight.x) / 2f, topRight.y + 2.5f, 0f);
        GameObject topBoundaryObject = GameObject.Find("TopBoundary");
        topBoundaryObject.transform.position = topBoundaryPosition;
        topBoundaryObject.transform.localScale = new Vector3(topRight.x - bottomLeft.x + 5f, 5f, 5f);

        // ステージの下壁の位置を更新
        Vector3 bottomBoundaryPosition = new Vector3((bottomLeft.x + topRight.x) / 2f, bottomLeft.y - 2.5f, 0f);
        GameObject bottomBoundaryObject = GameObject.Find("BottomBoundary");
        bottomBoundaryObject.transform.position = bottomBoundaryPosition;
        bottomBoundaryObject.transform.localScale = new Vector3(topRight.x - bottomLeft.x + 5f, 5f, 5f);  
    }
    void Update(){
        MoveAdjust();

    }
}
