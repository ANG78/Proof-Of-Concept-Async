namespace UIHowToWorkAsync
{
    partial class FormAsync
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.splitterLeft = new System.Windows.Forms.Splitter();
            this.PTop = new System.Windows.Forms.Panel();
            this.cmbTipoGrafica = new System.Windows.Forms.ComboBox();
            this.chkTiempoTicks = new System.Windows.Forms.CheckBox();
            this.chkOrdernar = new System.Windows.Forms.CheckBox();
            this.chConId = new System.Windows.Forms.CheckBox();
            this.chkHilos = new System.Windows.Forms.CheckBox();
            this.chkSerie = new System.Windows.Forms.CheckBox();
            this.splitterTop = new System.Windows.Forms.Splitter();
            this.panel3 = new System.Windows.Forms.Panel();
            this.grafica = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.options = new System.Windows.Forms.GroupBox();
            this.lblLevels = new System.Windows.Forms.Label();
            this.bttRun = new System.Windows.Forms.Button();
            this.bttNeWindow = new System.Windows.Forms.Button();
            this.cmbLevels = new System.Windows.Forms.ComboBox();
            this.PLeft_TOP = new System.Windows.Forms.Panel();
            this.panelMethods = new System.Windows.Forms.Panel();
            this.panelMethodsFlow = new System.Windows.Forms.FlowLayoutPanel();
            this.PLeftMain = new System.Windows.Forms.Panel();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.bttPrint = new System.Windows.Forms.Button();
            this.chkStartEnd = new System.Windows.Forms.CheckBox();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grafica)).BeginInit();
            this.options.SuspendLayout();
            this.PLeft_TOP.SuspendLayout();
            this.panelMethods.SuspendLayout();
            this.PLeftMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitterLeft
            // 
            this.splitterLeft.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitterLeft.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitterLeft.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitterLeft.Location = new System.Drawing.Point(356, 0);
            this.splitterLeft.Name = "splitterLeft";
            this.splitterLeft.Size = new System.Drawing.Size(5, 581);
            this.splitterLeft.TabIndex = 1;
            this.splitterLeft.TabStop = false;
            // 
            // PTop
            // 
            this.PTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.PTop.Location = new System.Drawing.Point(361, 0);
            this.PTop.Margin = new System.Windows.Forms.Padding(0);
            this.PTop.MinimumSize = new System.Drawing.Size(0, 5);
            this.PTop.Name = "PTop";
            this.PTop.Size = new System.Drawing.Size(716, 10);
            this.PTop.TabIndex = 2;
            // 
            // cmbTipoGrafica
            // 
            this.cmbTipoGrafica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoGrafica.FormattingEnabled = true;
            this.cmbTipoGrafica.Location = new System.Drawing.Point(119, 18);
            this.cmbTipoGrafica.Name = "cmbTipoGrafica";
            this.cmbTipoGrafica.Size = new System.Drawing.Size(94, 21);
            this.cmbTipoGrafica.TabIndex = 43;
            this.cmbTipoGrafica.SelectedIndexChanged += new System.EventHandler(this.cmbTipoGrafica_SelectedIndexChanged);
            // 
            // chkTiempoTicks
            // 
            this.chkTiempoTicks.AutoSize = true;
            this.chkTiempoTicks.Location = new System.Drawing.Point(6, 89);
            this.chkTiempoTicks.Name = "chkTiempoTicks";
            this.chkTiempoTicks.Size = new System.Drawing.Size(80, 17);
            this.chkTiempoTicks.TabIndex = 20;
            this.chkTiempoTicks.Text = "Time/Ticks";
            this.chkTiempoTicks.UseVisualStyleBackColor = true;
            this.chkTiempoTicks.CheckedChanged += new System.EventHandler(this.chkTiempoTicks_CheckedChanged);
            // 
            // chkOrdernar
            // 
            this.chkOrdernar.AutoSize = true;
            this.chkOrdernar.Checked = true;
            this.chkOrdernar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkOrdernar.Location = new System.Drawing.Point(6, 48);
            this.chkOrdernar.Name = "chkOrdernar";
            this.chkOrdernar.Size = new System.Drawing.Size(129, 17);
            this.chkOrdernar.TabIndex = 16;
            this.chkOrdernar.Text = "Order Series by Name";
            this.chkOrdernar.UseVisualStyleBackColor = true;
            this.chkOrdernar.CheckedChanged += new System.EventHandler(this.chkTiempoTicks_CheckedChanged);
            // 
            // chConId
            // 
            this.chConId.AutoSize = true;
            this.chConId.Checked = true;
            this.chConId.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chConId.Location = new System.Drawing.Point(122, 70);
            this.chConId.Name = "chConId";
            this.chConId.Size = new System.Drawing.Size(109, 17);
            this.chConId.TabIndex = 26;
            this.chConId.Text = "Name+Id Thread ";
            this.chConId.UseVisualStyleBackColor = true;
            this.chConId.Visible = false;
            this.chConId.CheckedChanged += new System.EventHandler(this.chkTiempoTicks_CheckedChanged);
            // 
            // chkHilos
            // 
            this.chkHilos.AutoSize = true;
            this.chkHilos.Location = new System.Drawing.Point(121, 89);
            this.chkHilos.Name = "chkHilos";
            this.chkHilos.Size = new System.Drawing.Size(109, 17);
            this.chkHilos.TabIndex = 17;
            this.chkHilos.Text = "Y-axis = IdThread";
            this.chkHilos.UseVisualStyleBackColor = true;
            this.chkHilos.CheckedChanged += new System.EventHandler(this.chkTiempoTicks_CheckedChanged);
            // 
            // chkSerie
            // 
            this.chkSerie.AutoSize = true;
            this.chkSerie.Location = new System.Drawing.Point(6, 68);
            this.chkSerie.Name = "chkSerie";
            this.chkSerie.Size = new System.Drawing.Size(92, 17);
            this.chkSerie.TabIndex = 18;
            this.chkSerie.Text = "Display Series";
            this.chkSerie.UseVisualStyleBackColor = true;
            this.chkSerie.CheckedChanged += new System.EventHandler(this.chkTiempoTicks_CheckedChanged);
            // 
            // splitterTop
            // 
            this.splitterTop.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitterTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.splitterTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitterTop.Location = new System.Drawing.Point(361, 10);
            this.splitterTop.Name = "splitterTop";
            this.splitterTop.Size = new System.Drawing.Size(716, 5);
            this.splitterTop.TabIndex = 3;
            this.splitterTop.TabStop = false;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel3.Controls.Add(this.grafica);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(361, 15);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(716, 566);
            this.panel3.TabIndex = 4;
            // 
            // grafica
            // 
            this.grafica.BorderlineWidth = 5;
            chartArea2.Name = "ChartArea1";
            this.grafica.ChartAreas.Add(chartArea2);
            this.grafica.Dock = System.Windows.Forms.DockStyle.Fill;
            legend2.Name = "Legend1";
            this.grafica.Legends.Add(legend2);
            this.grafica.Location = new System.Drawing.Point(0, 0);
            this.grafica.Name = "grafica";
            series2.ChartArea = "ChartArea1";
            series2.Legend = "Legend1";
            series2.Name = "Series1";
            this.grafica.Series.Add(series2);
            this.grafica.Size = new System.Drawing.Size(716, 566);
            this.grafica.TabIndex = 0;
            this.grafica.Text = "chart1";
            // 
            // options
            // 
            this.options.Controls.Add(this.chkStartEnd);
            this.options.Controls.Add(this.bttPrint);
            this.options.Controls.Add(this.lblLevels);
            this.options.Controls.Add(this.bttRun);
            this.options.Controls.Add(this.bttNeWindow);
            this.options.Controls.Add(this.chkSerie);
            this.options.Controls.Add(this.cmbTipoGrafica);
            this.options.Controls.Add(this.cmbLevels);
            this.options.Controls.Add(this.chkOrdernar);
            this.options.Controls.Add(this.chConId);
            this.options.Controls.Add(this.chkTiempoTicks);
            this.options.Controls.Add(this.chkHilos);
            this.options.Dock = System.Windows.Forms.DockStyle.Top;
            this.options.Location = new System.Drawing.Point(0, 0);
            this.options.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.options.Name = "options";
            this.options.Size = new System.Drawing.Size(356, 118);
            this.options.TabIndex = 44;
            this.options.TabStop = false;
            // 
            // lblLevels
            // 
            this.lblLevels.AutoSize = true;
            this.lblLevels.Location = new System.Drawing.Point(241, 23);
            this.lblLevels.Name = "lblLevels";
            this.lblLevels.Size = new System.Drawing.Size(38, 13);
            this.lblLevels.TabIndex = 44;
            this.lblLevels.Text = "Levels";
            // 
            // bttRun
            // 
            this.bttRun.Location = new System.Drawing.Point(8, 16);
            this.bttRun.Name = "bttRun";
            this.bttRun.Size = new System.Drawing.Size(68, 26);
            this.bttRun.TabIndex = 42;
            this.bttRun.Text = "Execute";
            this.bttRun.UseVisualStyleBackColor = true;
            this.bttRun.Click += new System.EventHandler(this.bttRun_Click);
            // 
            // bttNeWindow
            // 
            this.bttNeWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttNeWindow.Location = new System.Drawing.Point(239, 83);
            this.bttNeWindow.Name = "bttNeWindow";
            this.bttNeWindow.Size = new System.Drawing.Size(111, 27);
            this.bttNeWindow.TabIndex = 2;
            this.bttNeWindow.Text = "new Window!!";
            this.bttNeWindow.UseVisualStyleBackColor = true;
            this.bttNeWindow.Click += new System.EventHandler(this.button2_Click);
            // 
            // cmbLevels
            // 
            this.cmbLevels.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbLevels.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbLevels.FormattingEnabled = true;
            this.cmbLevels.Location = new System.Drawing.Point(303, 19);
            this.cmbLevels.Name = "cmbLevels";
            this.cmbLevels.Size = new System.Drawing.Size(41, 21);
            this.cmbLevels.TabIndex = 40;
            this.cmbLevels.SelectedIndexChanged += new System.EventHandler(this.cmbLevels_SelectedIndexChanged);
            // 
            // PLeft_TOP
            // 
            this.PLeft_TOP.AutoScroll = true;
            this.PLeft_TOP.AutoSize = true;
            this.PLeft_TOP.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.PLeft_TOP.BackColor = System.Drawing.SystemColors.Control;
            this.PLeft_TOP.Controls.Add(this.options);
            this.PLeft_TOP.Controls.Add(this.panelMethods);
            this.PLeft_TOP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PLeft_TOP.Location = new System.Drawing.Point(0, 0);
            this.PLeft_TOP.Name = "PLeft_TOP";
            this.PLeft_TOP.Size = new System.Drawing.Size(356, 581);
            this.PLeft_TOP.TabIndex = 45;
            // 
            // panelMethods
            // 
            this.panelMethods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMethods.AutoScroll = true;
            this.panelMethods.AutoSize = true;
            this.panelMethods.BackColor = System.Drawing.Color.Transparent;
            this.panelMethods.Controls.Add(this.panelMethodsFlow);
            this.panelMethods.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.panelMethods.Location = new System.Drawing.Point(3, 118);
            this.panelMethods.Name = "panelMethods";
            this.panelMethods.Padding = new System.Windows.Forms.Padding(1);
            this.panelMethods.Size = new System.Drawing.Size(350, 457);
            this.panelMethods.TabIndex = 43;
            // 
            // panelMethodsFlow
            // 
            this.panelMethodsFlow.AutoSize = true;
            this.panelMethodsFlow.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMethodsFlow.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panelMethodsFlow.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.panelMethodsFlow.Location = new System.Drawing.Point(1, 1);
            this.panelMethodsFlow.Name = "panelMethodsFlow";
            this.panelMethodsFlow.Size = new System.Drawing.Size(0, 0);
            this.panelMethodsFlow.TabIndex = 35;
            // 
            // PLeftMain
            // 
            this.PLeftMain.BackColor = System.Drawing.SystemColors.Control;
            this.PLeftMain.Controls.Add(this.splitter1);
            this.PLeftMain.Controls.Add(this.PLeft_TOP);
            this.PLeftMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.PLeftMain.Location = new System.Drawing.Point(0, 0);
            this.PLeftMain.Name = "PLeftMain";
            this.PLeftMain.Size = new System.Drawing.Size(356, 581);
            this.PLeftMain.TabIndex = 0;
            this.PLeftMain.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(356, 5);
            this.splitter1.TabIndex = 47;
            this.splitter1.TabStop = false;
            // 
            // bttPrint
            // 
            this.bttPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bttPrint.Location = new System.Drawing.Point(279, 51);
            this.bttPrint.Name = "bttPrint";
            this.bttPrint.Size = new System.Drawing.Size(68, 26);
            this.bttPrint.TabIndex = 45;
            this.bttPrint.Text = "Print";
            this.bttPrint.UseVisualStyleBackColor = true;
            this.bttPrint.Visible = false;
            this.bttPrint.Click += new System.EventHandler(this.bttPrint_Click);
            // 
            // chkStartEnd
            // 
            this.chkStartEnd.AutoSize = true;
            this.chkStartEnd.Checked = true;
            this.chkStartEnd.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkStartEnd.Location = new System.Drawing.Point(141, 48);
            this.chkStartEnd.Name = "chkStartEnd";
            this.chkStartEnd.Size = new System.Drawing.Size(125, 17);
            this.chkStartEnd.TabIndex = 46;
            this.chkStartEnd.Text = "Display START-END";
            this.chkStartEnd.UseVisualStyleBackColor = true;
            this.chkStartEnd.CheckedChanged += new System.EventHandler(this.chkTiempoTicks_CheckedChanged);
            // 
            // FormAsync
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1077, 581);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.splitterTop);
            this.Controls.Add(this.PTop);
            this.Controls.Add(this.splitterLeft);
            this.Controls.Add(this.PLeftMain);
            this.Name = "FormAsync";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grafica)).EndInit();
            this.options.ResumeLayout(false);
            this.options.PerformLayout();
            this.PLeft_TOP.ResumeLayout(false);
            this.PLeft_TOP.PerformLayout();
            this.panelMethods.ResumeLayout(false);
            this.panelMethods.PerformLayout();
            this.PLeftMain.ResumeLayout(false);
            this.PLeftMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Splitter splitterLeft;
        private System.Windows.Forms.Panel PTop;
        private System.Windows.Forms.Splitter splitterTop;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ComboBox cmbTipoGrafica;
        private System.Windows.Forms.DataVisualization.Charting.Chart grafica;
        private System.Windows.Forms.CheckBox chkTiempoTicks;
        private System.Windows.Forms.CheckBox chkOrdernar;
        private System.Windows.Forms.CheckBox chkHilos;
        private System.Windows.Forms.CheckBox chConId;
        private System.Windows.Forms.CheckBox chkSerie;
        private System.Windows.Forms.GroupBox options;
        private System.Windows.Forms.Button bttRun;
        private System.Windows.Forms.Panel PLeft_TOP;
        private System.Windows.Forms.Button bttNeWindow;
        private System.Windows.Forms.Panel panelMethods;
        private System.Windows.Forms.FlowLayoutPanel panelMethodsFlow;
        private System.Windows.Forms.ComboBox cmbLevels;
        private System.Windows.Forms.Panel PLeftMain;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Label lblLevels;
        private System.Windows.Forms.Button bttPrint;
        private System.Windows.Forms.CheckBox chkStartEnd;
    }
}