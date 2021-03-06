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
using System.Globalization;

namespace KIWI
{
    public partial class FormUserAnalysis : Form
    {
        private FormUserOutput mFormUserOutput;

        private TextBox[] txtOut = null;     //전체
        private TextBox[] txtBaseData = null;
        private TextBox[] txtBaseDataSum = null;
        private RichTextBox[] txtComments = null;
        private PictureBox[] picCompare = null;

        public FormUserAnalysis(FormUserOutput FormUserOutput)
        {
            InitializeComponent();

            //더블 버퍼
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);

            mFormUserOutput = FormUserOutput;
            
        }

        public FormUserAnalysis()
        {
            InitializeComponent();

            txtOut = new TextBox[64] { txtOut1, txtOut2, txtOut3, txtOut4, txtOut5, txtOut6, txtOut7, txtOut8, txtOut9, txtOut10,
            txtOut11, txtOut12, txtOut13, txtOut14, txtOut15, txtOut16, txtOut17, txtOut18, txtOut19, txtOut20,
            txtOut21, txtOut22, txtOut23, txtOut24, txtOut25, txtOut26, txtOut27, txtOut28, txtOut29, txtOut30,
            txtOut31, txtOut32, txtOut33, txtOut34, txtOut35, txtOut36, txtOut37, txtOut38, txtOut39, txtOut40,
            txtOut41, txtOut42, txtOut43, txtOut44, txtOut45, txtOut46, txtOut47, txtOut48, txtOut49, txtOut50,
            txtOut51, txtOut52, txtOut53, txtOut54, txtOut55, txtOut56, txtOut57, txtOut58, txtOut59, txtOut60,
            txtOut61, txtOut62, txtOut63, txtOut64
            };

            txtBaseData = new TextBox[10] { textBoxBase1, textBoxBase2, textBoxBase3, textBoxBase4, textBoxBase5, 
            textBoxBase6, textBoxBase7, textBoxBase8, textBoxBase9, textBoxBase10};

            txtBaseDataSum = new TextBox[6] { textBox1, textBox2, textBox3, textBox4, textBox5, textBox6};

            txtComments = new RichTextBox[3] { comments1, comments2, comments3 };

            picCompare = new PictureBox[16] { picCompare1, picCompare2, picCompare3, picCompare4, picCompare5, 
                picCompare6, picCompare7, picCompare8, picCompare9, picCompare10,
                picCompare11, picCompare12, picCompare13, picCompare14, picCompare15, picCompare16};

            setOut();
            setBaseData(CDataControl.g_BasicInput);
            setComments(CDataControl.g_ReportData);
            setCompare();
            setReferrence();
            setReferrence(true);

            
            OpenChart(chart1, CDataControl.g_ResultBusiness, CDataControl.g_ResultStore);
            OpenChart(chart5, CDataControl.g_ResultBusiness, CDataControl.g_ResultStore);
        }

        private void setReferrence(Boolean 업계평균이다 = false)
        {
            CResultData rdt = null;
            CResultData rd = null;
            CBasicInput bi = null;
            CBusinessData di = null;
            if (업계평균이다)
            {
                rdt = CDataControl.g_ResultBusinessTotal;
                rd = CDataControl.g_ResultBusiness;
                bi = CDataControl.g_BasicInput;
                di = CDataControl.g_BusinessAvg;
            }
            else
            {
                rdt = CDataControl.g_ResultStoreTotal;
                rd = CDataControl.g_ResultStore;
                bi = CDataControl.g_BasicInput;
                di = CDataControl.g_DetailInput;
            }

            if (
                rdt == null ||
                rd == null ||
                bi == null ||
                di == null)
                return;

            Double 가입자당ARPU = CommonUtil.Division(rdt.get도매_수익_가입자관리수수료()
                                   , bi.get누적가입자수_합계());

            Double 월평균인건비 = CommonUtil.Division((rdt.get전체_비용_인건비_급여_복리후생비() - di.get도소매_비용_복리후생비()), bi.get직원수_소계_합계());

            Double 판촉비비중 = CommonUtil.Division(Convert.ToDouble(di.get도매_비용_판매촉진비() + di.get소매_비용_판매촉진비())
                             , Convert.ToDouble(rdt.전체_비용_소계));

            Double 인당판매수량 = CommonUtil.Division((bi.get도매_월평균판매대수_신규()
                                    + bi.get도매_월평균판매대수_기변())
                                    , bi.get도매_직원수_소계());

            if (업계평균이다)
            {
                참조_업계평균_ARPU.Text = 가입자당ARPU.ToString();
                참조_업계평균_인건비.Text = 월평균인건비.ToString();
                참조_업계평균_판촉비비중.Text = (판촉비비중 * 100).ToString("0.0");
                참조_업계평균_인당판매수량.Text = 인당판매수량.ToString();
            }
            else
            {
                참조_당대리점_ARPU.Text = 가입자당ARPU.ToString();
                참조_당대리점_인건비.Text = 월평균인건비.ToString();
                참조_당대리점_판촉비비중.Text = (판촉비비중 * 100).ToString("0.0");
                참조_당대리점_인당판매수량.Text = 인당판매수량.ToString();
            }
        }

        private void setOut()
        {
            if (CDataControl.g_ResultBusinessTotal == null ||
                CDataControl.g_ResultBusiness == null ||
                CDataControl.g_ResultStoreTotal == null ||
                CDataControl.g_ResultStore == null) return;
            for (int i = 0; i < 16; i++)
            {
                txtOut[i].Text = CDataControl.g_ResultBusinessTotal.getArr전체_수익_비용_및_계산포함()[i].ToString();
                txtOut[i + 16].Text = CDataControl.g_ResultBusiness.getArr전체_수익_비용_및_계산포함()[i].ToString();
                txtOut[i + 32].Text = CDataControl.g_ResultStoreTotal.getArr전체_수익_비용_및_계산포함()[i].ToString();
                txtOut[i + 48].Text = CDataControl.g_ResultStore.getArr전체_수익_비용_및_계산포함()[i].ToString();
            }
        }


        private void setBaseData(CBasicInput _basicInput)
        {
            if (_basicInput == null) return;

            int i = 0;
            txtBaseData[i++].Text = _basicInput.getstr도매_누적가입자수();
            txtBaseData[i++].Text = _basicInput.getstr도매_월평균판매대수_신규();
            txtBaseData[i++].Text = _basicInput.getstr소매_월평균판매대수_신규();
            txtBaseData[i++].Text = _basicInput.getstr도매_월평균판매대수_기변();
            txtBaseData[i++].Text = _basicInput.getstr소매_월평균판매대수_기변();
            txtBaseData[i++].Text = _basicInput.getstr도매_월평균유통모델출고대수_소계();
            txtBaseData[i++].Text = _basicInput.getstr도매_거래선수_소계();
            txtBaseData[i++].Text = _basicInput.getstr소매_거래선수_소계();
            txtBaseData[i++].Text = _basicInput.getstr도매_직원수_소계();
            txtBaseData[i++].Text = _basicInput.getstr소매_직원수_소계();

            txtBaseDataSum[0].Text = txtBaseData[0].Text;
            txtBaseDataSum[1].Text = SumStringNumber(txtBaseData[1].Text,txtBaseData[2].Text);
            txtBaseDataSum[2].Text = SumStringNumber(txtBaseData[3].Text,txtBaseData[4].Text);
            txtBaseDataSum[3].Text = txtBaseData[5].Text;
            txtBaseDataSum[4].Text = SumStringNumber(txtBaseData[6].Text,txtBaseData[7].Text);
            txtBaseDataSum[5].Text = SumStringNumber(txtBaseData[8].Text,txtBaseData[9].Text);
        }

        private string SumStringNumber(string v0, string v1)
        {
            string result = "";

            try
            {
                Double num = Convert.ToDouble(v0.Replace(",", "")) + Convert.ToDouble(v1.Replace(",", ""));

                result = String.Format("{0:#,###}", num);
            }
            catch
            {
                result = "0";
            }

            return result;
        }


        private void setComments(CReportData _resultData)
        {
            if (_resultData == null) return;
            int i = 0;
            txtComments[i++].Text = _resultData.get분석내용_및_대리점_활동방향().ToString();
            txtComments[i++].Text = _resultData.getLG_지원_활동().ToString();
            txtComments[i++].Text = _resultData.get배경_및_이슈().ToString();
        }

        public void saveComments()
        {
            if (CDataControl.g_ReportData == null) return;
            CDataControl.g_ReportData.set분석내용_및_대리점_활동방향(txtComments[0].Text);        
            CDataControl.g_ReportData.setLG_지원_활동(txtComments[1].Text);
            CDataControl.g_ReportData.set배경_및_이슈(txtComments[2].Text);
        }

        private void setCompare() {
            // 17, 49
            Double convertedA;
            Double convertedB;

            for (int i = 0; i < 16; i++) {
                convertedA = Convert.ToDouble(txtOut[i + 16].Text.Replace(",", ""));
                convertedB = Convert.ToDouble(txtOut[i + 48].Text.Replace(",", ""));
                if (convertedA < convertedB) {
                    picCompare[i].Image = (i < 7 || i == 15) ? KIWI.Properties.Resources.녹색 : KIWI.Properties.Resources.빨강;
                }
                else if (convertedA > convertedB) {
                    picCompare[i].Image = (i < 7 || i == 15) ? KIWI.Properties.Resources.빨강 : KIWI.Properties.Resources.녹색;
                }
                else { picCompare[i].Image = KIWI.Properties.Resources.노랑; }
            }
        }

        // chart1 - 수익계정 전체    CResultData
        //private void OpenChart(Chart chart, excel.Worksheet sheet)
        private void OpenChart(Chart chart, CResultData _bizResult, CResultData _storeResult)
        {
            if (_bizResult == null || _storeResult == null) return;
            double[] yValues = null;
            double[] yValues2 = null;

            string[] xValues = null;

            if (chart.Name == "chart1")
            {
                xValues = new string[6] { "A", "B", "C", "D", "E", "F" };

                yValues = new double[6]{ 
                                         Convert.ToDouble(_bizResult.get전체_수익_가입자관리수수료()), 
                                         Convert.ToDouble(_bizResult.get전체_수익_CS관리수수료()), 
                                         Convert.ToDouble(_bizResult.get전체_수익_업무취급수수료()), 
                                         Convert.ToDouble(_bizResult.get전체_수익_사업자모델매입에따른추가수익()), 
                                         Convert.ToDouble(_bizResult.get전체_수익_유통모델매입에따른추가수익_현금_Volume()), 
                                         Convert.ToDouble(_bizResult.get전체_수익_직영매장판매수익())
                                        };

                yValues2 = new double[6]{ 
                                         Convert.ToDouble(_storeResult.get전체_수익_가입자관리수수료()), 
                                         Convert.ToDouble(_storeResult.get전체_수익_CS관리수수료()), 
                                         Convert.ToDouble(_storeResult.get전체_수익_업무취급수수료()), 
                                         Convert.ToDouble(_storeResult.get전체_수익_사업자모델매입에따른추가수익()), 
                                         Convert.ToDouble(_storeResult.get전체_수익_유통모델매입에따른추가수익_현금_Volume()), 
                                         Convert.ToDouble(_storeResult.get전체_수익_직영매장판매수익())
                                        };
            }
            else if (chart.Name == "chart5")
            {
                xValues = new string[7] { "A", "B", "C", "D", "E", "F", "G" };

                yValues = new double[7]{ 
                                         Convert.ToDouble(_bizResult.get전체_비용_대리점투자비용()), 
                                         Convert.ToDouble(_bizResult.get전체_비용_인건비_급여_복리후생비()), 
                                         Convert.ToDouble(_bizResult.get전체_비용_임차료()), 
                                         Convert.ToDouble(_bizResult.get전체_비용_이자비용()), 
                                         Convert.ToDouble(_bizResult.get전체_비용_부가세()),
                                         Convert.ToDouble(_bizResult.get전체_비용_법인세()),
                                         Convert.ToDouble(_bizResult.get전체_비용_기타판매관리비()) 
                                        };

                yValues2 = new double[7]{ 
                                         Convert.ToDouble(_storeResult.get전체_비용_대리점투자비용()), 
                                         Convert.ToDouble(_storeResult.get전체_비용_인건비_급여_복리후생비()), 
                                         Convert.ToDouble(_storeResult.get전체_비용_임차료()), 
                                         Convert.ToDouble(_storeResult.get전체_비용_이자비용()), 
                                         Convert.ToDouble(_storeResult.get전체_비용_부가세()),
                                         Convert.ToDouble(_storeResult.get전체_비용_법인세()),
                                         Convert.ToDouble(_storeResult.get전체_비용_기타판매관리비()) 
                                        };
            }
            
            chart.Series[0].Name = "업계평균";
            chart.Series[1].Name = "당대리점";

            chart.Series[0].Points.DataBindXY(xValues, yValues);
            chart.Series[1].Points.DataBindXY(xValues, yValues2);
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            FormChartViewer viewer = new FormChartViewer();
            viewer.MakeChart(sender as Chart);
            viewer.ShowDialog();
        }

        private void panel15_Paint(object sender, PaintEventArgs e)
        {

        }

        private string setTxtInput_TextChanged(object sender)
        {
            TextBox _TextBox = (sender as TextBox);

            try
            {
                Double num = Convert.ToDouble(_TextBox.Text.Replace(",", ""));

                if (_TextBox.Text.Length < 24 && _TextBox.Text.Length > 1)
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

        private void setTxtInput_TextChanged(object sender, EventArgs e)
        {
            setTxtInput_TextChanged(sender);
        }

       

    }
}
