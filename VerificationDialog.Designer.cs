namespace VisualEditor
{
    partial class VerificationDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose( bool disposing )
        {
            if( disposing ) {
                
                // Calls Dispose for the verificationView
                cleanUp();
                
                if( components != null ) {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.verificationView = new FCEngine.VisualComponents.VerificationView();
            this.SuspendLayout();
            // 
            // verificationView
            // 
            this.verificationView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verificationView.Location = new System.Drawing.Point(0, 0);
            this.verificationView.Name = "verificationView";
            this.verificationView.Size = new System.Drawing.Size(574, 418);
            this.verificationView.SplitterDistance = 239;
            this.verificationView.TabIndex = 0;
            this.verificationView.VerificationCompleted += new System.EventHandler<FCEngine.VisualComponents.VerificationCompletedEventArgs>(this.verificationView_VerificationCompleted);
            // 
            // VerificationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(574, 418);
            this.Controls.Add(this.verificationView);
            this.Name = "VerificationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Верификация данных";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VerificationForm_FormClosing);
            this.Load += new System.EventHandler(this.VerificationDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private FCEngine.VisualComponents.VerificationView verificationView;


    }
}