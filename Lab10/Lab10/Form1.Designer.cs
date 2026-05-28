using System.Windows.Forms.DataVisualization.Charting;

namespace Lab10
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnRun   = new System.Windows.Forms.Button();
            this.txtLog   = new System.Windows.Forms.TextBox();
            this.chart    = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ChartArea area = new ChartArea("MainArea");
            Legend    legend = new Legend("MainLegend");
            Series    sTime   = new Series("Время");
            Series    sIdeal  = new Series("Идеал");
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();

            // lblTitle
            this.lblTitle.Text      = "Вариант 8: палиндромы до 10 000 000, чей квадрат тоже палиндром";
            this.lblTitle.Location  = new System.Drawing.Point(10, 10);
            this.lblTitle.Size      = new System.Drawing.Size(880, 22);
            this.lblTitle.Font      = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold);

            // btnRun
            this.btnRun.Text     = "Запустить замеры";
            this.btnRun.Location = new System.Drawing.Point(10, 35);
            this.btnRun.Size     = new System.Drawing.Size(180, 30);
            this.btnRun.Click   += new System.EventHandler(this.btnRun_Click);

            // txtLog
            this.txtLog.Location   = new System.Drawing.Point(10, 75);
            this.txtLog.Size       = new System.Drawing.Size(430, 470);
            this.txtLog.Multiline  = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.ReadOnly   = true;
            this.txtLog.Font       = new System.Drawing.Font("Consolas", 9.5F);
            this.txtLog.BackColor  = System.Drawing.SystemColors.Window;

            // chart
            area.AxisX.Title             = "Лимит потоков";
            area.AxisY.Title             = "Время, мс";
            area.AxisX.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            area.AxisY.MajorGrid.LineColor = System.Drawing.Color.LightGray;
            this.chart.ChartAreas.Add(area);
            this.chart.Legends.Add(legend);

            sTime.ChartArea  = "MainArea";
            sTime.Legend     = "MainLegend";
            sTime.ChartType  = SeriesChartType.Line;
            sTime.BorderWidth = 2;
            sTime.MarkerStyle = MarkerStyle.Circle;
            sTime.MarkerSize  = 8;
            sTime.Color       = System.Drawing.Color.SteelBlue;

            sIdeal.ChartArea  = "MainArea";
            sIdeal.Legend     = "MainLegend";
            sIdeal.ChartType  = SeriesChartType.Line;
            sIdeal.BorderWidth = 2;
            sIdeal.MarkerStyle = MarkerStyle.Square;
            sIdeal.MarkerSize  = 8;
            sIdeal.Color       = System.Drawing.Color.DarkOrange;
            sIdeal.BorderDashStyle = ChartDashStyle.Dash;

            this.chart.Series.Add(sTime);
            this.chart.Series.Add(sIdeal);
            this.chart.Location = new System.Drawing.Point(450, 75);
            this.chart.Size     = new System.Drawing.Size(440, 470);

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize          = new System.Drawing.Size(900, 560);
            this.Text                = "Лабораторная работа №10 — TPL";
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.chart);
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label   lblTitle;
        private System.Windows.Forms.Button  btnRun;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
    }
}
