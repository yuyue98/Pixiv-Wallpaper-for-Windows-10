﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Pixiv_Wallpaper_WinUI.Model;
using Pixiv_Wallpaper_WinUI.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Pixiv_Wallpaper_WinUI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        private StorageFolder folder = ApplicationData.Current.LocalFolder;
        private Conf c = new Conf();
        private ResourceLoader loader = ResourceLoader.GetForCurrentView("Resources");
        public SettingPage()
        {
            this.InitializeComponent();

            //下拉框初始化   多语言适配
            timeSet.Items.Add(new KeyValuePair<string, int>(loader.GetString("15Mins"), 15));
            timeSet.Items.Add(new KeyValuePair<string, int>(loader.GetString("30Mins"), 30));
            timeSet.Items.Add(new KeyValuePair<string, int>(loader.GetString("60Mins"), 60));
            timeSet.Items.Add(new KeyValuePair<string, int>(loader.GetString("120Mins"), 120));
            timeSet.Items.Add(new KeyValuePair<string, int>(loader.GetString("180Mins"), 180));

            combox2.Items.Add(new KeyValuePair<string, string>(loader.GetString("ExtendedSession"), "ExtendedSession"));
            combox2.Items.Add(new KeyValuePair<string, string>(loader.GetString("BackgroundTask"), "BackgroundTask"));

            rankingMode.Items.Add(new KeyValuePair<string, string>(loader.GetString("Dayly"), "day"));
            rankingMode.Items.Add(new KeyValuePair<string, string>(loader.GetString("Weekly"), "week"));
            rankingMode.Items.Add(new KeyValuePair<string, string>(loader.GetString("Monthly"), "month"));

            themeSelect.Items.Add(new KeyValuePair<string, string>(loader.GetString("Dark"), "dark"));
            themeSelect.Items.Add(new KeyValuePair<string, string>(loader.GetString("Light"), "light"));

            CalcutateCacheSize();
            timeSet.SelectedValue = c.time;
            rankingMode.SelectedValue = c.rankingMode;
            themeSelect.SelectedValue = c.themeSelect;
            combox2.SelectedValue = c.backgroundMode;
            lock_switch.IsOn = c.lockscr;
            switch (c.mode)
            {
                case "Bookmark":
                    radiobutton1.IsChecked = true;
                    break;
                case "FollowIllust":
                    radiobutton2.IsChecked = true;
                    break;
                case "Recommendation":
                    radiobutton3.IsChecked = true;
                    break;
                case "Ranking":
                    radiobutton4.IsChecked = true;
                    break;
                default:
                    radiobutton1.IsChecked = true;
                    break;
            }
        }

        private async void openFilePath_Click(object sender, RoutedEventArgs e)
        {
            var t = new FolderLauncherOptions();
            await Launcher.LaunchFolderAsync(folder, t);
        }

        private async void clearPicture_Click(object sender, RoutedEventArgs e)
        {
            foreach (StorageFile f in await folder.GetItemsAsync())
            {
                if (!f.Name.Equals(c.lastImg.imgId + '.' + c.lastImg.format))
                {
                    await f.DeleteAsync();
                }
            }
            CalcutateCacheSize();
        }

        private void radiobutton2_Checked(object sender, RoutedEventArgs e)
        {
            c.mode = "FollowIllust";
            rankingMode.IsEnabled = false;
        }

        private void radiobutton3_Checked(object sender, RoutedEventArgs e)
        {
            c.mode = "Recommendation";
            rankingMode.IsEnabled = false;
        }

        private async Task<long> GetFolderSizeAsync()
        {
            var getFileSizeTasks = from file
                                   in await folder.CreateFileQuery().GetFilesAsync()
                                   select file.GetBasicPropertiesAsync().AsTask();
            var fileSizes = await Task.WhenAll(getFileSizeTasks);
            return fileSizes.Sum(i => (long)i.Size);
        }

        private async Task CalcutateCacheSize()
        {
            long current = await GetFolderSizeAsync();
            decimal sizeInMB = new decimal(current) / new decimal(1048576);
            cacheSize.Text = decimal.Round(sizeInMB, 2).ToString() + "MB";
        }

        private void combox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c.time = (int)timeSet.SelectedValue;
        }

        private void radiobutton1_Checked(object sender, RoutedEventArgs e)
        {
            c.mode = "Bookmark";
            rankingMode.IsEnabled = false;
        }

        private void combox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c.backgroundMode = combox2.SelectedValue.ToString();
        }

        private void lock_switch_Toggled(object sender, RoutedEventArgs e)
        {
            c.lockscr = lock_switch.IsOn;
        }

        private void reLogin_Click(object sender, RoutedEventArgs e)
        {
            Frame root = Window.Current.Content as Frame;
            c.RefreshToken = "Invalidation";
            root.Navigate(typeof(WebViewLogin));
        }

        private void radiobutton4_Checked(object sender, RoutedEventArgs e)
        {
            c.mode = "Ranking";
            rankingMode.IsEnabled = true;
        }

        private void rankingMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c.rankingMode = rankingMode.SelectedValue.ToString();
        }

        private void themeSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            c.themeSelect = themeSelect.SelectedValue.ToString();
        }
    }
}