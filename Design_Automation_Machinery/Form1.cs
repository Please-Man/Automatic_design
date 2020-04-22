using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Automation_Machinery
{
    public partial class Form_main : Form
    {
        CreateModel_Class cm = new CreateModel_Class();

        double dp, a, l0, k, k0, e_, f, r1, r2, r3; // V벨트 참조 변수
        double i1, i2, i3; //호칭 지름 범위 변수
        double di , t, b; // 안지름(샤프트 홀) 및 키 홈 변수

        public Form_main()
        {
            InitializeComponent();
        }

        private void comboBox_category_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_category.SelectedIndex == 0)
            {
                label2.Visible = true;
                comboBox_type.Visible = true;
            }
            else
            {
                label2.Visible = false;
                comboBox_type.Visible = false;
            }
        }

        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox_type.SelectedIndex == 0)
            {
                l0 = 8.0 / 2000;
                k = 2.7 / 1000;
                k0 = 6.3 / 1000;
                e_ = 0;
                f = 9.5 / 1000;
                label_parameter1.Text = " 호칭지름\n(50mm이상)";
                label_parameter2.Text = "r1(0.2~0.5)";
                label_parameter3.Text = "r2(0.5~1.0)";
                label_parameter4.Text = "r3(1.0~2.0)";
                i1 = 50;
                i2 = 71;
                i3 = 90;
            }
            else if (comboBox_type.SelectedIndex == 1)
            {
                l0 = 9.2 / 2000;
                k = 4.5 / 1000;
                k0 = 8.0 / 1000;
                e_ = 15;
                f = 10.0 / 1000;
                label_parameter1.Text = " 호칭지름\n(71mm이상)";
                label_parameter2.Text = "r1(0.2~0.5)";
                label_parameter3.Text = "r2(0.5~1.0)";
                label_parameter4.Text = "r3(1.0~2.0)";
                i1 = 71;
                i2 = 100;
                i3 = 125;
            }
            else if (comboBox_type.SelectedIndex == 2)
            {
                l0 = 12.5 / 2000;
                k = 5.5 / 1000;
                k0 = 9.5 / 1000;
                e_ = 19;
                f = 12.5 / 1000;
                label_parameter1.Text = " 호칭지름\n(125mm이상)";
                label_parameter2.Text = "r1(0.2~0.5)";
                label_parameter3.Text = "r2(0.5~1.0)";
                label_parameter4.Text = "r3(1.0~2.0)";
                i1 = 125;
                i2 = 169;
                i3 = 315;
            }
            else if (comboBox_type.SelectedIndex == 3) //C타입
            {
                l0 = 16.9 / 2000;
                k = 7 / 1000;
                k0 = 12 / 1000;
                e_ = 25.5;
                f = 17 / 1000;
                label_parameter1.Text = " 호칭지름\n(200mm이상)";
                label_parameter2.Text = "r1(0.2~0.5)";
                label_parameter3.Text = "r2(0.5~1.6)";
                label_parameter4.Text = "r3(2.0~3.0)";
                i1 = 200;
                i2 = 250;
                i3 = 315;
            }
            else if (comboBox_type.SelectedIndex == 4)
            {
                l0 = 24.6 / 2000;
                k = 9.5 / 1000;
                k0 = 15.5 / 1000;
                e_ = 37;
                f = 24 / 1000;
                label_parameter1.Text = " 호칭지름\n(355mm이상)";
                label_parameter2.Text = "r1(0.2~0.5)";
                label_parameter3.Text = "r2(1.6~2.0)";
                label_parameter4.Text = "r3(3.0~4.0)";
                i1 = 455;
                i2 = 355;
                i3 = 450;
            }
            else if (comboBox_type.SelectedIndex == 5)
            {
                l0 = 28.7 / 2000;
                k = 12.7 / 1000;
                k0 = 19.3 / 1000;
                e_ = 44.5;
                f = 29 / 1000;
                label_parameter1.Text = " 호칭지름\n(500mm이상)";
                label_parameter2.Text = "r1(0.2~0.5)";
                label_parameter3.Text = "r2(1.6~2.0)";
                label_parameter4.Text = "r3(4.0~5.0)";
                i1 = 635;
                i2 = 500;
                i3 = 630;
            }
        }

        private void button_drawing_Click(object sender, EventArgs e)
        {
            dp = Convert.ToDouble(textBox_parameter1.Text);
            r1 = Convert.ToDouble(textBox_parameter2.Text) / 1000;
            r2 = Convert.ToDouble(textBox_parameter3.Text) / 1000;
            r3 = Convert.ToDouble(textBox_parameter4.Text) / 1000;

            di = Convert.ToDouble(textBox_parameter4.Text);
            keyhole(ref di);

            if (dp > i2 && dp <= i3)
            {
                a = 180 - 18;
            }
            else if(dp > i3)
            {
                a = 180 - 19;
            }
            else if (dp >= i1 && dp <= i2) 
            {
                a = 180 - 17;
            }
            else
            {
                MessageBox.Show("재입력 하세요");
            }

            if (a != null)
            {
                Console.WriteLine(a); //디버깅 용
                dp = dp / 2000;
                cm.CreateV1(ref dp, ref a, ref l0, ref k, ref k0, ref f, ref r1, ref r2, ref r3);
                cm.CutKeyHole(ref di, ref t, ref b);
            }        
        }

        private void keyhole(ref double di)
        {
            if(di < 8) { t = 1.0; b = 2; }
            else if (di < 10) { t = 1.4; b = 3; }
            else if (di < 12) { t = 1.8; b = 4; }
            else if (di < 17) { t = 2.3; b = 5; }
            else if (di < 22) { t = 2.8; b = 6; }
            else if (di < 30) { t = 3.3; b = 8; }
            else if (di < 38) { t = 3.3; b = 10; }
            else if (di < 44) { t = 3.3; b = 12; }
            else if (di < 50) { t = 3.8; b = 14; }
            else if (di < 58) { t = 4.3; b = 16; }
            else if (di < 65) { t = 4.4; b = 18; }
            di = di / 1000;
            t = t / 1000;
            b = b / 1000;
        }
    }
}
