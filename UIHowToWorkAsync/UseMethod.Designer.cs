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
            this.cmbNextImpl = new System.Windows.Forms.ComboBox();
            this.groupTodo = new System.Windows.Forms.GroupBox();
            this.cmbMyImpl = new System.Windows.Forms.ComboBox();
            this.cmb = new System.Windows.Forms.ComboBox();
            this.lblTrack = new System.Windows.Forms.Label();
            this.trackMethod = new System.Windows.Forms.TrackBar();
            this.cmbNextAlg = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBoxMethod.SuspendLayout();
            this.groupTodo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMethod)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxMethod
            // 
            this.groupBoxMethod.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxMethod.Controls.Add(this.cmbNextImpl);
            this.groupBoxMethod.Controls.Add(this.groupTodo);
            this.groupBoxMethod.Controls.Add(this.cmbNextAlg);
            this.groupBoxMethod.Controls.Add(this.label2);
            this.groupBoxMethod.Location = new System.Drawing.Point(3, 0);
            this.groupBoxMethod.Name = "groupBoxMethod";
            this.groupBoxMethod.Size = new System.Drawing.Size(428, 82);
            this.groupBoxMethod.TabIndex = 10;
            this.groupBoxMethod.TabStop = false;
            this.groupBoxMethod.Text = "Method";
            this.groupBoxMethod.Enter += new System.EventHandler(this.groupBoxMethod_Enter);
            // 
            // cmbNextImpl
            // 
            this.cmbNextImpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNextImpl.FormattingEnabled = true;
            this.cmbNextImpl.Location = new System.Drawing.Point(305, 23);
            this.cmbNextImpl.Name = "cmbNextImpl";
            this.cmbNextImpl.Size = new System.Drawing.Size(62, 21);
            this.cmbNextImpl.TabIndex = 14;
            this.cmbNextImpl.SelectedIndexChanged += new System.EventHandler(this.cmbImplementation_SelectedIndexChanged);
            // 
            // groupTodo
            // 
            this.groupTodo.Controls.Add(this.cmbMyImpl);
            this.groupTodo.Controls.Add(this.cmb);
            this.groupTodo.Controls.Add(this.lblTrack);
            this.groupTodo.Controls.Add(this.trackMethod);
            this.groupTodo.Location = new System.Drawing.Point(12, 16);
            this.groupTodo.Name = "groupTodo";
            this.groupTodo.Size = new System.Drawing.Size(269, 58);
            this.groupTodo.TabIndex = 13;
            this.groupTodo.TabStop = false;
            // 
            // cmdMyImpl
            // 
            this.cmbMyImpl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMyImpl.FormattingEnabled = true;
            this.cmbMyImpl.Location = new System.Drawing.Point(143, 10);
            this.cmbMyImpl.Name = "cmdMyImpl";
            this.cmbMyImpl.Size = new System.Drawing.Size(62, 21);
            this.cmbMyImpl.TabIndex = 15;
            // 
            // cmb
            // 
            this.cmb.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb.FormattingEnabled = true;
            this.cmb.Location = new System.Drawing.Point(143, 32);
            this.cmb.Name = "cmb";
            this.cmb.Size = new System.Drawing.Size(109, 21);
            this.cmb.TabIndex = 13;
            this.cmb.SelectedIndexChanged += new System.EventHandler(this.cmb_SelectedIndexChanged_1);
            // 
            // lblTrack
            // 
            this.lblTrack.AutoSize = true;
            this.lblTrack.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrack.Location = new System.Drawing.Point(68, 22);
            this.lblTrack.Name = "lblTrack";
            this.lblTrack.Size = new System.Drawing.Size(24, 17);
            this.lblTrack.TabIndex = 12;
            this.lblTrack.Text = "50";
            // 
            // trackMethod
            // 
            this.trackMethod.Location = new System.Drawing.Point(4, 10);
            this.trackMethod.Margin = new System.Windows.Forms.Padding(0);
            this.trackMethod.Maximum = 100;
            this.trackMethod.Name = "trackMethod";
            this.trackMethod.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.trackMethod.Size = new System.Drawing.Size(67, 42);
            this.trackMethod.TabIndex = 11;
            this.trackMethod.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackMethod.Value = 50;
            this.trackMethod.Scroll += new System.EventHandler(this.trackMethod_Scroll);
            // 
            // cmbNextAlg
            // 
            this.cmbNextAlg.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNextAlg.FormattingEnabled = true;
            this.cmbNextAlg.Location = new System.Drawing.Point(305, 50);
            this.cmbNextAlg.Name = "cmbNextAlg";
            this.cmbNextAlg.Size = new System.Drawing.Size(109, 21);
            this.cmbNextAlg.TabIndex = 12;
            this.cmbNextAlg.SelectedIndexChanged += new System.EventHandler(this.cmbNextAlg_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 10;
            // 
            // UseMethod
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxMethod);
            this.Name = "UseMethod";
            this.Size = new System.Drawing.Size(434, 85);
            this.Load += new System.EventHandler(this.UseMethod_Load);
            this.groupBoxMethod.ResumeLayout(false);
            this.groupBoxMethod.PerformLayout();
            this.groupTodo.ResumeLayout(false);
            this.groupTodo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackMethod)).EndInit();
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
        private System.Windows.Forms.ComboBox cmbNextImpl;
        private System.Windows.Forms.ComboBox cmbMyImpl;
    }
}
