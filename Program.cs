using System;
using System.Drawing;
using System.Windows.Forms;

namespace Graph
{
    public class GraphForm : Form
    {
        // Константи для налаштувань графіка
        private const double XStart = -1; // Початкове значення X
        private const double XEnd = 2.3;  // Кінцеве значення X
        private const double DeltaX = 0.7; // Крок для X

        public GraphForm()
        {
            // Налаштування форми
            this.Text = "Графік функції";
            this.Size = new Size(800, 600);
            this.BackColor = Color.White;

            // Перерисовка графіка при зміні розміру
            this.Resize += (s, e) => this.Invalidate(); 
            // Перерисовка графіка при малюванні
            this.Paint += (s, e) => DrawGraph(e.Graphics); 
        }

        private void DrawGraph(Graphics graph)
        {
            // Очищаємо форму перед малюванням
            graph.Clear(Color.White);

            using (Pen axisPen = new Pen(Color.Black, 1)) // Перо для осей
            using (Pen pen = new Pen(Color.SlateBlue, 2)) // Перо для графіка
            {
                float widthForm = this.ClientSize.Width;
                float heightForm = this.ClientSize.Height;

                float offsetY = heightForm / 2; // Центрування графіка по осі Y

                // Визначаємо масштабування
                double scaleX = widthForm / (XEnd - XStart);
                double scaleY = heightForm / 4;

                // Малюємо осі X і Y
                graph.DrawLine(axisPen, 0, offsetY, widthForm, offsetY); // Вісь X
                graph.DrawLine(axisPen, widthForm / 2, 0, widthForm / 2, heightForm); // Вісь Y

                // Шрифт для підписів
                Font font = new Font("Arial", 10);
                Brush brush = Brushes.Black;

                // Підписи для осі X
                double tStep = 1.0; 
                for (double t = XStart; t <= XEnd; t += tStep)
                {
                    int screenX = (int)((t - XStart) * scaleX);
                    graph.DrawString(t.ToString("0.0"), font, brush, screenX, offsetY + 5);
                }

                // Підписи для осі Y
                double yStep = 0.5;
                for (double y = -2.0; y <= 2.0; y += yStep)
                {
                    int screenY = (int)(offsetY - y * scaleY);
                    graph.DrawString(y.ToString("0.0"), font, brush, widthForm / 2 + 5, screenY - 10);
                }

                // Малювання графіка функції y = (e^(2x) - 8) / (x + 3)
                double dt = 0.1;  // Крок для X
                int screenX1 = 0, screenY1 = 0;
                bool firstPoint = true;

                for (double t = XStart; t <= XEnd; t += DeltaX)
                {
                    // Обчислюємо значення функції
                    double y = (Math.Exp(2 * t) - 8) / (t + 3);

                    // Перетворюємо координати функції на координати екрану
                    int screenX2 = (int)((t - XStart) * scaleX);
                    int screenY2 = (int)(offsetY - y * scaleY);

                    if (!firstPoint)
                    {
                        graph.DrawLine(pen, screenX1, screenY1, screenX2, screenY2); // Малюємо лінію
                    }
                    else
                    {
                        firstPoint = false;
                    }

                    // Оновлюємо початкові координати для наступної точки
                    screenX1 = screenX2;
                    screenY1 = screenY2;
                }
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new GraphForm());
        }
    }
}
