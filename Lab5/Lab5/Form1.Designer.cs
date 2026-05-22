namespace Lab5
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Panel panelDraw;
        private System.Windows.Forms.TextBox textBoxInfo;
        private System.Windows.Forms.Label labelInfo;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.panelDraw = new System.Windows.Forms.Panel();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.labelInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // panelDraw
            //
            this.panelDraw.BackColor = System.Drawing.Color.White;
            this.panelDraw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelDraw.Location = new System.Drawing.Point(12, 12);
            this.panelDraw.Name = "panelDraw";
            this.panelDraw.Size = new System.Drawing.Size(760, 460);
            this.panelDraw.TabIndex = 0;
            this.panelDraw.Paint += new System.Windows.Forms.PaintEventHandler(this.panelDraw_Paint);
            this.panelDraw.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panelDraw_MouseClick);
            //
            // labelInfo
            //
            this.labelInfo.AutoSize = true;
            this.labelInfo.Location = new System.Drawing.Point(12, 485);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(110, 13);
            this.labelInfo.TabIndex = 1;
            this.labelInfo.Text = "Выбранная фигура:";
            //
            // textBoxInfo
            //
            this.textBoxInfo.Location = new System.Drawing.Point(130, 482);
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.ReadOnly = true;
            this.textBoxInfo.Size = new System.Drawing.Size(642, 20);
            this.textBoxInfo.TabIndex = 2;
            //
            // Form1
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 521);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.labelInfo);
            this.Controls.Add(this.panelDraw);
            this.Name = "Form1";
            this.Text = "Лабораторная работа №5 - Наследование и полиморфизм";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
