using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentShuffler
{
    public class Students
    {
        public static readonly IMongoCollection<Student> _students 
            = MongoDBTransactions.db.GetCollection<Student>("Students");

        public static async Task<List<Student>> GetAllStudents()
        {
            return await _students.Find(new BsonDocument()).ToListAsync();
        }

        public static async Task<List<Student>> GetStudentByFieldAsync(string key,BsonDocument condition)
        {
            //var filter = Builders<Student>.Filter.Eq(key, value).ToBsonDocument();
            var filter = new BsonDocument(key, condition);

            var result = await _students.Find(filter).ToListAsync();

            return result;
            
        }

        public static async Task<List<Student>> GetStudents(int startingFrom, int count)
        {
            var result = await _students.Find(new BsonDocument()).Skip(startingFrom).Limit(count).ToListAsync();

            return result;
        }

        public static async Task InsertStudent(Student student)
        {
            await _students.InsertOneAsync(student);
        }

        public static async Task<bool> DeleteStudentById(string id)
        {
            var filter = Builders<Student>.Filter.Eq("_id", id);
            var result = await _students.DeleteOneAsync(filter);

            return result.DeletedCount != 0;
        }

        public static async Task<long> DeleteAllStudents()
        {
            var filter = new BsonDocument();
            var result = await _students.DeleteManyAsync(filter);
            return result.DeletedCount;
        }

        public static async Task<bool> UpdateStudent(string id, string updateField, string updateValue)
        {
            var filter = Builders<Student>.Filter.Eq("_id", id);
            var update = Builders<Student>.Update.Set(updateField, updateValue);
            var result = await _students.UpdateOneAsync(filter, update);

            return result.ModifiedCount != 0;
        }

        public static async Task CreateIndexOnCollection(IMongoCollection<BsonDocument> collection,string field)
        {
            var keys = Builders<BsonDocument>.IndexKeys.Ascending(field);

            await collection.Indexes.CreateOneAsync(keys);
        }
    }
}
