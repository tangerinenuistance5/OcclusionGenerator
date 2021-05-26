namespace OcclusionGenerator
{
    partial class Main
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
            this.imap_Select = new System.Windows.Forms.Button();
            this.ityp_Select = new System.Windows.Forms.Button();
            this.itypSelectionBox = new System.Windows.Forms.TextBox();
            this.imapSelectionBox = new System.Windows.Forms.TextBox();
            this.generateAudioOcclusion = new System.Windows.Forms.Button();
            this.actionsLog = new System.Windows.Forms.TextBox();
            this.portalEntityDisplay = new System.Windows.Forms.ListBox();
            this.refreshButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // imap_Select
            // 
            this.imap_Select.Location = new System.Drawing.Point(239, 36);
            this.imap_Select.Name = "imap_Select";
            this.imap_Select.Size = new System.Drawing.Size(75, 23);
            this.imap_Select.TabIndex = 0;
            this.imap_Select.Text = "imap";
            this.imap_Select.UseVisualStyleBackColor = true;
            this.imap_Select.Click += new System.EventHandler(this.imap_Select_Click);
            // 
            // ityp_Select
            // 
            this.ityp_Select.Location = new System.Drawing.Point(239, 11);
            this.ityp_Select.Name = "ityp_Select";
            this.ityp_Select.Size = new System.Drawing.Size(75, 23);
            this.ityp_Select.TabIndex = 1;
            this.ityp_Select.Text = "ityp";
            this.ityp_Select.UseVisualStyleBackColor = true;
            this.ityp_Select.Click += new System.EventHandler(this.ityp_Select_Click);
            // 
            // itypSelectionBox
            // 
            this.itypSelectionBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.itypSelectionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.itypSelectionBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.itypSelectionBox.Location = new System.Drawing.Point(10, 12);
            this.itypSelectionBox.Name = "itypSelectionBox";
            this.itypSelectionBox.ReadOnly = true;
            this.itypSelectionBox.Size = new System.Drawing.Size(223, 20);
            this.itypSelectionBox.TabIndex = 2;
            // 
            // imapSelectionBox
            // 
            this.imapSelectionBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.imapSelectionBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imapSelectionBox.ForeColor = System.Drawing.SystemColors.InfoText;
            this.imapSelectionBox.Location = new System.Drawing.Point(10, 38);
            this.imapSelectionBox.Name = "imapSelectionBox";
            this.imapSelectionBox.ReadOnly = true;
            this.imapSelectionBox.Size = new System.Drawing.Size(223, 20);
            this.imapSelectionBox.TabIndex = 3;
            // 
            // generateAudioOcclusion
            // 
            this.generateAudioOcclusion.Location = new System.Drawing.Point(10, 176);
            this.generateAudioOcclusion.Name = "generateAudioOcclusion";
            this.generateAudioOcclusion.Size = new System.Drawing.Size(75, 23);
            this.generateAudioOcclusion.TabIndex = 4;
            this.generateAudioOcclusion.Text = "Generate";
            this.generateAudioOcclusion.UseVisualStyleBackColor = true;
            this.generateAudioOcclusion.Click += new System.EventHandler(this.generateAudioOcclusion_Click);
            // 
            // actionsLog
            // 
            this.actionsLog.Location = new System.Drawing.Point(10, 64);
            this.actionsLog.Multiline = true;
            this.actionsLog.Name = "actionsLog";
            this.actionsLog.ReadOnly = true;
            this.actionsLog.Size = new System.Drawing.Size(304, 106);
            this.actionsLog.TabIndex = 5;
            // 
            // portalEntityDisplay
            // 
            this.portalEntityDisplay.BackColor = System.Drawing.SystemColors.Control;
            this.portalEntityDisplay.FormattingEnabled = true;
            this.portalEntityDisplay.Location = new System.Drawing.Point(321, 11);
            this.portalEntityDisplay.Name = "portalEntityDisplay";
            this.portalEntityDisplay.Size = new System.Drawing.Size(301, 160);
            this.portalEntityDisplay.TabIndex = 6;
            // 
            // refreshButton
            // 
            this.refreshButton.Location = new System.Drawing.Point(91, 176);
            this.refreshButton.Name = "refreshButton";
            this.refreshButton.Size = new System.Drawing.Size(75, 23);
            this.refreshButton.TabIndex = 7;
            this.refreshButton.Text = "Refresh";
            this.refreshButton.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.ClientSize = new System.Drawing.Size(634, 209);
            this.Controls.Add(this.refreshButton);
            this.Controls.Add(this.portalEntityDisplay);
            this.Controls.Add(this.actionsLog);
            this.Controls.Add(this.generateAudioOcclusion);
            this.Controls.Add(this.imapSelectionBox);
            this.Controls.Add(this.itypSelectionBox);
            this.Controls.Add(this.ityp_Select);
            this.Controls.Add(this.imap_Select);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Main";
            this.Text = "Occlusion Generator";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button imap_Select;
        private System.Windows.Forms.Button ityp_Select;
        private System.Windows.Forms.TextBox itypSelectionBox;
        private System.Windows.Forms.TextBox imapSelectionBox;
        private System.Windows.Forms.Button generateAudioOcclusion;
        private System.Windows.Forms.TextBox actionsLog;
        private System.Windows.Forms.ListBox portalEntityDisplay;
        private System.Windows.Forms.Button refreshButton;
    }
}

