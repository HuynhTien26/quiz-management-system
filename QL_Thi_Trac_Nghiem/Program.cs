using System;
using System.Windows.Forms;
using QL_Thi_Trac_Nghiem.GUI;

namespace QL_Thi_Trac_Nghiem
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new FormDangNhap()); // Dòng đã sửa
        }
    }

    
}