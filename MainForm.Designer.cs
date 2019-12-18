namespace VisualEditor
{
    partial class MainForm
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
            if(disposing) 
            {
                // Close the project and unload the engine
                CleanUp();

                if(components != null) 
                {
                    components.Dispose();
                }
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
            this.loadButton = new System.Windows.Forms.Button();
            this.documentView = new FCEngine.VisualComponents.DocumentView();
            this.unloadButton = new System.Windows.Forms.Button();
            this.verifyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(182, 13);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(114, 23);
            this.loadButton.TabIndex = 0;
            this.loadButton.Text = "Следующий";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Visible = false;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // documentView
            // 
            this.documentView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.documentView.DocumentViewLayoutOrientation = FCEngine.VisualComponents.DocumentView.Orientation.Horizontal;
            this.documentView.Location = new System.Drawing.Point(12, 50);
            this.documentView.Name = "documentView";
            this.documentView.Size = new System.Drawing.Size(744, 572);
            this.documentView.SplitterDistance = 296;
            this.documentView.TabIndex = 3;
            // 
            // unloadButton
            // 
            this.unloadButton.Location = new System.Drawing.Point(422, 13);
            this.unloadButton.Name = "unloadButton";
            this.unloadButton.Size = new System.Drawing.Size(114, 23);
            this.unloadButton.TabIndex = 2;
            this.unloadButton.Text = "Сохранить";
            this.unloadButton.UseVisualStyleBackColor = true;
            this.unloadButton.Click += new System.EventHandler(this.unloadButton_Click);
            // 
            // verifyButton
            // 
            this.verifyButton.Location = new System.Drawing.Point(302, 13);
            this.verifyButton.Name = "verifyButton";
            this.verifyButton.Size = new System.Drawing.Size(114, 23);
            this.verifyButton.TabIndex = 1;
            this.verifyButton.Text = "Верифицировать";
            this.verifyButton.UseVisualStyleBackColor = true;
            this.verifyButton.Click += new System.EventHandler(this.verifyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Palatino Linotype", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "Действия с документом:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Imprint MT Shadow", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(542, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Готово";
            this.label2.Visible = false;
            // 
            // MainForm
            // 
            this.AcceptButton = this.loadButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(768, 634);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.verifyButton);
            this.Controls.Add(this.documentView);
            this.Controls.Add(this.unloadButton);
            this.Controls.Add(this.loadButton);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Детальная проверка распознанных документов";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button loadButton;
        private FCEngine.VisualComponents.DocumentView documentView;
        private System.Windows.Forms.Button unloadButton;
        private System.Windows.Forms.Button verifyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

