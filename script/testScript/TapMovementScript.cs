using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapMovementScript : MonoBehaviour
{
    private bool isMoving = false; // 移動中かどうかのフラグ
    private Vector3 targetPosition; // タップした位置の座標

    private Vector3 targetBackPosition; // タップした位置の座標
    float movementSpeed = 5f; // 移動速度

    public GameObject targetObj;//メインで動かすオブジェクト
    float traceDistance = 0.001f; // 軌跡の間隔
    private Vector3[] tracePoints; // 自機の軌跡の座標配列
    private int currentTraceIndex = 0; // 現在の軌跡インデックス
    private Transform[] segments;//追随してくるオブジェクト郡
    private Vector3[] segmentPositions;

    public GameObject box;//このボックスの子要素が追随してくる

    public int objGroupNum;//ボックスの子要素をカウントして

    private void Start()
    {
        //今回は4体を想定している
        segments = new Transform[box.transform.childCount];
        segmentPositions = new Vector3[segments.Length];

        objGroupNum = box.transform.childCount;

        // スネークのセグメントの位置を取得
        for (int i = 0; i < segments.Length; i++)
        {
            segments[i] = box.transform.GetChild(i);
            segmentPositions[i] = segments[i].position;
        }
        // pastPositions = new Queue<Vector3>();
        pastPositions = new Dictionary<int, Vector3>();

    }
    Vector3 swipeDelta;//前のいち情報との差を入れる
    float swipeThreshold = 0.1f;
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // タップした位置を取得
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // 高さは現在の位置に合わせる
            targetBackPosition = targetPosition;
            // 移動フラグを立てて移動開始
            isMoving = true;
            isFollowing = true;

        }
        if (Input.GetMouseButton(0))
        {
            // タップした位置を取得
            targetPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            targetPosition.z = 0; // 高さは現在の位置に合わせる
            swipeDelta = targetPosition - targetBackPosition;
            Debug.Log("swipeDelta.magnitude" + swipeDelta.magnitude + ":" + swipeThreshold);
            if (swipeDelta.magnitude < swipeThreshold)
            {
                isFollowing = false;
            }
            else if (swipeDelta.magnitude >= swipeThreshold)
            {
                isFollowing = true;
                Debug.Log("on");
                targetBackPosition = targetPosition;
                //追随するオブジェクトの参照する位置情報配列番号を指定追加
                for (int i = 0; i < objGroupNum; i++)
                {
                    pos(i, i * 10 + 10);
                }

            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            isFollowing = false;

        }

        if (isMoving)
        {
            // ターゲットの位置に向かって移動する

            targetObj.transform.position = Vector3.MoveTowards(targetObj.transform.position, targetPosition, 1.5f);
        }
    }


    int traceDelayFrames = 50; // 取得するフレーム数
    private Dictionary<int, Vector3> pastPositions;

    private bool isFollowing = false;
    private int currentFrame = 0; // 現在のフレーム数
    void pos(int n, int frame)
    {
        if (!isFollowing)
        {
            return; // 追尾停止フラグが立っている場合は処理を終了する
        }
        // 現在の主人公の位置情報を辞書に追加
        pastPositions[currentFrame] = targetObj.transform.position;

        // 遅延フレーム数を超えるまで古い要素を削除
        int oldestFrame = currentFrame - traceDelayFrames;
        if (oldestFrame >= 0)
        {
            pastPositions.Remove(oldestFrame);
        }

        // 遅延フレーム数後の位置情報を取得してオブジェクトを移動
        int targetFrame = currentFrame - frame;
        if (pastPositions.ContainsKey(targetFrame))
        {
            Vector3 targetPosition = pastPositions[targetFrame];
            //配列に入ったオブジェクトに指定した前の位置に移動させる
            segments[n].transform.position = targetPosition;
        }

        currentFrame++;
    }
}
