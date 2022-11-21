using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MongoDB.Driver;
using UnityEditor.PackageManager;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{
    public TMP_InputField TextName;
    public TMP_InputField TextIdade;
    public Text Textshowname;

    private string nameToDatabase;
    private string idadeToDatabase;
    public DatabaseConnection DatabaseConnection;
    public GameObject panelHistorico;
    public List<string> documents;


    private void Start()
    {
        DatabaseConnection.GetDatabase();
        ShowHistory();
    }

    public void SendInfo()
    {
        nameToDatabase = TextName.text;
        idadeToDatabase = TextIdade.text;
        DatabaseConnection.CreateDocument(nameToDatabase, idadeToDatabase);
        Debug.Log("enviou");
        TextName.text = "";
        TextIdade.text = "";
    }

    public void ShowHistory()
    {
        var filtro2 = Builders<NameData>.Filter.Where(x => x.Name != null);
        IAsyncCursor<NameData> hs2 = DatabaseConnection._nameData.FindSync(filtro2);
        string nomes = "";
        
        hs2.ForEachAsync((bsonDoc) =>
        {
            nomes = bsonDoc.Name;
            documents.Add(nomes);
        });
        
        foreach (string name in documents)
        {
            Textshowname.text += name + " \n";
        }
    }

    public void showPanelHistorico()
    {
        panelHistorico.SetActive(true);
    }

    
}
