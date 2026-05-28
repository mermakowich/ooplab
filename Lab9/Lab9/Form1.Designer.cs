namespace Lab9
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
            this.lblUserName     = new System.Windows.Forms.Label();
            this.userNameTextBox = new System.Windows.Forms.TextBox();
            this.loginButton     = new System.Windows.Forms.Button();
            this.logoutButton    = new System.Windows.Forms.Button();
            this.lblTitle        = new System.Windows.Forms.Label();
            this.titleTextBox    = new System.Windows.Forms.TextBox();
            this.lblPrice        = new System.Windows.Forms.Label();
            this.priceTextBox    = new System.Windows.Forms.TextBox();
            this.publishButton   = new System.Windows.Forms.Button();
            this.lblAds          = new System.Windows.Forms.Label();
            this.adsListBox      = new System.Windows.Forms.ListBox();
            this.SuspendLayout();

            // lblUserName
            this.lblUserName.Text     = "Имя пользователя:";
            this.lblUserName.Location = new System.Drawing.Point(12, 15);
            this.lblUserName.Size     = new System.Drawing.Size(115, 18);

            // userNameTextBox
            this.userNameTextBox.Location = new System.Drawing.Point(133, 12);
            this.userNameTextBox.Size     = new System.Drawing.Size(180, 22);

            // loginButton
            this.loginButton.Text     = "Войти";
            this.loginButton.Location = new System.Drawing.Point(325, 10);
            this.loginButton.Size     = new System.Drawing.Size(95, 27);
            this.loginButton.Click   += new System.EventHandler(this.loginButton_Click);

            // logoutButton
            this.logoutButton.Text     = "Выйти";
            this.logoutButton.Location = new System.Drawing.Point(425, 10);
            this.logoutButton.Size     = new System.Drawing.Size(95, 27);
            this.logoutButton.Click   += new System.EventHandler(this.logoutButton_Click);

            // lblTitle
            this.lblTitle.Text     = "Заголовок:";
            this.lblTitle.Location = new System.Drawing.Point(12, 55);
            this.lblTitle.Size     = new System.Drawing.Size(115, 18);

            // titleTextBox
            this.titleTextBox.Location = new System.Drawing.Point(133, 52);
            this.titleTextBox.Size     = new System.Drawing.Size(285, 22);

            // lblPrice
            this.lblPrice.Text     = "Цена (руб.):";
            this.lblPrice.Location = new System.Drawing.Point(12, 85);
            this.lblPrice.Size     = new System.Drawing.Size(115, 18);

            // priceTextBox
            this.priceTextBox.Location = new System.Drawing.Point(133, 82);
            this.priceTextBox.Size     = new System.Drawing.Size(140, 22);

            // publishButton
            this.publishButton.Text     = "Опубликовать";
            this.publishButton.Location = new System.Drawing.Point(290, 80);
            this.publishButton.Size     = new System.Drawing.Size(128, 27);
            this.publishButton.Click   += new System.EventHandler(this.publishButton_Click);

            // lblAds
            this.lblAds.Text     = "Объявления:";
            this.lblAds.Location = new System.Drawing.Point(12, 120);
            this.lblAds.Size     = new System.Drawing.Size(115, 18);

            // adsListBox
            this.adsListBox.Location = new System.Drawing.Point(12, 140);
            this.adsListBox.Size     = new System.Drawing.Size(508, 290);
            this.adsListBox.Font     = new System.Drawing.Font("Consolas", 9.5F);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(534, 445);
            this.Text                = "Лабораторная работа №9 — Доска объявлений";
            this.Controls.Add(this.lblUserName);
            this.Controls.Add(this.userNameTextBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.logoutButton);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.titleTextBox);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.priceTextBox);
            this.Controls.Add(this.publishButton);
            this.Controls.Add(this.lblAds);
            this.Controls.Add(this.adsListBox);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label   lblUserName;
        private System.Windows.Forms.TextBox userNameTextBox;
        private System.Windows.Forms.Button  loginButton;
        private System.Windows.Forms.Button  logoutButton;
        private System.Windows.Forms.Label   lblTitle;
        private System.Windows.Forms.TextBox titleTextBox;
        private System.Windows.Forms.Label   lblPrice;
        private System.Windows.Forms.TextBox priceTextBox;
        private System.Windows.Forms.Button  publishButton;
        private System.Windows.Forms.Label   lblAds;
        private System.Windows.Forms.ListBox adsListBox;
    }
}
