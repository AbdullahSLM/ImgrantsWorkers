using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImgrantsWorkers
{
    public partial class AddEditWorker : Form
    {

        public Worker Worker;

        public OnComplete<Worker> onComplete;


        public AddEditWorker(Worker worker)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;

            Worker = worker;
            nameField.Text = Worker.Name;
            natField.Text = Worker.Nationality;
            birthdayField.Value = Worker.Birthday;
        }


        public AddEditWorker()
        {
            this.StartPosition = FormStartPosition.CenterParent;
            InitializeComponent();
        }


        private void saveBtn_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(nameField.Text.Trim()))
            {
                MessageBox.Show("Name Field Required !!!");
                return;
            }

            if(Worker == null)
            {
                // Add new worker
                this.Worker = new Worker();
                Worker.Name = nameField.Text;
                Worker.Nationality = natField.Text;
                Worker.Birthday = birthdayField.Value;
                DB.InsertWorker(Worker, onComplete);
            } else
            {
                // Updated existing worker
                Worker.Name = nameField.Text;
                Worker.Nationality = natField.Text;
                Worker.Birthday = birthdayField.Value;
                DB.UpdateWorker(Worker, onComplete);
            }

            this.DialogResult = DialogResult.OK;
        }
    }
}
