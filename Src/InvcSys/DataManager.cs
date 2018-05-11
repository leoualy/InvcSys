using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InvcSys
{
    internal class DataManager
    {
        static BlueViewModel mBlueViewModel;
        static HomeWindow mHomeWindow;
        internal static void RegistData(BlueViewModel model)
        {
            mBlueViewModel = model;
        }
        internal static void RegistHomeWindow(HomeWindow window)
        {
            mHomeWindow = window;
        }
        internal static void Update(int id, int window)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                if (!mHomeWindow.IsActive)
                {
                    mHomeWindow.Activate();
                }
                mBlueViewModel.UpdateData(id, window);
            });
        }


    }
}
