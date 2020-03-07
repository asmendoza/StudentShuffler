using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentShuffler
{
    public class Activity
    {
        private string RID;
        private string RType;
        private string RDesc;
        private string RCourseID;
        private string RDoneDate;
        private string RCreateDateTime;
        private string RSavedDateTime;
        private double RGrade;
        private string RStudentID;
        private int RCtr;

        private readonly IMongoCollection<BsonDocument> activities;
        private readonly UpdateOptions updateOptions = new UpdateOptions { IsUpsert = true };

        public Activity(string rCourseID, string rDesc, string rDateTime, double rGrade
            , string rStudentID, string activityType)
        {
            RCourseID = rCourseID;
            RDesc = rDesc;
            RDoneDate = rDateTime;
            RGrade = rGrade;
            RStudentID = rStudentID;
            RID = RCourseID.Replace(" ","") 
                + RStudentID.Replace(" ", "") 
                + RCtr.ToString();
            RCreateDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            RType = activityType;

            activities =
            MongoDBTransactions.db.GetCollection<BsonDocument>(activityType);

            
        }

        public Activity(BsonDocument r)
        {
            if(r.GetElement("_id") != null)
            {
                RID = r.GetValue("_id").ToString();
                RType = r.GetValue("Type").ToString();
                RDesc = r.GetValue("Desc").ToString();
                RCourseID = r.GetValue("CourseCode").ToString();
                RDoneDate = r.GetValue("DoneDate").ToString();
                RCreateDateTime = r.GetValue("CreateDateTime").ToString();
                RSavedDateTime = r.GetValue("SavedDateTime").ToString();
                RStudentID = r.GetValue("StudentID").ToString();
                RGrade = r.GetValue("Grade").ToDouble();
            }
        }

        public BsonDocument TheRecitation()
        {
            RSavedDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            BsonDocument r = new BsonDocument();
            r.Add("_id", RID);
            r.Add("Type", RType);
            r.Add("Desc", RDesc);
            r.Add("CourseID", RCourseID);
            r.Add("DoneDate", RDoneDate);
            r.Add("CreateDateTime", RCreateDateTime);
            r.Add("SavedDateTime", RSavedDateTime);
            r.Add("Grade", RGrade);
            r.Add("StudentID", RStudentID);

            return r;
        }

        public string ID() => RID;

        public bool Save()
        {
            bool resp;

            try
            {
                var g = activities.UpdateOneAsync(new BsonDocument("_id", RID)
                    , new BsonDocument("$set", TheRecitation()), updateOptions);
                Task.WaitAll(g);
                resp = g.Result.IsAcknowledged;
            }
            catch(Exception e)
            {
                Console.WriteLine("Error in Recitation.Save() method :: " + e.Message);
                resp = false;
            }
            return resp;
        }

    }
}
