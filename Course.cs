using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace StudentShuffler
{
    public class Course
    {
        private string CourseID;
        private string CourseCode;
        private string CourseAY;
        private string CourseSemester;
        public string CourseDesc { get; set;}
        public string CourseSched { get; set; }
        public string CourseInstructor { get; set; }
        public string CourseRoom { get; set; }

        private readonly IMongoCollection<BsonDocument> courses =
            MongoDBTransactions.db.GetCollection<BsonDocument>("Courses");
        private readonly UpdateOptions updateOptions = new UpdateOptions { IsUpsert = true };

        public Course(string courseCode, string courseAY, string courseSemester)
        {
            CourseCode = courseCode;
            CourseAY = courseAY;
            CourseSemester = courseSemester;
            CourseID = CourseCode.Replace(" ", "")
                + CourseSemester.Replace(" ", "")
                + CourseAY.Replace(" ", "");
        }

        public Course(BsonDocument c)
        {
            if(c.GetElement("_id") != null)
            {
                CourseID = c.GetValue("_id").ToString();
                CourseCode = c.GetValue("CourseCode").ToString();
                CourseDesc = c.GetValue("CourseDesc").ToString();
                CourseSched = c.GetValue("CourseSched").ToString();
                CourseAY = c.GetValue("CourseAY").ToString();
                CourseSemester = c.GetValue("CourseSemester").ToString();
                CourseInstructor = c.GetValue("CourseInstructor").ToString();
                CourseRoom = c.GetValue("CourseRoom").ToString();
            }
        }
        public Course(string courseCode, string courseDesc, string courseSched
            , string courseAY, string courseSemester, string courseInstructor
            , string courseRoom)
        {
            CourseCode = courseCode;
            CourseDesc = courseDesc;
            CourseSched = courseSched;
            CourseAY = courseAY;
            CourseSemester = courseSemester;
            CourseInstructor = courseInstructor;
            CourseRoom = courseRoom;
            CourseID = CourseCode.Replace(" ", "")
                + CourseSemester.Replace(" ", "")
                + CourseAY.Replace(" ", "");
        }

        public BsonDocument TheCourse()
        {
            BsonDocument c = new BsonDocument();
            
            c.Add("_id", CourseID);
            c.Add("CourseCode", CourseCode);
            c.Add("CourseDesc", CourseDesc);
            c.Add("CourseSched", CourseSched);
            c.Add("CourseAY", CourseAY);
            c.Add("CourseSemester", CourseSemester);
            c.Add("CourseInstructor", CourseInstructor);
            c.Add("CourseRoom", CourseRoom);
            return c;
        }

        public string CID() => CourseID;
        public string CAY() => CourseAY;
        public string CCode() => CourseCode;
        public string CSemester() => CourseSemester;

        public bool Save()
        {
            bool resp;

            try
            {
                var g = courses.UpdateOneAsync(new BsonDocument("_id", CourseID)
                    , new BsonDocument("$set", TheCourse()), updateOptions);
                Task.WaitAll(g);
                resp = g.Result.IsAcknowledged;
            }
            catch (Exception e)
            {
                Console.WriteLine("Error in Course.Save() method :: " + e.Message);
                resp = false;
            }
            return resp;
        }
    }
}
