using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public Stats player;
    public string saveID;

    private string GetPath()
    {
        return @"c:\BearBear\" + saveID + ".txt";
    }

    public void LoadPlayer()
    {
        string path = GetPath();
        if (!File.Exists(path))
        {
            Debug.LogError("FILE NOT FOUND");
        }
        else
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                //order: mhp, chp, atk, def, satk, sdef, spd, pos
                s = sr.ReadLine();
                player.ModMHP(Int32.Parse(s));
                s = sr.ReadLine();
                player.SetCHP(Int32.Parse(s));
                s = sr.ReadLine();
                player.ModATK(Int32.Parse(s));
                s = sr.ReadLine();
                player.ModDEF(Int32.Parse(s));
                s = sr.ReadLine();
                player.ModSATK(Int32.Parse(s));
                s = sr.ReadLine();
                player.ModSDEF(Int32.Parse(s));
                s = sr.ReadLine();
                player.ModSPD(Int32.Parse(s));
                s = sr.ReadLine();
                string xString = s.Substring(s.IndexOf("(")+1, s.IndexOf(",")-1);
                s = s.Substring(s.IndexOf(","));
                string tempString = s.Substring(s.IndexOf(",")+1);
                String yString = tempString.Substring(0, tempString.IndexOf(","));
/*                Debug.Log((xString) + "||" + (yString));
                Debug.Log(float.Parse(xString) + "||" + float.Parse(yString));*/
                player.gameObject.transform.position = new Vector3(float.Parse(xString), float.Parse(yString), 0);
            }
        }
    }

    public void SavePlayer()
    {
        string path = GetPath();
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine(player.GetMHP().ToString());
            sw.WriteLine(player.GetCHP().ToString());
            sw.WriteLine(player.GetATK().ToString());
            sw.WriteLine(player.GetDEF().ToString());
            sw.WriteLine(player.GetSATK().ToString());
            sw.WriteLine(player.GetSDEF().ToString());
            sw.WriteLine(player.GetSPD().ToString());
            sw.WriteLine(player.gameObject.transform.localPosition.ToString());
        }
    }


    
}
