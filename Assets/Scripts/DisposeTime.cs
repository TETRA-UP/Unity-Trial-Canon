using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeTime : MonoBehaviour
{
    // 消えるまでの時間
    [SerializeField]
    float timeLimit = 10.0f;

    // 生成された時間
    float appearTime;


    // 自分が生成されたときに行われる処理
    void Awake()
    {
        // シーンが始まってからの経過時間を取得
        appearTime = Time.realtimeSinceStartup;
    }

    // 毎フレーム行われる処理
    void Update()
    {
        // 経過時間が過ぎていたら
        if (CheckTimeLimit()) {
            // 自分を削除する
            GameObject.Destroy(gameObject);
        }
    }

    // 制限時間を過ぎているか確認
    bool CheckTimeLimit()
    {
        // 生成されてからの経過時間を計算
        float elapsedTime = Time.realtimeSinceStartup - appearTime;
        return  elapsedTime >= timeLimit;
    }
}
