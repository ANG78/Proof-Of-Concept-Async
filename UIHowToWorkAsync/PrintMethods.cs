using System.Drawing;
using System.Windows.Forms;
using HowToWorkAsync;
namespace UIHowToWorkAsync
{
    public class PrintMethods
    {

        private TreeView tree;
        private TreeNode current = null;
        private string callNext = "";
        private Color colorNext = Color.Black;

        Color[] colors = new Color[] { Color.Brown, Color.Blue, Color.Green, Color.Red, Color.Violet, Color.Black };

        public PrintMethods(TreeView tr)
        {
            tree = tr;
            tree.Nodes.Clear();
            tree.Nodes.Add("-- RUN");
            tree.CollapseAll();
            tree.ExpandAll();
        }

        private TreeNode Write(TreeNode node, string message, Color color)
        {
            var nodeResult = node.Nodes.Add(message);
            nodeResult.ImageIndex = 0;
            nodeResult.ForeColor = (color);
            nodeResult.ExpandAll();
            return nodeResult;
        }

        public void Print(IUseMethod parameter)
        {
            if (parameter == null || parameter.Implementation == null)
                return;

            string literal = parameter.IdMethod;
            var color = colors[parameter.Level % colors.Length];

            if (parameter.Level == 0)
                colorNext = color;

            TreeNode parent = current ?? tree.Nodes[tree.Nodes.Count - 1];

            if (string.IsNullOrWhiteSpace(callNext))
            {
                current = Write(parent, literal, colorNext);
            }
            else
            {
                current = Write(parent, callNext, colorNext);
                var colorHeader = colors[parameter.Level % colors.Length];
                current = Write(current, literal, colorHeader);
            }
            
            
            colorNext = color;
            var current2 = current;

            string nextImp, getNextSting, todo;
            nextImp = getNextSting = "";

            if (parameter.Implementation is ICallNextDescription)
            {
                nextImp = ((ICallNextDescription)parameter.Implementation).PreDescription();
                getNextSting = ((ICallNextDescription)parameter.Implementation).PostDescription();
            }
            todo = parameter.Implementation.DoIndependetWork.Description();


            if (parameter.Next != null)
            {
                nextImp = nextImp.Replace("Next", parameter.Next.IdMethod);
                callNext = @"VAR X" + parameter.Level + " = " + nextImp;
                Print(parameter.Next);
                Write(current2, @"VAR Y" + parameter.Level + " = " + todo, color);
                getNextSting = getNextSting.Replace("Next", "X" + parameter.Level);
                Write(current2, @"Return " + getNextSting + " +  Y" + parameter.Level, color);
            }
            else
            {
                Write(current2, @"Return " + todo, color);
            }
        }

    }
}
