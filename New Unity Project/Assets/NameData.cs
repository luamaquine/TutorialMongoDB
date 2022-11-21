using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class NameData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] public ObjectId _id { get; set; }
    
    
    [BsonElement("UserName")]
    public string Name { get; set; }
    
    [BsonElement("Email")]
    public string Idade { get; set; }

    public NameData(string name, string age, string password, int coinsWallet)
    {
        this.Name = name;
        this.Idade = age;
    }
}
