using Octave.NET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Simulacion
{
    public partial class Form1 : Form
    {
        int i = 0;
        int j = 0;
        double[] vector1 = new double[3];
        double[] vector2 = new double[3];
        double[] vector3 = new double[3];
        double[] vectort = new double[3];
        int x0m1; 
        int x0m2;
        int x0m3;
        int w0k1;
        int w0k2;
        int w0k3;
        int x0k4;
        int x0k5;
        int x0k6;
        int w0k7;
        int w0b1;
        int x0b2;
        int w0b2;
        int x0b3;
        int w0b3;
        int w0k4;
        int w0k5;
        int w0k6;
        int x0k7;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            x0m1 = pbm1.Location.Y;
            x0m2 = pbm2.Location.Y;
            x0m3 = pbm3.Location.Y;
            w0k1 = pbk1.Height;
            w0k2 = pbk2.Height;
            w0k3 = pbk3.Height;
            x0k4 = pbk4.Top;
            w0k4 = pbk4.Height;
            x0k5 = pbk5.Top;
            w0k5 = pbk5.Height;
            x0k6 = pbk6.Top;
            w0k6 = pbk6.Height;
            w0k7 = pbk7.Height;
            x0k7 = pbk7.Top;
            w0b1 = pbb1.Height;
            w0b2 = pbb2.Height;
            w0b3 = pbb3.Height;
            x0b2 = pbb2.Top;
            x0b3 = pbb3.Top;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Respuesta al Paso")
            {
                chartm1.Show();
                chartm2.Show();
                chartm3.Show();
                charttiempom1.Hide();
                charttiempom2.Hide();
                charttiempom3.Hide();
            }
            else {
                chartm1.Hide();
                chartm2.Hide();
                chartm3.Hide();
                charttiempom1.Show();
                charttiempom2.Show();
                charttiempom3.Show();
            }
        }

        private void Rtiempo_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonprocess_Click(object sender, EventArgs e)
        {
            i = 0;
            chartm1.Series[0].Points.Clear();
            chartm2.Series[0].Points.Clear();
            chartm3.Series[0].Points.Clear();
            OctaveContext.OctaveSettings.OctaveCliPath = @""+tbOctave.Text;
            var Octave = new OctaveContext();
            string instrucciones = "clc;"
                                    + "clear;"
                                    + "pkg load control;"
                                    + "s=tf('s');"
                                    + "k1=" + tbk1.Text + ";"
                                    + "k2=" + tbk2.Text + ";"
                                    + "k3=" + tbk3.Text + ";"
                                    + "k4=" + tbk4.Text + ";"
                                    + "k5=" + tbk5.Text + ";"
                                    + "k6=" + tbk6.Text + ";"
                                    + "k7=" + tbk7.Text + ";"
                                    + "b1=" + tbb1.Text + ";"
                                    + "b2=" + tbb2.Text + ";"
                                    + "b3=" + tbb3.Text + ";"
                                    + "m1=" + textm1.Text + ";"
                                    + "m2=" + textm2.Text + ";"
                                    + "m3=" + textm3.Text + ";"
                                    + "a1=m1*s^2+k1+k2+k3+k4;"
                                    + "a2=k3+k4;"
                                    + "a3=m2*s^2+k3+b1*s+k3+b2*s;"
                                    + "a4=b2*s+k6;"
                                    + "a5=m3*s^2+k3+k5+k6+b2*s+k7+b3*s;"
                                    + "a6=k4+k5;"
                                    + "a7=b2*s+k6;"
                                    + "G1=(a3*a5-a4*a7)/(a3*a1*a5-a4*a1*a7-a6*a3*a2);"
                                    + "G3=(G1*a6*a3)/(a3*a5-a4*a7);"
                                    + "G2=G3*a4/a3;"
                                    + "[y,t] = step(G1);"
                                    + "c=length(t);"
                                    + "tiempo=t(c);"
                              + "[y1, t1]=step(G1, tiempo, tiempo/" + tbpos.Text + ");"
                              + "[y2, t1]=step(G2, tiempo, tiempo/" + tbpos.Text + ");"
                              + "[y3, t1]=step(G3, tiempo, tiempo/" + tbpos.Text + ");";
            Octave.Execute(instrucciones);
            Array.Resize(ref vectort, Int32.Parse(tbpos.Text));
            Array.Resize(ref vector1, Int32.Parse(tbpos.Text));
            Array.Resize(ref vector2, Int32.Parse(tbpos.Text));
            Array.Resize(ref vector3, Int32.Parse(tbpos.Text)); 
            vector1 = Octave.Execute("y1").AsVector();
            vectort = Octave.Execute("t1").AsVector();
            vector2 = Octave.Execute("y2").AsVector();
            vector3 = Octave.Execute("y3").AsVector();

            buttonprocess.Enabled = false;
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            chartm1.Series[0].Points.AddXY(vectort[i], vector1[i]);
            chartm2.Series[0].Points.AddXY(vectort[i], vector2[i]);
            chartm3.Series[0].Points.AddXY(vectort[i], vector3[i]);
            //masas
            pbm1.Location = new Point(pbm1.Location.X, x0m1 + Convert.ToInt32(vector1[i] * 500));
            pbm2.Location = new Point(pbm2.Location.X, x0m2 + Convert.ToInt32(vector2[i] * 500));
            pbm3.Location = new Point(pbm3.Location.X, x0m3 + Convert.ToInt32(vector3[i] * 500));

            //resortes
            pbk1.Height = w0k1 + Convert.ToInt32(vector1[i] * 500);
            pbk2.Height = w0k2 + Convert.ToInt32(vector1[i] * 500);
            pbk3.Height = w0k3 + Convert.ToInt32(vector2[i] * 500);
            pbk4.Top = x0k4 + Convert.ToInt32(vector1[i] * 500);
            pbk5.Top = x0k5 + Convert.ToInt32(vector1[i] * 500);
            pbk6.Top = x0k6 + Convert.ToInt32(vector2[i] * 500);
            pbk4.Height = w0k4 + Convert.ToInt32(vector3[i] * 500) - Convert.ToInt32(vector1[i] * 500);
            pbk5.Height = w0k5 + Convert.ToInt32(vector3[i] * 500) - Convert.ToInt32(vector1[i] * 500);
            pbk6.Height = w0k6 + Convert.ToInt32(vector3[i] * 500) - Convert.ToInt32(vector2[i] * 500);

            pbk7.Height = w0k7 - Convert.ToInt32(vector3[i] * 500);
            pbk7.Top = x0k7 + Convert.ToInt32(vector3[i] * 500);

            //amortiguadores
            pbb1.Height = w0b1 + Convert.ToInt32(vector2[i] * 500);

            pbb2.Height = w0b2 - Convert.ToInt32(vector2[i] * 500) + Convert.ToInt32(vector3[i] * 500);
            pbb2.Top = x0b2 + Convert.ToInt32(vector2[i] * 500);

            pbb3.Height = w0b3 - Convert.ToInt32(vector3[i] * 500);
            pbb3.Top = x0b3 + Convert.ToInt32(vector3[i] * 500);
            i++;

            if (i == Int32.Parse(tbpos.Text))
            {
                

                dataGridView1.Rows.Add();
                dataGridView1.Rows[j].Cells[0].Value = tbk1.Text;
                dataGridView1.Rows[j].Cells[1].Value = tbk2.Text;
                dataGridView1.Rows[j].Cells[2].Value = tbk3.Text;
                dataGridView1.Rows[j].Cells[3].Value = tbk4.Text;
                dataGridView1.Rows[j].Cells[4].Value = tbk5.Text;
                dataGridView1.Rows[j].Cells[5].Value = tbk6.Text;
                dataGridView1.Rows[j].Cells[6].Value = tbk7.Text;
                dataGridView1.Rows[j].Cells[7].Value = tbb1.Text;
                dataGridView1.Rows[j].Cells[8].Value = tbb2.Text;
                dataGridView1.Rows[j].Cells[9].Value = tbb3.Text;
                dataGridView1.Rows[j].Cells[10].Value = textm1.Text;
                dataGridView1.Rows[j].Cells[11].Value = textm2.Text;
                dataGridView1.Rows[j].Cells[12].Value = textm3.Text;
                dataGridView1.Rows[j].Cells[13].Value = tbpos.Text;


                Bitmap captura = new Bitmap(chartm1.Width, chartm1.Height); 
                chartm1.DrawToBitmap(captura, chartm1.DisplayRectangle); 
                dataGridView1.Rows[j].Cells[14].Value = captura;

                Bitmap captura2 = new Bitmap(chartm2.Width, chartm2.Height);
                chartm2.DrawToBitmap(captura2, chartm2.DisplayRectangle);
                dataGridView1.Rows[j].Cells[15].Value = captura2;

                Bitmap captura3 = new Bitmap(chartm3.Width, chartm3.Height);
                chartm3.DrawToBitmap(captura3, chartm3.DisplayRectangle);
                dataGridView1.Rows[j].Cells[16].Value = captura3;
                j++;

                timer1.Enabled = false;
                buttonprocess.Enabled = true;


            }
        }

        private void pbm1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }
    }
}
