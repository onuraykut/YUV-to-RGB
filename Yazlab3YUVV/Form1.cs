using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Yazlab3YUVV
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string sFileName,saveName;
        string selectedYuv;
        int width=0;
        int height=0;
        int k = 0;
        int toplam_frame = 0;
        int[] y_degerleri = new int[0];
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog choofdlog = new OpenFileDialog();
            choofdlog.Filter = "All Files (*.*)|*.*";
            choofdlog.FilterIndex = 1;
            choofdlog.Multiselect = true;

            if (choofdlog.ShowDialog() == DialogResult.OK)
            {
                sFileName = choofdlog.FileName;
                // string[] arrAllFiles = choofdlog.FileNames; //used when Multiselect = true 
                label2.Text=sFileName;          
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedYuv = this.comboBox1.GetItemText(this.comboBox1.SelectedItem);
        }

        void YuvRead444()
        {
            byte[] bytes = File.ReadAllBytes(sFileName);
            
            //  int width = 720, height = 576;
            int y = 0, u = 0, v = 0;
            toplam_frame = bytes.Length/(width * height * 3);
            int y_tut = 0,u_tut=0,v_tut=0;
            for (int k = 0; k < toplam_frame; k++)
            {
                Bitmap bit = new Bitmap(width, height);
               
                y = (width * height)+v;
            u = y + (width * height);
            v = u + (width * height);
                int sayac = 0;
                for (int x = 0; x < height; x++)
            {

                for (int z = 0; z < width; z++)
                {
                        
                    int yy = bytes[y-(width*height)+sayac];
                    int uu = bytes[y + sayac];
                    int vv = bytes[u + sayac];
                      //  if (k == 1)
                        //    Console.WriteLine(yy + "-" + uu + "-" + vv);
                    int[] rgbs = yuvtorgb(yy, uu, vv);

                    bit.SetPixel(z, x, Color.FromArgb(rgbs[0], rgbs[1], rgbs[2]));

                    sayac++;
                }
                //  sayac++;
            }
                y_tut = y;
                v_tut = v;
                u_tut=u;
                bit.Save(saveName + "\\deneme444" + "-" + k + ".bmp", ImageFormat.Bmp);
            }
            MessageBox.Show("Tamamlandı",
 "Yazlab3",
 MessageBoxButtons.OK,
 MessageBoxIcon.Warning // for Warning  
                        //MessageBoxIcon.Error // for Error 
                        //MessageBoxIcon.Information  // for Information
                        //MessageBoxIcon.Question // for Question
);
        }
        void YuvRead422()
        {
            byte[] bytes = File.ReadAllBytes(sFileName);
          
            //  int width = 720, height = 576;
            int y = 0, u = 0, v = 0;
            toplam_frame = bytes.Length / (width * height * 2);
            int y_tut = 0, u_tut = 0, v_tut = 0;
           // y_ler = new int[toplam_frame, width * height];
            for (int k = 0; k < toplam_frame; k++)
            {
                Bitmap bit = new Bitmap(width, height);

                y = (width * height) + v;
                u = y + (width * height/2);
                v = u + (width * height/2);
                int sayac = 0, sayac2 = 0;
                int yy, uu, vv;
                int u_yedek = 0;
                int v_yedek = 0;
                for (int x = 0; x < height; x++)
                {

                    for (int z = 0; z < width; z++)
                    {

                        if (z % 2 == 0)
                        {
                            yy = bytes[y - (width * height) + sayac];
                            uu = bytes[y + sayac2];
                            vv = bytes[u + sayac2];
                            u_yedek = uu;
                            v_yedek = vv;
                        }
                        else
                        {
                            yy = bytes[y - (width * height) + sayac];
                            uu = u_yedek;
                            vv = v_yedek;
                            sayac2--;
                        }

                       
                        //  if (k == 1)
                        //    Console.WriteLine(yy + "-" + uu + "-" + vv);
                        int[] rgbs = yuvtorgb(yy, uu, vv);
                       // y_ler[k,sayac] = yy;
                        bit.SetPixel(z, x, Color.FromArgb(rgbs[0], rgbs[1], rgbs[2]));

                        sayac++;
                        sayac2++;
                    }
                    //  sayac++;
                }
                y_tut = y;
                bit.Save(saveName + "\\deneme422" + "-" + k + ".bmp", ImageFormat.Bmp);
            }
            MessageBox.Show("Tamamlandı",
  "Yazlab3",
  MessageBoxButtons.OK,
  MessageBoxIcon.Warning // for Warning  
                         //MessageBoxIcon.Error // for Error 
                         //MessageBoxIcon.Information  // for Information
                         //MessageBoxIcon.Question // for Question
 );
        }
        void YuvRead420()
        {
            byte[] bytes = File.ReadAllBytes(sFileName);
            int sayac5 = 0;
            //  int width = 720, height = 576;
            int y = 0, u = 0, v = 0;
            toplam_frame = bytes.Length / (width * height * 2);
            int y_tut = 0, u_tut = 0, v_tut = 0;
            //    y_ler = new int[toplam_frame, width * height];
            y_degerleri = new int[width * height * toplam_frame];
            for (int k = 0; k < toplam_frame; k++)
            {
                Bitmap bit = new Bitmap(width, height);

                y = (width * height) + v;
                u = y + (width * height / 4);
                v = u + (width * height / 4);
                int sayac = 0, sayac2 = 0;
                int yy, uu, vv;
                for (int x = 0; x < height; x++)
                {

                    for (int z = 0; z < width; z++)
                    {

                        /*   if (z % 2 == 0)
                           {
                               yy = bytes[y - (width * height) + sayac];
                               uu = bytes[y + sayac2];
                               vv = bytes[u + sayac2];
                               u_yedek = uu;
                               v_yedek = vv;
                           }
                           else
                           {
                               yy = bytes[y - (width * height) + sayac];
                               uu = u_yedek;
                               vv = v_yedek;
                               sayac2--;
                           } */
                        yy = bytes[y - (width * height) + sayac];
                      
                        //  if (k == 1)
                        //    Console.WriteLine(yy + "-" + uu + "-" + vv);
                        int rgbs = yuvtorgb_Y(yy);
                        y_degerleri[sayac5] = rgbs;
                        // y_ler[k, sayac] = yy;
                        bit.SetPixel(z, x, Color.FromArgb(rgbs,rgbs,rgbs));

                        sayac++;
                        sayac2++;
                        sayac5++;
                    }
                    //  sayac++;
                }
                y_tut = y;
                bit.Save(saveName+"\\deneme420" + "-" + k + ".bmp", ImageFormat.Bmp);
                
            }
            MessageBox.Show("Tamamlandı",
 "Yazlab3",
 MessageBoxButtons.OK,
 MessageBoxIcon.Warning // for Warning  
                        //MessageBoxIcon.Error // for Error 
                        //MessageBoxIcon.Information  // for Information
                        //MessageBoxIcon.Question // for Question
);
        }
        public static int[] yuvtorgb(int y, int u, int v)
        {
            int[] rgb = new int[3];
            double c = y + 1.4075 * (v - 128);
            double b = y - 0.3455 * (u - 128) - 0.7169 * (v - 128);
            double z = y + 1.7790 * (u - 128);
          
            if (c > 255)
            {
                c = 255;
            }
            else if (c < 0)
            {
                c = 0;
            }
            if (b > 255)
            {
                b = 255;
            }
            else if (b < 0)
            {
                b = 0;
            }
            if (z > 255)
            {
                z = 255;
            }
            else if (z < 0)
            {
                z = 0;
            }
            rgb[0] = (int)c;
            rgb[1] = (int)b;
            rgb[2] = (int)z;


            return rgb;
        }
        public static int yuvtorgb_Y(int y)
        {
            int rgb=0;
            double c = y;


            if (c > 255)
            {
                c = 255;
            }
            else if (c < 0)
            {
                c = 0;
            }
            rgb = (int)c;
           


            return rgb;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            width = Int32.Parse( textBox1.Text);
            height = Int32.Parse(textBox2.Text);
            if (selectedYuv.Equals("444")) {
                YuvRead444();
            }
            else if (selectedYuv.Equals("422"))
            {
                YuvRead422();
            }
            else if (selectedYuv.Equals("420"))
            {
                YuvRead420();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (k < toplam_frame && k >= 0)
            {
                /*  
                    //  pictureBox1.Image = Image.FromFile("C:\\Users\\kryptows\\Downloads\\yuvs\\deneme444" + "-" + k + ".bmp"); */
                //bit2.Save("C:\\Users\\kryptows\\Downloads\\denemeasd.bmp", ImageFormat.Bmp);
                //  pictureBox1.Image = bit2
                k++;
                if (selectedYuv.Equals("444"))
                {
                    pictureBox1.Image = Image.FromFile(saveName + "\\deneme444" + "-" + k + ".bmp");
                }
                else if (selectedYuv.Equals("422"))
                {
                    pictureBox1.Image = Image.FromFile(saveName + "\\deneme422" + "-" + k + ".bmp");
                }
                else if (selectedYuv.Equals("420"))
                {
                    pictureBox1.Image = Image.FromFile(saveName + "\\deneme420" + "-" + k + ".bmp");
                }
               
                label6.Text = k + ".frame";

                // }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (k < toplam_frame && k >= 0)
            {
                k--;
                if (selectedYuv.Equals("444"))
                {
                    pictureBox1.Image = Image.FromFile(saveName + "\\deneme444" + "-" + k + ".bmp");
                }
                else if (selectedYuv.Equals("422"))
                {
                    pictureBox1.Image = Image.FromFile(saveName + "\\deneme422" + "-" + k + ".bmp");
                }
                else if (selectedYuv.Equals("420"))
                {
                    pictureBox1.Image = Image.FromFile(saveName + "\\deneme420" + "-" + k + ".bmp");
                }
                label6.Text = k + ".frame";
                
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (var fldrDlg = new FolderBrowserDialog())
            {
                if (fldrDlg.ShowDialog() == DialogResult.OK)
                {
                    //fldrDlg.SelectedPath -- your result
                    saveName=fldrDlg.SelectedPath;
                }
            }
        }
    }
}
