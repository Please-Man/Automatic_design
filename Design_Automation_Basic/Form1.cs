using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Design_Automation_Basic
{
    public partial class Form1 : Form
    {
        double size1, size2, size3;

        CreateModel_Class cm = new CreateModel_Class();
        CreateDrafting_Class cd = new CreateDrafting_Class();

        public Form1()
        {
            InitializeComponent();
        }

        private void assembly_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_Material1_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (comboBox_Material1.SelectedIndex == 0)
            {
                string[] material = { "1023 탄소 강판(SS)", "201 강화 스테인리스 강 (SS)", "A286 철기 초내열 합금", "AISI 1010 강, 열간압연봉",
                                      "AISI 1015 강, 냉간압연 (SS)","AISI 1020 강", "Alloy Steel","주조 탄소강","주조 스테인리스 강","크롬 스테인리스 강",
                                      "아연 도금 강","스테인리스 강(페라이트)" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 1)
            {
                string[] material = { "연성 주철", "회주철", "가단주철" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 2)
            {
                string[] material = { "1060 합금", "1100-H18 로드(SS)" , "1345 합금","1350 합금","201.0-T43 절연 주조(SS)",
                                      "3003 합금","4032-T6","5052-H32","알루미나"};
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 3)
            {
                string[] material = { "알루미늄 청동", "황동", "구리", "망간 청동", "주석 베어링 청동", "구리제" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 4)
            {
                string[] material = { "공업용 순수 2등급 경도(SS)", "Ti-8Mn 강화 시트 (SS)" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 5)
            {
                string[] material = { "아연 AC41A 합금, 주조" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 6)
            {
                string[] material = { "마그네슘 합금", "Monel(R) 400" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 7)
            {
                string[] material = { "ABS","ABS PC","아크릴(내충격성)","CA","에폭시, 비충전","EPDM","나일론 101","PF","PEI",
                                        "폴리에스터 수지","폴리에테르 폴리올","PET", "PPE","PP 단일중합체"};
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 8)
            {
                string[] material = { "베릴륨", "코발트", "몰리브덴", "니켈", "순금", "순납", "순은", "티타늄", "텅스텐", "바나듐", "지르코늄" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 9)
            {
                string[] material = { "공기", "도자기", "골판지", "유리", "C (흑연)", "고무", "물" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 10)
            {
                string[] material = { "A-유리섬유", "C-유리섬유", "E-유리섬유", "S-유리섬유" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 11)
            {
                string[] material = { "Zoltek Panex 33" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 12)
            {
                string[] material = { "실리콘", "이산화 규소" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 13)
            {
                string[] material = { "부틸", "천연 고무", "SBR", "실리콘 고무" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
            if (comboBox_Material1.SelectedIndex == 14)
            {
                string[] material = { "오크", "소나무", "티크", "삼나무" };
                comboBox_Material2.Items.Clear();
                comboBox_Material2.Items.AddRange(material);
            }
        }

        private void material_Click(object sender, EventArgs e)
        {
            string material = comboBox_Material2.Text.ToString();
            cm.Material(ref material);
        }

        private void Drafting_Click(object sender, EventArgs e)
        {
            
            saveFileDialog.Title = "저장경로 지정";
            saveFileDialog.OverwritePrompt = true;
            saveFileDialog.Filter = "part file (*.sldprt)|*.sldprt|C# file (*.cs)|*.cs";
            saveFileDialog.ShowDialog();
            string filename = saveFileDialog.FileName; //선택 경로를 스트링 변수에 할당
            Console.WriteLine(filename);  

            cd.SaveAndOpen(ref filename);
            cd.Drafting_create(ref filename);
        }

        private void calculate_Click(object sender, EventArgs e)
        {
            cm.Calculate_model();
            MessageBox.Show(cm.Result_cal, "물성치");
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
            size1 = Convert.ToDouble(textBox_property1.Text) / 1000; //텍스트 박스 실수 변환
            size2 = Convert.ToDouble(textBox_property2.Text) / 1000;
            size3 = Convert.ToDouble(textBox_property3.Text) / 1000;


            if (comboBox1.SelectedIndex == 0)
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
