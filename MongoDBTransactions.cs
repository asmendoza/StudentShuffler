using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.GridFS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentShuffler
{
    public class MongoDBTransactions
    {
        public static readonly MongoClient client = new MongoClient("mongodb://localhost:27021");

        public static readonly IMongoDatabase db = client.GetDatabase("SchoolBSU");

        private static readonly GridFSBucket gridFS = new GridFSBucket(db, new GridFSBucketOptions
                                                        { 
                                                            BucketName = "photoID",
                                                            ChunkSizeBytes = 1048576,
                                                            WriteConcern = WriteConcern.WMajority,
                                                            ReadPreference = ReadPreference.Secondary
                                                        });

        public static IMongoCollection<BsonDocument> Collection(string collName)
        {
            IMongoCollection<BsonDocument> c = db.GetCollection<BsonDocument>(collName);
            
            return c;
        }

        public static Image ImageGridFS(string photoID)
        {
            Image p = null;
            FileStream f;
            try
            {
                var t = gridFS.DownloadAsBytesByNameAsync(photoID);
                Task.WaitAll(t);
                var bytes = t.Result;

                using(f = new FileStream(photoID, FileMode.Create))
                {
                    f.Write(bytes, 0, bytes.Length);
                    p = Image.FromStream(f);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in MongoDBTransactions.ImageGridFS() method :: " + e.Message);
            }
            return p;
        }

        public static async Task<BsonObjectId> ImageUploadAsync(string photo, string name)
        {
            BsonObjectId id = null;
            
            try
            {
                using (var fs = new FileStream(photo, FileMode.Open))
                {
                    id = await gridFS.UploadFromStreamAsync(name, fs);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in MongoDBTransanctions.ImageUploadAsync() method :: " 
                    + e.Message);
            }

            return id;
        }
    }
}
