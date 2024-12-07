using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graph
{
    public partial class Form1 : Form
    {
        Graphics graph;
        Pen pen;
        public Form1()
        {
            InitializeComponent();
            graph = CreateGraphics();
            pen = new Pen(Color.SlateBlue, 2);
            this.Resize += new EventHandler(Form1_Resize);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            // Очищуємо область перед новим малюванням
            graph.Clear(Color.White);
            DrawGraph();
        }

        private void DrawGraph()
        {
            // Встановлюємо діапазон значень x та крок
            double xStart = -1;
            double xEnd = 2.3;
            double step = 0.7;
            
            // Масштабування для пристосування до вікна
            float width = this.ClientSize.Width;
            float height = this.ClientSize.Height;
            
            // Центруємо графік по осі X і Y
            float originX = width / 2;
            float originY = height / 2;
            
            // Малюємо осі координат
            graph.DrawLine(Pens.Black, 0, originY, width, originY); // X-axis
            graph.DrawLine(Pens.Black, originX, 0, originX, height); // Y-axis
            
            // Обчислюємо точку на графіку для кожного x
            for (double x = xStart; x <= xEnd; x += step)
            {
                double y = CalculateY(x); // Обчислюємо y по формулі

                // Масштабуємо значення для відображення на екрані
                float screenX = (float)(originX + x * 50); // 50 - коефіцієнт масштабування по осі X
                float screenY = (float)(originY - y * 50); // 50 - коефіцієнт масштабування по осі Y

                // Малюємо точку на графіку
                graph.FillEllipse(Brushes.SlateBlue, screenX, screenY, 2, 2);
            }
        }

        // Функція для обчислення значення y по заданій формулі
        private double CalculateY(double x)
        {
            if (x + 3 == 0)
            {
                return double.NaN; // уникнути ділення на нуль
            }
            return (Math.Exp(2 * x) - 8) / (x + 3);
        }
    }
}
