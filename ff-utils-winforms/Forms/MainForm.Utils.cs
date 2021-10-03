﻿using Nmkoder.Extensions;
using Nmkoder.GuiHelpers;
using Nmkoder.IO;
using Nmkoder.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAPICodePack.Taskbar;
using ImageMagick;
using Nmkoder.UI.Tasks;
using Nmkoder.Main;
using Nmkoder.Data;
using Nmkoder.Data.Ui;

namespace Nmkoder.Forms
{
    partial class MainForm
    {
        private RunTask.TaskType currentTask;

        public RunTask.TaskType GetUtilsTaskType ()
        {
            return currentTask;
        }

        private void SelectReadBitrates(object sender, EventArgs e)
        {
            currentTask = RunTask.TaskType.UtilReadBitrates;
            UpdatePanels();
        }

        private void SelectGetMetrics(object sender, EventArgs e)
        {
            currentTask = RunTask.TaskType.UtilGetMetrics;
            UpdatePanels();
        }

        private void utilsMetricsConfBtn_Click(object sender, EventArgs e)
        {
            Utils.UtilsMetricsForm form = new Utils.UtilsMetricsForm(UtilGetMetrics.runVmaf, false, false);
            form.ShowDialog();
            UtilGetMetrics.runVmaf = form.CheckedBoxes[0];
            UtilGetMetrics.runSsim = form.CheckedBoxes[1];
            UtilGetMetrics.runPsnr = form.CheckedBoxes[2];
            UtilGetMetrics.vidLq = form.VideoLq;
            UtilGetMetrics.vidHq = form.VideoHq;
        }

        private void UpdatePanels ()
        {
            utilsBitratesPanel.BorderStyle = (currentTask == RunTask.TaskType.UtilReadBitrates) ? BorderStyle.FixedSingle : BorderStyle.None;
            utilsMetricsPanel.BorderStyle = (currentTask == RunTask.TaskType.UtilGetMetrics) ? BorderStyle.FixedSingle : BorderStyle.None;
        }
    }
}
