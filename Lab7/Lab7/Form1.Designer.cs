namespace Lab7
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
            this.menuStrip1       = new System.Windows.Forms.MenuStrip();
            this.menuGame         = new System.Windows.Forms.ToolStripMenuItem();
            this.menuNew          = new System.Windows.Forms.ToolStripMenuItem();
            this.menuReset        = new System.Windows.Forms.ToolStripMenuItem();
            this.labelGamer1      = new System.Windows.Forms.Label();
            this.labelGamer2      = new System.Windows.Forms.Label();
            this.labelScoreTitle  = new System.Windows.Forms.Label();
            this.labelScore1      = new System.Windows.Forms.Label();
            this.labelScore2      = new System.Windows.Forms.Label();
            this.btnNew           = new System.Windows.Forms.Button();
            this.btnReset         = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            //
            // menuStrip1
            //
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuGame });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(560, 28);
            this.menuStrip1.Text = "menuStrip1";
            //
            // menuGame
            //
            this.menuGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.menuNew, this.menuReset });
            this.menuGame.Name = "menuGame";
            this.menuGame.Text = "Игра";
            //
            // menuNew
            //
            this.menuNew.Name = "menuNew";
            this.menuNew.Text = "Новая игра";
            this.menuNew.Click += new System.EventHandler(this.menuNew_Click);
            //
            // menuReset
            //
            this.menuReset.Name = "menuReset";
            this.menuReset.Text = "Сбросить счёт";
            this.menuReset.Click += new System.EventHandler(this.menuReset_Click);
            //
            // labelGamer1  — "Игрок 1", слева
            //
            this.labelGamer1.AutoSize = true;
            this.labelGamer1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelGamer1.Location = new System.Drawing.Point(50, 33);
            this.labelGamer1.Name = "labelGamer1";
            this.labelGamer1.Text = "Игрок 1";
            //
            // labelGamer2  — "Игрок 2", справа
            //
            this.labelGamer2.AutoSize = true;
            this.labelGamer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelGamer2.Location = new System.Drawing.Point(390, 33);
            this.labelGamer2.Name = "labelGamer2";
            this.labelGamer2.Text = "Игрок 2";
            //
            // labelScoreTitle  — "Счет", по центру
            //
            this.labelScoreTitle.AutoSize = true;
            this.labelScoreTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 28F);
            this.labelScoreTitle.Location = new System.Drawing.Point(210, 30);
            this.labelScoreTitle.Name = "labelScoreTitle";
            this.labelScoreTitle.Text = "Счет";
            //
            // labelScore1  — число под "Игрок 1"
            //
            this.labelScore1.AutoSize = true;
            this.labelScore1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelScore1.Location = new System.Drawing.Point(84, 62);
            this.labelScore1.Name = "labelScore1";
            this.labelScore1.Text = "0";
            //
            // labelScore2  — число под "Игрок 2"
            //
            this.labelScore2.AutoSize = true;
            this.labelScore2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.labelScore2.Location = new System.Drawing.Point(424, 62);
            this.labelScore2.Name = "labelScore2";
            this.labelScore2.Text = "0";
            //
            // btnReset  — "Сбросить счёт", слева под полем
            //
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnReset.Location = new System.Drawing.Point(50, 558);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(190, 42);
            this.btnReset.Text = "Сбросить счёт";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            //
            // btnNew  — "Новая игра", справа под полем
            //
            this.btnNew.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.btnNew.Location = new System.Drawing.Point(320, 558);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(190, 42);
            this.btnNew.Text = "Новая игра";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 615);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.labelScore2);
            this.Controls.Add(this.labelScore1);
            this.Controls.Add(this.labelScoreTitle);
            this.Controls.Add(this.labelGamer2);
            this.Controls.Add(this.labelGamer1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Крестики-нолики";
            this.Paint      += new System.Windows.Forms.PaintEventHandler(this.Form1_Paint);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseClick);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuGame;
        private System.Windows.Forms.ToolStripMenuItem menuNew;
        private System.Windows.Forms.ToolStripMenuItem menuReset;
        private System.Windows.Forms.Label labelGamer1;
        private System.Windows.Forms.Label labelGamer2;
        private System.Windows.Forms.Label labelScoreTitle;
        private System.Windows.Forms.Label labelScore1;
        private System.Windows.Forms.Label labelScore2;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnReset;
    }
}
