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
            Debug.LogWarning("Data 폴더 생성");

            Directory.CreateDirectory(commonDataPath);
        }

        Debug.Log(commonDataPath);
    }

    public void SaveData(string fileName, string newData)
    {
        Debug.Log(fileName + " 파일을 저장합니다.");

        File.WriteAllText(Path.Combine(commonDataPath, fileName), newData);
    }

    public bool TryLoadData(string fileName, out string dataAdress)
    {
        if (File.Exists(Path.Combine(commonDataPath, fileName.ToString()))) // 경로에 파일이 존재할 경우
        {
            Debug.Log("저장된 " + fileName + " 파일을 가져오는 데 성공했습니다.");

            dataAdress = File.ReadAllText(Path.Combine(commonDataPath, fileName));
            return true;
        }
        else // 경로에 파일이 존재하지 않을 경우
        {
            Debug.Log("저장된 " + fileName + " 파일이 존재하지 않습니다.");

            dataAdress = null;
            return false;
        }
    }

    public void DelectData(string fileName)
    {
        if (File.Exists(Path.Combine(commonDataPath, fileName.ToString()))) // 경로에 파일이 존재할 경우
        {
            File.Delete(Path.Combine(commonDataPath, fileName.ToString()));

            Debug.Log(fileName + " 파일을 삭제했습니다.");
        }
        else // 경로에 파일이 존재하지 않을 경우
        {
            Debug.Log(fileName + " 파일이 존재하지 않습니다.");
        }
    }
}