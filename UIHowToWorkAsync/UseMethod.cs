using System;
using System.Windows.Forms;
using HowToWorkAsync;

namespace UIHowToWorkAsync
{


    public partial class UseMethod : UserControl, IUseMethod
    {

        
        public UseMethod()
        {
            InitializeComponent();

            foreach (var aux in Enum.GetValues(typeof(ETypeWork)))
            {
                cmb.Items.Add(aux);
            }

            foreach (var aux in Enum.GetValues(typeof(ETypeImpl)))
            {
                cmbImplementation.Items.Add(aux);
            }

            SetValuesCall();

            SetLabelUnit();

            EventChangeTheLastMethod += EventNextNullHandler;
        }

        private void trackMetodo1_Scroll(object sender, EventArgs e)
        {

        }

        private void trackMethod_Scroll(object sender, EventArgs e)
        {
            SetLabelUnit();
        }

        private void cmb_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            SetLabelUnit();
        }

        private void SetValuesCall()
        {
            foreach (var aux in Enum.GetValues(typeof(ECallNext)))
            {
                cmbAlg.Items.Add(aux);
            }
        }


        private void cmbImplementation_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventChange?.Invoke((ETypeImpl)cmbImplementation.SelectedIndex);
            EventChangeTheLastMethod?.Invoke((ETypeImpl)cmbImplementation.SelectedIndex);
        }

        private void groupBoxMethod_Enter(object sender, EventArgs e)
        {

        }

        delegate void MethodSetDelegate(Control control, MethodAux v);
        delegate T MethodGetDelegate<T>(Control control, MethodGetAux<T> v);
        delegate void MethodAux();
        delegate T MethodGetAux<T>();


        private void ModifyMethod(Control control, MethodAux v)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodSetDelegate(ModifyMethod), control, v);
            }
            v.Invoke();
        }

        private T GetMethod<T>(Control control, MethodGetAux<T> v)
        {
            if (control.InvokeRequired)
            {
                return (T)control.Invoke(new MethodGetDelegate<T>(GetMethod<T>), control, v);
            }
            return v.Invoke();
        }



        public ETypeWork TypeWork
        {
            get
            {

                return GetMethod(cmb, () => { return (ETypeWork)cmb.SelectedIndex; });
            }
            set
            {
                ModifyMethod(cmb, () => { cmb.SelectedIndex = (int)value; });
            }
        }

        public int NumSteps
        {
            get
            {
                return GetMethod(trackMethod, () => { return trackMethod.Value; });
            }
            set
            {
                ModifyMethod(trackMethod, () => { trackMethod.Value = (int)value; });
            }

        }

        private void EventNextHandler(ETypeImpl newType)
        {
            ModifyMethod(cmbAlg, () => { 

            var newValues = newType.HowToBeCalled();

            var t2 = cmbAlg.SelectedItem;
            cmbAlg.Items.Clear();
            foreach (var aux in newValues)
            {
                cmbAlg.Items.Add(aux);
            }

            if (t2 != null && cmbAlg.Items.Contains(t2))
            {
                cmbAlg.SelectedItem = t2;
            }
            });

        }

        private void EventNextNullHandler(ETypeImpl newType)
        {
            ModifyMethod(cmbAlg, () => {

                var newValues = newType.HowToBeCalled();

                var t2 = cmbAlg.SelectedItem;
                cmbAlg.Items.Clear();
                foreach (var aux in newValues)
                {
                    cmbAlg.Items.Add(aux);
                }

                if (t2 != null && cmbAlg.Items.Contains(t2))
                {
                    cmbAlg.SelectedItem = t2;
                }
            });

        }

        

        IUseMethod next = null;
        public IUseMethod Next
        {
            get
            {
                return next;
            }
            set
            {
                next = value;
                if (next != null)
                {
                    EventChangeTheLastMethod += null;
                    next.EventChange += EventNextHandler;
                }
                else
                {
                    EventChangeTheLastMethod += EventNextNullHandler;
                }
            }
        }

        private int level = 0;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                ModifyMethod(cmbAlg, () => { this.groupBoxMethod.Text = "Method" + level; });
            }
        }


        public ECallNext CallNext
        {
            get => GetMethod(cmbAlg, () => { return (ECallNext)cmbAlg.SelectedIndex; });
            set => ModifyMethod(cmbAlg, () => { cmbAlg.SelectedIndex = (int)value; });
        }

        public ETypeImpl TypeImplementation
        {
            get => GetMethod(cmbAlg, () => { return (ETypeImpl)cmbImplementation.SelectedIndex; });
            set => ModifyMethod(cmbAlg, () => { cmbImplementation.SelectedIndex = (int)value; });
        }
        public EventNextMethodWasChanged EventChange { get ; set; }
        public EventNextMethodWasChanged EventChangeTheLastMethod { get; set; }

        public string IdMethod => GetMethod(groupBoxMethod, () => { return (string)groupBoxMethod.Text; });


        private void SetLabelUnit()
        {
            ModifyMethod(lblTrack, () =>
            {
                var selected = (ETypeWork)cmb.SelectedIndex;
                lblTrack.Text = this.trackMethod.Value * selected.Factor() + " " + selected.Unit();
            });
        }

        private void UseMethod_Load(object sender, EventArgs e)
        {

        }

        public string ValidateConfigurations()
        {
            if (Next != null)
            {
                if ((int)CallNext == -1)
                {
                    return "CallNext  is not well selected For Method Level " + level;
                }

                string resultNExt = Next.ValidateConfigurations();
                if (!string.IsNullOrEmpty(resultNExt))
                {
                    return resultNExt;
                }

            }

            return null;
        }

    }
}
