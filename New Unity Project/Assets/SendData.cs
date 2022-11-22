using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MongoDB.Driver;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{
    public TMP_InputField TextName;
    public TMP_InputField TextIdade;
    public TMP_InputField TextPesquisar;
    public Text Textshowname;
    public Text Textshowname2;
    public Text TextShowIdade;

    private string nameToDatabase;
    private string idadeToDatabase;
    public DatabaseConnection DatabaseConnection;
    public GameObject panelHistorico;
    public GameObject panelPesquisar;
    public List<string> documents;
    public GameObject closepanel;

    private void Start()
    {
        DatabaseConnection.GetDatabase();
        ShowHistory();
        ShowName();
       
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

    public void closePanel()
    {
        panelHistorico.SetActive(false);
        panelPesquisar.SetActive(false);
    }

    public void showPanelPesquisar()
    {
        panelPesquisar.SetActive(true);
    }

    public void ShowName()
    {
        var pesquisar = TextPesquisar.text;
        var filtro2 = Builders<NameData>.Filter.Where(x => x.Name == pesquisar);
        IAsyncCursor<NameData> dt2 = DatabaseConnection._nameData.FindSync(filtro2);
        string nome = "";
        string idade = "";
        dt2.ForEachAsync((bsonDoc) =>
        {
            nome = bsonDoc.Name;
            idade = bsonDoc.Idade;
        });
        
        var filtro3 = Builders<NameData>.Filter.Where(x => x.Idade == pesquisar);
        IAsyncCursor<NameData> dt3 = DatabaseConnection._nameData.FindSync(filtro3);
        dt3.ForEachAsync((bsonDoc) =>
        {
            nome = bsonDoc.Name;
            idade = bsonDoc.Idade;
        });
        
        Textshowname2.text = nome;
        TextShowIdade.text = idade;
    }
}
