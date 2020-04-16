namespace SchemaConverter
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
			this.OpenDocx = new System.Windows.Forms.Button();
			this.SaveXml = new System.Windows.Forms.Button();
			this.StatusTextBox = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// OpenDocx
			// 
			this.OpenDocx.Location = new System.Drawing.Point(95, 192);
			this.OpenDocx.Margin = new System.Windows.Forms.Padding(105, 0, 105, 5);
			this.OpenDocx.Name = "OpenDocx";
			this.OpenDocx.Size = new System.Drawing.Size(190, 26);
			this.OpenDocx.TabIndex = 0;
			this.OpenDocx.Text = "Открыть DOCX-файл";
			this.OpenDocx.UseVisualStyleBackColor = true;
			this.OpenDocx.Click += new System.EventHandler(this.OpenDocx_Click);
			// 
			// SaveXml
			// 
			this.SaveXml.Location = new System.Drawing.Point(95, 223);
			this.SaveXml.Margin = new System.Windows.Forms.Padding(105, 0, 105, 5);
			this.SaveXml.Name = "SaveXml";
			this.SaveXml.Size = new System.Drawing.Size(190, 26);
			this.SaveXml.TabIndex = 2;
			this.SaveXml.Text = "Сохранить XML-файл";
			this.SaveXml.UseVisualStyleBackColor = true;
			this.SaveXml.Click += new System.EventHandler(this.SaveXml_Click);
			// 
			// StatusTextBox
			// 
			this.StatusTextBox.Location = new System.Drawing.Point(95, 12);
			this.StatusTextBox.Name = "StatusTextBox";
			this.StatusTextBox.ReadOnly = true;
			this.StatusTextBox.Size = new System.Drawing.Size(190, 85);
			this.StatusTextBox.TabIndex = 3;
			this.StatusTextBox.Text = "Программа готова к работе";
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 261);
			this.Controls.Add(this.StatusTextBox);
			this.Controls.Add(this.SaveXml);
			this.Controls.Add(this.OpenDocx);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(400, 300);
			this.MinimumSize = new System.Drawing.Size(400, 300);
			this.Name = "Form1";
			this.Text = "Конвертер";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button OpenDocx;
		private System.Windows.Forms.Button SaveXml;
		private System.Windows.Forms.RichTextBox StatusTextBox;
	}
}

