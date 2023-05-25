using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class FileDataHandler
{
    // �f�B���N�g���̃p�X��
    private string dataDirPath = "";
    // �t�@�C���̖��O
    private string dataFileName = "";

    private bool encryptData = false;
    private string codeWord = "chicken";

    // �R���X�g���N�^�������ɏ�2�̕ϐ����Z�b�g����
    public FileDataHandler(string _dataDirPath, string _dataFileName, bool _encryptData)
    {
        this.dataDirPath = _dataDirPath;
        this.dataFileName = _dataFileName;
        this.encryptData = _encryptData;
    }

    public void Save(GameData _data)
    {
        // �p�X��A��
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            // Path.GetDirectoryName()��fullPath���쐬���邽�߂̐e�f�B���N�g���̃t�H���_���擾 => "C:\Users\name\Documents"
            // Directory.CreateDirectory()�͏�L��"C:\Users\name\Documents"�����݂��Ȃ��ꍇ�ɁA�����t�H���_������Ă����
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //�@_data��Json�`���ɕϊ�����������dataToStore�ɓ���
            string dataToStore = JsonUtility.ToJson(_data, true);

            if (encryptData)
            {
                dataToStore = EncryDecypt(dataToStore);
            }

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                //dataToStore��data.json�ɏ�������
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        // try�̒��ŃG���[���o���炱�������Ă΂��
        catch (Exception e)
        {
            // ���O�\��
            Debug.Log(e);
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData loadData = null;

        // fullPath�����݂��邩�`�F�b�N
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                // FireMode.Open�ŊJ�����[�h��data.json���J��
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        // ReadToEnd�͍ŏ�����Ō�܂Ńf�[�^��ǂݍ���
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                if (encryptData)
                {
                    dataToLoad = EncryDecypt(dataToLoad);
                }

                // GameData�^��Json�t�@�C����ϊ�����
                loadData = JsonUtility.FromJson<GameData>(dataToLoad);
            }

            catch (Exception e)
            {
                Debug.Log(e);
            }
        }
        // �G���[���łȂ����Json����ǂݍ��񂾃f�[�^��GameData�N���X�̒l�Ƃ��ċA��
        return loadData;
    }

    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }

    private string EncryDecypt(string _data)
    {
        string modifiedData = "";

        for (int i = 0; i < _data.Length; i++)
        {
            modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]); 
        }

        return modifiedData;
    }
}