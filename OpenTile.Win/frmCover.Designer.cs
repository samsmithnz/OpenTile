namespace OpenTile.Win
{
    partial class frmCover
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
            this.txtMap = new System.Windows.Forms.TextBox();
            this.btnGenerateMap = new System.Windows.Forms.Button();
            this.btnDebugPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
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
            // frmCover
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(793, 694);
            this.Controls.Add(this.btnDebugPrint);
            this.Controls.Add(this.txtMap);
            this.Controls.Add(this.btnGenerateMap);
            this.Name = "frmCover";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tile Generator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox txtMap;
        private System.Windows.Forms.Button btnGenerateMap;
        private System.Windows.Forms.Button btnDebugPrint;
    }
}

