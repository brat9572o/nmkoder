﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageMagick;
using Nmkoder.UI.Tasks;
using Nmkoder.Main;
using Nmkoder.Data;
using Nmkoder.Data.Ui;
using Nmkoder.Properties;
using System;
using Nmkoder.UI;

namespace Nmkoder.Forms
{
    partial class MainForm
    {
        public void RefreshFileListUi ()
        {
            bool mfm = RunTask.currentFileListMode == RunTask.FileListMode.MultiFileInput;

            addTracksFromFileBtn.Visible = mfm && (MediaFile)fileList.SelectedItem != null;
            addTracksFromFileBtn.Text = AreAnyTracksLoaded() ? "Add Tracks To List" : "Load File";
        }

        private async void fileListMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            RunTask.FileListMode oldMode = RunTask.currentFileListMode;
            RunTask.FileListMode newMode = (RunTask.FileListMode)fileListMode.SelectedIndex;

            if (oldMode == RunTask.FileListMode.MultiFileInput && newMode == RunTask.FileListMode.BatchProcess)
            {
                ClearCurrentFile();
            }

            RunTask.currentFileListMode = newMode;

            Text = $"NMKODER [{(RunTask.currentFileListMode == RunTask.FileListMode.MultiFileInput ? "MFM" : "BPM")}]";

            SaveUiConfig();
            RefreshFileListUi();

            if (oldMode == RunTask.FileListMode.BatchProcess && newMode == RunTask.FileListMode.MultiFileInput)
            {
                if (fileList.Items.Count == 1 && !AreAnyTracksLoaded())
                    await MediaInfo.LoadFirstFile(((MediaFile)fileList.Items[0]).File.FullName);
            }
        }

        private async void addTracksFromFileBtn_Click(object sender, EventArgs e)
        {
            addTracksFromFileBtn.Enabled = false;

            if (AreAnyTracksLoaded())
                await MediaInfo.AddStreamsToList((MediaFile)fileList.SelectedItem, true);
            else
                await MediaInfo.LoadFirstFile(((MediaFile)fileList.SelectedItem).File.FullName);

            addTracksFromFileBtn.Enabled = true;
        }

        private void fileList_SelectedIndexChanged(object sender = null, EventArgs e = null)
        {
            RefreshFileListUi();
        }

        private void fileListCleanBtn_Click(object sender, EventArgs e)
        {
            fileList.Items.RemoveAt(fileList.SelectedIndex);
        }

        private void fileListMoveUpBtn_Click(object sender, EventArgs e)
        {

        }

        private void fileListMoveDownBtn_Click(object sender, EventArgs e)
        {

        }

        private bool AreAnyTracksLoaded ()
        {
            return streamList.Items.Count > 0;
        }
    }
}
