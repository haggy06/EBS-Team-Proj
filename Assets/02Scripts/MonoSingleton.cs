using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.IO;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Inst
    {
        get
        {
            if (instance == null)
            {
                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null) // 씬 자체에 컴포넌트 전혀 없을 경우
                {
                    Debug.Log(typeof(T).Name + " 생성");

                    Instantiate(Resources.Load<T>(Path.Combine("MonoSingletons", typeof(T).Name)));

                    instance = (T)FindObjectOfType(typeof(T));
                }
            }
            return instance;
        }
    }

    public void Awake()
    {
        if (transform.parent != null && transform.root != null) // 이 오브젝트에 부모 오브젝트가 존재하는 경우
        {
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}