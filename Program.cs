using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace mongo_db_sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new MongoClient("mongodb+srv://jitu:S9gAmsDPqt63CqcM@cluster0-3qkxm.gcp.mongodb.net/test?retryWrites=true&w=majority");
            var database = client.GetDatabase("test");

            var collection = database.GetCollection<BsonDocument>("grades");
            if (collection == null)
            {
                var createTask = database.CreateCollectionAsync("grades");
                createTask.Wait();
            }

            //var collection = database.GetCollection<BsonDocument>("grades");
            var document = new BsonDocument { { "student_id", 10000 }, {
                "scores",
                new BsonArray {
                new BsonDocument { { "type", "exam" }, { "score", 88.12334193287023 } },
                new BsonDocument { { "type", "quiz" }, { "score", 74.92381029342834 } },
                new BsonDocument { { "type", "homework" }, { "score", 89.97929384290324 } },
                new BsonDocument { { "type", "homework" }, { "score", 82.12931030513218 } }
                }
                }, { "class_id", 480 }
        };

            var insertTask = collection.InsertOneAsync(document);
            insertTask.Wait();
            Console.WriteLine(collection.EstimatedDocumentCount());
            Console.ReadLine();
        }
    }
}
