namespace OpenTile.Win
{
    partial class frmPossibleTiles
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtRange = new System.Windows.Forms.TextBox();
            this.txtMap = new System.Windows.Forms.TextBox();
            this.btnGenerateMap = new System.Windows.Forms.Button();
            this.btnDebugPrint = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtActionPoints = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(579, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 20);
            this.label1.TabIndex = 17;
            this.label1.Text = "Range:";
            // 
            // txtRange
            // 
            this.txtRange.Location = new System.Drawing.Point(646, 23);
            this.txtRange.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtRange.Name = "txtRange";
            this.txtRange.Size = new System.Drawing.Size(38, 26);
            this.txtRange.TabIndex = 16;
            this.txtRange.Text = "5";
            // 
            // txtMap
            // 
            this.txtMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMap.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMap.Location = new System.Drawing.Point(18, 65);
            this.txtMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtMap.Multiline = true;
            this.txtMap.Name = "txtMap";
            this.txtMap.Size = new System.Drawing.Size(1150, 944);
            this.txtMap.TabIndex = 13;
            // 
            // btnGenerateMap
            // 
            this.btnGenerateMap.Location = new System.Drawing.Point(18, 20);
            this.btnGenerateMap.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnGenerateMap.Name = "btnGenerateMap";
            this.btnGenerateMap.Size = new System.Drawing.Size(166, 35);
            this.btnGenerateMap.TabIndex = 12;
            this.btnGenerateMap.Text = "Generate Map";
            this.btnGenerateMap.UseVisualStyleBackColor = true;
            this.btnGenerateMap.Click += new System.EventHandler(this.btnGenerateMap_Click);
            // 
            // btnDebugPrint
            // 
            this.btnDebugPrint.Location = new System.Drawing.Point(1062, 20);
            this.btnDebugPrint.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnDebugPrint.Name = "btnDebugPrint";
            this.btnDebugPrint.Size = new System.Drawing.Size(108, 35);
            this.btnDebugPrint.TabIndex = 25;
            this.btnDebugPrint.Text = "Debug";
            this.btnDebugPrint.UseVisualStyleBackColor = true;
            this.btnDebugPrint.Click += new System.EventHandler(this.btnDebugPrint_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(701, 27);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 20);
            this.label2.TabIndex = 27;
            this.label2.Text = "Action Points:";
            // 
            // txtActionPoints
            // 
            this.txtActionPoints.Location = new System.Drawing.Point(815, 23);
            this.txtActionPoints.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtActionPoints.Name = "txtActionPoints";
            this.txtActionPoints.Size = new System.Drawing.Size(38, 26);
            this.txtActionPoints.TabIndex = 26;
            this.txtActionPoints.Text = "2";
            // 
            // frmPossibleTiles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1190, 1023);
            this.Controls.Add(this.txtActionPoints);
            this.Controls.Add(this.btnDebugPrint);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtRange);
            this.Controls.Add(this.txtMap);
            this.Controls.Add(this.btnGenerateMap);
            this.Controls.Add(this.label2);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "frmPossibleTiles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tile Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRange;
        private System.Windows.Forms.TextBox txtMap;
        private System.Windows.Forms.Button btnGenerateMap;
        private System.Windows.Forms.Button btnDebugPrint;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtActionPoints;
    }
}

