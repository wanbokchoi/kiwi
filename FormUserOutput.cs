﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms.DataVisualization.Charting;

namespace KIWI
{
    public partial class FormUserOutput : Form
    {
        private FormUserOutput mFormUserOutput;

        private TextBox[] txtOut = null;     //전체
        private TextBox[] txtWOut = null;    //도매
        private TextBox[] txtROut = null;    //소매

        private TextBox[] vtxtOutTotal = null;     //전체 Total
        private TextBox[] vtxtOut = null;          //전체
        private TextBox[] vtxtWOutTotal = null;    //도매 Total
        private TextBox[] vtxtWOut = null;         //도매
        private TextBox[] vtxtROutTotal = null;    //소매 Total
        private TextBox[] vtxtROut = null;         //소매

        private Label[] labelWon = null;
        private Label[] labelWon2 = null;
        private Label[] labelWon3 = null;

        //월평균 판매대수 도매
        private string txtInput4 = "0";
        //월평균 판매대수 계 엑셀 셀 번호
        string ColumnNameInput4 = "F10";
        //월평균 판매대수 소매
        private string txtInput16 = "0";
        //월평균 판매대수 계 엑셀 셀 번호
        string ColumnNameInput16 = "G10";
        //월평균 판매대수 계
        private string txtInput25 = "0";
        //월평균 판매대수 계 엑셀 셀 번호
        string ColumnName_Input25 = "H10";

        public FormUserOutput()
        {
            InitializeComponent();

            labelWon = new Label[6] { label115, label116, label121, label122, label123, label124};
            labelWon2 = new Label[6] {label125, label126, label127, label128, label129, label130};
            labelWon3 = new Label[6] {label131, label132, label133, label134, label135, label136};
            
            vtxtOutTotal = new TextBox[42]{ 
                txtOut1, txtOut2, txtOut3, txtOut4, txtOut5, txtOut6, txtOut7, txtOut8, txtOut9, txtOut10,txtOut11, txtOut12, txtOut13, txtOut14, txtOut15, txtOut16, 
                txtWOut1, txtWOut2, txtWOut3, txtWOut4, txtWOut5, txtWOut6, txtWOut7, txtWOut8, txtWOut9, txtWOut10,txtWOut11, txtWOut12, txtWOut13, txtWOut14, 
                txtROut1, txtROut2, txtROut3, txtROut4, txtROut5, txtROut6, txtROut7, txtROut8, txtROut9, txtROut10,txtROut11, txtROut12
            };

            vtxtOut = new TextBox[42]{ 
                txtOut17, txtOut18, txtOut19, txtOut20, txtOut21, txtOut22, txtOut23, txtOut24, txtOut25, txtOut26, txtOut27, txtOut28, txtOut29, txtOut30, txtOut31, txtOut32,
                txtWOut15, txtWOut16, txtWOut17, txtWOut18, txtWOut19, txtWOut20,txtWOut21, txtWOut22, txtWOut23, txtWOut24, txtWOut25, txtWOut26, txtWOut27, txtWOut28,
                txtROut13, txtROut14, txtROut15, txtROut16, txtROut17, txtROut18, txtROut19, txtROut20, txtROut21, txtROut22, txtROut23, txtROut24
            };

            vtxtWOutTotal = new TextBox[42]{ 
                txtOut33, txtOut34, txtOut35, txtOut36, txtOut37, txtOut38, txtOut39, txtOut40, txtOut41, txtOut42, txtOut43, txtOut44, txtOut45, txtOut46, txtOut47, txtOut48,
                txtWOut29, txtWOut30, txtWOut31, txtWOut32, txtWOut33, txtWOut34, txtWOut35, txtWOut36, txtWOut37, txtWOut38, txtWOut39, txtWOut40, txtWOut41, txtWOut42,
                txtROut25, txtROut26, txtROut27, txtROut28, txtROut29, txtROut30, txtROut31, txtROut32, txtROut33, txtROut34, txtROut35, txtROut36
            };

            vtxtWOut = new TextBox[42]{ 
                txtOut49, txtOut50, txtOut51, txtOut52, txtOut53, txtOut54, txtOut55, txtOut56, txtOut57, txtOut58, txtOut59, txtOut60, txtOut61, txtOut62, txtOut63, txtOut64,
                txtWOut43, txtWOut44, txtWOut45, txtWOut46, txtWOut47, txtWOut48, txtWOut49, txtWOut50, txtWOut51, txtWOut52, txtWOut53, txtWOut54, txtWOut55, txtWOut56,
                txtROut37, txtROut38, txtROut39, txtROut40, txtROut41, txtROut42, txtROut43, txtROut44, txtROut45, txtROut46, txtROut47, txtROut48
            };

            vtxtROutTotal = new TextBox[42]{ 
                txtOut65, txtOut66, txtOut67, txtOut68, txtOut69, txtOut70, txtOut71, txtOut72, txtOut73, txtOut74, txtOut75, txtOut76, txtOut77, txtOut78, txtOut79, txtOut80,
                txtWOut57, txtWOut58, txtWOut59, txtWOut60, txtWOut61, txtWOut62, txtWOut63, txtWOut64, txtWOut65, txtWOut66, txtWOut67, txtWOut68, txtWOut69, txtWOut70,
                txtROut49, txtROut50, txtROut51, txtROut52, txtROut53, txtROut54, txtROut55, txtROut56, txtROut57, txtROut58, txtROut59, txtROut60
            };

            vtxtROut = new TextBox[42]{ 
                txtOut81, txtOut82, txtOut83, txtOut84, txtOut85, txtOut86, txtOut87, txtOut88, txtOut89, txtOut90, txtOut91, txtOut92, txtOut93, txtOut94, txtOut95, txtOut96,
                txtWOut71, txtWOut72, txtWOut73, txtWOut74, txtWOut75, txtWOut76, txtWOut77, txtWOut78, txtWOut79, txtWOut80, txtWOut81, txtWOut82, txtWOut83, txtWOut84,
                txtROut61, txtROut62, txtROut63, txtROut64, txtROut65, txtROut66, txtROut67, txtROut68, txtROut69, txtROut70, txtROut71, txtROut72
            };

            txtOut = new TextBox[96] { txtOut1, txtOut2, txtOut3, txtOut4, txtOut5, txtOut6, txtOut7, txtOut8, txtOut9, txtOut10,
            txtOut11, txtOut12, txtOut13, txtOut14, txtOut15, txtOut16, txtOut17, txtOut18, txtOut19, txtOut20,
            txtOut21, txtOut22, txtOut23, txtOut24, txtOut25, txtOut26, txtOut27, txtOut28, txtOut29, txtOut30,
            txtOut31, txtOut32, txtOut33, txtOut34, txtOut35, txtOut36, txtOut37, txtOut38, txtOut39, txtOut40,
            txtOut41, txtOut42, txtOut43, txtOut44, txtOut45, txtOut46, txtOut47, txtOut48, txtOut49, txtOut50,
            txtOut51, txtOut52, txtOut53, txtOut54, txtOut55, txtOut56, txtOut57, txtOut58, txtOut59, txtOut60,
            txtOut61, txtOut62, txtOut63, txtOut64, txtOut65, txtOut66, txtOut67, txtOut68, txtOut69, txtOut70,
            txtOut71, txtOut72, txtOut73, txtOut74, txtOut75, txtOut76, txtOut77, txtOut78, txtOut79, txtOut80,
            txtOut81, txtOut82, txtOut83, txtOut84, txtOut85, txtOut86, txtOut87, txtOut88, txtOut89, txtOut90,
            txtOut91, txtOut92, txtOut93, txtOut94, txtOut95, txtOut96
            };
            txtWOut = new TextBox[84] { txtWOut1, txtWOut2, txtWOut3, txtWOut4, txtWOut5, txtWOut6, txtWOut7, txtWOut8, txtWOut9, txtWOut10,
            txtWOut11, txtWOut12, txtWOut13, txtWOut14, txtWOut15, txtWOut16, txtWOut17, txtWOut18, txtWOut19, txtWOut20,
            txtWOut21, txtWOut22, txtWOut23, txtWOut24, txtWOut25, txtWOut26, txtWOut27, txtWOut28, txtWOut29, txtWOut30,
            txtWOut31, txtWOut32, txtWOut33, txtWOut34, txtWOut35, txtWOut36, txtWOut37, txtWOut38, txtWOut39, txtWOut40,
            txtWOut41, txtWOut42, txtWOut43, txtWOut44, txtWOut45, txtWOut46, txtWOut47, txtWOut48, txtWOut49, txtWOut50,
            txtWOut51, txtWOut52, txtWOut53, txtWOut54, txtWOut55, txtWOut56, txtWOut57, txtWOut58, txtWOut59, txtWOut60,
            txtWOut61, txtWOut62, txtWOut63, txtWOut64, txtWOut65, txtWOut66, txtWOut67, txtWOut68, txtWOut69, txtWOut70,
            txtWOut71, txtWOut72, txtWOut73, txtWOut74, txtWOut75, txtWOut76, txtWOut77, txtWOut78, txtWOut79, txtWOut80,
            txtWOut81, txtWOut82, txtWOut83, txtWOut84
            };
            txtROut = new TextBox[72] { txtROut1, txtROut2, txtROut3, txtROut4, txtROut5, txtROut6, txtROut7, txtROut8, txtROut9, txtROut10,
            txtROut11, txtROut12, txtROut13, txtROut14, txtROut15, txtROut16, txtROut17, txtROut18, txtROut19, txtROut20,
            txtROut21, txtROut22, txtROut23, txtROut24, txtROut25, txtROut26, txtROut27, txtROut28, txtROut29, txtROut30,
            txtROut31, txtROut32, txtROut33, txtROut34, txtROut35, txtROut36, txtROut37, txtROut38, txtROut39, txtROut40,
            txtROut41, txtROut42, txtROut43, txtROut44, txtROut45, txtROut46, txtROut47, txtROut48, txtROut49, txtROut50,
            txtROut51, txtROut52, txtROut53, txtROut54, txtROut55, txtROut56, txtROut57, txtROut58, txtROut59, txtROut60,
            txtROut61, txtROut62, txtROut63, txtROut64, txtROut65, txtROut66, txtROut67, txtROut68, txtROut69, txtROut70,
            txtROut71, txtROut72
            };
            //// ReadOnly설정
            //for (int i = 0; i < txtOut.Length; i++)
            //{

            //    txtOut[i].ReadOnly = true;
            //    txtOut[i].BackColor = Color.White;
            //    txtOut[i].BorderStyle = BorderStyle.None;

            //    if (i < txtWOut.Length)
            //    {
            //        txtWOut[i].ReadOnly = true;
            //        txtWOut[i].BackColor = Color.White;
            //        txtWOut[i].BorderStyle = BorderStyle.None;
            //    }
            //    if (i < txtROut.Length)
            //    {
            //        txtROut[i].ReadOnly = true;
            //        txtROut[i].BackColor = Color.White;
            //        txtROut[i].BorderStyle = BorderStyle.None;
            //    }
            //}
            pnlChart.Visible = false;
            // 결과 취득 클래스
            if (CommonUtil.isLoadedDataFromFile)
            {
                radioButton1.Enabled = true;
                radioButton1.Checked = CommonUtil.isSelectExistData;
                setAllOutFormat(CommonUtil.isSelectExistData);
            }
            else
            {
                radioButton1.Enabled = false;
                radioButton1.Checked = false;
                setAllOutFormat(false);
            }
        }



        public FormUserOutput(FormUserOutput formUserOutput)
        {
            InitializeComponent();
            //더블 버퍼
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            mFormUserOutput = formUserOutput;
        }


        private void setAllOutFormat(bool isFile)
        {
            List<Double[]> 전체 = new List<Double[]>();

            if (isFile)
            {
                setTexList(vtxtOutTotal, CDataControl.g_FileResultBusinessTotal.getArrayOutput전체());
                setTexList(vtxtOut, CDataControl.g_FileResultBusiness.getArrayOutput전체());

                setTexList(vtxtWOutTotal, CDataControl.g_FileResultStoreTotal.getArrayOutput전체());
                setTexList(vtxtWOut, CDataControl.g_FileResultStore.getArrayOutput전체());

                setTexList(vtxtROutTotal, CDataControl.g_FileResultFutureTotal.getArrayOutput전체());
                setTexList(vtxtROut, CDataControl.g_FileResultFuture.getArrayOutput전체());

                전체.Add(CDataControl.g_FileResultBusiness.getArrayOutput전체());
                전체.Add(CDataControl.g_FileResultStore.getArrayOutput전체());
                전체.Add(CDataControl.g_FileResultFuture.getArrayOutput전체());
            }
            else
            {
                setTexList(vtxtOutTotal, CDataControl.g_ResultBusinessTotal.getArrayOutput전체());
                setTexList(vtxtOut, CDataControl.g_ResultBusiness.getArrayOutput전체());

                setTexList(vtxtWOutTotal, CDataControl.g_ResultStoreTotal.getArrayOutput전체());
                setTexList(vtxtWOut, CDataControl.g_ResultStore.getArrayOutput전체());

                setTexList(vtxtROutTotal, CDataControl.g_ResultFutureTotal.getArrayOutput전체());
                setTexList(vtxtROut, CDataControl.g_ResultFuture.getArrayOutput전체());

                전체.Add(CDataControl.g_ResultBusiness.getArrayOutput전체());
                전체.Add(CDataControl.g_ResultStore.getArrayOutput전체());
                전체.Add(CDataControl.g_ResultFuture.getArrayOutput전체());

            }

            OpenChart(chart1, 전체);
            OpenChart(chart2, 전체);
            OpenChart(chart3, 전체);
            OpenChart(chart4, 전체);
            OpenChart(chart5, 전체);
            OpenChart(chart6, 전체);
        }

        private void setTexList(TextBox[] _txtList, Double[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                _txtList[i].Text = CommonUtil.NullToString0(arr[i]);
                if (_txtList[i] == txtROut24 || _txtList[i] == txtROut48 || _txtList[i] == txtROut72)
                    continue;
                setTxtInput_TextChanged(_txtList[i]);
            }
        }


        private void OpenChart(Chart chart, List<Double[]> lists)
        {
            Double[] yValues = null;
            Double[] yValues2 = null;
            Double[] yValues3 = null;

            string[] xValues = null;

            chart.Series[0].Name = "업계평균";
            chart.Series[1].Name = "당대리점(현재수익)";
            chart.Series[2].Name = "당대리점(미래수익)";

            if (chart.Name == "chart1")
            {
                xValues = new string[6] { "누적가입자 수수료", "CS관리수수료", "월단위 업무취급 수수료", "사업자모델 매입에 따른 추가수익", "유통모델 매입에 따른 추가수익(현금+Volume)", "직영매장 판매수익" };

                yValues = new Double[6] { lists[0].ToArray()[0], lists[0].ToArray()[1], lists[0].ToArray()[2], lists[0].ToArray()[3], lists[0].ToArray()[4], lists[0].ToArray()[5] };
                yValues2 = new Double[6] { lists[1].ToArray()[0], lists[1].ToArray()[1], lists[1].ToArray()[2], lists[1].ToArray()[3], lists[1].ToArray()[4], lists[1].ToArray()[5] };
                yValues3 = new Double[6] { lists[2].ToArray()[0], lists[2].ToArray()[1], lists[2].ToArray()[2], lists[2].ToArray()[3], lists[2].ToArray()[4], lists[2].ToArray()[5] };

                chart.Series[0].Points.DataBindXY(xValues, yValues);
                chart.Series[1].Points.DataBindXY(xValues, yValues2);
                chart.Series[2].Points.DataBindXY(xValues, yValues3);
            }
            else if (chart.Name == "chart2")
            {
                xValues = new string[4] { "누적가입자 수수료", "CS관리수수료", "사업자모델 매입에 따른 추가수익", "유통모델 매입에 따른 추가수익(현금+Volume)" };

                yValues = new Double[4] { lists[0].ToArray()[16], lists[0].ToArray()[17], lists[0].ToArray()[18], lists[0].ToArray()[19] };
                yValues2 = new Double[4] { lists[1].ToArray()[16], lists[1].ToArray()[17], lists[1].ToArray()[18], lists[1].ToArray()[19] };
                yValues3 = new Double[4] { lists[2].ToArray()[16], lists[2].ToArray()[17], lists[2].ToArray()[18], lists[2].ToArray()[19] };

                chart.Series[0].Points.DataBindXY(xValues, yValues);
                chart.Series[1].Points.DataBindXY(xValues, yValues2);
                chart.Series[2].Points.DataBindXY(xValues, yValues3);
            }
            else if (chart.Name == "chart3")
            {
                xValues = new string[2] { "월단위 업무취급 수수료", "직영매장 판매수익" };

                yValues = new Double[2] { lists[0].ToArray()[30], lists[0].ToArray()[31] };
                yValues2 = new Double[2] { lists[1].ToArray()[30], lists[1].ToArray()[31] };
                yValues3 = new Double[2] { lists[2].ToArray()[30], lists[2].ToArray()[31] };

                chart.Series[0].Points.DataBindXY(xValues, yValues);
                chart.Series[1].Points.DataBindXY(xValues, yValues2);
                chart.Series[2].Points.DataBindXY(xValues, yValues3);
            }
            else if (chart.Name == "chart4")
            {
                xValues = new string[7] { "대리점 투자비용", "인건비(급여,복리후생비)", "임차료", "이자비용", "부가세", "법인세", "기타관리비용" };

                yValues = new Double[7] { lists[0].ToArray()[7], lists[0].ToArray()[8], lists[0].ToArray()[9], lists[0].ToArray()[10], lists[0].ToArray()[11], lists[0].ToArray()[12], lists[0].ToArray()[13] };
                yValues2 = new Double[7] { lists[1].ToArray()[7], lists[1].ToArray()[8], lists[1].ToArray()[9], lists[1].ToArray()[10], lists[1].ToArray()[11], lists[1].ToArray()[12], lists[1].ToArray()[13] };
                yValues3 = new Double[7] { lists[2].ToArray()[7], lists[2].ToArray()[8], lists[2].ToArray()[9], lists[2].ToArray()[10], lists[2].ToArray()[11], lists[2].ToArray()[12], lists[2].ToArray()[13] };

                chart.Series[0].Points.DataBindXY(xValues, yValues);
                chart.Series[1].Points.DataBindXY(xValues, yValues2);
                chart.Series[2].Points.DataBindXY(xValues, yValues3);
            }
            else if (chart.Name == "chart5")
            {
                xValues = new string[7] { "대리점 투자비용", "인건비(급여,복리후생비)", "임차료", "이자비용", "부가세", "법인세", "기타관리비용" };

                yValues = new Double[7] { lists[0].ToArray()[21], lists[0].ToArray()[22], lists[0].ToArray()[23], lists[0].ToArray()[24], lists[0].ToArray()[25], lists[0].ToArray()[26], lists[0].ToArray()[27] };
                yValues2 = new Double[7] { lists[1].ToArray()[21], lists[1].ToArray()[22], lists[1].ToArray()[23], lists[1].ToArray()[24], lists[1].ToArray()[25], lists[1].ToArray()[26], lists[1].ToArray()[27] };
                yValues3 = new Double[7] { lists[2].ToArray()[21], lists[2].ToArray()[22], lists[2].ToArray()[23], lists[2].ToArray()[24], lists[2].ToArray()[25], lists[2].ToArray()[26], lists[2].ToArray()[27] };

                chart.Series[0].Points.DataBindXY(xValues, yValues);
                chart.Series[1].Points.DataBindXY(xValues, yValues2);
                chart.Series[2].Points.DataBindXY(xValues, yValues3);
            }
            else if (chart.Name == "chart6")
            {
                xValues = new string[6] { "인건비(급여,복리후생비)", "임차료", "이자비용", "부가세", "법인세", "기타관리비용" };

                yValues = new Double[6] { lists[0].ToArray()[33], lists[0].ToArray()[34], lists[0].ToArray()[35], lists[0].ToArray()[36], lists[0].ToArray()[37], lists[0].ToArray()[38] };
                yValues2 = new Double[6] { lists[1].ToArray()[33], lists[1].ToArray()[34], lists[1].ToArray()[35], lists[1].ToArray()[36], lists[1].ToArray()[37], lists[1].ToArray()[38] };
                yValues3 = new Double[6] { lists[2].ToArray()[33], lists[2].ToArray()[34], lists[2].ToArray()[35], lists[2].ToArray()[36], lists[2].ToArray()[37], lists[2].ToArray()[38] };

                chart.Series[0].Points.DataBindXY(xValues, yValues);
                chart.Series[1].Points.DataBindXY(xValues, yValues2);
                chart.Series[2].Points.DataBindXY(xValues, yValues3);
            }
            chart.ChartAreas[0].AxisX.LabelStyle.Angle = 0;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            pnlChart.Visible = true;
            for (int i = 0; i < labelWon.Length; i++)
            {
                labelWon[i].Visible = false;
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            pnlChart.Visible = false;
            for(int i =0; i < labelWon.Length; i++){
                labelWon[i].Visible = true;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            pnlChart2.Visible = true;
            for (int i = 0; i < labelWon.Length; i++)
            {
                labelWon2[i].Visible = false;
            }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            pnlChart2.Visible = false;
            for (int i = 0; i < labelWon.Length; i++)
            {
                labelWon2[i].Visible = true;
            }
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            pnlChart3.Visible = true;
            for (int i = 0; i < labelWon.Length; i++)
            {
                labelWon3[i].Visible = false;
            }
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            pnlChart3.Visible = false;
            for (int i = 0; i < labelWon.Length; i++)
            {
                labelWon3[i].Visible = true;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            setAllOutFormat((sender as RadioButton).Checked);
        }

        private string setTxtInput_TextChanged(object sender)
        {
            TextBox _TextBox = (sender as TextBox);
            try
            {
                Double num = Convert.ToDouble(_TextBox.Text.Replace(",", ""));

                if (_TextBox.Text.Length < 24 && _TextBox.Text.Length > 2)
                {
                    int saveCursor = _TextBox.Text.Length - _TextBox.SelectionStart;

                    //if (_TextBox.Text.Length > 3)
                    _TextBox.Text = String.Format("{0:#,###}", num);

                    if (_TextBox.Text.Length < saveCursor)
                        _TextBox.SelectionStart = 0;
                    else
                        _TextBox.SelectionStart = _TextBox.Text.Length - saveCursor;
                }
                else if (_TextBox.Text.Length > 23)
                {
                    int saveCursor = _TextBox.SelectionStart - 1;
                    _TextBox.Text = _TextBox.Text.Remove(saveCursor, 1);
                    _TextBox.SelectionStart = saveCursor;
                }
            }
            catch
            {
                _TextBox.Text = "0";
                _TextBox.SelectionStart = 1;
            }

            return _TextBox.Text;
        }

        private void set점별손익추정_TextChanged(object sender, EventArgs e)
        {
            if ((sender as TextBox).Text == "")
                return;
            (sender as TextBox).Text = "";
            //if ((sender as TextBox).Text.EndsWith("점 기준"))
            //    return;
            //(sender as TextBox).Text += "점 기준";
        }
    }
}
