using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 誰に向かって進んでいくか
    public GameObject target = null;

    // ディレクターを探すことを回避する
    public GameDirector director = null;

    // 敵が近寄ってくる速度
    [SerializeField]
    float speed = 10.0f;

    // 敵を倒したときのスコア
    [SerializeField]
    uint score = 10;

    // ライフ
    [SerializeField]
    int life = 1;

    // 生き残れる時間
    [SerializeField]
    float timeLimit = 15.0f;

    // 生成された時間
    float appearTime;


    // 自分が生成されたときに行われる処理
    void Awake()
    {
        appearTime = Time.realtimeSinceStartup;
    }

    // 毎フレーム行われる処理
    void Update()
    {
        // 制限時間を過ぎているなら
        if (CheckTimeLimit()) {
            // 自身を削除する
            GameObject.Destroy(gameObject);
        }

        // ライフが0以下のとき
        if (life <= 0) {
            // 自身を削除する
            GameObject.Destroy(gameObject);
            return; // 処理を中断
        }

        // ターゲットに近づく
        MoveToTarget();
    }

    // 制限時間を過ぎているか確認
    bool CheckTimeLimit()
    {
        // 生成されてからの経過時間を計算
        float elapsedTime = Time.realtimeSinceStartup - appearTime;
        return  elapsedTime >= timeLimit;
    }

    // ターゲットに近づく
    void MoveToTarget()
    {
        // ターゲットが存在しないなら 処理を中断する
        if (!target.activeSelf)  return;

        // 自分が進む向きを計算
        var toVec = (target.transform.position - transform.position).normalized;
        // 移動する
        transform.position += toVec * speed * Time.deltaTime;
    }

    // 何かに触れたときに行われる処理
    void OnTriggerEnter(Collider other)
    {
        // ぶつかったものが弾だったとき
        if (other.tag == "Bullet") {
            life -= 1;
            // 弾を削除
            GameObject.Destroy(other.gameObject);
        }
    }

    // 自身が削除されるときに行われる処理
    void OnDestroy()
    {
        // ゲームオーバーでなければ、スコアを加算
        if (!director.IsOver) {
            director.score += score;
        }
    }
}
