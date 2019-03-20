using System;
using System.Windows.Forms;

namespace UIHowToWorkAsync
{
    public static class HelperUI
    {
        public delegate void MethodSetDelegate(Control control, MethodAux v);
        public delegate T MethodGetDelegate<T>(Control control, MethodGetAux<T> v);
        public delegate void MethodAux();
        public delegate T MethodGetAux<T>();

        public static void ModifyMethod(Control control, MethodAux v)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(new MethodSetDelegate(ModifyMethod), control, v);
            }
            v.Invoke();
        }

        public static T GetMethod<T>(Control control, HelperUI.MethodGetAux<T> v)
        {
            if (control.InvokeRequired)
            {
                return (T)control.Invoke(new HelperUI.MethodGetDelegate<T>(GetMethod<T>), control, v);
            }
            return v.Invoke();
        }


    }

    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmConfigurador());
            Application.Run(new FormAsync());
        }


    }
}
