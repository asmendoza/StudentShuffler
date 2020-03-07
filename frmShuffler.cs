using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentShuffler
{
    public partial class frmShuffler : Form
    {
        List<Student> theStudents = new List<Student>();

        Random randomNumbers = new Random();

        bool ShuffleFast;
        int maxInterval = 300;
        int incDec = 10;

        public frmShuffler()
        {
            InitializeComponent();
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            
        }

        private void frmShuffler_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.F5)
            {
                //shuffle
                tmrShuffle.Enabled = !tmrShuffle.Enabled;
            }
        }

        private void cboSection_Click(object sender, EventArgs e)
        {
           
        }

        private int GetNumber()
        {
            int face = randomNumbers.Next(theStudents.Count);

            return face;
        }

        private void frmShuffler_Load(object sender, EventArgs e)
        {
            BsonDocument filter = new BsonDocument("StudSection", new BsonDocument("$ne","MSCS"));

            using (var result = Students._students.Distinct<string>("StudSection", filter).ToListAsync())
            {
                Task.WaitAll(result);
                List<string> g = result.Result;
                cboSection.DataSource = g;
            }

            cboSection.Refresh();
            
            lblInitials.Height = picPhoto.Height;
            lblInitials.Width = picPhoto.Width;
            lblInitials.Top = tableLayoutPanel1.Top;
            lblInitials.Left = tableLayoutPanel1.Left;
            tableLayoutPanel1.SendToBack();
            lblInitials.BringToFront();
            lblInitials.Visible = true;
            lblInitials.Font = new Font(FontFamily.GenericMonospace,200);
            lblInitials.TextAlign = ContentAlignment.MiddleCenter;
            lblInitials.BackColor = Color.Gray;
            lblInitials.ForeColor = Color.SkyBlue;
        }

        private void tmrShuffle_Tick(object sender, EventArgs e)
        {
            Student selectedStud = theStudents[GetNumber()];
            if (selectedStud.HasAnswered == false)
            {
                lblName.Text = selectedStud.StudName;
                lblStudNo.Text = selectedStud.StudNo;
                lblInitials.Text = lblName.Text.Substring(0, 2);
            }

            if (ShuffleFast == true && tmrShuffle.Interval > incDec)
                tmrShuffle.Interval -= incDec;
            else if (ShuffleFast == false && tmrShuffle.Interval < maxInterval)
                tmrShuffle.Interval += incDec;
            else if (tmrShuffle.Interval == maxInterval)
            {
                tmrShuffle.Enabled = false;
                cboSection.Enabled = true;
            }


            selectedStud = null;
        }

        private void btnShuffle_Click(object sender, EventArgs e)
        {
            ShuffleFast = !ShuffleFast;
            if (tmrShuffle.Enabled == false)
            {
                tmrShuffle.Interval = maxInterval;
                tmrShuffle.Enabled = true;
                tmrShuffle.Start();
                cboSection.Enabled = false;
            }

        }

        private void tmrInterval_Tick(object sender, EventArgs e)
        {
            if (ShuffleFast == true && tmrShuffle.Interval > incDec)
                tmrShuffle.Interval -= incDec;
            else if (ShuffleFast == false && tmrShuffle.Interval < maxInterval)
                tmrShuffle.Interval += incDec;
            else if (tmrShuffle.Interval == maxInterval)
            {
                tmrShuffle.Enabled = false;
                cboSection.Enabled = true;
            }

        }

        private void cboSection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private async void cboSection_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            btnShuffle.Enabled = false;

            var gg = await Students.GetStudentByFieldAsync("StudSection", new BsonDocument("$eq",cboSection.Text));

            theStudents.Clear();
            theStudents = gg;

            btnShuffle.Enabled = true;
        }
    }
}
