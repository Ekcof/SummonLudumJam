using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class GoogleSheetsParser
{
    public static ResultObject[] ParseGoogleSheet(string googleSheetURL)
    {
        List<ResultObject> localizationList = new List<ResultObject>();

        UnityWebRequest www = UnityWebRequest.Get(googleSheetURL);
        www.SendWebRequest();

        while (!www.isDone) { }

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка загрузки данных из Google Sheets: " + www.error);
        }
        else
        {
            string data = www.downloadHandler.text;
            localizationList = ParseData(data);
        }

        return localizationList.ToArray();
    }

    private static List<ResultObject> ParseData(string data)
    {
        List<ResultObject> ResultList = new List<ResultObject>();

        string[] lines = data.Split('\n');

        for (int i = 1; i < lines.Length; i++)
        {
            List<string> cells = SplitCsvLine(lines[i]);

            if (cells.Count >= 4)
            {
                ResultObject key = new ResultObject();
                key.Id = cells[0];
                key.RuLocalization = cells[1];
                key.EnLocalization = cells[2];

                List<int> combination = new List<int>();
                for (int j = 3; j < cells.Count; j++)
                {
                    if (int.TryParse(cells[j], out int number))
                    {
                        combination.Add(number);
                    }
                }
                key.Combination = combination.ToArray();

                ResultList.Add(key);
            }
        }

        return ResultList;
    }

    private static List<string> SplitCsvLine(string line)
    {
        var result = new List<string>();
        var inQuotes = false;
        var value = new StringBuilder();

        for (int i = 0; i < line.Length; i++)
        {
            var c = line[i];

            if (c == '\"')
            {
                Debug.Log("Found quote");
                inQuotes = !inQuotes;
            }
            else
            if (c == ',' && !inQuotes)
            {
                result.Add(value.ToString());
                value.Length = 0;
            }
            else
            {
                value.Append(c);
            }
        }

        result.Add(value.ToString());

        for (int i = 0; i < result.Count; i++)
        {
            result[i] = result[i].Trim();
            if (result[i].StartsWith("\"") && result[i].EndsWith("\""))
            {
                result[i] = result[i].Substring(1, result[i].Length - 2);
            }
        }

        return result;
    }
}