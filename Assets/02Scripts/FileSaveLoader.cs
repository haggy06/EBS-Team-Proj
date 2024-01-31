using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class FileSaveLoader : MonoSingleton<FileSaveLoader>
{
    private string commonDataPath;

    private new void Awake()
    {
        base.Awake();

        commonDataPath = Path.Combine(Application.persistentDataPath, "Data");

        if (!File.Exists(commonDataPath))
        {
            Debug.LogWarning("Data ���� ����");

            Directory.CreateDirectory(commonDataPath);
        }

        Debug.Log(commonDataPath);
    }

    public void SaveData(string fileName, string newData)
    {
        Debug.Log(fileName + " ������ �����մϴ�.");

        File.WriteAllText(Path.Combine(commonDataPath, fileName), newData);
    }

    public bool TryLoadData(string fileName, out string dataAdress)
    {
        if (File.Exists(Path.Combine(commonDataPath, fileName.ToString()))) // ��ο� ������ ������ ���
        {
            Debug.Log("����� " + fileName + " ������ �������� �� �����߽��ϴ�.");

            dataAdress = File.ReadAllText(Path.Combine(commonDataPath, fileName));
            return true;
        }
        else // ��ο� ������ �������� ���� ���
        {
            Debug.Log("����� " + fileName + " ������ �������� �ʽ��ϴ�.");

            dataAdress = null;
            return false;
        }
    }

    public void DelectData(string fileName)
    {
        if (File.Exists(Path.Combine(commonDataPath, fileName.ToString()))) // ��ο� ������ ������ ���
        {
            File.Delete(Path.Combine(commonDataPath, fileName.ToString()));

            Debug.Log(fileName + " ������ �����߽��ϴ�.");
        }
        else // ��ο� ������ �������� ���� ���
        {
            Debug.Log(fileName + " ������ �������� �ʽ��ϴ�.");
        }
    }
}