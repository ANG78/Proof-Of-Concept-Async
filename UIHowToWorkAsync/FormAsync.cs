using HowToWorkAsync;
using HowToWorkAsync.Process;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using UIConclusionesAsync;

namespace UIHowToWorkAsync
{
    public partial class FormAsync : Form
    {
        Report informe;
        UseMethod Method = null;
        private ETypeDoIndependentWork DefaultETypeWork = ETypeDoIndependentWork.LOOPING;
        private ECallNext DefaultECallNext = ECallNext.WAIT_AFTER;
        private int DefaulttrackMethod = 25;
        private ETypeImpl DefaultETypeImplementation = ETypeImpl.ASYNC;
        private EStrategyDoIndependentWork DefaultDoIndependentWork = EStrategyDoIndependentWork.WRAPPER_ASYNC;
        private int MaxMethods = 4;
        private int DefaultNumLevel = 2;


        public FormAsync()
        {
            InitializeComponent();


            for (int i = 1; i <= MaxMethods; i++)
                cmbLevels.Items.Add(i);

            cmbLevels.SelectedItem = DefaultNumLevel;

            List<SeriesChartType> listadoCharType = new List<SeriesChartType>() { SeriesChartType.Point, SeriesChartType.Line };

            foreach (var aux in listadoCharType)
            {
                cmbTipoGrafica.Items.Add(aux);
            }
            cmbTipoGrafica.SelectedItem = SeriesChartType.Point;
            bttCreateMethods_Click(this, null);

        }

        private void bttCreateMethods_Click(object sender, EventArgs e)
        {
            this.Enabled = false;

            var selected = (int)cmbLevels.SelectedItem;
            panelMethodsFlow.Controls.Clear();

            int topLocation = 0;


            List<IUseMethod> methods = new List<IUseMethod>();
            int height = 0;
            int width = 0;
            for (int i = 1; i <= selected; i++)
            {

                UseMethod Met = new UseMethod();

                if (i == 1)
                {
                    height = Met.Height + 15;
                    width = Met.Width + 20;
                    Method = Met;
                }

                //Met.EventChange = null;
                Met.Level = i;
                Met.Name = "useMethod" + i;
                Met.Next = null;
                Met.NumSteps = DefaulttrackMethod;
                Met.Size = new System.Drawing.Size(446, 89);
                Met.TabIndex = 30;
                Met.TypeDoIndependentWork = DefaultETypeWork;
                Met.TypeImpl = DefaultETypeImplementation;
                Met.CallNext = DefaultECallNext;
                Met.StrategyDoIndependentWork = DefaultDoIndependentWork;


                topLocation = i * (Met.Height + 1);
                // Met.Location = new System.Drawing.Point(0, topLocation);
                panelMethodsFlow.Controls.Add(Met);
                methods.Add(Met);
            }

            panelMethodsFlow.Width = width;
            panelMethodsFlow.Height = (methods.Count * height);
            PLeftMain.MaximumSize = new System.Drawing.Size() { Width = width + 20 };
            PLeftMain.Width = width + 15;
            methods.Reverse();

            IUseMethod current = null;
            foreach (var auxMet in methods)
            {
                auxMet.Next = current;
                current = auxMet;
            }

            this.Enabled = true;
        }

        private string GetNameforReport()
        {
            string name = cmbTipoGrafica.SelectedItem.ToString() + "_" + this.Method.NameForReport;
            return name;
        }

        private async void bttRun_Click(object sender, EventArgs e)
        {
            if (Method == null)
            {
                MessageBox.Show("The test is not well configured");
                return;
            }

            string validation = Method.ValidateConfigurations();
            if (!string.IsNullOrEmpty(validation))
            {
                MessageBox.Show(validation);
                return;
            }

            bttPrint.Visible = false;

            this.Enabled = false; /*deshabilita pantalla*/

            ReportGenerator reporter = new ReportGenerator();
            IGetBase implementacionMain = null;
            informe = null;
            try
            {
                Launcher launcherMethods = null;
                implementacionMain = HowToWorkAsync.ImpDynamic.FactoryImpl.GetInstance(Method, reporter);
                string resValidate = implementacionMain.Validate();
                if (!string.IsNullOrEmpty(resValidate))
                {
                    MessageBox.Show(resValidate);
                    this.Enabled = true;
                    return;
                }
                try
                {

                    launcherMethods = new Launcher(reporter, implementacionMain);
                    informe = await launcherMethods.Run();
                }
                catch (Exception ex1)
                {
                    reporter.FillingOutTheReport(ETypePoint.START_END, "Ex1", -1, Thread.CurrentThread.ManagedThreadId);
                    MessageBox.Show(ex1.Message);
                }
            }
            catch (Exception ex)
            {
                reporter.FillingOutTheReport(ETypePoint.START_END, "Ex2", -1, Thread.CurrentThread.ManagedThreadId);
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }


            try
            {
                if (informe != null)
                {
                    var proc = CreateProcessor();
                    proc.Execute(informe);
                    if (chkAutoSave.Checked)
                    {
                        informe.ScenarioName = GetNameforReport();
                        proc.WriteToFile(informe, @"C:\Source\Repos\saved\");
                    }
                }
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }

            this.Enabled = true;
            bttPrint.Visible = true;
        }

        private void cmbTipoGrafica_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Enabled = false;
            foreach (var serie in grafica.Series)
            {
                serie.ChartType = (SeriesChartType)cmbTipoGrafica.SelectedItem;
            }
            this.Enabled = true;
        }

        IProcessReportStrategy CreateProcessor()
        {
            return new StrategyCreateGraphic(grafica,
                                      chkOrdernar.Checked,
                                      chkSerie.Checked,
                                      chConId.Checked,
                                      chkHilos.Checked,
                                      chkTiempoTicks.Checked,
                                      this.chkStartEnd.Checked,
                                      this.chkLostPoints.Checked,
                                      (SeriesChartType)cmbTipoGrafica.SelectedItem);
        }


        private void HelperProcesar()
        {
            if (informe == null)
                return;

            if (this.Enabled != false)
                this.Enabled = false;

            try
            {

                var proc = CreateProcessor();
                proc.Execute(informe);
                if (chkAutoSave.Checked)
                {
                    informe.ScenarioName = GetNameforReport();
                    proc.WriteToFile(informe, @"C:\Source\Repos\saved\");
                }
                
            }
            catch (Exception ex)
            {
                this.Enabled = true;
                MessageBox.Show(ex.Message);
            }

            if (this.Enabled == false)
                this.Enabled = true;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var form = new FormAsync();
            form.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HelperProcesar();
        }

        private void chkTiempoTicks_CheckedChanged(object sender, EventArgs e)
        {
            HelperProcesar();
        }

        private void cmbLevels_SelectedIndexChanged(object sender, EventArgs e)
        {
            bttCreateMethods_Click(sender, e);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bttPrint_Click(object sender, EventArgs e)
        {
            if (Method != null)
            {
                bttPrint.Visible = false;
                var form = new FormTree();
                form.Print(Method);
                bttPrint.Visible = true;
            }
        }

        private void options_Enter(object sender, EventArgs e)
        {

        }

        private void FormAsync_Load(object sender, EventArgs e)
        {

        }
    }
}
