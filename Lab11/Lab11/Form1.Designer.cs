namespace Lab11
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
            this.lblLogin    = new System.Windows.Forms.Label();
            this.loginBox    = new System.Windows.Forms.TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.loginButton = new System.Windows.Forms.Button();
            this.loadButton  = new System.Windows.Forms.Button();
            this.statusLabel = new System.Windows.Forms.Label();
            this.listView    = new System.Windows.Forms.ListView();
            this.SuspendLayout();

            // lblLogin
            this.lblLogin.Text     = "Логин:";
            this.lblLogin.Location = new System.Drawing.Point(12, 15);
            this.lblLogin.Size     = new System.Drawing.Size(60, 18);

            // loginBox
            this.loginBox.Text     = "admin";
            this.loginBox.Location = new System.Drawing.Point(78, 12);
            this.loginBox.Size     = new System.Drawing.Size(120, 22);

            // lblPassword
            this.lblPassword.Text     = "Пароль:";
            this.lblPassword.Location = new System.Drawing.Point(210, 15);
            this.lblPassword.Size     = new System.Drawing.Size(60, 18);

            // passwordBox
            this.passwordBox.Text         = "password123";
            this.passwordBox.PasswordChar = '*';
            this.passwordBox.Location     = new System.Drawing.Point(275, 12);
            this.passwordBox.Size         = new System.Drawing.Size(120, 22);

            // loginButton
            this.loginButton.Text     = "Войти";
            this.loginButton.Location = new System.Drawing.Point(410, 10);
            this.loginButton.Size     = new System.Drawing.Size(110, 27);
            this.loginButton.Click   += new System.EventHandler(this.loginButton_Click);

            // loadButton
            this.loadButton.Text     = "Загрузить бронирования";
            this.loadButton.Location = new System.Drawing.Point(530, 10);
            this.loadButton.Size     = new System.Drawing.Size(200, 27);
            this.loadButton.Click   += new System.EventHandler(this.loadButton_Click);

            // statusLabel
            this.statusLabel.Text      = "Не авторизован";
            this.statusLabel.Location  = new System.Drawing.Point(12, 50);
            this.statusLabel.Size      = new System.Drawing.Size(900, 22);
            this.statusLabel.AutoEllipsis = true;
            this.statusLabel.Font      = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold);

            // listView
            this.listView.Location  = new System.Drawing.Point(12, 80);
            this.listView.Size      = new System.Drawing.Size(900, 380);
            this.listView.View      = System.Windows.Forms.View.Details;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.Columns.Add("ID",              60);
            this.listView.Columns.Add("Имя",            120);
            this.listView.Columns.Add("Фамилия",        120);
            this.listView.Columns.Add("Цена",            90);
            this.listView.Columns.Add("Депозит",         70);
            this.listView.Columns.Add("Заезд",          100);
            this.listView.Columns.Add("Выезд",          100);
            this.listView.Columns.Add("Доп. пожелания", 200);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(924, 475);
            this.Text                = "Лабораторная работа №11 — HTTP + REST API";
            this.Controls.Add(this.lblLogin);
            this.Controls.Add(this.loginBox);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.passwordBox);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.listView);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label    lblLogin;
        private System.Windows.Forms.TextBox  loginBox;
        private System.Windows.Forms.Label    lblPassword;
        private System.Windows.Forms.TextBox  passwordBox;
        private System.Windows.Forms.Button   loginButton;
        private System.Windows.Forms.Button   loadButton;
        private System.Windows.Forms.Label    statusLabel;
        private System.Windows.Forms.ListView listView;
    }
}
