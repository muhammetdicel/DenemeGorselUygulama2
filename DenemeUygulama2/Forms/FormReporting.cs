﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenemeUygulama2.Forms
{
    public partial class FormReporting : Form
    {
        public FormReporting()
        {
            InitializeComponent();
        }

        private void FormReporting_Load(object sender, EventArgs e)
        {
            LoadTheme();
        }
        private void LoadTheme()
        {
            foreach (Control btns in this.Controls)
            {
                if (btns.GetType() == typeof(Button))
                {
                    Button btn = (Button)btns;
                    btn.BackColor = ThemeColor.PrimaryColor;
                    btn.ForeColor = Color.White;
                    btn.FlatAppearance.BorderColor = ThemeColor.SeconderyColor;
                }
            }
            label4.ForeColor = ThemeColor.SeconderyColor;
            label5.ForeColor = ThemeColor.PrimaryColor;
        }
    }
}
