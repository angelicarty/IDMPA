using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileContainer : MonoBehaviour
{
    public string fileName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
