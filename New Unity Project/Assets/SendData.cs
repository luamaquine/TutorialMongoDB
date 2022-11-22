using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using MongoDB.Driver;
using UnityEngine.UI;

public class SendData : MonoBehaviour
{
    //Inputs para obter dados escritos
    public TMP_InputField TextName;
    public TMP_InputField TextIdade;
    public TMP_InputField TextPesquisar;
    //Textfielsds para exibição dos dados
    public Text Textshowname;
    public Text Textshowname2;
    public Text TextShowIdade;
    //variaveis que receberão os dados do inputs
    private string nameToDatabase;
    private string idadeToDatabase;
    //Conexão do database
    public DatabaseConnection DatabaseConnection;
    //Paneis para melhor visualização
    public GameObject panelHistorico;
    public GameObject panelPesquisar;
    //Visualiazação do historico de todos os nomes que irão aparecer na consulta
    public List<string> documents;

    private void Start()
    {
        DatabaseConnection.GetDatabase();
        ShowHistory();
        ShowName();
    }
    
    public void SendInfo()
    {
        //variáveis que receberão o input
        nameToDatabase = TextName.text;
        idadeToDatabase = TextIdade.text;
        
        //Criação do documento com as variáveis
        DatabaseConnection.CreateDocument(nameToDatabase, idadeToDatabase);
        Debug.Log("enviou");
        
        //Limpeza dos inputs
        TextName.text = "";
        TextIdade.text = "";
    }

    public void ShowHistory()
    {
        //variável que irá fazer a pesquisa e trará todas as informações dos nomes
        var filtro2 = Builders<NameData>.Filter.Where(x => x.Name != null);
        IAsyncCursor<NameData> hs2 = DatabaseConnection._nameData.FindSync(filtro2);
        string nomes = "";
        
        hs2.ForEachAsync((bsonDoc) =>
        {
            //Adição de todos os nomes do documento Bson a uma lista de documentos
            nomes = bsonDoc.Name;
            documents.Add(nomes);
        });
        
        foreach (string name in documents)
        {
            //Exibição dos nomes 
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
        //variavel que receberá dados do input
        var pesquisar = TextPesquisar.text;
        
        //Filtro que fará a comparação do que foi posto no input e se for um nome irá seguir este filtro
        var filtro2 = Builders<NameData>.Filter.Where(x => x.Name == pesquisar);
        IAsyncCursor<NameData> dt2 = DatabaseConnection._nameData.FindSync(filtro2);
        string nome = "";
        string idade = "";
        dt2.ForEachAsync((bsonDoc) =>
        {
            nome = bsonDoc.Name;
            idade = bsonDoc.Idade;
        });
        
        //Filtro que fará a comparação do que foi posto no input e se for uma idade irá seguir este filtro
        var filtro3 = Builders<NameData>.Filter.Where(x => x.Idade == pesquisar);
        IAsyncCursor<NameData> dt3 = DatabaseConnection._nameData.FindSync(filtro3);
        dt3.ForEachAsync((bsonDoc) =>
        {
            nome = bsonDoc.Name;
            idade = bsonDoc.Idade;
        });
        
        //Exibição do que foi procurado
        Textshowname2.text = nome;
        TextShowIdade.text = idade;
    }
}
