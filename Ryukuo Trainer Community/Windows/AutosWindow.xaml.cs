/*
Copyright 2016 Ryukuo Trainer Developers

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

    http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/
using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Interop;

namespace Ryukuo_Trainer_Community.Windows
{
    /// <summary>
    /// Interaction logic for AutosWindow.xaml
    /// </summary>
    public partial class AutosWindow : Window
    {

        private MainWindow mainWindow;
        public AutosWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        private void autoWindow_Loaded(object sender, RoutedEventArgs e)
        {
            IntPtr hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }

        [DllImport("user32.dll", EntryPoint= "PostMessageW")]
        private static extern bool PostMessage(int hwnd, int wMsg, int wParam, int lParam);

        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private static extern int FindWindow(string lpClassName, string lpWindowName);
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        [DllImport("user32.dll")]
        private static extern int MapVirtualKey(uint uCode, uint uMapType);

        public void OnKey(byte key)
        {
            int hwnd = FindWindow("MapleStoryClass", null);
            int lParam = (MapVirtualKey(key, 0) << 16) + 1;
            PostMessage(hwnd, WM_KEYDOWN, key, lParam);
            PostMessage(hwnd, WM_KEYUP, key, lParam);
        }

        private bool bAutoAttack = false;
        private int uAutoAttack = 250;

        private bool bAutoLoot = false;
        private int uAutoLoot = 50;



        private void automaticAttack()
        {
            while (bAutoAttack)
            {
                OnKey(0x11); //VK_CONTROL
                Thread.Sleep(uAutoAttack);
            }
        }

        private void automaticLoot()
        {
            while (bAutoLoot)
            {
                OnKey(0x60); //VK_NUMPAD0
                Thread.Sleep(uAutoLoot);
            }
        }


        public void EnableHacks()
        {
            if (autoAttackCheckBox.IsChecked == true)
            {
                uAutoAttack = Int32.Parse(autoAttackTextBox.Text);
                bAutoAttack = true;

                new Thread(automaticAttack).Start();
            }


            if (autoLootCheckBox.IsChecked == true)
            {
                uAutoLoot = Int32.Parse(autoLootTextBox.Text);
                bAutoLoot = true;

                new Thread(automaticLoot).Start();
            }

        }


        public void DisableHacks()
        {
            bAutoAttack = false;
            bAutoLoot = false;
        }

    }
}
