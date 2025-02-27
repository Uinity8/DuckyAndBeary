using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Extensions
{
    static public string FormatTime(this float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return $"{minutes:00}:{seconds:00}";
    }
}
