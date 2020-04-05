namespace The_tool_of_Automatic_design_ver1._1
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
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_property1 = new System.Windows.Forms.Label();
            this.textBox_property1 = new System.Windows.Forms.TextBox();
            this.label_property2 = new System.Windows.Forms.Label();
            this.textBox_property2 = new System.Windows.Forms.TextBox();
            this.label_property3 = new System.Windows.Forms.Label();
            this.textBox_property3 = new System.Windows.Forms.TextBox();
            this.drawing = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "block",
            "clynder",
            "hollow_clynder"});
            this.comboBox1.Location = new System.Drawing.Point(31, 52);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 0;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(59, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "카테고리";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(497, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "미리보기";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(280, 52);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(484, 355);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // label_property1
            // 
            this.label_property1.AutoSize = true;
            this.label_property1.Location = new System.Drawing.Point(31, 95);
            this.label_property1.Name = "label_property1";
            this.label_property1.Size = new System.Drawing.Size(0, 12);
            this.label_property1.TabIndex = 4;
            // 
            // textBox_property1
            // 
            this.textBox_property1.Location = new System.Drawing.Point(31, 111);
            this.textBox_property1.Name = "textBox_property1";
            this.textBox_property1.Size = new System.Drawing.Size(100, 21);
            this.textBox_property1.TabIndex = 5;
            // 
            // label_property2
            // 
            this.label_property2.AutoSize = true;
            this.label_property2.Location = new System.Drawing.Point(31, 150);
            this.label_property2.Name = "label_property2";
            this.label_property2.Size = new System.Drawing.Size(0, 12);
            this.label_property2.TabIndex = 6;
            // 
            // textBox_property2
            // 
            this.textBox_property2.Location = new System.Drawing.Point(31, 166);
            this.textBox_property2.Name = "textBox_property2";
            this.textBox_property2.Size = new System.Drawing.Size(100, 21);
            this.textBox_property2.TabIndex = 7;
            // 
            // label_property3
            // 
            this.label_property3.AutoSize = true;
            this.label_property3.Location = new System.Drawing.Point(31, 206);
            this.label_property3.Name = "label_property3";
            this.label_property3.Size = new System.Drawing.Size(0, 12);
            this.label_property3.TabIndex = 8;
            // 
            // textBox_property3
            // 
            this.textBox_property3.Location = new System.Drawing.Point(31, 222);
            this.textBox_property3.Name = "textBox_property3";
            this.textBox_property3.Size = new System.Drawing.Size(100, 21);
            this.textBox_property3.TabIndex = 9;
            // 
            // drawing
            // 
            this.drawing.Location = new System.Drawing.Point(31, 383);
            this.drawing.Name = "drawing";
            this.drawing.Size = new System.Drawing.Size(75, 23);
            this.drawing.TabIndex = 10;
            this.drawing.Text = "Drawing";
            this.drawing.UseVisualStyleBackColor = true;
            this.drawing.Click += new System.EventHandler(this.drawing_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.drawing);
            this.Controls.Add(this.textBox_property3);
            this.Controls.Add(this.label_property3);
            this.Controls.Add(this.textBox_property2);
            this.Controls.Add(this.label_property2);
            this.Controls.Add(this.textBox_property1);
            this.Controls.Add(this.label_property1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBox1);
            this.Name = "Form1";
            this.Text = "Automatic_design";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label_property1;
        private System.Windows.Forms.TextBox textBox_property1;
        private System.Windows.Forms.Label label_property2;
        private System.Windows.Forms.TextBox textBox_property2;
        private System.Windows.Forms.Label label_property3;
        private System.Windows.Forms.TextBox textBox_property3;
        private System.Windows.Forms.Button drawing;
    }
}

