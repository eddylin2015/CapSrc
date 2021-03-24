using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace CapSrc
{
    public partial class Form1 : Form
    {
        HotKeyRegister hotKeyToRegister = null;

        Keys registerKey = Keys.None;

        KeyModifiers registerModifiers = KeyModifiers.None;

        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            cap();
        }
        private static Bitmap bmpScreenshot;
        private static Graphics gfxScreenshot;
        private static string dir = @"d:\";
        void cap()
        {
            this.Hide();
            // Set the bitmap object to the size of the screen
            bmpScreenshot = new Bitmap(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height, PixelFormat.Format32bppArgb);
            // Create a graphics object from the bitmap
            gfxScreenshot = Graphics.FromImage(bmpScreenshot);
            // Take the screenshot from the upper left corner to the right bottom corner
            gfxScreenshot.CopyFromScreen(Screen.PrimaryScreen.Bounds.X, Screen.PrimaryScreen.Bounds.Y, 0, 0, Screen.PrimaryScreen.Bounds.Size, CopyPixelOperation.SourceCopy);
            // Save the screenshot to the specified path that the user has chosen
            
            bmpScreenshot.Save(dir + DateTime.Now.ToString("yyyyMMddhhmmss") + ".png", ImageFormat.Png);
            // Show the form again
            this.Show();
        }
     
        void HotKeyPressed(object sender, EventArgs e)
        {
            cap();
            /*
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.WindowState = FormWindowState.Normal;
            }
            this.Activate();
             * */
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            try
            {
                // Register the hotkey.
                this.registerKey = Keys.F12;
                this.registerModifiers = KeyModifiers.Alt;
                hotKeyToRegister = new HotKeyRegister(this.Handle, 100,
                    this.registerModifiers, this.registerKey);

                // Register the HotKeyPressed event.
                hotKeyToRegister.HotKeyPressed += new EventHandler(HotKeyPressed);

            }
            catch (ArgumentException argumentException)
            {
                MessageBox.Show(argumentException.Message);
            }
            catch (ApplicationException applicationException)
            {
                MessageBox.Show(applicationException.Message);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dispose the hotKeyToRegister.
            if (hotKeyToRegister != null)
            {
                hotKeyToRegister.Dispose();
                hotKeyToRegister = null;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dir = textBox1.Text;
        }

    }
 
}
