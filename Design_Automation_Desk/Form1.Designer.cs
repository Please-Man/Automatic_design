namespace Design_Automation_Desk
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.Button_drawing = new System.Windows.Forms.Button();
            this.textBox_propery1 = new System.Windows.Forms.TextBox();
            this.textBox_propery2 = new System.Windows.Forms.TextBox();
            this.textBox_propery3 = new System.Windows.Forms.TextBox();
            this.textBox_propery4 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.comboBox_material = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.Button_edit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // Button_drawing
            // 
            this.Button_drawing.Location = new System.Drawing.Point(26, 352);
            this.Button_drawing.Name = "Button_drawing";
            this.Button_drawing.Size = new System.Drawing.Size(75, 23);
            this.Button_drawing.TabIndex = 0;
            this.Button_drawing.Text = "Drawing";
            this.Button_drawing.UseVisualStyleBackColor = true;
            this.Button_drawing.Click += new System.EventHandler(this.Button_drawing_Click);
            // 
            // textBox_propery1
            // 
            this.textBox_propery1.Location = new System.Drawing.Point(85, 92);
            this.textBox_propery1.Name = "textBox_propery1";
            this.textBox_propery1.Size = new System.Drawing.Size(100, 21);
            this.textBox_propery1.TabIndex = 1;
            // 
            // textBox_propery2
            // 
            this.textBox_propery2.Location = new System.Drawing.Point(85, 130);
            this.textBox_propery2.Name = "textBox_propery2";
            this.textBox_propery2.Size = new System.Drawing.Size(100, 21);
            this.textBox_propery2.TabIndex = 2;
            // 
            // textBox_propery3
            // 
            this.textBox_propery3.Location = new System.Drawing.Point(85, 168);
            this.textBox_propery3.Name = "textBox_propery3";
            this.textBox_propery3.Size = new System.Drawing.Size(100, 21);
            this.textBox_propery3.TabIndex = 3;
            // 
            // textBox_propery4
            // 
            this.textBox_propery4.Location = new System.Drawing.Point(85, 205);
            this.textBox_propery4.Name = "textBox_propery4";
            this.textBox_propery4.Size = new System.Drawing.Size(100, 21);
            this.textBox_propery4.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "길이";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 6;
            this.label2.Text = "너비";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 176);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "두께";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(24, 213);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "높이";
            // 
            // comboBox_material
            // 
            this.comboBox_material.FormattingEnabled = true;
            this.comboBox_material.Items.AddRange(new object[] {
            "오크",
            "삼나무",
            "티크",
            "소나무"});
            this.comboBox_material.Location = new System.Drawing.Point(64, 257);
            this.comboBox_material.Name = "comboBox_material";
            this.comboBox_material.Size = new System.Drawing.Size(121, 20);
            this.comboBox_material.TabIndex = 9;
            this.comboBox_material.SelectedIndexChanged += new System.EventHandler(this.comboBox_material_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 265);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "재질";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "기본",
            "서랍",
            "책장"});
            this.comboBox2.Location = new System.Drawing.Point(64, 31);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 20);
            this.comboBox2.TabIndex = 11;
            this.comboBox2.SelectedIndexChanged += new System.EventHandler(this.comboBox2_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(24, 39);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 12);
            this.label6.TabIndex = 12;
            this.label6.Text = "종류";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(363, 92);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(387, 237);
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // Button_edit
            // 
            this.Button_edit.Location = new System.Drawing.Point(141, 352);
            this.Button_edit.Name = "Button_edit";
            this.Button_edit.Size = new System.Drawing.Size(75, 23);
            this.Button_edit.TabIndex = 14;
            this.Button_edit.Text = "Edit";
            this.Button_edit.UseVisualStyleBackColor = true;
            this.Button_edit.Click += new System.EventHandler(this.Button_edit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.Button_edit);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.comboBox_material);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_propery4);
            this.Controls.Add(this.textBox_propery3);
            this.Controls.Add(this.textBox_propery2);
            this.Controls.Add(this.textBox_propery1);
            this.Controls.Add(this.Button_drawing);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_drawing;
        private System.Windows.Forms.TextBox textBox_propery1;
        private System.Windows.Forms.TextBox textBox_propery2;
        private System.Windows.Forms.TextBox textBox_propery3;
        private System.Windows.Forms.TextBox textBox_propery4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ComboBox comboBox_material;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button Button_edit;
    }
}

