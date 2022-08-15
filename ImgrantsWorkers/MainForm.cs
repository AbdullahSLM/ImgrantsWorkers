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
    public partial class MainForm : Form
    {

        private List<Worker> workers = new List<Worker>();

        public MainForm()
        {
            InitializeComponent();
            workers = DB.SelectWorkers();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            foreach (var worker in workers)
                AddNewWorker(worker);
            MainForm_Resize(sender, e);
        }


        private void deleteWorkerCard(Worker worker)
        {
            foreach (Control control in flowLayoutPanel.Controls)
            {
                Console.WriteLine(control);
                if (control is WorkerCard)
                {
                    var wc = (WorkerCard)control;
                    if (wc.Worker.ID == worker.ID)
                    {
                        flowLayoutPanel.Controls.Remove(control);
                    }
                }
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var addForm = new AddEditWorker();

            var result = addForm.ShowDialog();

            if (result == DialogResult.OK)
                AddNewWorker(addForm.Worker);
        }


        private void AddNewWorker(Worker worker) {

            EventHandler editHandler = (s, ev) => {
                var addForm = new AddEditWorker(worker);
                var result = addForm.ShowDialog();

                if (result == DialogResult.OK)
                {
                    foreach (var control in flowLayoutPanel.Controls)
                    {
                        if (control is WorkerCard)
                        {
                            var workerCard = (WorkerCard)control;
                            if (workerCard.Worker == worker)
                            {
                                workerCard.Worker = addForm.Worker;
                                workerCard.Refresh();
                                break;
                            }
                        }
                    }
                }
            };


            EventHandler deleteHandler = (s, ev) =>
            {
                try
                {
                    deleteWorkerCard(worker);
                    workers.Remove(worker);
                    DB.DeleteWorker(worker.ID);
                }
                catch { }
            };


            var card = new WorkerCard(worker, editHandler, deleteHandler);
            flowLayoutPanel.Controls.Add(card);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            int w = 0;
            int n = 0;
            do
            {
                n++;
                w = flowLayoutPanel.Width / n;
            } while (w > 300);
            w -= 6; // Margin
            w -= 18 / n;

            Console.WriteLine($"Width = {flowLayoutPanel.Width}");
            Console.WriteLine($"n = {n}");
            Console.WriteLine($"w = {w}");

            foreach (Control control in flowLayoutPanel.Controls)
            {
                control.Width = w;
                control.Height = w;
            }
        }
    }
}
