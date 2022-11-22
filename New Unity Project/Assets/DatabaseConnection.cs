using UnityEngine;
using MongoDB.Driver;

public class DatabaseConnection : MonoBehaviour
{
    //Conexão via url
    MongoClient client = new MongoClient("mongodb+srv://maquine:adastra123@cluster0.l67bkdp.mongodb.net/test");
    //Variável do database
    public IMongoDatabase Database;
    //Variável da coleção
    public IMongoCollection<NameData> _nameData;

    void Start()
    {
        //Inicialização do database
        GetDatabase();
    }

    public void GetDatabase()
    {
        //Configuração do database
        Database = client.GetDatabase("Tutorial");
        _nameData = Database.GetCollection<NameData>("NameData");
    }
    
    public void CreateDocument(string name, string idade)
    {
        //Criação do Documentos com parametro name e idade
        NameData usuario = new NameData(name,idade);
        _nameData.InsertOne(usuario);
    }
}


