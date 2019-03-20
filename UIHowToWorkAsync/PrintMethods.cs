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

            string nextImp = parameter.Implementation.CallNextDescription();
            string getNextSting = parameter.Implementation.HowToGetResultNextDescription();

            if (parameter.Next != null)
            {

                callNext = @"VAR X" + parameter.Level + " = " + nextImp;
                Print(parameter.Next);
                Write(current2, @"VAR Y" + parameter.Level + " = MyWork()", color);
                Write(current2, @"Return X" + parameter.Level + " + Y" + parameter.Level, color);


            }
            else
            {
                Write(current2, @"Return " + nextImp, color);
            }
        }

    }
}
