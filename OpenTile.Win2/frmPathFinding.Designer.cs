namespace OpenTile.Win
{
    partial class frmPathFinding
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtFinishZ = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFinishX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtStartZ = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStartX = new System.Windows.Forms.TextBox();
            this.optRandomExample = new System.Windows.Forms.RadioButton();
            this.optBaseExample = new System.Windows.Forms.RadioButton();
            this.txtMap = new System.Windows.Forms.TextBox();
            this.btnGenerateMap = new System.Windows.Forms.Button();
            this.btnDebugPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(625, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "FinishZ:";
            // 
            // txtFinishZ
            // 
            this.txtFinishZ.Location = new System.Drawing.Point(675, 15);
            this.txtFinishZ.Name = "txtFinishZ";
            this.txtFinishZ.Size = new System.Drawing.Size(27, 20);
            this.txtFinishZ.TabIndex = 22;
            this.txtFinishZ.Text = "2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(542, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 21;
            this.label3.Text = "FinishX:";
            // 
            // txtFinishX
            // 
            this.txtFinishX.Location = new System.Drawing.Point(592, 15);
            this.txtFinishX.Name = "txtFinishX";
            this.txtFinishX.Size = new System.Drawing.Size(27, 20);
            this.txtFinishX.TabIndex = 20;
            this.txtFinishX.Text = "5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(464, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 19;
            this.label2.Text = "StartZ:";
            // 
            // txtStartZ
            // 
            this.txtStartZ.Location = new System.Drawing.Point(509, 15);
            this.txtStartZ.Name = "txtStartZ";
            this.txtStartZ.Size = new System.Drawing.Size(27, 20);
            this.txtStartZ.TabIndex = 18;
            this.txtStartZ.Text = "2";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(386, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "StartX:";
            // 
            // txtStartX
            // 
            this.txtStartX.Location = new System.Drawing.Point(431, 15);
            this.txtStartX.Name = "txtStartX";
            this.txtStartX.Size = new System.Drawing.Size(27, 20);
            this.txtStartX.TabIndex = 16;
            this.txtStartX.Text = "1";
            // 
            // optRandomExample
            // 
            this.optRandomExample.AutoSize = true;
            this.optRandomExample.Location = new System.Drawing.Point(227, 16);
            this.optRandomExample.Name = "optRandomExample";
            this.optRandomExample.Size = new System.Drawing.Size(137, 17);
            this.optRandomExample.TabIndex = 15;
            this.optRandomExample.Text = "Random Example (25%)";
            this.optRandomExample.UseVisualStyleBackColor = true;
            // 
            // optBaseExample
            // 
            this.optBaseExample.AutoSize = true;
            this.optBaseExample.Checked = true;
            this.optBaseExample.Location = new System.Drawing.Point(129, 16);
            this.optBaseExample.Name = "optBaseExample";
            this.optBaseExample.Size = new System.Drawing.Size(92, 17);
            this.optBaseExample.TabIndex = 14;
            this.optBaseExample.TabStop = true;
            this.optBaseExample.Text = "Base Example";
            this.optBaseExample.UseVisualStyleBackColor = true;
            // 
            // txtMap
            // 
            this.txtMap.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMap.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMap.Location = new System.Drawing.Point(12, 42);
            this.txtMap.Multiline = true;
            this.txtMap.Name = "txtMap";
            this.txtMap.Size = new System.Drawing.Size(768, 640);
            this.txtMap.TabIndex = 13;
            // 
            // btnGenerateMap
            // 
            this.btnGenerateMap.Location = new System.Drawing.Point(12, 13);
            this.btnGenerateMap.Name = "btnGenerateMap";
            this.btnGenerateMap.Size = new System.Drawing.Size(111, 23);
            this.btnGenerateMap.TabIndex = 12;
            this.btnGenerateMap.Text = "Generate Map";
            this.btnGenerateMap.UseVisualStyleBackColor = true;
            this.btnGenerateMap.Click += new System.EventHandler(this.btnGenerateMap_Click);
            // 
            // btnDebugPrint
            // 
            this.btnDebugPrint.Location = new System.Drawing.Point(708, 12);
            this.btnDebugPrint.Name = "btnDebugPrint";
            this.btnDebugPrint.Size = new System.Drawing.Size(72, 23);
            this.btnDebugPrint.TabIndex = 24;
            this.btnDebugPrint.Text = "Debug";
            this.btnDebugPrint.UseVisualStyleBackColor = true;
            this.btnDebugPrint.Click += new System.EventHandler(this.btnDebugPrint_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 694);
            this.Controls.Add(this.btnDebugPrint);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtFinishZ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtFinishX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtStartZ);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStartX);
            this.Controls.Add(this.optRandomExample);
            this.Controls.Add(this.optBaseExample);
            this.Controls.Add(this.txtMap);
            this.Controls.Add(this.btnGenerateMap);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tile Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtFinishZ;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFinishX;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtStartZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtStartX;
        private System.Windows.Forms.RadioButton optRandomExample;
        private System.Windows.Forms.RadioButton optBaseExample;
        private System.Windows.Forms.TextBox txtMap;
        private System.Windows.Forms.Button btnGenerateMap;
        private System.Windows.Forms.Button btnDebugPrint;
    }
}

