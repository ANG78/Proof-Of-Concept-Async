namespace UIHowToWorkAsync
{
    partial class UseMethod
    {
        /// <summary> 
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de componentes

        /// <summary> 
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxMethod = new System.Windows.Forms.GroupBox();
            this.cmbNextAlg = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.cmbImpl = new System.Windows.Forms.ComboBox();
            this.groupTodo = new System.Windows.Forms.GroupBox();
            this.cmbMyImpl = new System.Windows.Forms.ComboBox();
            this.cmb = new System.Windows.Forms.ComboBox();
            this.lblTrack = new System.Windows.Forms.Label();
            this.trackMethod = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gbNextCall = new System.Windows.Forms.GroupBox();
            this.lblNext = new System.Windows.Forms.Label();
            this.groupBoxMethod.SuspendLayout();
            this.groupTodo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMethod)).BeginInit();
            this.gbNextCall.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMethod
            // 
            this.groupBoxMethod.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBoxMethod.Controls.Add(this.gbNextCall);
            this.groupBoxMethod.Controls.Add(this.label1);
            this.groupBoxMethod.Controls.Add(this.lbName);
            this.groupBoxMethod.Controls.Add(this.cmbImpl);
            this.groupBoxMethod.Controls.Add(this.groupTodo);
            this.groupBoxMethod.Controls.Add(this.label2);
            this.groupBoxMethod.Location = new System.Drawing.Point(3, 0);
            this.groupBoxMethod.Name = "groupBoxMethod";
            this.groupBoxMethod.Size = new System.Drawing.Size(356, 124);
            this.groupBoxMethod.TabIndex = 10;
            this.groupBoxMethod.TabStop = false;
            this.groupBoxMethod.Enter += new System.EventHandler(this.groupBoxMethod_Enter);
            // 
            // cmbNextAlg
            // 
            this.cmbNextAlg.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbNextAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNextAlg.FormattingEnabled = true;
            this.cmbNextAlg.Location = new System.Drawing.Point(106, 9);
            this.cmbNextAlg.Name = "cmbNextAlg";
            this.cmbNextAlg.Size = new System.Drawing.Size(180, 21);
            this.cmbNextAlg.TabIndex = 12;
            this.cmbNextAlg.SelectedIndexChanged += new System.EventHandler(this.cmbNextAlg_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Type Of Impl.";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbName.Location = new System.Drawing.Point(6, 13);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(51, 15);
            this.lbName.TabIndex = 16;
            this.lbName.Text = "lbName";
            // 
            // cmbImpl
            // 
            this.cmbImpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImpl.FormattingEnabled = true;
            this.cmbImpl.Location = new System.Drawing.Point(9, 51);
            this.cmbImpl.Name = "cmbImpl";
            this.cmbImpl.Size = new System.Drawing.Size(84, 21);
            this.cmbImpl.TabIndex = 14;
            this.cmbImpl.SelectedIndexChanged += new System.EventHandler(this.cmbImplementation_SelectedIndexChanged);
            // 
            // groupTodo
            // 
            this.groupTodo.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.groupTodo.Controls.Add(this.cmbMyImpl);
            this.groupTodo.Controls.Add(this.cmb);
            this.groupTodo.Controls.Add(this.lblTrack);
            this.groupTodo.Controls.Add(this.trackMethod);
            this.groupTodo.Location = new System.Drawing.Point(104, 7);
            this.groupTodo.Name = "groupTodo";
            this.groupTodo.Size = new System.Drawing.Size(244, 71);
            this.groupTodo.TabIndex = 13;
            this.groupTodo.TabStop = false;
            this.groupTodo.Text = "DoIndependetWork";
            // 
            // cmbMyImpl
            // 
            this.cmbMyImpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMyImpl.FormattingEnabled = true;
            this.cmbMyImpl.Location = new System.Drawing.Point(8, 44);
            this.cmbMyImpl.Name = "cmbMyImpl";
            this.cmbMyImpl.Size = new System.Drawing.Size(180, 21);
            this.cmbMyImpl.TabIndex = 15;
            this.cmbMyImpl.SelectedIndexChanged += new System.EventHandler(this.cmbMyImpl_SelectedIndexChanged);
            // 
            // cmb
            // 
            this.cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb.FormattingEnabled = true;
            this.cmb.Location = new System.Drawing.Point(8, 17);
            this.cmb.Name = "cmb";
            this.cmb.Size = new System.Drawing.Size(109, 21);
            this.cmb.TabIndex = 13;
            this.cmb.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged_1);
            // 
            // lblTrack
            // 
            this.lblTrack.AutoSize = true;
            this.lblTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrack.Location = new System.Drawing.Point(176, 20);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(24, 17);
            this.lblTrack.TabIndex = 12;
            this.lblTrack.Text = "50";
            // 
            // trackMethod
            // 
            this.trackMethod.Location = new System.Drawing.Point(129, 9);
            this.trackMethod.Margin = new System.Windows.Forms.Padding(0);
            this.trackMethod.Maximum = 100;
            this.trackMethod.Name = "trackMethod";
            this.trackMethod.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackMethod.Size = new System.Drawing.Size(50, 42);
            this.trackMethod.TabIndex = 11;
            this.trackMethod.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackMethod.Value = 50;
            this.trackMethod.Scroll += new System.EventHandler(this.trackMethod_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 10;
            // 
            // gbNextCall
            // 
            this.gbNextCall.BackColor = System.Drawing.Color.LightSteelBlue;
            this.gbNextCall.Controls.Add(this.lblNext);
            this.gbNextCall.Controls.Add(this.cmbNextAlg);
            this.gbNextCall.Location = new System.Drawing.Point(6, 81);
            this.gbNextCall.Name = "gbNextCall";
            this.gbNextCall.Size = new System.Drawing.Size(342, 36);
            this.gbNextCall.TabIndex = 20;
            this.gbNextCall.TabStop = false;
            // 
            // lblNext
            // 
            this.lblNext.AutoSize = true;
            this.lblNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNext.Location = new System.Drawing.Point(5, 14);
            this.lblNext.Name = "lblNext";
            this.lblNext.Size = new System.Drawing.Size(80, 13);
            this.lblNext.TabIndex = 13;
            this.lblNext.Text = "Next call to..";
            // 
            // UseMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.groupBoxMethod);
            this.MinimumSize = new System.Drawing.Size(350, 0);
            this.Name = "UseMethod";
            this.Size = new System.Drawing.Size(362, 127);
            this.Load += new System.EventHandler(this.UseMethod_Load);
            this.groupBoxMethod.ResumeLayout(false);
            this.groupBoxMethod.PerformLayout();
            this.groupTodo.ResumeLayout(false);
            this.groupTodo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMethod)).EndInit();
            this.gbNextCall.ResumeLayout(false);
            this.gbNextCall.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxMethod;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbNextAlg;
        private System.Windows.Forms.GroupBox groupTodo;
        private System.Windows.Forms.Label lblTrack;
        private System.Windows.Forms.TrackBar trackMethod;
        private System.Windows.Forms.ComboBox cmb;
        private System.Windows.Forms.ComboBox cmbImpl;
        private System.Windows.Forms.ComboBox cmbMyImpl;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbNextCall;
        private System.Windows.Forms.Label lblNext;
    }
}
