namespace OutlookEmailAddIn
{
    partial class ReplyAssistDesignForm
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblEmailContent = new System.Windows.Forms.Label();
            this.txtEmailContent = new System.Windows.Forms.TextBox();
            this.lblTone = new System.Windows.Forms.Label();
            this.cmbTone = new System.Windows.Forms.ComboBox();
            this.btnGenerateReply = new System.Windows.Forms.Button();
            this.lblGeneratedReply = new System.Windows.Forms.Label();
            this.txtGeneratedReply = new System.Windows.Forms.TextBox();
            this.btnCopyReply = new System.Windows.Forms.Button();
            this.btnInsertReply = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font(
                "Segoe UI",
                16F,
                System.Drawing.FontStyle.Bold
            );
            this.lblTitle.Location = new System.Drawing.Point(30, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(170, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Reply Assist";

            // 
            // lblEmailContent
            // 
            this.lblEmailContent.AutoSize = true;
            this.lblEmailContent.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold
            );
            this.lblEmailContent.Location = new System.Drawing.Point(30, 75);
            this.lblEmailContent.Name = "lblEmailContent";
            this.lblEmailContent.Size = new System.Drawing.Size(120, 19);
            this.lblEmailContent.TabIndex = 1;
            this.lblEmailContent.Text = "Email Content";

            // 
            // txtEmailContent
            // 
            this.txtEmailContent.Location = new System.Drawing.Point(30, 105);
            this.txtEmailContent.Multiline = true;
            this.txtEmailContent.Name = "txtEmailContent";
            this.txtEmailContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmailContent.Size = new System.Drawing.Size(620, 130);
            this.txtEmailContent.TabIndex = 2;

            // 
            // lblTone
            // 
            this.lblTone.AutoSize = true;
            this.lblTone.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold
            );
            this.lblTone.Location = new System.Drawing.Point(30, 260);
            this.lblTone.Name = "lblTone";
            this.lblTone.Size = new System.Drawing.Size(85, 19);
            this.lblTone.TabIndex = 3;
            this.lblTone.Text = "Reply Tone";

            // 
            // cmbTone
            // 
            this.cmbTone.DropDownStyle =
                System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTone.FormattingEnabled = true;
            this.cmbTone.Items.AddRange(new object[]
            {
                "Professional",
                "Friendly",
                "Formal",
                "Short & Simple"
            });
            this.cmbTone.Location = new System.Drawing.Point(30, 290);
            this.cmbTone.Name = "cmbTone";
            this.cmbTone.Size = new System.Drawing.Size(220, 24);
            this.cmbTone.TabIndex = 4;

            // 
            // btnGenerateReply
            // 
            this.btnGenerateReply.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold
            );
            this.btnGenerateReply.Location = new System.Drawing.Point(30, 335);
            this.btnGenerateReply.Name = "btnGenerateReply";
            this.btnGenerateReply.Size = new System.Drawing.Size(180, 40);
            this.btnGenerateReply.TabIndex = 5;
            this.btnGenerateReply.Text = "Generate Reply";
            this.btnGenerateReply.UseVisualStyleBackColor = true;

            // 
            // lblGeneratedReply
            // 
            this.lblGeneratedReply.AutoSize = true;
            this.lblGeneratedReply.Font = new System.Drawing.Font(
                "Segoe UI",
                10F,
                System.Drawing.FontStyle.Bold
            );
            this.lblGeneratedReply.Location = new System.Drawing.Point(30, 405);
            this.lblGeneratedReply.Name = "lblGeneratedReply";
            this.lblGeneratedReply.Size = new System.Drawing.Size(120, 19);
            this.lblGeneratedReply.TabIndex = 6;
            this.lblGeneratedReply.Text = "Generated Reply";

            // 
            // txtGeneratedReply
            // 
            this.txtGeneratedReply.Location = new System.Drawing.Point(30, 435);
            this.txtGeneratedReply.Multiline = true;
            this.txtGeneratedReply.Name = "txtGeneratedReply";
            this.txtGeneratedReply.ScrollBars =
                System.Windows.Forms.ScrollBars.Vertical;
            this.txtGeneratedReply.Size = new System.Drawing.Size(620, 130);
            this.txtGeneratedReply.TabIndex = 7;

            // 
            // btnCopyReply
            // 
            this.btnCopyReply.Font = new System.Drawing.Font(
                "Segoe UI",
                9F,
                System.Drawing.FontStyle.Bold
            );
            this.btnCopyReply.Location = new System.Drawing.Point(30, 585);
            this.btnCopyReply.Name = "btnCopyReply";
            this.btnCopyReply.Size = new System.Drawing.Size(140, 35);
            this.btnCopyReply.TabIndex = 8;
            this.btnCopyReply.Text = "Copy Reply";
            this.btnCopyReply.UseVisualStyleBackColor = true;

            // 
            // btnInsertReply
            // 
            this.btnInsertReply.Font = new System.Drawing.Font(
                "Segoe UI",
                9F,
                System.Drawing.FontStyle.Bold
            );
            this.btnInsertReply.Location = new System.Drawing.Point(190, 585);
            this.btnInsertReply.Name = "btnInsertReply";
            this.btnInsertReply.Size = new System.Drawing.Size(140, 35);
            this.btnInsertReply.TabIndex = 9;
            this.btnInsertReply.Text = "Insert Reply";
            this.btnInsertReply.UseVisualStyleBackColor = true;

            // 
            // ReplyAssistDesignForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 650);

            this.Controls.Add(this.btnInsertReply);
            this.Controls.Add(this.btnCopyReply);
            this.Controls.Add(this.txtGeneratedReply);
            this.Controls.Add(this.lblGeneratedReply);
            this.Controls.Add(this.btnGenerateReply);
            this.Controls.Add(this.cmbTone);
            this.Controls.Add(this.lblTone);
            this.Controls.Add(this.txtEmailContent);
            this.Controls.Add(this.lblEmailContent);
            this.Controls.Add(this.lblTitle);

            this.Name = "ReplyAssistDesignForm";
            this.StartPosition =
                System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Reply Assist";

            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblEmailContent;
        private System.Windows.Forms.TextBox txtEmailContent;
        private System.Windows.Forms.Label lblTone;
        private System.Windows.Forms.ComboBox cmbTone;
        private System.Windows.Forms.Button btnGenerateReply;
        private System.Windows.Forms.Label lblGeneratedReply;
        private System.Windows.Forms.TextBox txtGeneratedReply;
        private System.Windows.Forms.Button btnCopyReply;
        private System.Windows.Forms.Button btnInsertReply;
    }
}