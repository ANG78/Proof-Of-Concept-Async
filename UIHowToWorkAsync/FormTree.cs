using HowToWorkAsync;
using System.Threading.Tasks;
using System.Windows.Forms;
using UIHowToWorkAsync;

namespace UIConclusionesAsync
{
    public partial class FormTree : Form
    {
        public FormTree()
        {
            InitializeComponent();
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            tree.ExpandAll();
        }

        public void Print(IUseMethod Method)
        {

            HelperUI.ModifyMethod(tree, () =>
            {
                PrintMethods printer = new PrintMethods(tree);
                printer.Print(Method);



                tree.ExpandAll();
                ShowDialog();
            });

        }

        private void Expand(TreeNode nodeAux)
        {

            if (nodeAux != null)
            {

            }


            foreach (TreeNode node in nodeAux.Nodes)
            {
                Expand(node);
            }

        }

    }




}
