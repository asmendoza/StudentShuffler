using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentShuffler
{
    public partial class StudentCRUD : Form
    {
        public StudentCRUD()
        {
            InitializeComponent();
        }

        private void btnChoose_Click(object sender, EventArgs e)
        {
            //openFileDialog1.Filter = "*.jpg, *.jpeg, *.png";
            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.ShowDialog();
            //pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
            
            

            lblFile.Text = openFileDialog1.FileName;
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            //Student st = new Student()
            //{
            //    StudNo = txtID.Text, 
            //    StudName = txtName.Text, 
            //    StudSection = txtSection.Text, "CS121" };
            //Course cs = new Course("CS121", "Advanced Computer Programming"
            //    , "Saturday, 13:00-18:00", "2019-2020", "Second", "Aris Gail Mendoza", "COMP Lab");
            //cs.Save();
            //st.studPhotoFile = lblFile.Text;
            ////st.studPhoto = pictureBox1.Image;
            //await st.ProcessPhotoAsync();
            //st.Save();

            //st.AddCourse(cs.CID());
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var s = Student.students.FindAsync<BsonDocument>(new BsonDocument("_id", txtInput.Text));
            //Task.WaitAll(s);
            //var cur = s.Result;

            //while (cur.MoveNext())
            //{
            //    foreach(BsonDocument dco in cur.Current)
            //    {
            //        Student theStudent = new Student(dco, "BSCS 1201");

            //        txtName.Text = theStudent.StudentName();
            //        txtID.Text = theStudent.StudentNo();
            //        txtSection.Text = theStudent.StudentSection();
            //        pictureBox1.Image = theStudent.PullImageAsync();
            //    }
            //}
            

        }
    }
}
