using System.CodeDom.Compiler;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Hard_Lab_5
{
    public partial class Form1 : Form
    {
        Bitmap map;
        Graphics graphics;
        bool isTree;

        struct position
        {
            public float x, y, angle;
            public position(float x, float y, float angle)
            {
                this.x = x;
                this.y = y;
                this.angle = angle;
            }
        }

        public Form1()
        {
            InitializeComponent();
            map = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(map);
            isTree = checkBox.Checked;
        }

        private void ButtonRunClick(object sender, EventArgs e)
        {
            var rand = new Random();

            graphics.Clear(pictureBox.BackColor);

            var pattern = @"\s*(?<atom>\d+)\s+(?<angle>[-+]?\d+\.?\d+)\s+" +
                @"(?<startVector>\S+)\s*";

            string[] allText = textBox.Text.Split('\n');
            var match = Regex.Match(allText[0], pattern);

            if (!match.Success)
                return;

            int n = Int32.Parse(match.Groups[1].Value);
            string start = match.Groups[3].Value;
            float addAngle = float.Parse(match.Groups[2].Value, CultureInfo.InvariantCulture);

            Dictionary<char, string> rules = new Dictionary<char, string>();
            string[] sRules = allText[1..];
            foreach (string sRule in sRules)
            {
                pattern = @"\s*(?<atom>.)\s*->\s*(?<rule>.+)\s*\r?";
                match = Regex.Match(sRule, pattern);
                if (match.Success)
                    rules[char.Parse(match.Groups[1].Value)] =
                        match.Groups[2].Value;
            }
            if (rules.Count == 0)
                return;

            string temp = "";
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < start.Length; j++)
                {
                    if (rules.ContainsKey(start[j]))
                        temp += rules[start[j]];
                    else
                        temp += start[j];
                }
                start = temp;
                temp = "";
            }

            float startX = 1000;
            float startY = 1000;
            float minX = 0;
            float maxX = 0;
            float minY = 0;
            float maxY = 0;

            float x = startX;
            float y = startY;

            float addMove = 1;
            float angle = 90;

            Stack<position> stack = new Stack<position>();
            Queue<float> angles = new Queue<float>();

            bool isRandom = false;

            for (int i = 0; i < start.Length; i++)
            {
                if (start[i] == 'F')
                {
                    float nextX = x + (float)Math.Cos(angle * Math.PI / 180) * addMove;
                    float nextY = y - (float)Math.Sin(angle * Math.PI / 180) * addMove;
                    x = nextX;
                    y = nextY;

                    if (startX - minX > x)
                        minX = startX - x;
                    if (startY - minY > y)
                        minY = startY - y;
                    if (startX + maxX < x)
                        maxX = x - startX;
                    if (startY + maxY < y)
                        maxY = y - startY;
                }
                else if (start[i] == '+')
                {
                    if (isRandom)
                    {
                        var randomAngle = (float)rand.NextDouble() * addAngle;
                        angles.Enqueue(randomAngle);
                        angle += randomAngle;
                    }
                    else
                        angle += addAngle;
                    angle %= 360;
                }
                else if (start[i] == '-')
                {
                    if (isRandom)
                    {
                        var randomAngle = (float)rand.NextDouble() * addAngle;
                        angles.Enqueue(randomAngle);
                        angle -= randomAngle;
                    }
                    else
                        angle -= addAngle;
                    angle %= 360;
                }
                else if (start[i] == '[')
                {
                    stack.Push(new position(x, y, angle));
                }
                else if (start[i] == ']')
                {
                    if (stack.Count == 0)
                        continue;
                    position pos = stack.Pop();
                    x = pos.x;
                    y = pos.y;
                    angle = pos.angle;
                }
                else if (start[i] == '@')
                {
                    isRandom = true;
                }
            }

            map = new Bitmap(pictureBox.Width, pictureBox.Height);
            graphics = Graphics.FromImage(map);

            if (!isTree && ((map.Width >= map.Height && minX + maxX < minY + maxY) ||
                (map.Width < map.Height && minX + maxX > minY + maxY)))
            {
                angle = 180;
                float tempC = minX;
                minX = minY;
                minY = maxX;
                maxX = maxY;
                maxY = tempC;
            }
            else
                angle = 90;
            float newAddMove1 = map.Height / (minY + maxY);
            float newAddMove2 = map.Width / (minX + maxX);
            if (newAddMove1 < newAddMove2)
                addMove = newAddMove1;
            else
                addMove = newAddMove2;

            float width = isTree ? 20 : 2;
            int iter = 0;

            Color color = isTree ? Color.FromArgb(255, 150, 75, 0) : Color.White;
            Pen pen;
            pen = new Pen(color, width);

            x = map.Width * minX / (minX + maxX);
            y = map.Height * minY / (minY + maxY);

            for (int i = 0; i < start.Length; i++)
            {
                if (start[i] == 'F')
                {
                    float nextX = x + (float)Math.Cos(angle * Math.PI / 180) * addMove;
                    float nextY = y - (float)Math.Sin(angle * Math.PI / 180) * addMove;
                    graphics.DrawLine(pen, new PointF(x, y), new PointF(nextX, nextY));
                    x = nextX;
                    y = nextY;
                }
                else if (start[i] == '+')
                {
                    angle += isRandom ? angles.Dequeue() : addAngle;
                    angle %= 360;
                }
                else if (start[i] == '-')
                {
                    angle -= isRandom ? angles.Dequeue() : addAngle;
                    angle %= 360;
                }
                else if (start[i] == '[')
                {
                    stack.Push(new position(x, y, angle));
                    if (isTree)
                    {
                        iter++;
                        float newWidth = (n - iter) * 1.0f / (n * 1.0f) * width;
                        if (newWidth < 1)
                            newWidth = 1;
                        double r;
                        double g;
                        if (n >= iter)
                        {
                            r = Math.Sqrt(n * (n - iter)) / n * 150;
                            g = 128 - Math.Sqrt(n * (n - iter)) / n * 53;
                        }
                        else
                        {
                            r = 0;
                            g = 128;
                        }
                        color = Color.FromArgb((int)r, (int)g, 0);
                        pen = new Pen(color, newWidth);
                    }
                }
                else if (start[i] == ']')
                {
                    if (stack.Count == 0)
                        continue;
                    position pos = stack.Pop();
                    x = pos.x;
                    y = pos.y;
                    angle = pos.angle;
                    if (isTree)
                    {
                        iter--;
                        float newWidth = (n - iter) * 1.0f / (n * 1.0f) * width;
                        if (newWidth < 1)
                            newWidth = 1;
                        double r;
                        double g;
                        if (n >= iter)
                        {
                            r = Math.Sqrt(n * (n - iter)) / n * 150;
                            g = 128 - Math.Sqrt(n * (n - iter)) / n * 53;
                        }
                        else
                        {
                            r = 0;
                            g = 128;
                        }
                        color = Color.FromArgb((int)r, (int)g, 0);
                        pen = new Pen(color, newWidth);
                    }
                }
            }

            pictureBox.Image = map;
        }

        private void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            isTree = checkBox.Checked;
        }
    }
}