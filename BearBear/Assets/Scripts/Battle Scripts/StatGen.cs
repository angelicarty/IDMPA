using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Stat { MHP, ATK, DEF, SATK, SDEF, SPD }

public class StatGen : MonoBehaviour
{//class of functions related to stat gain as a result of combat

    public int value_scale;//amount stats increase
    public int probability_scale;//amount actions contribute to probability
    public int probability_cap;//max probablity of a stat increasing
    private int[] probability;
    public void resetStatGen()
    {
        probability = new int[6];
        //probability[Random.Range(0, 7)] += 10;//random stat has a chance of increasing, regardless of actions
    }

    public void addProb(int stat)
    {
        probability[stat] += probability_scale;
    }

    public bool[] calcStat()
    {//outputs whether a stat changes, assumes delta is 1
        bool[] table = new bool[probability.Length];
        int value = 0;
        for (int i = 0; i < probability.Length; i++)
        {
            if (probability[i] > probability_cap)
            {
                probability[i] = probability_cap;
            }
            value = Random.Range(0, 101);
            if (value <= probability[i])
            {
                table[i] = true;
            }
            else
            {
                table[i] = false;
            }

        }
        return table;
    }
}
