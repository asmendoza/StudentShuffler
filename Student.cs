using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace StudentShuffler
{
    public class Student
    {
        [BsonId]
        public string StudNo { get; set; }

        [BsonElement("StudName")]
        public string StudName { get; set; }

        [BsonElement("StudSection")]
        public string StudSection { get; set; }

        [BsonElement("StudPhoto")]
        public BsonObjectId StudPhoto { get; set; }

        [BsonElement("Courses")]
        public BsonArray Courses { get; set; }

        [BsonElement("Recitations")]
        public BsonArray Recitations { get; set; }

        [BsonIgnore]
        public bool HasAnswered { get; set; }
    }
}
