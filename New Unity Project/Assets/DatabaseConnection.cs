using UnityEngine;
using MongoDB.Driver;

public class DatabaseConnection : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://maquine:adastra123@cluster0.l67bkdp.mongodb.net/test");

    public IMongoDatabase Database;
    public IMongoCollection<NameData> _nameData;

    void Start()
    {
        GetDatabase();
    }

    public void GetDatabase()
    {
        Database = client.GetDatabase("Tutorial");
        _nameData = Database.GetCollection<NameData>("NameData");
    }
    
    public void CreateDocument(string name, string idade)
    {
        NameData usuario = new NameData(name,idade);
        _nameData.InsertOne(usuario);
    }
}


