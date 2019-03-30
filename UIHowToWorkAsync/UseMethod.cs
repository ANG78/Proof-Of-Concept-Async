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

            foreach (var aux in Enum.GetValues(typeof(EStrategyDoIndependentWork)))
            {
                cmbMyImpl.Items.Add(aux);
            }

            foreach (var aux in Enum.GetValues(typeof(ETypeDoIndependentWork)))
            {
                cmb.Items.Add(aux);
            }

            foreach (var aux in Enum.GetValues(typeof(ETypeImpl)))
            {
                cmbImpl.Items.Add(aux);
            }

            SetValuesCall();

            SetLabelUnit();

            StrategyDoIndependentWork = EStrategyDoIndependentWork.NORMAL;

            EventChange += EventNextHandler;

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
                cmbNextAlg.Items.Add(aux);
            }
        }


        private void cmbImplementation_SelectedIndexChanged(object sender, EventArgs e)
        {
            EventChange?.Invoke((ETypeImpl)cmbImpl.SelectedIndex);
            NextEventChange?.Invoke((ETypeImpl)cmbImpl.SelectedIndex);
        }

        private void groupBoxMethod_Enter(object sender, EventArgs e)
        {
        }




        public ETypeDoIndependentWork TypeDoIndependentWork
        {
            get
            {
                return HelperUI.GetMethod(cmb, () => { return (ETypeDoIndependentWork)cmb.SelectedIndex; });
            }
            set
            {
                HelperUI.ModifyMethod(cmb, () => { cmb.SelectedIndex = (int)value; });
            }
        }

        public int NumSteps
        {
            get
            {
                return HelperUI.GetMethod(trackMethod, () => { return trackMethod.Value; });
            }
            set
            {
                HelperUI.ModifyMethod(trackMethod, () => { trackMethod.Value = (int)value; });
            }

        }

        private void EventNextHandler(ETypeImpl newType)
        {
            HelperUI.ModifyMethod(cmbNextAlg, () =>
            {
                if (Next != null)
                {
                    /*cmbNextAlg*/
                    var newValues = newType.HowToCallTheNextOne(next.TypeImpl);
                    var current = cmbNextAlg.SelectedItem;
                    cmbNextAlg.Items.Clear();
                    foreach (var aux in newValues)
                    {
                        cmbNextAlg.Items.Add(aux);
                    }

                    if (current != null && cmbNextAlg.Items.Contains(current))
                    {
                        cmbNextAlg.SelectedItem = current;
                    }

                    if (cmbNextAlg.SelectedItem == null && newValues.Count == 1)
                    {
                        cmbNextAlg.SelectedItem = newValues[0];
                    }
                }
            });

            HelperUI.ModifyMethod(cmbMyImpl, () =>
            {
                /*cmbImpl*/
                var newValuesToBeImplemented = newType.DoMyWork();
                var currentImplementation = cmbMyImpl.SelectedItem;
                cmbMyImpl.Items.Clear();
                foreach (var aux in newValuesToBeImplemented)
                {
                    cmbMyImpl.Items.Add(aux);
                }


                if (currentImplementation != null && cmbMyImpl.Items.Contains(currentImplementation))
                {
                    cmbMyImpl.SelectedItem = currentImplementation;
                }

                if (cmbMyImpl.SelectedItem == null && newValuesToBeImplemented.Count == 1)
                {
                    cmbMyImpl.SelectedItem = newValuesToBeImplemented[0];
                }
            });
        }

        private void NextEventChangeHandler(ETypeImpl newType)
        {
            HelperUI.ModifyMethod(cmbNextAlg, () =>
            {
                if (Next != null)
                {
                    var newValues = newType.HowToCallTheNextOne(Next.TypeImpl);

                    var current = cmbNextAlg.SelectedItem;
                    cmbNextAlg.Items.Clear();
                    foreach (var aux in newValues)
                    {
                        cmbNextAlg.Items.Add(aux);
                    }

                    if (current != null && cmbNextAlg.Items.Contains(current))
                    {
                        cmbNextAlg.SelectedItem = current;
                    }

                    if (cmbNextAlg.SelectedItem == null && newValues.Count == 1)
                    {
                        cmbNextAlg.SelectedItem = newValues[0];
                    }
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
                    next.EventChange += NextEventChangeHandler;
                }

                HelperUI.ModifyMethod(cmbImpl, () =>
                {
                    cmbNextAlg.Visible = (next != null);
                    gbNextCall.Visible = (next != null);
                    if (next != null)
                    {
                        groupBoxMethod.Height = 125;
                        lblNext.Text = "Call to " + next.IdMethod;
                    }
                    else
                    {
                        groupBoxMethod.Height = 85;
                    }


                });
            }
        }

        private int level = 0;
        public int Level
        {
            get { return level; }
            set
            {
                level = value;
                HelperUI.ModifyMethod(cmbNextAlg, () => { this.lbName.Text = "Method" + level; });
            }
        }


        public ECallNext CallNext
        {
            get => HelperUI.GetMethod(cmbNextAlg, () => { return (ECallNext)cmbNextAlg.SelectedIndex; });
            set => HelperUI.ModifyMethod(cmbNextAlg, () => { cmbNextAlg.SelectedIndex = (int)value; });
        }


        public EStrategyDoIndependentWork StrategyDoIndependentWork
        {
            get => HelperUI.GetMethod(cmbNextAlg, () => { return (EStrategyDoIndependentWork)cmbMyImpl.SelectedIndex; });
            set => HelperUI.ModifyMethod(cmbNextAlg, () => { cmbMyImpl.SelectedIndex = (int)value; });
        }

        public ETypeImpl TypeImpl
        {
            get => HelperUI.GetMethod(cmbNextAlg, () => { return (ETypeImpl)cmbImpl.SelectedIndex; });
            set => HelperUI.ModifyMethod(cmbNextAlg, () => { cmbImpl.SelectedIndex = (int)value; });
        }
        public EventNextMethodWasChanged EventChange { get; set; }
        public EventNextMethodWasChanged NextEventChange { get; set; }

        public string IdMethod => HelperUI.GetMethod(lbName, () => { return (string)lbName.Text; });

        public IGetBase Implementation { get; set; }


        private void SetLabelUnit()
        {
            HelperUI.ModifyMethod(lblTrack, () =>
            {
                var selected = (ETypeDoIndependentWork)cmb.SelectedIndex;
                lblTrack.Text = this.trackMethod.Value * selected.Factor() + " " + selected.Unit();
            });
        }

        private void UseMethod_Load(object sender, EventArgs e)
        {

        }

        public string ValidateConfigurations()
        {
            HelperUI.GetMethod(this, () =>
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
            });
            return null;
        }

        private void cmbNextAlg_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbMyImpl_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
