namespace ModServerFrame
{
    partial class Form1
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
            this.DgEntradasPLC = new System.Windows.Forms.DataGridView();
            this.DgSalidasPLC = new System.Windows.Forms.DataGridView();
            this.DgSalidasSIM = new System.Windows.Forms.DataGridView();
            this.DgEntradasSIM = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.LblPLCConnected = new System.Windows.Forms.Label();
            this.LblSIMConnected = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.DgEntradasPLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgSalidasPLC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgSalidasSIM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgEntradasSIM)).BeginInit();
            this.SuspendLayout();
            // 
            // DgEntradasPLC
            // 
            this.DgEntradasPLC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgEntradasPLC.Location = new System.Drawing.Point(57, 32);
            this.DgEntradasPLC.Name = "DgEntradasPLC";
            this.DgEntradasPLC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgEntradasPLC.Size = new System.Drawing.Size(478, 305);
            this.DgEntradasPLC.TabIndex = 0;
            this.DgEntradasPLC.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgEntradasPLC_CellContentClick);
            this.DgEntradasPLC.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgEntradasPLC_CellFormatting);
            this.DgEntradasPLC.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgEntradasSIM_DataError);
            // 
            // DgSalidasPLC
            // 
            this.DgSalidasPLC.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgSalidasPLC.Location = new System.Drawing.Point(57, 356);
            this.DgSalidasPLC.Name = "DgSalidasPLC";
            this.DgSalidasPLC.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgSalidasPLC.Size = new System.Drawing.Size(478, 323);
            this.DgSalidasPLC.TabIndex = 1;
            this.DgSalidasPLC.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgSalidasPLC_CellFormatting);
            this.DgSalidasPLC.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgEntradasSIM_DataError);
            // 
            // DgSalidasSIM
            // 
            this.DgSalidasSIM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgSalidasSIM.Location = new System.Drawing.Point(586, 356);
            this.DgSalidasSIM.Name = "DgSalidasSIM";
            this.DgSalidasSIM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgSalidasSIM.Size = new System.Drawing.Size(478, 323);
            this.DgSalidasSIM.TabIndex = 2;
            this.DgSalidasSIM.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgSalidasSIM_CellFormatting);
            this.DgSalidasSIM.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgEntradasSIM_DataError);
            // 
            // DgEntradasSIM
            // 
            this.DgEntradasSIM.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgEntradasSIM.Location = new System.Drawing.Point(586, 32);
            this.DgEntradasSIM.Name = "DgEntradasSIM";
            this.DgEntradasSIM.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.DgEntradasSIM.Size = new System.Drawing.Size(478, 305);
            this.DgEntradasSIM.TabIndex = 3;
            this.DgEntradasSIM.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgEntradasSIM_CellContentClick);
            this.DgEntradasSIM.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgEntradasSIM_CellFormatting);
            this.DgEntradasSIM.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DgEntradasSIM_DataError);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "EntradasPLC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 340);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SalidasPLC";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(583, 340);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "SalidasSIM";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(583, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "EntradasSIM";
            // 
            // LblPLCConnected
            // 
            this.LblPLCConnected.AutoSize = true;
            this.LblPLCConnected.Location = new System.Drawing.Point(11, 93);
            this.LblPLCConnected.Name = "LblPLCConnected";
            this.LblPLCConnected.Size = new System.Drawing.Size(21, 13);
            this.LblPLCConnected.TabIndex = 8;
            this.LblPLCConnected.Text = "Lbl";
            // 
            // LblSIMConnected
            // 
            this.LblSIMConnected.AutoSize = true;
            this.LblSIMConnected.Location = new System.Drawing.Point(545, 93);
            this.LblSIMConnected.Name = "LblSIMConnected";
            this.LblSIMConnected.Size = new System.Drawing.Size(35, 13);
            this.LblSIMConnected.TabIndex = 9;
            this.LblSIMConnected.Text = "label6";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 715);
            this.Controls.Add(this.LblSIMConnected);
            this.Controls.Add(this.LblPLCConnected);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DgEntradasSIM);
            this.Controls.Add(this.DgSalidasSIM);
            this.Controls.Add(this.DgSalidasPLC);
            this.Controls.Add(this.DgEntradasPLC);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DgEntradasPLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgSalidasPLC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgSalidasSIM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgEntradasSIM)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView DgEntradasPLC;
        private System.Windows.Forms.DataGridView DgSalidasPLC;
        private System.Windows.Forms.DataGridView DgSalidasSIM;
        private System.Windows.Forms.DataGridView DgEntradasSIM;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label LblPLCConnected;
        private System.Windows.Forms.Label LblSIMConnected;
    }
}

