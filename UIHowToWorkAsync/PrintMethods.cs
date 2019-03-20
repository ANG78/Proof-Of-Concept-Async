using System;
using System.Collections.Generic;
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

        Color[] colors = new Color[] { Color.Brown, Color.Blue, Color.Green, Color.Red, Color.Violet };

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
            string literal = parameter.IdMethod;
            var color = colors[parameter.Level % colors.Length];

            if (parameter.Level == 0)
                colorNext = color;

            TreeNode parent = current ?? tree.Nodes[tree.Nodes.Count - 1];
            current = Write(parent, callNext + literal + "     (" + parameter.TypeImplementation + ")", colorNext);

            colorNext = color;
            var current2 = current;


            if (parameter.Next != null)
            {


                switch (parameter.TypeImplementation)
                {
                    case ETypeImpl.ASYNC:
                        {

                            string nextImp = "";
                            if (parameter.Next.TypeImplementation == ETypeImpl.ASYNC)
                            {
                                nextImp = "";
                            }
                            else
                            {
                                nextImp = "RUN.TASK ";
                            }

                            switch (parameter.CallNext)
                            {
                                case ECallNext.WAIT_FIRST:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = WAIT " + nextImp;
                                        Print(parameter.Next);
                                        Write(current2, @"VAR Y" + parameter.Level + " = TODO " + literal, color);
                                        Write(current2, @"Return X" + parameter.Level + " + Y" + parameter.Level, color);
                                    }
                                    break;
                                case ECallNext.WAIT_AFTER:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = " + nextImp;
                                        Print(parameter.Next);
                                        Write(current2, @"VAR Y" + parameter.Level + " = TODO " + literal, color);
                                        Write(current2, @"WAIT X" + parameter.Level, color);
                                        Write(current2, @"Return X" + parameter.Level + " + Y" + parameter.Level, color);
                                    }
                                    break;
                                case ECallNext.AWAITER_AFTER:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = " + nextImp;
                                        Print(parameter.Next);
                                        current2.Nodes.Add(@"VAR Y" + parameter.Level + " = TODO " + literal);
                                        current2.Nodes.Add(@"X" + parameter.Level + ".AWAITER.GETRESULT");
                                        current2.Nodes.Add(@"Return X" + parameter.Level + " + Y" + parameter.Level);
                                    }
                                    break;
                                case ECallNext.NOT_WAIT:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = " + nextImp;
                                        Print(parameter.Next);
                                        current2.Nodes.Add(@"VAR Y" + parameter.Level + " = TODO " + literal);
                                        current2.Nodes.Add(@"Return X" + parameter.Level + " + Y" + parameter.Level);

                                    }
                                    break;
                            }
                            break;
                        }

                    case ETypeImpl.SYNC:
                        {
                            switch (parameter.CallNext)
                            {
                                case ECallNext.WAIT_FIRST:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = WAIT ";
                                        Print(parameter.Next);
                                        Write(current2, @"VAR Y" + parameter.Level + " = TODO " + literal, color);
                                        Write(current2, @"Return X" + parameter.Level + " + Y" + parameter.Level, color);
                                    }
                                    break;
                                case ECallNext.WAIT_AFTER:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = ";
                                        Print(parameter.Next);
                                        Write(current2, @"VAR Y" + parameter.Level + " = TODO " + literal, color);
                                        Write(current2, @"WAIT X" + parameter.Level, color);
                                        Write(current2, @"Return X" + parameter.Level + " + Y" + parameter.Level, color);
                                    }
                                    break;
                                case ECallNext.AWAITER_AFTER:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = ";
                                        Print(parameter.Next);
                                        current2.Nodes.Add(@"VAR Y" + parameter.Level + " = TODO " + literal);
                                        current2.Nodes.Add(@"X" + parameter.Level + ".AWAITER.GETRESULT");
                                        current2.Nodes.Add(@"Return X" + parameter.Level + " + Y" + parameter.Level);
                                    }
                                    break;
                                case ECallNext.NOT_WAIT:
                                    {
                                        callNext = @"VAR X" + parameter.Level + " = ";
                                        Print(parameter.Next);
                                        current2.Nodes.Add(@"VAR Y" + parameter.Level + " = TODO " + literal);
                                        current2.Nodes.Add(@"Return X" + parameter.Level + " + Y" + parameter.Level);

                                    }
                                    break;
                            }
                            break;
                        }

                }

            }
            else
            {

                if (parameter.TypeImplementation == ETypeImpl.SYNC)
                {
                    Write(current2, @"RETURN TODO " + literal, color);

                }
                else
                {
                    switch (parameter.CallNext)
                    {
                        case ECallNext.WAIT_FIRST:
                        case ECallNext.WAIT_AFTER:
                            Write(current2, @"RETURN WAIT RUN.TASK (TODO" + literal + ")", color);
                            break;
                        case ECallNext.AWAITER_AFTER:
                            Write(current2, @"RETURN {TODO" + literal + "}.AWAITER.GETRESULT", color);
                            break;
                        case ECallNext.NOT_WAIT:
                            Write(current2, @"RETURN {TODO" + literal + "}", color);
                            break;
                    }
                }
            }
        }
    }
}
