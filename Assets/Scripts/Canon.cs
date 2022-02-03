using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canon : MonoBehaviour
{
    // 弾のプレファブ
    [SerializeField]
    GameObject bulletPrefab = null;

    // 弾の初速
    [SerializeField]
    float shotPower = 2000.0f;

    // 角度が変わる速度
    [SerializeField]
    float rotateSpeed = 1.0f;

    // 大砲の角度の最小と最大
    [SerializeField] [Range(5.0f, 150.0f)]
    float minV = 10, maxV = 100.0f;


    // 自身が生成されたときに行われる処理
    void Awake()
    {
        // 最大角度が最小角度より小さいとき、エラーを出力する
        /// if は1行だけなら{}が必要ない
        if (minV > maxV)  Debug.LogError("最大角度が最小角度より小さく設定されています");
    }

    // 毎フレーム行われる処理
    void Update()
    {
        RotateAxis();

        // ボタンを押したとき
        /// KeyCode.〇〇がボタン
        /// はじめはスペースキーが設定されています。
        if (Input.GetKeyDown(KeyCode.Space)) {
            Shot();
        }
    }

    // 矢印キーで向きを変える
    void RotateAxis()
    {
        // 入力を検出
        Vector2 input = Vector3.zero;
        input.x = Input.GetAxis("Vertical");    // 縦入力をx軸回転に反映
        input.y = Input.GetAxis("Horizontal");  // 横入力をy軸回転に反映

        // 入力と速度で回転量を計算
        Vector3 angle = transform.rotation.eulerAngles;
        angle.x -= input.x * rotateSpeed;
        angle.y += input.y * rotateSpeed;

        // 最小角度と最大角度を超えないように調整
        if (angle.x < minV) angle.x = minV; // 最小 (上)
        if (angle.x > maxV) angle.x = maxV; // 最大 (下)

        // 回転させる
        transform.rotation = Quaternion.Euler(angle);
    }

    // 弾を撃つ
    void Shot()
    {
        // ゲームに弾を登場させる
        var instance = GameObject.Instantiate(bulletPrefab);
        // 弾のRigidbodyを取得する
        var rigidbody = instance.GetComponent<Rigidbody>();

        // 弾の位置を調整
        float scale = transform.localScale.x;   // 弾の直径を調べる
        instance.transform.position = this.transform.position + (transform.up * scale);

        // 弾に加える力の計算
        var force = transform.up * shotPower;
        // 弾に力を加える
        rigidbody.AddForce(force);
    }

    // 何かに触れたときに行われる処理
    void OnTriggerEnter(Collider other)
    {
        // ぶつかった相手が敵だったとき
        if (other.tag == "Enemy") {
            // 自身を非表示(無効)にする
            gameObject.SetActive(false);

            // Directorを探す
            var director = GameObject.Find("Director");
            // ゲームオーバーフラグを立てる
            director.GetComponent<GameDirector>().IsOver = true;
        }
    }
}
