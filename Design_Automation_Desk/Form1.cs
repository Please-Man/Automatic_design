using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design_Automation_Desk
{
    public partial class Form1 : Form
    {
        double x, y, t, h;
        string fileName1, fileName2, fileName3, fileName4, fileName5, material;

        private void Button_edit_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(); // 다이어로그 보이기
            string folderName = folderBrowserDialog1.SelectedPath; // 폴더 경로를 string변수에 할당
            fileName1 = folderName + "\\desk.sldprt"; // 책상의 이름 
            fileName2 = folderName + "\\pillar.sldprt"; // 기둥의 이름 
            fileName4 = folderName + "\\bookcase.sldprt"; //책장의 이름

            x = Convert.ToDouble(textBox_propery1.Text) / 1000.0;
            y = Convert.ToDouble(textBox_propery2.Text) / 1000.0;
            t = Convert.ToDouble(textBox_propery3.Text) / 1000.0;
            h = Convert.ToDouble(textBox_propery4.Text) / 1000.0;

            cm.Edit(ref x, ref y, ref t, ref h, ref fileName1, ref fileName2, ref fileName4, ref material);
        }

        private int desk_type;

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            desk_type = comboBox2.SelectedIndex;
        }

        private void comboBox_material_SelectedIndexChanged(object sender, EventArgs e)
        {
            material = comboBox_material.Text;
        }

        CreateModel_Class cm = new CreateModel_Class();

        public Form1()
        {
            InitializeComponent();
        }

        private void Button_drawing_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog(); // 다이어로그 보이기
            string folderName = folderBrowserDialog1.SelectedPath; // 폴더 경로를 string변수에 할당
            fileName1 = folderName + "\\desk.sldprt"; // 책상의 이름 및 확장자를 경로에 더해서 자동 저장
            fileName2 = folderName + "\\pillar.sldprt"; // 기둥의 이름 및 확장자를 경로에 더해서 자동저장
            fileName3 = folderName + "\\assembly_desk.sldasm"; // 어셈블리의 이름 및 확장자를 경로에 더해서 자동저장
            fileName4 = folderName + "\\bookcase.sldprt"; //책장의 이름
            fileName5 = folderName + "\\Drafting_desk.slddrw";

            x = Convert.ToDouble(textBox_propery1.Text) / 1000.0;
            y = Convert.ToDouble(textBox_propery2.Text) / 1000.0;
            t = Convert.ToDouble(textBox_propery3.Text) / 1000.0;
            h = Convert.ToDouble(textBox_propery4.Text) / 1000.0;

            cm.CreateDesk(ref desk_type,ref x, ref y, ref t, ref h,ref fileName1,ref fileName2,ref fileName3,ref fileName4, ref fileName5,ref material);          
        }
    }
}
