using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PC_KAB_KLATEN_JOKO_SUPRIYANTO
{
    static class Program
    {
        static IMandhegParkingSystemDataContext context;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormLogin());
        }

        public static Form GetInstanceOf(Type form)
        {
            foreach (Form item in Application.OpenForms)
            {
                if (item.GetType() == form) return item;
            }
            return (Form)Activator.CreateInstance(form);
        }

        public static IMandhegParkingSystemDataContext GetContext()
        {
            if (context is null) return new MandhegParkingSystemDataContext();
            else return context;
        }
    }
}
