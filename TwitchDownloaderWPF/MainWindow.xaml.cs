﻿using AutoUpdaterDotNET;
using System;
using System.IO;
using System.Net;
using System.Windows;
using TwitchDownloaderWPF.Properties;
using Xabe.FFmpeg.Downloader;
using static TwitchDownloaderWPF.App;

namespace TwitchDownloaderWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static PageVodDownload pageVodDownload = new PageVodDownload();
        public static PageClipDownload pageClipDownload = new PageClipDownload();
        public static PageChatDownload pageChatDownload = new PageChatDownload();
        public static PageChatUpdate pageChatUpdate = new PageChatUpdate();
        public static PageChatRender pageChatRender = new PageChatRender();
        public static PageQueue pageQueue = new PageQueue();

        public MainWindow()
        {
            InitializeComponent();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private void btnVodDownload_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = pageVodDownload;
        }

        private void btnClipDownload_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = pageClipDownload;
        }

        private void btnChatDownload_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = pageChatDownload;
        }

        private void btnChatUpdate_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = pageChatUpdate;
        }

        private void btnChatRender_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = pageChatRender;
        }

        private void btnQueue_Click(object sender, RoutedEventArgs e)
        {
            Main.Content = pageQueue;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AppSingleton.RequestAppThemeChange();

            Main.Content = pageVodDownload;
            if (Settings.Default.UpgradeRequired)
            {
                Settings.Default.Upgrade();
                Settings.Default.UpgradeRequired = false;
                Settings.Default.Save();
            }

            if (!File.Exists("ffmpeg.exe"))
                await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Full);

            Version currentVersion = new Version("1.52.4");
            Title = $"Twitch Downloader v{currentVersion}";
            AutoUpdater.InstalledVersion = currentVersion;
#if !DEBUG
            AutoUpdater.RunUpdateAsAdmin = false;
            AutoUpdater.Start("https://downloader-update.twitcharchives.workers.dev");
#endif
        }
    }
}
