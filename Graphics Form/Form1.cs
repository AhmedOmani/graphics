using System;
using System.Windows.Forms;
using System.Drawing;

namespace Graphics_Form
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Yellow;
            button1.ForeColor = Color.Black;
            using (var inputForm = new InputForm("Enter values for DDA Line", "x0", "y0", "xEnd", "yEnd"))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    int x0 = int.Parse(inputForm.Value1);
                    int y0 = int.Parse(inputForm.Value2);
                    int xEnd = int.Parse(inputForm.Value3);
                    int yEnd = int.Parse(inputForm.Value4);

                    Graphics g = this.CreateGraphics();
                    g.Clear(this.BackColor);
                    DrawDDALine(g, x0, y0, xEnd, yEnd);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Yellow; 
            button1.ForeColor = Color.Black;
            using (var inputForm = new InputForm("Enter values for Midpoint Circle", "xc", "yc", "radius"))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    int xc = int.Parse(inputForm.Value1);
                    int yc = int.Parse(inputForm.Value2);
                    int r = int.Parse(inputForm.Value3);

                    Graphics g = this.CreateGraphics();
                    g.Clear(this.BackColor);
                    DrawMidpointCircle(g, xc, yc, r);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button1.BackColor = Color.Yellow;  
            button1.ForeColor = Color.Black;
            using (var inputForm = new InputForm("Enter values for Bresenham Circle", "xc", "yc", "radius"))
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    int xc = int.Parse(inputForm.Value1);
                    int yc = int.Parse(inputForm.Value2);
                    int r = int.Parse(inputForm.Value3);

                    Graphics g = this.CreateGraphics();
                    g.Clear(this.BackColor);
                    DrawBresenhamCircle(g, xc, yc, r);
                }
            }
        }

        private void DrawDDALine(Graphics g, int x0, int y0, int xEnd, int yEnd)
        {
            int dx = xEnd - x0, dy = yEnd - y0;
            int steps = Math.Abs(dx) > Math.Abs(dy) ? Math.Abs(dx) : Math.Abs(dy);

            float xIncrement = dx / (float)steps;
            float yIncrement = dy / (float)steps;

            float x = x0;
            float y = y0;
            g.FillRectangle(Brushes.Black, x, y, 2, 2);

            for (int k = 0; k < steps; k++)
            {
                x += xIncrement;
                y += yIncrement;
                g.FillRectangle(Brushes.Black, x, y, 2, 2);
            }
        }

        private void DrawMidpointCircle(Graphics g, int xc, int yc, int r)
        {
            int x = 0, y = r;
            int d = 1 - r;
            CirclePoints(g, xc, yc, x, y);

            while (x < y)
            {
                if (d < 0)
                    d += 2 * x + 3;
                else
                {
                    d += 2 * (x - y) + 5;
                    y--;
                }
                x++;
                CirclePoints(g, xc, yc, x, y);
            }
        }

        private void DrawBresenhamCircle(Graphics g, int xc, int yc, int r)
        {
            int x = 0, y = r;
            int d = 3 - 2 * r;
            CirclePoints(g, xc, yc, x, y);

            while (y >= x)
            {
                x++;
                if (d > 0)
                {
                    y--;
                    d = d + 4 * (x - y) + 10;
                }
                else
                {
                    d = d + 4 * x + 6;
                }
                CirclePoints(g, xc, yc, x, y);
            }
        }

        private void CirclePoints(Graphics g, int xc, int yc, int x, int y)
        {
            g.FillRectangle(Brushes.Black, xc + x, yc + y, 2, 2);
            g.FillRectangle(Brushes.Black, xc - x, yc + y, 2, 2);
            g.FillRectangle(Brushes.Black, xc + x, yc - y, 2, 2);
            g.FillRectangle(Brushes.Black, xc - x, yc - y, 2, 2);
            g.FillRectangle(Brushes.Black, xc + y, yc + x, 2, 2);
            g.FillRectangle(Brushes.Black, xc - y, yc + x, 2, 2);
            g.FillRectangle(Brushes.Black, xc + y, yc - x, 2, 2);
            g.FillRectangle(Brushes.Black, xc - y, yc - x, 2, 2);
        }
    }

    public class InputForm : Form
    {
        private Label[] labels;
        private TextBox[] textBoxes;
        private Button okButton, cancelButton;
        public string Value1 => textBoxes.Length > 0 ? textBoxes[0].Text : null;
        public string Value2 => textBoxes.Length > 1 ? textBoxes[1].Text : null;
        public string Value3 => textBoxes.Length > 2 ? textBoxes[2].Text : null;
        public string Value4 => textBoxes.Length > 3 ? textBoxes[3].Text : null;

        public InputForm(string title, params string[] labelsText)
        {
            Text = title;
            labels = new Label[labelsText.Length];
            textBoxes = new TextBox[labelsText.Length];
            for (int i = 0; i < labelsText.Length; i++)
            {
                labels[i] = new Label() { Text = labelsText[i], Top = 10 + 30 * i, Left = 10, Width = 50 };
                textBoxes[i] = new TextBox() { Top = 10 + 30 * i, Left = 70, Width = 200 };
                Controls.Add(labels[i]);
                Controls.Add(textBoxes[i]);
            }
            okButton = new Button() { Text = "OK", Top = 10 + 30 * labelsText.Length, Left = 70, DialogResult = DialogResult.OK };
            cancelButton = new Button() { Text = "Cancel", Top = 10 + 30 * labelsText.Length, Left = 150, DialogResult = DialogResult.Cancel };
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            AcceptButton = okButton;
            CancelButton = cancelButton;
        }
    }
}

