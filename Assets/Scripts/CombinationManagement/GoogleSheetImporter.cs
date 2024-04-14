using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEditor.AssetImporters;

[CreateAssetMenu(fileName = "GoogleSheetImporter", menuName = "Localization/Google Sheet Importer")]
public class GoogleSheetImporter : ScriptableObject
{
    public ResultHandler _resultHandler; // ссылка на ScriptableObject для хранения данных
    private const string googleSheetURL = "https://docs.google.com/spreadsheets/d/e/2PACX-1vQVvz0THR9VaHFaWAJkN_3MZXEfemv6Z_VgJbnb59PDpTo3Yd5rgCOj-IWNPH6u-SR27RfJnQ5TUNka/pub?gid=0&single=true&output=csv";

    public void ImportData()
    {
        if (_resultHandler != null)
        {
            _resultHandler.Combinations = GoogleSheetsParser.ParseGoogleSheet(googleSheetURL);
            EditorUtility.SetDirty(_resultHandler); // Помечаем объект как измененный, чтобы Unity сохранял его
        }
    }
}

[CustomEditor(typeof(GoogleSheetImporter))]
public class GoogleSheetImporterEditor : Editor
{

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GoogleSheetImporter importer = (GoogleSheetImporter)target;

        if (GUILayout.Button("Import ResultsData from Google Sheet"))
        {
            importer.ImportData();
        }
    }
}