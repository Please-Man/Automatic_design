namespace Design_Automation_Machinery
{
    partial class Form_main
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
            this.button_drawing = new System.Windows.Forms.Button();
            this.comboBox_category = new System.Windows.Forms.ComboBox();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_parameter1 = new System.Windows.Forms.TextBox();
            this.textBox_parameter2 = new System.Windows.Forms.TextBox();
            this.textBox_parameter3 = new System.Windows.Forms.TextBox();
            this.textBox_parameter4 = new System.Windows.Forms.TextBox();
            this.label_parameter1 = new System.Windows.Forms.Label();
            this.label_parameter2 = new System.Windows.Forms.Label();
            this.label_parameter3 = new System.Windows.Forms.Label();
            this.label_parameter4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_parameter5 = new System.Windows.Forms.TextBox();
            this.button_drafting = new System.Windows.Forms.Button();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // button_drawing
            // 
            this.button_drawing.Location = new System.Drawing.Point(16, 362);
            this.button_drawing.Name = "button_drawing";
            this.button_drawing.Size = new System.Drawing.Size(75, 23);
            this.button_drawing.TabIndex = 0;
            this.button_drawing.Text = "Drawing";
            this.button_drawing.UseVisualStyleBackColor = true;
            this.button_drawing.Click += new System.EventHandler(this.button_drawing_Click);
            // 
            // comboBox_category
            // 
            this.comboBox_category.FormattingEnabled = true;
            this.comboBox_category.Items.AddRange(new object[] {
            "V벨트풀리",
            "스프로킷"});
            this.comboBox_category.Location = new System.Drawing.Point(49, 40);
            this.comboBox_category.Name = "comboBox_category";
            this.comboBox_category.Size = new System.Drawing.Size(121, 20);
            this.comboBox_category.TabIndex = 1;
            this.comboBox_category.SelectedIndexChanged += new System.EventHandler(this.comboBox_category_SelectedIndexChanged);
            // 
            // comboBox_type
            // 
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Items.AddRange(new object[] {
            "M",
            "A",
            "B",
            "C",
            "D",
            "E"});
            this.comboBox_type.Location = new System.Drawing.Point(49, 86);
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.Size = new System.Drawing.Size(54, 20);
            this.comboBox_type.TabIndex = 2;
            this.comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "카테고리";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(62, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "타입";
            // 
            // textBox_parameter1
            // 
            this.textBox_parameter1.Location = new System.Drawing.Point(97, 132);
            this.textBox_parameter1.Name = "textBox_parameter1";
            this.textBox_parameter1.Size = new System.Drawing.Size(73, 21);
            this.textBox_parameter1.TabIndex = 5;
            // 
            // textBox_parameter2
            // 
            this.textBox_parameter2.Location = new System.Drawing.Point(97, 159);
            this.textBox_parameter2.Name = "textBox_parameter2";
            this.textBox_parameter2.Size = new System.Drawing.Size(73, 21);
            this.textBox_parameter2.TabIndex = 6;
            // 
            // textBox_parameter3
            // 
            this.textBox_parameter3.Location = new System.Drawing.Point(97, 186);
            this.textBox_parameter3.Name = "textBox_parameter3";
            this.textBox_parameter3.Size = new System.Drawing.Size(73, 21);
            this.textBox_parameter3.TabIndex = 7;
            // 
            // textBox_parameter4
            // 
            this.textBox_parameter4.Location = new System.Drawing.Point(97, 213);
            this.textBox_parameter4.Name = "textBox_parameter4";
            this.textBox_parameter4.Size = new System.Drawing.Size(73, 21);
            this.textBox_parameter4.TabIndex = 8;
            // 
            // label_parameter1
            // 
            this.label_parameter1.AutoSize = true;
            this.label_parameter1.Location = new System.Drawing.Point(12, 132);
            this.label_parameter1.Name = "label_parameter1";
            this.label_parameter1.Size = new System.Drawing.Size(53, 12);
            this.label_parameter1.TabIndex = 9;
            this.label_parameter1.Text = "호칭지름";
            // 
            // label_parameter2
            // 
            this.label_parameter2.AutoSize = true;
            this.label_parameter2.Location = new System.Drawing.Point(29, 168);
            this.label_parameter2.Name = "label_parameter2";
            this.label_parameter2.Size = new System.Drawing.Size(15, 12);
            this.label_parameter2.TabIndex = 10;
            this.label_parameter2.Text = "r1";
            // 
            // label_parameter3
            // 
            this.label_parameter3.AutoSize = true;
            this.label_parameter3.Location = new System.Drawing.Point(29, 195);
            this.label_parameter3.Name = "label_parameter3";
            this.label_parameter3.Size = new System.Drawing.Size(15, 12);
            this.label_parameter3.TabIndex = 11;
            this.label_parameter3.Text = "r2";
            // 
            // label_parameter4
            // 
            this.label_parameter4.AutoSize = true;
            this.label_parameter4.Location = new System.Drawing.Point(29, 222);
            this.label_parameter4.Name = "label_parameter4";
            this.label_parameter4.Size = new System.Drawing.Size(15, 12);
            this.label_parameter4.TabIndex = 12;
            this.label_parameter4.Text = "r3";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Design_Automation_Machinery.Properties.Resources.V_belt;
            this.pictureBox1.Location = new System.Drawing.Point(263, 71);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(508, 334);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 13;
            this.pictureBox1.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 248);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 14;
            this.label3.Text = "안지름";
            // 
            // textBox_parameter5
            // 
            this.textBox_parameter5.Location = new System.Drawing.Point(97, 239);
            this.textBox_parameter5.Name = "textBox_parameter5";
            this.textBox_parameter5.Size = new System.Drawing.Size(73, 21);
            this.textBox_parameter5.TabIndex = 15;
            // 
            // button_drafting
            // 
            this.button_drafting.Location = new System.Drawing.Point(124, 362);
            this.button_drafting.Name = "button_drafting";
            this.button_drafting.Size = new System.Drawing.Size(75, 23);
            this.button_drafting.TabIndex = 16;
            this.button_drafting.Text = "Drafting";
            this.button_drafting.UseVisualStyleBackColor = true;
            this.button_drafting.Click += new System.EventHandler(this.button_drafting_Click);
            // 
            // Form_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_drafting);
            this.Controls.Add(this.textBox_parameter5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label_parameter4);
            this.Controls.Add(this.label_parameter3);
            this.Controls.Add(this.label_parameter2);
            this.Controls.Add(this.label_parameter1);
            this.Controls.Add(this.textBox_parameter4);
            this.Controls.Add(this.textBox_parameter3);
            this.Controls.Add(this.textBox_parameter2);
            this.Controls.Add(this.textBox_parameter1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_type);
            this.Controls.Add(this.comboBox_category);
            this.Controls.Add(this.button_drawing);
            this.Name = "Form_main";
            this.Text = "Macinery";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_drawing;
        private System.Windows.Forms.ComboBox comboBox_category;
        private System.Windows.Forms.ComboBox comboBox_type;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_parameter1;
        private System.Windows.Forms.TextBox textBox_parameter2;
        private System.Windows.Forms.TextBox textBox_parameter3;
        private System.Windows.Forms.TextBox textBox_parameter4;
        private System.Windows.Forms.Label label_parameter1;
        private System.Windows.Forms.Label label_parameter2;
        private System.Windows.Forms.Label label_parameter3;
        private System.Windows.Forms.Label label_parameter4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_parameter5;
        private System.Windows.Forms.Button button_drafting;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

