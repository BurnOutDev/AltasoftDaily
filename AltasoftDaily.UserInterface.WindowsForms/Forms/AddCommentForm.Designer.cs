namespace AltasoftDaily.UserInterface.WindowsForms.Forms
{
    partial class AddCommentForm
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
            this.tbxNewComment = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.gridHostory = new System.Windows.Forms.DataGridView();
            this.cbxCommentType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.gridHostory)).BeginInit();
            this.SuspendLayout();
            // 
            // tbxNewComment
            // 
            this.tbxNewComment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbxNewComment.Location = new System.Drawing.Point(12, 211);
            this.tbxNewComment.Multiline = true;
            this.tbxNewComment.Name = "tbxNewComment";
            this.tbxNewComment.Size = new System.Drawing.Size(361, 73);
            this.tbxNewComment.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Location = new System.Drawing.Point(298, 290);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "შენახვა";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.b_Click);
            // 
            // gridHostory
            // 
            this.gridHostory.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridHostory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridHostory.Location = new System.Drawing.Point(12, 12);
            this.gridHostory.Name = "gridHostory";
            this.gridHostory.Size = new System.Drawing.Size(361, 193);
            this.gridHostory.TabIndex = 4;
            // 
            // cbxCommentType
            // 
            this.cbxCommentType.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.cbxCommentType.FormattingEnabled = true;
            this.cbxCommentType.Location = new System.Drawing.Point(12, 291);
            this.cbxCommentType.Name = "cbxCommentType";
            this.cbxCommentType.Size = new System.Drawing.Size(280, 21);
            this.cbxCommentType.TabIndex = 5;
            // 
            // AddCommentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(385, 322);
            this.Controls.Add(this.cbxCommentType);
            this.Controls.Add(this.gridHostory);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.tbxNewComment);
            this.Name = "AddCommentForm";
            this.Text = "AddCommentForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AddCommentForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.gridHostory)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbxNewComment;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView gridHostory;
        private System.Windows.Forms.ComboBox cbxCommentType;
    }
}