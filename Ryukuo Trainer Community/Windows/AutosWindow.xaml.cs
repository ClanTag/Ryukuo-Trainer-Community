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
            int lParam = (MapVirtualKey(key, 0) << 16) + 2;
            PostMessage(hwnd, WM_KEYDOWN, key, lParam);
            PostMessage(hwnd, WM_KEYUP, key, lParam);
        }

        private int delay = 150;
        private bool bAutoAttack = false;
        private bool truAutoAttack = false;
        private int uAutoAttack = 250;
        
        private bool bAutoLoot = false;
        private bool truLoot = false;
        private int uAutoLoot = 50;

		private bool bAutoSkillOne = false;
        private int uAutoSkillOne = 170000;

        private bool bAutoSkillTwo = false;
        private int uAutoSkillTwo = 170000;

        private bool bAutoSkillThree = false;
        private int uAutoSkillThree = 170000;

        private bool bAutoSkillFour = false;
        private int uAutoSkillFour = 170000;

        private bool bAutoSkillFive = false;
        private int uAutoSkillFive = 170000;



        private void verifyAttack()
        {
          while (truAutoAttack)
            {
                Thread.Yield();
                new Thread(automaticAttack).Start();
                Thread.Sleep(6000);
            }

        }


        private void verifyLoot()
        {
            while (truLoot)
            {
                Thread.Yield();
                new Thread(automaticLoot).Start();
                Thread.Sleep(6000);
            }

        }
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
		
		  private void automaticSkillOne()
        {
            while (bAutoSkillOne)
            {

                bAutoAttack = false;
                bAutoLoot = false;
                Thread.Sleep(1234);
                OnKey(0x31); //VK_one
                Thread.Sleep(delay);
                OnKey(0x31); //VK_one
                Thread.Sleep(delay);
                OnKey(0x31); //VK_one
                Thread.Sleep(delay);
                OnKey(0x31); //VK_one
                bAutoAttack = true;
               Thread.Sleep(uAutoSkillOne);
                
            }
        }

        private void automaticSkillTwo()
        {
            while (bAutoSkillTwo)
            {
                bAutoAttack = false;
                bAutoLoot = false;
                Thread.Sleep(2234);
                OnKey(0x32); //VK_one
                Thread.Sleep(delay);
                OnKey(0x32); //VK_one
                Thread.Sleep(delay);
                OnKey(0x32); //VK_one
                Thread.Sleep(delay);
                OnKey(0x32); //VK_one
                bAutoAttack = true;
                Thread.Sleep(uAutoSkillTwo);
            }
        }

        private void automaticSkillThree()
        {
            while (bAutoSkillThree)
            {
                bAutoAttack = false;
                bAutoLoot = false;
                Thread.Sleep(3234);
                OnKey(0x33); //VK_one
                Thread.Sleep(delay);
                OnKey(0x33); //VK_one
                Thread.Sleep(delay);
                OnKey(0x33); //VK_one
                Thread.Sleep(delay);
                OnKey(0x33); //VK_one
                Thread.Sleep(uAutoSkillThree);
            }
        }

        private void automaticSkillFour()
        {
            while (bAutoSkillFour)
            {
                bAutoAttack = (false);
                bAutoLoot = false;
                Thread.Sleep(4234);
                OnKey(0x34); //VK_one
                Thread.Sleep(delay);
                OnKey(0x34); //VK_one
                Thread.Sleep(delay);
                OnKey(0x34); //VK_one
                Thread.Sleep(delay);
                OnKey(0x34); //VK_one
                bAutoAttack = (true);
                Thread.Sleep(uAutoSkillFour);
            }
        }

        private void automaticSkillFive()
        {
            while (bAutoSkillFive)
            {
                bAutoAttack = false;
                bAutoLoot = false;
                Thread.Sleep(5234);
                OnKey(0x35); //VK_one
                Thread.Sleep(delay);
                OnKey(0x35); //VK_one
                Thread.Sleep(delay);
                OnKey(0x35); //VK_one
                Thread.Sleep(delay);
                OnKey(0x35); //VK_one
                Thread.Sleep(uAutoSkillFive);
            }
        }
        public void EnableHacks()
        {

                            
                if (autoSkillOneCheckBox.IsChecked == true)
                {
                    uAutoSkillOne = Int32.Parse(autoSkillOneTextBox.Text);
                    bAutoSkillOne = true;

                    new Thread(automaticSkillOne).Start();
                }

                if (autoSkillTwoCheckBox.IsChecked == true)
                {
                    uAutoSkillTwo = Int32.Parse(autoSkillTwoTextBox.Text);
                    bAutoSkillTwo = true;

                    new Thread(automaticSkillTwo).Start();
                }

                if (autoSkillThreeCheckBox.IsChecked == true)
                {
                    uAutoSkillThree = Int32.Parse(autoSkillThreeTextBox.Text);
                    bAutoSkillThree = true;

                    new Thread(automaticSkillThree).Start();
                }

                if (autoSkillFourCheckBox.IsChecked == true)
                {
                    uAutoSkillFour = Int32.Parse(autoSkillFourTextBox.Text);
                    bAutoSkillFour = true;

                    new Thread(automaticSkillFour).Start();
                }

                if (autoSkillFiveCheckBox.IsChecked == true)
                {
                    uAutoSkillFive = Int32.Parse(autoSkillFiveTextBox.Text);
                    bAutoSkillFive = true;

                    new Thread(automaticSkillFive).Start();
                }

           
                if (autoAttackCheckBox.IsChecked == true)
                {
                    uAutoAttack = Int32.Parse(autoAttackTextBox.Text);
                    truAutoAttack = true;
                    new Thread(verifyAttack).Start();
                }


                if (autoLootCheckBox.IsChecked == true)
                {
                    uAutoLoot = Int32.Parse(autoLootTextBox.Text);
                    truLoot = true;
                    new Thread(verifyLoot).Start();
                }
                
            

        }


        public void DisableHacks()
        {
            bAutoAttack = false;
            bAutoLoot = false;
            bAutoSkillOne = false;
            bAutoSkillTwo = false;
            bAutoSkillThree = false;
            bAutoSkillFour = false;
            bAutoSkillFive = false;
        }

    }
}
