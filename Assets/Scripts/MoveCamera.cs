using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    // ターゲット(誰についていくか)
    [SerializeField]
    GameObject target = null;

    // カメラとターゲットの距離
    [SerializeField]
    float distance = 10.0f;

    // カメラの高さ
    [SerializeField]
    float height = 15.0f;


    // 毎フレーム行われる処理
    void Update()
    {
        // ターゲットが存在するなら
        if (target != null) {
            ApproachTarget();
        }
    }

    // ターゲットに近づく
    void ApproachTarget()
    {
        // ターゲットの座標
        Vector3 targetPos = target.transform.position;

        // ターゲットから離れる距離の計算
        var distVec = target.transform.up * (-distance);
        // 座標の計算
        var pos = targetPos + distVec;
        pos.y += height;

        // 移動する
        transform.position = pos;
        // 向きを変える
        transform.LookAt(target.transform.position + new Vector3(0.0f, 2.0f, 0.0f));
    }
}
