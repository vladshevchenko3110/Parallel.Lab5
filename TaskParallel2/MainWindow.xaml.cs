using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace TaskParallel5._2
{
    public partial class MainWindow : Window
    {
        
        public class Box
        {
            public int Width { get; set; }
            public int Length { get; set; }
            public int Height { get; set; }
        }

        const uint N = 12500000; 
        List<Box> boxes1;
        List<Box> boxes2;
        List<Box> boxes3;
        List<Box> boxes4;
        public MainWindow()
        {
            InitializeComponent();
            boxes1 = GenerateBoxes();
            boxes2 = GenerateBoxes();
            boxes3 = GenerateBoxes();
            boxes4 = GenerateBoxes();
            InfoTextBox.Text = $"{N*4}";
        }


        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            
            ResultTextBox.Text = "";
            ResultTextBox1.Text = "";
            ResultTextBox2.Text = "";
            ResultTextBox3.Text = "";
            
            var timer = new Stopwatch();
            int count = 0;
            int count1 = 0;

            timer.Start();
            foreach (Box box in boxes1)
            {
                if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                {
                    count++;
                }
            };

            foreach (Box box in boxes2)
            {
                if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                {
                    count++;
                }
            };

            foreach (Box box in boxes3)
            {
                if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                {
                    count++;
                }
            };

            foreach (Box box in boxes4)
            {
                if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                {
                    count++;
                }
            };

            timer.Stop();
            
            ResultTextBox2.AppendText($"{count}");
            
            ResultTextBox3.AppendText($"{timer.ElapsedMilliseconds} ms");
           

            timer.Restart();
            Task taskA = Task.Run(() =>
            {
                foreach(Box box in boxes1)
                {                        
                    if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                    {
                        Interlocked.Increment(ref count1);                               
                    }
                };              

            });
            Task taskB = Task.Run(() =>
            {
                foreach (Box box in boxes2)
                {
                    if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                    {
                        Interlocked.Increment(ref count1);
                    }
                };

            });
            Task taskC = Task.Run(() =>
            {
                foreach (Box box in boxes3)
                {
                    if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                    {
                        Interlocked.Increment(ref count1);
                    }
                };

            });
            Task taskD = Task.Run(() =>
            {
                foreach (Box box in boxes4)
                {
                    if (box.Width > 150 && box.Length > 150 && box.Height > 150)
                    {
                        Interlocked.Increment(ref count1);
                    }
                };

            });
            Task.WaitAll(taskA,taskB,taskC,taskD);
            timer.Stop();
            
            ResultTextBox.AppendText($"{count1}");
            ResultTextBox1.AppendText($"{timer.ElapsedMilliseconds} ms");          


        }
        


        private List<Box> GenerateBoxes()
        {
            Random random = new Random();
            List<Box> boxes = new List<Box>();

            
            for (int i = 0; i < N; i++)
            {
                boxes.Add(new Box
                {
                    Width = random.Next(20, 201),
                    Length = random.Next(20, 201),
                    Height = random.Next(20, 201)
                });
            }

            return boxes;
        }
    }
}
