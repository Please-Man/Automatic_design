using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.sw3dprinter;
using SolidWorks.Interop.swcommands;
using SolidWorks.Interop.swconst;
using SolidWorks.Interop.swdimxpert;
using SolidWorks.Interop.swdocumentmgr;
using SolidWorks.Interop.swmotionstudy;
using SolidWorks.Interop.swpublished;
using SolidWorks.Interop.SWRoutingLib;

namespace The_tool_of_Automatic_design_ver1._1
{
    public partial class Form1 : Form
    {
        double size1,size2,size3;

        CreateModel_Class cm = new CreateModel_Class();

        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                pictureBox1.Image = Properties.Resources.block;
                label_property1.Text = "a";
                label_property2.Text = "b";
                label_property3.Text = "h";
                textBox_property2.Visible = true;
            }
            if (comboBox1.SelectedIndex == 1)
            {
                pictureBox1.Image = Properties.Resources.clynder;
                label_property1.Text = "d";
                label_property2.Text = " ";
                label_property3.Text = "h";
                textBox_property2.Visible = false;
                textBox_property2.Text = "5"; // 클릭 이벤트시 더블형태의 컨버트의 에러를 막기위한 임시 상수
            }
            if (comboBox1.SelectedIndex == 2)
            {
                pictureBox1.Image = Properties.Resources.hollow_clynder;
                label_property1.Text = "do";
                label_property2.Text = "di";
                label_property3.Text = "h";
                textBox_property2.Visible = true;
            }
        }

        private void drawing_Click(object sender, EventArgs e)
        {
            size1 = Convert.ToDouble(textBox_property1.Text)/1000; //텍스트 박스 실수 변환
            size2 = Convert.ToDouble(textBox_property2.Text)/1000;
            size3 = Convert.ToDouble(textBox_property3.Text)/1000;

            
            if(comboBox1.SelectedIndex == 0)
            {
                cm.CreateB(ref size1, ref size2, ref size3); 
            }
            if (comboBox1.SelectedIndex == 1)
            {
                cm.CreateC(ref size1, ref size3);
            }
            if (comboBox1.SelectedIndex == 2)
            {
                cm.CreateHC(ref size1, ref size2, ref size3);
            }
        }
    }
}
