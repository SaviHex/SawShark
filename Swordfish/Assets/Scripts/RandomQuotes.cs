using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Linq;

public class RandomQuotes
{
    private TextAsset file;
    public string[] entries;
    public List<int> ignoreEntries;

    public RandomQuotes(TextAsset file)
    {
        this.file = file;
        entries = Regex.Split(this.file.text, "\r\n");
        ignoreEntries = new List<int>();
    }

    public string PickRandomQuote()
    {
        var choiceSet = ExcludeIndexesFrom(entries, ignoreEntries.ToArray());
        int i = Random.Range(0, choiceSet.Length);
        //Debug.Log("Choice: " + i + " = " + choiceSet[i]);
        return choiceSet[i];
    }

    private string[] ExcludeIndexesFrom(string[] array, int[] ignoredIndexes)
    {
        List<string> result = array.ToList();

        for(int i = 0; i< ignoredIndexes.Length; i++)
        {
            result.Remove(array[i]);
        }

        return result.ToArray();
    }  
}
