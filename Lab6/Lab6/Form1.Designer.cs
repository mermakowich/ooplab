namespace Lab6
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.labelName = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.labelPriority = new System.Windows.Forms.Label();
            this.numericPriority = new System.Windows.Forms.NumericUpDown();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.labelFilter = new System.Windows.Forms.Label();
            this.comboBoxFilter = new System.Windows.Forms.ComboBox();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonDone = new System.Windows.Forms.Button();
            this.labelTasks = new System.Windows.Forms.Label();
            this.labelLog = new System.Windows.Forms.Label();
            this.listBoxTasks = new System.Windows.Forms.ListBox();
            this.listBoxLog = new System.Windows.Forms.ListBox();
            this.labelStats = new System.Windows.Forms.Label();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericPriority)).BeginInit();
            this.SuspendLayout();
            //
            // labelName
            //
            this.labelName.AutoSize = true;
            this.labelName.Location = new System.Drawing.Point(12, 15);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(99, 13);
            this.labelName.Text = "Название задачи:";
            //
            // textBoxName
            //
            this.textBoxName.Location = new System.Drawing.Point(120, 12);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(220, 20);
            //
            // labelPriority
            //
            this.labelPriority.AutoSize = true;
            this.labelPriority.Location = new System.Drawing.Point(12, 43);
            this.labelPriority.Name = "labelPriority";
            this.labelPriority.Size = new System.Drawing.Size(63, 13);
            this.labelPriority.Text = "Приоритет:";
            //
            // numericPriority
            //
            this.numericPriority.Location = new System.Drawing.Point(120, 41);
            this.numericPriority.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            this.numericPriority.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericPriority.Value = new decimal(new int[] { 3, 0, 0, 0 });
            this.numericPriority.Name = "numericPriority";
            this.numericPriority.Size = new System.Drawing.Size(60, 20);
            //
            // buttonAdd
            //
            this.buttonAdd.Location = new System.Drawing.Point(200, 39);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(140, 25);
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            //
            // labelFilter
            //
            this.labelFilter.AutoSize = true;
            this.labelFilter.Location = new System.Drawing.Point(380, 15);
            this.labelFilter.Name = "labelFilter";
            this.labelFilter.Size = new System.Drawing.Size(48, 13);
            this.labelFilter.Text = "Фильтр:";
            //
            // comboBoxFilter
            //
            this.comboBoxFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxFilter.Location = new System.Drawing.Point(440, 12);
            this.comboBoxFilter.Name = "comboBoxFilter";
            this.comboBoxFilter.Size = new System.Drawing.Size(220, 21);
            this.comboBoxFilter.SelectedIndexChanged += new System.EventHandler(this.comboBoxFilter_SelectedIndexChanged);
            //
            // buttonDelete
            //
            this.buttonDelete.Location = new System.Drawing.Point(440, 39);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(105, 25);
            this.buttonDelete.Text = "Удалить";
            this.buttonDelete.UseVisualStyleBackColor = true;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            //
            // buttonDone
            //
            this.buttonDone.Location = new System.Drawing.Point(555, 39);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(105, 25);
            this.buttonDone.Text = "Выполнено";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            //
            // labelTasks
            //
            this.labelTasks.AutoSize = true;
            this.labelTasks.Location = new System.Drawing.Point(12, 80);
            this.labelTasks.Name = "labelTasks";
            this.labelTasks.Size = new System.Drawing.Size(80, 13);
            this.labelTasks.Text = "Список задач:";
            //
            // labelLog
            //
            this.labelLog.AutoSize = true;
            this.labelLog.Location = new System.Drawing.Point(380, 80);
            this.labelLog.Name = "labelLog";
            this.labelLog.Size = new System.Drawing.Size(95, 13);
            this.labelLog.Text = "Журнал событий:";
            //
            // listBoxTasks
            //
            this.listBoxTasks.FormattingEnabled = true;
            this.listBoxTasks.Location = new System.Drawing.Point(12, 100);
            this.listBoxTasks.Name = "listBoxTasks";
            this.listBoxTasks.Size = new System.Drawing.Size(360, 290);
            //
            // listBoxLog
            //
            this.listBoxLog.FormattingEnabled = true;
            this.listBoxLog.Location = new System.Drawing.Point(380, 100);
            this.listBoxLog.Name = "listBoxLog";
            this.listBoxLog.Size = new System.Drawing.Size(280, 290);
            //
            // labelStats
            //
            this.labelStats.AutoSize = true;
            this.labelStats.Location = new System.Drawing.Point(12, 400);
            this.labelStats.Name = "labelStats";
            this.labelStats.Text = "Всего: 0 | Выполнено: 0 | Осталось: 0";
            //
            // labelStatus
            //
            this.labelStatus.AutoSize = true;
            this.labelStatus.Location = new System.Drawing.Point(12, 425);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Text = "Готово";
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 460);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.labelPriority);
            this.Controls.Add(this.numericPriority);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.labelFilter);
            this.Controls.Add(this.comboBoxFilter);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.labelTasks);
            this.Controls.Add(this.labelLog);
            this.Controls.Add(this.listBoxTasks);
            this.Controls.Add(this.listBoxLog);
            this.Controls.Add(this.labelStats);
            this.Controls.Add(this.labelStatus);
            this.Name = "Form1";
            this.Text = "Менеджер задач — Лабораторная работа №6";
            ((System.ComponentModel.ISupportInitialize)(this.numericPriority)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label labelPriority;
        private System.Windows.Forms.NumericUpDown numericPriority;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.Label labelFilter;
        private System.Windows.Forms.ComboBox comboBoxFilter;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.Label labelTasks;
        private System.Windows.Forms.Label labelLog;
        private System.Windows.Forms.ListBox listBoxTasks;
        private System.Windows.Forms.ListBox listBoxLog;
        private System.Windows.Forms.Label labelStats;
        private System.Windows.Forms.Label labelStatus;
    }
}
