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
            current = Write(parent, callNext + " " + literal + "     (" + parameter.TypeNextImpl + ")", colorNext);

            colorNext = color;
            var current2 = current;

            string nextImp, getNextSting, todo;
            nextImp = getNextSting = "";

            if (parameter.Implementation is IGetStringIn2Phases)
            {
                nextImp = ((IGetStringIn2Phases)parameter.Implementation).CallNextDescription();
                getNextSting = ((IGetStringIn2Phases)parameter.Implementation).HowToGetResultNextDescription();
            }
            todo = parameter.Implementation.MyWorkDescription();


            if (parameter.Next != null)
            {
                callNext = @"VAR X" + parameter.Level + " = " + nextImp;
                Print(parameter.Next);
                Write(current2, @"VAR Y" + parameter.Level + " = " + todo, color);
                Write(current2, @"Return X" + parameter.Level + getNextSting + " + Y" + parameter.Level, color);
            }
            else
            {
                Write(current2, @"Return " + todo, color);
            }
        }

    }
}
