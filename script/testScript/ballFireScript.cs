using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;//Addressablesを使うのに必要
using UnityEngine.ResourceManagement.AsyncOperations;
public class ballFireScript : MonoBehaviour
{
    
    public int poolSize = 100;       // オブジェクトプールのサイズ
    public List<GameObject> ballObjList;
    public List<GameObject> ballObjLList;
    public List<GameObject> ballObjRList;

    const string ballPrefabConst = "Assets/script/testScript/ball.prefab";

    void Start()
    {
        // オブジェクトプールリストを初期化する
        ballObjList = new List<GameObject>();
        ballObjLList = new List<GameObject>();
        ballObjRList = new List<GameObject>();

        //プレファブをロードしてオブジェクトプールにして配列に入れる
        loadAsset(ballPrefabConst, ballObjList, new Vector3(0, 1, 0));
        loadAsset(ballPrefabConst, ballObjLList, new Vector3(1, 1, 0));
        loadAsset(ballPrefabConst, ballObjRList, new Vector3(-1, 1, 0));
    }

    float fireInterval = 1f;     // 弾を発射する間隔

    private float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= fireInterval)
        {
            // 弾の発射
            Vector3 spawnPosition = transform.position;    // 発射位置は指定されたオブジェクトの位置とする
            Vector3 direction = new Vector3(0, 1, 0) * 1;             // 前方向に発射する例
            ForwardFire(spawnPosition, direction);
            SideFire(spawnPosition, direction);
            timer = 0f;   // タイマーリセット
        }
    }

    void ForwardFire(Vector3 spawnPosition, Vector3 direction)
    {
        // 使用可能な弾を探す
        GameObject bullet = GetInactiveBullet(ballObjList);
        if (bullet != null)
        {
            // 弾をアクティブにする
            bullet.SetActive(true);
            // 弾の位置と向きを設定する
            bullet.transform.position = spawnPosition + new Vector3(0, 0.5f, 0);
            bullet.transform.forward = direction;


        }
    }
    void SideFire(Vector3 spawnPosition, Vector3 direction)
    {
        // 使用可能な弾を探す
        GameObject bullet1 = GetInactiveBullet(ballObjLList);
        GameObject bullet2 = GetInactiveBullet(ballObjRList);
        if (bullet1 != null)
        {
            // 弾をアクティブにする
            bullet1.SetActive(true);
            // 弾の位置と向きを設定する
            bullet1.transform.position = spawnPosition + new Vector3(0, 0.5f, 0);

        }
        if (bullet2 != null)
        {
            // 弾をアクティブにする
            bullet2.SetActive(true);
            // 弾の位置と向きを設定する
            bullet2.transform.position = spawnPosition + new Vector3(0, 0.5f, 0);

        }
    }
    private AsyncOperationHandle<GameObject> prefabHandle;
    //ロードアセット
    private async void loadAsset(string adrress, List<GameObject> list, Vector3 typeVector3)
    {
        // Addressables.LoadAssetAsyncで読み込む
        prefabHandle = Addressables.LoadAssetAsync<GameObject>(adrress);

        // .Taskで読み込み完了までawaitできる
        GameObject prefab = await prefabHandle.Task;
        //オブジェクトプールを作成
        objPool(list, prefab, typeVector3);
        Addressables.Release(prefabHandle);

    }
    //玉を収容するボックス
    public GameObject atkBox;
    //オブジェクトプール
    int poolNumber = 50;
    void objPool(List<GameObject> list, GameObject instanTiateObj, Vector3 typeVector3)
    {
        //オブジェクトプールを作成
        for (int i = 0; i < poolNumber; i++)
        {
            GameObject d = Instantiate(instanTiateObj, transform.position, Quaternion.identity);
            d.transform.parent = atkBox.transform;
            //飛んでいく方向をVector３で設定
            d.GetComponent<ballColliderScript>().TypesChange(typeVector3);
            d.SetActive(false);

            list.Add(d);
        }

    }
    GameObject GetInactiveBullet(List<GameObject> poolList)
    {
        // オブジェクトプール内で非アクティブな弾を探す

        for (int i = 0; i < poolList.Count; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                return poolList[i];
            }
        }


        // 使用可能な弾が見つからない場合はnullを返す
        return null;
    }
}
