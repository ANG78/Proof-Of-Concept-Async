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

            foreach (var aux in Enum.GetValues(typeof(ETypeImpl)))
            {
                cmbMyImpl.Items.Add(aux);
            }

            foreach (var aux in Enum.GetValues(typeof(ETypeWork)))
            {
                cmb.Items.Add(aux);
            }

            foreach (var aux in Enum.GetValues(typeof(ETypeImpl)))
            {
                cmbNextImpl.Items.Add(aux);
            }

            SetValuesCall();

            SetLabelUnit();

            EventChangeTheLastMethod += EventNextNullHandler;

            MyImpl = ETypeImpl.SYNC;
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
            EventChange?.Invoke((ETypeImpl)cmbNextImpl.SelectedIndex);
            EventChangeTheLastMethod?.Invoke((ETypeImpl)cmbNextImpl.SelectedIndex);
        }

        private void groupBoxMethod_Enter(object sender, EventArgs e)
        {

        }




        public ETypeWork TypeWork
        {
            get
            {

                return HelperUI.GetMethod(cmb, () => { return (ETypeWork)cmb.SelectedIndex; });
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

                var newValues = newType.HowToBeCalled();

                var t2 = cmbNextAlg.SelectedItem;
                cmbNextAlg.Items.Clear();
                foreach (var aux in newValues)
                {
                    cmbNextAlg.Items.Add(aux);
                }

                if (t2 != null && cmbNextAlg.Items.Contains(t2))
                {
                    cmbNextAlg.SelectedItem = t2;
                }
            });

        }

        private void EventNextNullHandler(ETypeImpl newType)
        {
            HelperUI.ModifyMethod(cmbNextAlg, () =>
            {

                var newValues = newType.HowToBeCalled();

                var t2 = cmbNextAlg.SelectedItem;
                cmbNextAlg.Items.Clear();
                foreach (var aux in newValues)
                {
                    cmbNextAlg.Items.Add(aux);
                }

                if (t2 != null && cmbNextAlg.Items.Contains(t2))
                {
                    cmbNextAlg.SelectedItem = t2;
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
                HelperUI.ModifyMethod(cmbNextAlg, () => { this.groupBoxMethod.Text = "Method" + level; });
            }
        }


        public ECallNext CallNext
        {
            get => HelperUI.GetMethod(cmbNextAlg, () => { return (ECallNext)cmbNextAlg.SelectedIndex; });
            set => HelperUI.ModifyMethod(cmbNextAlg, () => { cmbNextAlg.SelectedIndex = (int)value; });
        }


        public ETypeImpl MyImpl
        {
            get => HelperUI.GetMethod(cmbNextAlg, () => { return (ETypeImpl)cmbMyImpl.SelectedIndex; });
            set => HelperUI.ModifyMethod(cmbNextAlg, () => { cmbMyImpl.SelectedIndex = (int)value; });
        }
        
        public ETypeImpl TypeNextImpl
        {
            get => HelperUI.GetMethod(cmbNextAlg, () => { return (ETypeImpl)cmbNextImpl.SelectedIndex; });
            set => HelperUI.ModifyMethod(cmbNextAlg, () => { cmbNextImpl.SelectedIndex = (int)value; });
        }
        public EventNextMethodWasChanged EventChange { get; set; }
        public EventNextMethodWasChanged EventChangeTheLastMethod { get; set; }

        public string IdMethod => HelperUI.GetMethod(groupBoxMethod, () => { return (string)groupBoxMethod.Text; });

        public IGetBase Implementation { get; set; }
        

        private void SetLabelUnit()
        {
            HelperUI.ModifyMethod(lblTrack, () =>
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
    }
}
