using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class NameData
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)] public ObjectId _id { get; set; }
    
    
    [BsonElement("Name")]
    public string Name { get; set; }
    
    [BsonElement("Age")]
    public string Idade { get; set; }

    public NameData(string name, string age)
    {
        this.Name = name;
        this.Idade = age;
    }
}
