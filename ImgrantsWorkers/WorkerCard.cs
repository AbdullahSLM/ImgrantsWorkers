using System.Windows.Forms;
using System.Drawing;
using System;

namespace ImgrantsWorkers
{
    public class WorkerCard : TableLayoutPanel
    {

        public Worker Worker;

        public WorkerCard(Worker worker, EventHandler onEditBtnClick, EventHandler onDeleteBtnClick)
        {
            this.Worker = worker;

            this.BackColor = SystemColors.ControlLight;
            this.ColumnCount = 1;
            this.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.Controls.Add(this.birthdayLabel, 0, 2);
            this.Controls.Add(this.natLabel, 0, 1);
            this.Controls.Add(this.nameLabel, 0, 0);
            this.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.RowCount = 4;
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
            this.RowStyles.Add(new RowStyle(SizeType.Percent, 35F));
            this.Size = new Size(250, 250);
            //this.TabIndex = 1;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.Dock = DockStyle.Fill;
            this.nameLabel.Font = new Font("Tahoma", 20F);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = worker.Name;
            this.nameLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // natLabel
            // 
            this.natLabel.AutoSize = true;
            this.natLabel.Dock = DockStyle.Fill;
            this.natLabel.Font = new Font("Tahoma", 12F);
            this.natLabel.TabIndex = 1;
            this.natLabel.Text = $"({worker.Nationality})";
            this.natLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // birthdayLabel
            // 
            this.birthdayLabel.AutoSize = true;
            this.birthdayLabel.Dock = DockStyle.Fill;
            this.birthdayLabel.Font = new Font("Tahoma", 12F);
            this.birthdayLabel.TabIndex = 2;
            this.birthdayLabel.Text = worker.Birthday.ToString("yyyy MMMM dd");
            this.birthdayLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((AnchorStyles)((((AnchorStyles.Top | AnchorStyles.Bottom)
            | AnchorStyles.Left)
            | AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            this.tableLayoutPanel2.Controls.Add(this.deleteBtn, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.upsertDateLabel, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.editBtn, 0, 0);
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // upsertDateLabel
            // 
            this.upsertDateLabel.AutoSize = true;
            this.upsertDateLabel.Dock = DockStyle.Fill;
            this.upsertDateLabel.Font = new Font("Tahoma", 12F);
            this.upsertDateLabel.TabIndex = 3;
            this.upsertDateLabel.Text = worker.CreatedAt.ToString("yyyy/MM/dd") + "\n" + worker.UpdatedAt.ToString("yyyy/MM/dd");
            this.upsertDateLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // deleteBtn
            // 
            this.deleteBtn.BackgroundImage = global::ImgrantsWorkers.Properties.Resources.recycle_bin;
            this.deleteBtn.BackgroundImageLayout = ImageLayout.Zoom;
            this.deleteBtn.Dock = DockStyle.Bottom;
            this.deleteBtn.FlatAppearance.BorderSize = 0;
            this.deleteBtn.FlatStyle = FlatStyle.Flat;
            this.deleteBtn.TabIndex = 4;
            this.deleteBtn.UseVisualStyleBackColor = true;
            this.deleteBtn.Click += onDeleteBtnClick;
            // 
            // editBtn
            // 
            this.editBtn.BackgroundImage = global::ImgrantsWorkers.Properties.Resources.pencil;
            this.editBtn.BackgroundImageLayout = ImageLayout.Zoom;
            this.editBtn.Dock = DockStyle.Bottom;
            this.editBtn.FlatAppearance.BorderSize = 0;
            this.editBtn.FlatStyle = FlatStyle.Flat;
            this.editBtn.TabIndex = 0;
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += onEditBtnClick;

        }

        override
        public void Refresh()
        {
            base.Refresh();
            this.nameLabel.Text = Worker.Name;
            this.natLabel.Text = $"({Worker.Nationality})";
            this.birthdayLabel.Text = Worker.Birthday.ToString();
            this.upsertDateLabel.Text = Worker.CreatedAt.ToString() + "\n" + Worker.UpdatedAt.ToString();
        }

        private Label nameLabel = new Label();
        private Label natLabel = new Label();
        private Label birthdayLabel = new Label();
        private TableLayoutPanel tableLayoutPanel2 = new TableLayoutPanel();
        private Button deleteBtn = new Button();
        private Label upsertDateLabel = new Label();
        private Button editBtn = new Button();

        
    }
}
