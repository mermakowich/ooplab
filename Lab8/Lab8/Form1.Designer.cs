namespace Lab8
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
            this.lblTitle      = new System.Windows.Forms.Label();
            this.lblRaw        = new System.Windows.Forms.Label();
            this.btnLoadRss    = new System.Windows.Forms.Button();
            this.txtRaw        = new System.Windows.Forms.TextBox();
            this.lblParsed     = new System.Windows.Forms.Label();
            this.btnParseXml   = new System.Windows.Forms.Button();
            this.txtParsed     = new System.Windows.Forms.TextBox();
            this.lblDb         = new System.Windows.Forms.Label();
            this.btnSaveToDb   = new System.Windows.Forms.Button();
            this.btnReadFromDb = new System.Windows.Forms.Button();
            this.txtDb         = new System.Windows.Forms.TextBox();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Text      = "Лента новостей Коммерсантъ";
            this.lblTitle.Location  = new System.Drawing.Point(10, 10);
            this.lblTitle.Size      = new System.Drawing.Size(860, 28);
            this.lblTitle.Font      = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblRaw
            this.lblRaw.Text     = "Исходный XML:";
            this.lblRaw.Location = new System.Drawing.Point(10, 50);
            this.lblRaw.Size     = new System.Drawing.Size(150, 20);

            // btnLoadRss
            this.btnLoadRss.Text     = "Загрузить RSS";
            this.btnLoadRss.Location = new System.Drawing.Point(715, 47);
            this.btnLoadRss.Size     = new System.Drawing.Size(155, 27);
            this.btnLoadRss.Click   += new System.EventHandler(this.btnLoadRss_Click);

            // txtRaw
            this.txtRaw.Location    = new System.Drawing.Point(10, 78);
            this.txtRaw.Size        = new System.Drawing.Size(860, 175);
            this.txtRaw.Multiline   = true;
            this.txtRaw.ScrollBars  = System.Windows.Forms.ScrollBars.Both;
            this.txtRaw.ReadOnly    = true;
            this.txtRaw.BackColor   = System.Drawing.SystemColors.Window;
            this.txtRaw.WordWrap    = false;

            // lblParsed
            this.lblParsed.Text     = "Разобранные новости:";
            this.lblParsed.Location = new System.Drawing.Point(10, 265);
            this.lblParsed.Size     = new System.Drawing.Size(200, 20);

            // btnParseXml
            this.btnParseXml.Text     = "Разобрать XML";
            this.btnParseXml.Location = new System.Drawing.Point(715, 262);
            this.btnParseXml.Size     = new System.Drawing.Size(155, 27);
            this.btnParseXml.Click   += new System.EventHandler(this.btnParseXml_Click);

            // txtParsed
            this.txtParsed.Location   = new System.Drawing.Point(10, 293);
            this.txtParsed.Size       = new System.Drawing.Size(860, 195);
            this.txtParsed.Multiline  = true;
            this.txtParsed.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtParsed.ReadOnly   = true;
            this.txtParsed.BackColor  = System.Drawing.SystemColors.Window;

            // lblDb
            this.lblDb.Text     = "База данных (SQLite):";
            this.lblDb.Location = new System.Drawing.Point(10, 500);
            this.lblDb.Size     = new System.Drawing.Size(200, 20);

            // btnSaveToDb
            this.btnSaveToDb.Text     = "Сохранить в БД";
            this.btnSaveToDb.Location = new System.Drawing.Point(565, 497);
            this.btnSaveToDb.Size     = new System.Drawing.Size(145, 27);
            this.btnSaveToDb.Click   += new System.EventHandler(this.btnSaveToDb_Click);

            // btnReadFromDb
            this.btnReadFromDb.Text     = "Читать из БД";
            this.btnReadFromDb.Location = new System.Drawing.Point(720, 497);
            this.btnReadFromDb.Size     = new System.Drawing.Size(150, 27);
            this.btnReadFromDb.Click   += new System.EventHandler(this.btnReadFromDb_Click);

            // txtDb
            this.txtDb.Location   = new System.Drawing.Point(10, 528);
            this.txtDb.Size       = new System.Drawing.Size(860, 195);
            this.txtDb.Multiline  = true;
            this.txtDb.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtDb.ReadOnly   = true;
            this.txtDb.BackColor  = System.Drawing.SystemColors.Window;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(884, 738);
            this.Text                = "Лабораторная работа №8 — RSS + SQLite";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblRaw);
            this.Controls.Add(this.btnLoadRss);
            this.Controls.Add(this.txtRaw);
            this.Controls.Add(this.lblParsed);
            this.Controls.Add(this.btnParseXml);
            this.Controls.Add(this.txtParsed);
            this.Controls.Add(this.lblDb);
            this.Controls.Add(this.btnSaveToDb);
            this.Controls.Add(this.btnReadFromDb);
            this.Controls.Add(this.txtDb);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label   lblTitle;
        private System.Windows.Forms.Label   lblRaw;
        private System.Windows.Forms.Button  btnLoadRss;
        private System.Windows.Forms.TextBox txtRaw;
        private System.Windows.Forms.Label   lblParsed;
        private System.Windows.Forms.Button  btnParseXml;
        private System.Windows.Forms.TextBox txtParsed;
        private System.Windows.Forms.Label   lblDb;
        private System.Windows.Forms.Button  btnSaveToDb;
        private System.Windows.Forms.Button  btnReadFromDb;
        private System.Windows.Forms.TextBox txtDb;
    }
}
