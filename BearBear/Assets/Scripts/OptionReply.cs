using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class OptionReply
{
    public string talkerName;
    public string option;
    [TextArea(3,10)]
    public string[] reply;
}