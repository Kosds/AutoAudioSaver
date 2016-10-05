namespace AutoAudioSaver
{
    partial class TrackListForm
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
            this.trackListBox = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.SaveButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.DownloadingPath = new System.Windows.Forms.TextBox();
            this.ChangePath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // trackListBox
            // 
            this.trackListBox.FormattingEnabled = true;
            this.trackListBox.Location = new System.Drawing.Point(1, 2);
            this.trackListBox.Name = "trackListBox";
            this.trackListBox.Size = new System.Drawing.Size(282, 186);
            this.trackListBox.TabIndex = 0;
            this.trackListBox.DoubleClick += new System.EventHandler(this.trackListBox_DoubleClick);
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(1, 194);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(282, 23);
            this.progressBar.TabIndex = 1;
            // 
            // SaveButton
            // 
            this.SaveButton.Location = new System.Drawing.Point(1, 223);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(91, 23);
            this.SaveButton.TabIndex = 2;
            this.SaveButton.Text = "Скачать новые";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // DownloadingPath
            // 
            this.DownloadingPath.Location = new System.Drawing.Point(98, 225);
            this.DownloadingPath.Name = "DownloadingPath";
            this.DownloadingPath.Size = new System.Drawing.Size(157, 20);
            this.DownloadingPath.TabIndex = 3;
            // 
            // ChangePath
            // 
            this.ChangePath.Location = new System.Drawing.Point(259, 224);
            this.ChangePath.Name = "ChangePath";
            this.ChangePath.Size = new System.Drawing.Size(21, 23);
            this.ChangePath.TabIndex = 4;
            this.ChangePath.Text = " ";
            this.ChangePath.UseVisualStyleBackColor = true;
            this.ChangePath.Click += new System.EventHandler(this.ChangePath_Click);
            // 
            // TrackListForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 251);
            this.Controls.Add(this.ChangePath);
            this.Controls.Add(this.DownloadingPath);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.trackListBox);
            this.Name = "TrackListForm";
            this.Text = "TrackList";
            this.Load += new System.EventHandler(this.TrackListLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox trackListBox;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.TextBox DownloadingPath;
        private System.Windows.Forms.Button ChangePath;
    }
}