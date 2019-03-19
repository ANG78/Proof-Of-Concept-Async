using HowToWorkAsync;
using HowToWorkAsync.Process;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;



namespace UIHowToWorkAsync
{
    public partial class FormAsync : Form
    {
        Report informe;
        UseMethod Method = null;
        private ETypeWork DefaultETypeWork = ETypeWork.LOOPING;
        private ECallNext DefaultECallNext = ECallNext.WAIT_AFTER;
        private int DefaulttrackMethod = 25;
        private ETypeImpl DefaultETypeImplementation = ETypeImpl.ASYNC;
        private int MaxMethods = 6;
        private int DefaultNumLevel = 4;


        public FormAsync()
        {
            InitializeComponent();


            for (int i = 2; i <= MaxMethods; i++)
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
            for (int i = 0; i < selected; i++)
            {

                UseMethod Met = new UseMethod();

                if (i == 0)
                {
                    height = Met.Height + 15;
                    width = Met.Width + 20;
                    Method = Met;
                }

                Met.EventChange = null;
                Met.Level = i;
                Met.Name = "useMethod" + i;
                Met.Next = null;
                Met.NumSteps = DefaulttrackMethod;
                Met.Size = new System.Drawing.Size(446, 89);
                Met.TabIndex = 30;
                Met.TypeWork = DefaultETypeWork;
                Met.TypeImplementation = DefaultETypeImplementation;
                Met.CallNext = DefaultECallNext;

                topLocation = i * (Met.Height + 1);
                // Met.Location = new System.Drawing.Point(0, topLocation);
                panelMethodsFlow.Controls.Add(Met);
                methods.Add(Met);
            }

            panelMethodsFlow.Width = width;
            panelMethodsFlow.Height = (methods.Count * height);
            PLeftMain.MaximumSize = new System.Drawing.Size() { Width = width + 15 };
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

        private async void bttRun_Click(object sender, EventArgs e)
        {
            if (Method == null )
            {
                MessageBox.Show("The test is not well configured");
                return;
            }
                

            string validation =Method.ValidateConfigurations();
            if (!string.IsNullOrEmpty(validation))
            {
                MessageBox.Show(validation);
                return;
            }
            

            this.Enabled = false; /*deshabilita pantalla*/


            ReportGenerator reporter = new ReportGenerator();
            IGetBase implementacionMain = null;
            try
            {

               // await Task.Run(() =>
              //  {
                    ProgramLanzador lanzador = null;
                    implementacionMain = HowToWorkAsync.ImpDynamic.FactoryImplClass.GetInstance(Method, reporter);

                    try
                    {

                        lanzador = new ProgramLanzador(reporter, implementacionMain);
                        informe = await lanzador.LanzarImplementacionYObtenerSerie();
                    }
                    catch (Exception ex1)
                    {
                        reporter.Generate("Ex1", -1);
                        MessageBox.Show(ex1.Message);
                    }
               // }
              //);

            }
            catch (Exception ex)
            {
                reporter.Generate("Ex2", -1);
                MessageBox.Show(ex.Message);
            }
            finally
            {
            }



            PrintMethods printer = new PrintMethods(tree);
            printer.Print(Method);




            CrearProcesador().Ejecutar(informe);
            this.Enabled = true;
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

        IStrategyProcesarInforme CrearProcesador()
        {
            return new StrategyPintar(grafica,
                                      chkOrdernar.Checked,
                                      chkSerie.Checked,
                                      chConId.Checked,
                                      chkHilos.Checked,
                                      chkTiempoTicks.Checked,
                                      (SeriesChartType)cmbTipoGrafica.SelectedItem);
        }


        private void HelperProcesar(bool aplicar = true)
        {
            if (this.Enabled != false)
                this.Enabled = false;

            try
            {
                var proc = CrearProcesador();
                if (aplicar)
                {
                    proc.Ejecutar(informe);
                }
                else
                {
                    proc.Volcar(informe, "c:\\volcar" + DateTime.Now.ToString().Replace("/", "_").Replace(":", "_") + ".csv");
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
            var Formulario = new FormAsync();
            Formulario.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            HelperProcesar(false);
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
    }
}
