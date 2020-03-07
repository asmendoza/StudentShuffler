using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentShuffler
{
    public static class ActivityLists
    {
        public const string RECITATION = "Recitations";
        public const string ASSIGNMENT = "Assignments";
        public const string SEATWORK = "SeatWorks";
        public const string LABWORK = "LaboratoryWorks";
        public const string GROUPWORK = "GroupWorks";
        public const string QUIZ = "Quizzes";
        public const string ATTENDANCE = "Attendance";
        public const string PROJECT = "Projects";
        public const string EXAM = "Exams";
        public const string OTHER = "Others";

    }

    public enum SBSUActivities
    {
        RECITATION,
        ASSIGNMENT,
        SEATWORK,
        LABWORK,
        GROUPWORK,
        QUIZ,
        ATTENDANCE,
        PROJECT,
        EXAM,
        Others
    }

    
}
