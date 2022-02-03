using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    // ディレクターのプレファブ
    [SerializeField]
    GameObject directorPref = null;
    GameDirector director;

    // 敵のプレファブ
    [SerializeField]
    GameObject enemyPref = null;

    // ターゲットのプレファブ
    [SerializeField]
    GameObject targetPref = null;

    // 敵が出現する距離 (近)
    [SerializeField]  [Range(10.0f, 100.0f)]
    float nRange = 30.0f;
    // 敵が出現する距離 (遠)
    [SerializeField]  [Range(10.0f, 100.0f)]
    float fRange = 80.0f;

    // 敵が出現する頻度 (秒)
    [SerializeField]
    float generateWeight = 3.0f;

    // 前回生成させた時間
    float preGenerateTime;
    // 経過時間
    float elapsedTime;


    // 自分が生成されたときに行われる処理
    void Awake()
    {
        // ディレクターのプレファブが設定されていないならエラーを出力
        director = directorPref.GetComponent<GameDirector>();
        if(director is null)  Debug.LogError("ディレクターのプレファブが設定されていません");

        // 敵のプレファブが設定されていならエラーを出力
        if (enemyPref is null)  Debug.LogError("敵のプレファブが設定されていません");

        // 前回生成された時間を今の時間に設定
        preGenerateTime = Time.realtimeSinceStartup;
    }

    // 毎フレーム呼ばれる処理
    void Update()
    {
        // ゲームオーバーなら処理を中断する
        if (director.IsOver)  return;

        // 前回生成させてから、指定時間過ぎている
        if (elapsedTime >= generateWeight) {
            // ランダムな方向を取得
            float angle = Random.Range(0.0f, 360.0f);
            // 指定した距離の間でランダムな距離を取得
            float range = Random.Range(nRange, fRange);

            // 距離と角度で座標を設定
            Vector3 pos = Quaternion.Euler(0.0f, angle, 0.0f) * Vector3.one;
            pos *= range;
            pos += targetPref.transform.position;
            pos.y = 20.0f;

            // 敵を生成する
            GenerateObject(pos);
            // 経過時間をリセット
            elapsedTime = 0.0f;
        }
        // でなければ
        else{
            // 経過時間を加算する
            elapsedTime += Time.deltaTime;
        }
    }

    // 指定した位置に敵を生成する
    void GenerateObject(Vector3 pos)
    {
        var obj = GameObject.Instantiate(enemyPref);
        var enemy = obj.GetComponent<Enemy>();
        obj.transform.position = pos;
        enemy.director = director;
        enemy.target = targetPref;
    }
}
