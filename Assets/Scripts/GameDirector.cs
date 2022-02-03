using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    // 点数 符号なし整数
    public uint score = 0;

    // Textのプレファブ
    [SerializeField]
    GameObject textPref = null;
    // コンポーネントを毎回取得することを回避する
    UnityEngine.UI.Text text;

    // ゲームオーバーのプレファブ
    [SerializeField]
    GameObject gameOverPref = null;

    // ゲームオーバーフラグ
    bool isOver;
    // プロパティ
    public bool IsOver
    {
        // 取得する
        get => isOver;
        // 設定する
        set {
            isOver = value;
            // ゲームオーバーの文字を表示 or 非表示にする
            gameOverPref.SetActive(value);
        }
    }


    // Updateの前に一度だけ行われる処理
    void Start()
    {
        // textPrefのTextコンポーネントを取得
        text = textPref.GetComponent<UnityEngine.UI.Text>();
        // テキストコンポーネントの取得に失敗
        if (text is null) {
            Debug.LogError("テキストのプレファブが設定されていません");
        }

        // ゲームオーバーのフラグを無効にする
        IsOver = false;
    }

    // 毎フレーム行われる処理
    void Update()
    {
        // ゲームオーバーなら
        if (IsOver) {

        }

        // スコアを画面に表示させる
        ShowScore();

        // Escapeキーでゲームを終了
        if(Input.GetKeyDown(KeyCode.Escape))  this.Quit();
    }

    // 点数を画面に表示する
    void ShowScore()
    {
        string str = "SCORE：";
        str += score.ToString();
        text.text = str;
    }

    // アプリを終了させる
    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
        UnityEngine.Application.Quit();
#endif
    }
}
