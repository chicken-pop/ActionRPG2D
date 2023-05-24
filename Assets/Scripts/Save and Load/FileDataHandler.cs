using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    // ディレクトリのパス名
    private string dataDirPath = "";
    // ファイルの名前
    private string dataFileName = "";

    // コンストラクタ生成時に上2つの変数をセットする
    public FileDataHandler(string _dataDirPath, string _dataFileName)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
    }

    public void Save(GameData _data)
    {
        // パスを連結
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // Path.GetDirectoryName()はfullPathを作成するための親ディレクトリのフォルダを取得 => "C:\Users\name\Documents"
            // Directory.CreateDirectory()は上記の"C:\Users\name\Documents"が存在しない場合に、同じフォルダを作ってくれる
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //　_dataをJson形式に変換した文字列がdataToStoreに入る
            string dataToStore = JsonUtility.ToJson(_data, true);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                //dataToStoreをdata.jsonに書き込む
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        // tryの中でエラーが出たらこっちが呼ばれる
        catch (Exception e)
        {
            // ログ表示
            Debug.Log(e);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        // fullPathが存在するかチェック
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                // FireMode.Openで開くモードでdata.jsonを開く
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        // ReadToEndは最初から最後までデータを読み込む
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                // GameData型にJsonファイルを変換する
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }

            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        // エラーがでなければJsonから読み込んだデータがGameDataクラスの値として帰る
        return loadData;
    }
}