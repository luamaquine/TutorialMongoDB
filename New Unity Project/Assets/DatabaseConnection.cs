using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Driver;

public class DatabaseConnection : MonoBehaviour
{
    MongoClient client = new MongoClient("mongodb+srv://maquine:adastra123@cluster0.l67bkdp.mongodb.net/?retryWrites=true&w=majority");

    public IMongoDatabase Database;
    public IMongoCollection<NameData> _nameData;

    void Start()
    {
        GetDatabase();
    }

    private void GetDatabase()
    {
        Database = client.GetDatabase("Tutorial");
        _nameData = Database.GetCollection<NameData>("NameData");
    }
}


