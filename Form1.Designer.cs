namespace SimpleImageToMP4
{
	partial class Form1
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			btnSelectDirectory = new Button();
			label1 = new Label();
			txtSelectedDirectory = new TextBox();
			btnStartConvert = new Button();
			comboBox1 = new ComboBox();
			chkWhatsapp = new CheckBox();
			cmbFPS = new ComboBox();
			label2 = new Label();
			label3 = new Label();
			progressBar1 = new ProgressBar();
			statusLabel = new Label();
			SuspendLayout();
			// 
			// btnSelectDirectory
			// 
			btnSelectDirectory.Location = new Point(496, 83);
			btnSelectDirectory.Name = "btnSelectDirectory";
			btnSelectDirectory.Size = new Size(32, 23);
			btnSelectDirectory.TabIndex = 0;
			btnSelectDirectory.Text = "...";
			btnSelectDirectory.UseVisualStyleBackColor = true;
			btnSelectDirectory.Click += btnSelectDirectory_Click_1;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Location = new Point(14, 59);
			label1.Name = "label1";
			label1.Size = new Size(26, 15);
			label1.TabIndex = 1;
			label1.Text = "FPS";
			// 
			// txtSelectedDirectory
			// 
			txtSelectedDirectory.Location = new Point(14, 84);
			txtSelectedDirectory.Name = "txtSelectedDirectory";
			txtSelectedDirectory.Size = new Size(476, 23);
			txtSelectedDirectory.TabIndex = 2;
			// 
			// btnStartConvert
			// 
			btnStartConvert.Location = new Point(453, 115);
			btnStartConvert.Name = "btnStartConvert";
			btnStartConvert.Size = new Size(75, 23);
			btnStartConvert.TabIndex = 3;
			btnStartConvert.Text = "Convert";
			btnStartConvert.UseVisualStyleBackColor = true;
			btnStartConvert.Click += btnStartConvert_Click;
			// 
			// comboBox1
			// 
			comboBox1.FormattingEnabled = true;
			comboBox1.Items.AddRange(new object[] { "1920x1080", "1280x720", "720x480" });
			comboBox1.Location = new Point(199, 56);
			comboBox1.Name = "comboBox1";
			comboBox1.Size = new Size(121, 23);
			comboBox1.TabIndex = 5;
			// 
			// chkWhatsapp
			// 
			chkWhatsapp.AutoSize = true;
			chkWhatsapp.Location = new Point(340, 57);
			chkWhatsapp.Name = "chkWhatsapp";
			chkWhatsapp.Size = new Size(150, 19);
			chkWhatsapp.TabIndex = 7;
			chkWhatsapp.Text = "Optimize for WhatsApp";
			chkWhatsapp.UseVisualStyleBackColor = true;
			// 
			// cmbFPS
			// 
			cmbFPS.FormattingEnabled = true;
			cmbFPS.Items.AddRange(new object[] { "30", "60" });
			cmbFPS.Location = new Point(46, 57);
			cmbFPS.Name = "cmbFPS";
			cmbFPS.Size = new Size(63, 23);
			cmbFPS.TabIndex = 8;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Location = new Point(130, 60);
			label2.Name = "label2";
			label2.Size = new Size(63, 15);
			label2.TabIndex = 9;
			label2.Text = "Resolution";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
			label3.Location = new Point(14, 9);
			label3.Name = "label3";
			label3.Size = new Size(245, 21);
			label3.TabIndex = 10;
			label3.Text = "Simple Image 2 MP4 Converter";
			// 
			// progressBar1
			// 
			progressBar1.Location = new Point(14, 113);
			progressBar1.Name = "progressBar1";
			progressBar1.Size = new Size(193, 23);
			progressBar1.TabIndex = 11;
			// 
			// statusLabel
			// 
			statusLabel.AutoSize = true;
			statusLabel.Location = new Point(220, 119);
			statusLabel.Name = "statusLabel";
			statusLabel.Size = new Size(39, 15);
			statusLabel.TabIndex = 12;
			statusLabel.Text = "Status";
			// 
			// Form1
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(542, 148);
			Controls.Add(statusLabel);
			Controls.Add(progressBar1);
			Controls.Add(label3);
			Controls.Add(label2);
			Controls.Add(cmbFPS);
			Controls.Add(chkWhatsapp);
			Controls.Add(comboBox1);
			Controls.Add(btnStartConvert);
			Controls.Add(txtSelectedDirectory);
			Controls.Add(label1);
			Controls.Add(btnSelectDirectory);
			MaximizeBox = false;
			Name = "Form1";
			Text = "Simple Image 2 MP4 Converter";
			FormClosing += Form1_FormClosing;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private Button btnSelectDirectory;
		private Label label1;
		private TextBox txtSelectedDirectory;
		private Button btnStartConvert;
		private ComboBox comboBox1;
		private CheckBox chkWhatsapp;
		private ComboBox cmbFPS;
		private Label label2;
		private Label label3;
		private ProgressBar progressBar1;
		private Label statusLabel;
	}
}
