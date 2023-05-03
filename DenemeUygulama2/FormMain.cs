using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DenemeUygulama2
{
    public partial class FormMain : System.Windows.Forms.Form
    {
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        

        public FormMain()
        {   
            InitializeComponent();
            random= new Random();
            btnCloseChildForm.Visible = false;
            this.Text = String.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);






        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
               index =random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color); ;
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                    panelUst.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SeconderyColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    btnCloseChildForm.Visible = true;
                }
            }
        }
         
        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(51, 51, 76);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
                }
            }
        }

        private void OpenChildFrom (Form childFrom, object btnSender)
        {
            if (activeForm  !=null)
            {
                activeForm.Close();
            }
            ActivateButton(btnSender);
            activeForm = childFrom;
            childFrom.TopLevel = false;
            childFrom.FormBorderStyle = FormBorderStyle.None;
            childFrom.Dock=DockStyle.Fill;
            this.panelAnaBolum.Controls.Add(childFrom);
            this.panelAnaBolum.Tag = childFrom;
            childFrom.BringToFront();
            childFrom.Show();
            lblBaslik.Text = childFrom.Text;


        }



        private void btnProducts_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new Forms.FormProducts(), sender);
        }

        private void btnOrders_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new Forms.FormOrders(), sender);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new Forms.FormCustomer(), sender);
        }

        private void btnReporting_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new Forms.FormReporting(), sender);
        }

        private void btnNortifications_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new Forms.FormNotifications(), sender);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            OpenChildFrom(new Forms.FormSetting(), sender);
        }

        private void btnCloseChildForm_Click(object sender, EventArgs e)
        {
            if (activeForm != null) 
                activeForm.Close();
            Reset();
                
        }
        private void Reset()
        {
            DisableButton();
            lblBaslik.Text = "HOME";
            panelUst.BackColor = Color.FromArgb(0,150,136);
            panelLogo   .BackColor = Color.FromArgb(39,39,58);
            currentButton = null;
            btnCloseChildForm.Visible = false;
        }

        private void panelUst_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if(WindowState==FormWindowState.Normal)
                this.WindowState=FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState=(FormWindowState)FormWindowState.Normal;
        }
    }
}
