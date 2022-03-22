using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _931903.gorbatyuk.anastasiya.lab1
{
    public partial class Form1 : Form
    {
        void Funtcion()
        {
            /*размер таблицы*/
            

            /*создание столбцов*/
            //1 столбец, текстовый
            DataGridViewTextBoxColumn column0 = new DataGridViewTextBoxColumn();
            column0.Name = "time";
            column0.HeaderText = "Time Step";
            //2 столбец, текстовый
            DataGridViewTextBoxColumn column1 = new DataGridViewTextBoxColumn();
            column1.Name = "distance";
            column1.HeaderText = "Distance";
            //3 столбец, текстовый
            DataGridViewTextBoxColumn column2 = new DataGridViewTextBoxColumn();
            column2.Name = "maxHeight";
            column2.HeaderText = "Max Height";
            //4 столбец, текстовый
            DataGridViewTextBoxColumn column3 = new DataGridViewTextBoxColumn();
            column3.Name = "speedEnd";
            column3.HeaderText = "Speed at the end point";
            //добавляем столбцы
            dataGridView1.Columns.AddRange(column0, column1, column2, column3);




        }
        public Form1()
        {
            InitializeComponent();

            Funtcion();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


       
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        const double g = 9.81;
        const double C = 0.15;
        const double rho = 1.29;

        double height, angle, speed, S, m, dt;
        double cosa, sina, k;
        double t,y,x, vx, vy,xmax, ymax, speed_end;
        int lineCounter = 0;
        //для таблицы
        //время шага - dt
        //дистанция - x
        //максимальная высота - ymax
        //скорость при приземлении - speed_end

        private void btStart_Click(object sender, EventArgs e)
        {
            height = (double)edHeight.Value;
            angle = (double)edAngle.Value;
            speed = (double)edSpeed.Value;
            S = (double)edSize.Value;
            m = (double)edWeight.Value;
            dt = (double)edStep.Value;

            cosa = Math.Cos(angle * Math.PI / 180);
            sina = Math.Sin(angle * Math.PI / 180);
            k = (0.5 * C * S * rho)/m;

            t = 0;
            x = 0;
            y = height;
            ymax = height;

            vx = speed * cosa;
            vy = speed * sina;

            chart1.Series[lineCounter].Points.Clear();
            chart1.Series[lineCounter].Points.AddXY(x, y) ;

            do
            {
                double vx_old = vx;
                double vy_old = vy;
                double root = Math.Sqrt(vx * vx + vy * vy);

                t = t + dt;

                vx = vx_old - k * vx_old * root * dt;
                vy = vy_old - (g + k * root * vy_old) * dt;

                x = x + vx * dt;
                y = y + vy * dt;
                speed_end = root;
                chart1.Series[lineCounter].Points.AddXY(x, y);
                if (y > ymax) ymax = y;
                xmax = x;

            } 
            while (y>0);
            lineCounter = (lineCounter + 1) % 5;

            //создание ячеек
            DataGridViewCell time = new DataGridViewTextBoxCell();
            DataGridViewCell distance = new DataGridViewTextBoxCell();
            DataGridViewCell maxHeight = new DataGridViewTextBoxCell();
            DataGridViewCell speedEnd = new DataGridViewTextBoxCell();
            //заполнение ячеек
            time.Value = dt;
            distance.Value = xmax;
            maxHeight.Value = ymax;
            speedEnd.Value = speed_end;
            //создание строки
            DataGridViewRow row = new DataGridViewRow();
            //добавление ячеек в строку
            row.Cells.AddRange(time, distance, maxHeight, speedEnd);
            //добавление строки в таблицу
            dataGridView1.Rows.Add(row);

            //обновляем таблицу
            dataGridView1.Refresh();
        }
    }
}
