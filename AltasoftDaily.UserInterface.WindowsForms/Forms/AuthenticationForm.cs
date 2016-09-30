﻿using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AltasoftDaily.Core;
using AltasoftDaily.Domain.POCO;
using System.Reflection;
using AltasoftDaily.UserInterface.WindowsForms.Controls;

namespace AltasoftDaily.UserInterface.WindowsForms
{
    public partial class AuthenticationForm : MetroForm
    {
        private User _user { get; set; }
        private int _deptId { get; set; }

        public AuthenticationForm()
        {
            InitializeComponent();

            tbxUsername.Text = Properties.Settings.Default.Username;
            cbxDept.SelectedIndex = Properties.Settings.Default.SelectedBranch;

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void ShowLoading()
        {
            loadingControl1.Visible = true;
            foreach (Control item in this.Controls)
            {
                if (item is LoadingControl)
                    continue;
                item.Enabled = false;
            }
            loadingControl1.Refresh();
        }

        private void HideLoading()
        {
            loadingControl1.Visible = false;
            foreach (Control item in this.Controls)
            {
                if (item is LoadingControl)
                    continue;
                item.Enabled = true;
            }
            loadingControl1.Refresh();
        }

        private async void btnEnter_Click(object sender, EventArgs e)
        {
            ShowLoading();

            await Task.Run(() =>
            {
                try
                {
                    bool authenticated;
                    string response;
                    _user = UserManagement.Authenticate(tbxUsername.Text, tbxPassword.Text, cbxDept.SelectedIndex, out authenticated, out response);

                    if (!authenticated)
                    {
                        MessageBox.Show(response);

                        return;
                    }
                    LoggingManagement.LogSign(SignType.SignIn, _user);

                    Properties.Settings.Default.Username = tbxUsername.Text;
                    Properties.Settings.Default.SelectedBranch = cbxDept.SelectedIndex;
                    Properties.Settings.Default.Save();

                    Close();
                }
                catch (Exception ex)
                {
                    LoggingManagement.LogException(ex, null);
                    throw;
                }
            });

            HideLoading();
        }

        public User GetUser()
        {
            return _user;
        }
        public int GetDeptId()
        {
            return _deptId;
        }

        private void AuthenticationForm_Load(object sender, EventArgs e)
        {

        }

        private void cbxDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            _deptId = cbxDept.SelectedIndex;
        }
    }
}
