namespace MonsterTradingTerminal
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lbScripts = new System.Windows.Forms.ListBox();
            this.dgvResults = new System.Windows.Forms.DataGridView();
            this.Script = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TotalTrades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinTrades = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.WinPercent = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopLossHit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TakeProfitHit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ProfitLoss = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lbStrategy = new System.Windows.Forms.ListBox();
            this.btnBacktest = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).BeginInit();
            this.SuspendLayout();
            // 
            // lbScripts
            // 
            this.lbScripts.FormattingEnabled = true;
            this.lbScripts.Items.AddRange(new object[] {
            "NIFTY",
            "TATAMOTORS"});
            this.lbScripts.Location = new System.Drawing.Point(7, 31);
            this.lbScripts.Name = "lbScripts";
            this.lbScripts.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.lbScripts.Size = new System.Drawing.Size(153, 368);
            this.lbScripts.TabIndex = 0;
            // 
            // dgvResults
            // 
            this.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResults.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Script,
            this.TotalTrades,
            this.WinTrades,
            this.WinPercent,
            this.StopLossHit,
            this.TakeProfitHit,
            this.ProfitLoss});
            this.dgvResults.Location = new System.Drawing.Point(166, 31);
            this.dgvResults.Name = "dgvResults";
            this.dgvResults.Size = new System.Drawing.Size(744, 368);
            this.dgvResults.TabIndex = 1;
            // 
            // Script
            // 
            this.Script.HeaderText = "Script";
            this.Script.Name = "Script";
            // 
            // TotalTrades
            // 
            this.TotalTrades.HeaderText = "Total Trades";
            this.TotalTrades.Name = "TotalTrades";
            // 
            // WinTrades
            // 
            this.WinTrades.HeaderText = "Win Trades";
            this.WinTrades.Name = "WinTrades";
            // 
            // WinPercent
            // 
            this.WinPercent.HeaderText = "Win %";
            this.WinPercent.Name = "WinPercent";
            // 
            // StopLossHit
            // 
            this.StopLossHit.HeaderText = "Stop Loss Hit";
            this.StopLossHit.Name = "StopLossHit";
            // 
            // TakeProfitHit
            // 
            this.TakeProfitHit.HeaderText = "Take Profit Hit";
            this.TakeProfitHit.Name = "TakeProfitHit";
            // 
            // ProfitLoss
            // 
            this.ProfitLoss.HeaderText = "Profit/Loss";
            this.ProfitLoss.Name = "ProfitLoss";
            // 
            // lbStrategy
            // 
            this.lbStrategy.FormattingEnabled = true;
            this.lbStrategy.Items.AddRange(new object[] {
            "Fake Crossover",
            "SMA50 Strategy",
            "Stock Pro Strategy"});
            this.lbStrategy.Location = new System.Drawing.Point(916, 31);
            this.lbStrategy.Name = "lbStrategy";
            this.lbStrategy.Size = new System.Drawing.Size(153, 368);
            this.lbStrategy.TabIndex = 2;
            // 
            // btnBacktest
            // 
            this.btnBacktest.Location = new System.Drawing.Point(445, 419);
            this.btnBacktest.Name = "btnBacktest";
            this.btnBacktest.Size = new System.Drawing.Size(160, 23);
            this.btnBacktest.TabIndex = 3;
            this.btnBacktest.Text = "Backtest";
            this.btnBacktest.UseVisualStyleBackColor = true;
            this.btnBacktest.Click += new System.EventHandler(this.BtnBacktest_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1076, 452);
            this.Controls.Add(this.btnBacktest);
            this.Controls.Add(this.lbStrategy);
            this.Controls.Add(this.dgvResults);
            this.Controls.Add(this.lbScripts);
            this.Name = "frmMain";
            this.Text = "Monster Trading Terminal";
            ((System.ComponentModel.ISupportInitialize)(this.dgvResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbScripts;
        private System.Windows.Forms.DataGridView dgvResults;
        private System.Windows.Forms.ListBox lbStrategy;
        private System.Windows.Forms.Button btnBacktest;
        private System.Windows.Forms.DataGridViewTextBoxColumn Script;
        private System.Windows.Forms.DataGridViewTextBoxColumn TotalTrades;
        private System.Windows.Forms.DataGridViewTextBoxColumn WinTrades;
        private System.Windows.Forms.DataGridViewTextBoxColumn WinPercent;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopLossHit;
        private System.Windows.Forms.DataGridViewTextBoxColumn TakeProfitHit;
        private System.Windows.Forms.DataGridViewTextBoxColumn ProfitLoss;
    }
}

