using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;


namespace TaskParallel1
{


    public partial class MainWindow : Window
    {
        public class Order
        {
           
            public int ProductId { get; set; }
            public DateTime CreateDateTime { get; set; }
            public DateTime FinishDateTime { get; set; }
            public Status OrderStatus { get; set; }

        }
        public enum Status
        {
            Confirmed = 0,
            Rejected = 1,
        }

        const uint N = 2000000;
        ConcurrentBag<Order> orders1 = new ConcurrentBag<Order>();
        ConcurrentBag<Order> orders2 = new ConcurrentBag<Order>();
        ConcurrentBag<Order> orders3 = new ConcurrentBag<Order>();
        ConcurrentBag<Order> orders4 = new ConcurrentBag<Order>();
        ConcurrentBag<Order> confirmedorders = new ConcurrentBag<Order>();
        ConcurrentBag<Order> rejectedorders = new ConcurrentBag<Order>();
        ConcurrentBag<Order> recentorders = new ConcurrentBag<Order>();
        ConcurrentBag<Order> taskconfirmedorders = new ConcurrentBag<Order>();
        ConcurrentBag<Order> taskrejectedorders = new ConcurrentBag<Order>();
        ConcurrentBag<Order> taskrecentorders = new ConcurrentBag<Order>();

        public MainWindow()
        {
            InitializeComponent();
            orders1 = GenerateOrders();
            orders2 = GenerateOrders();
            orders3 = GenerateOrders();
            orders4 = GenerateOrders();
            InfoTextBox.Text = $"{N * 4}";
        }
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            var timer = new Stopwatch();
            ResultTextBox1.Text = "";
            ResultTextBox2.Text = "";            
            var cdate = new DateTime(2022, 12, 1);

            timer.Start();
            foreach (Order order in orders1)
            {
                if (order.OrderStatus == (Status)0)
                {
                    confirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    rejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    recentorders.Add(order);
                }
            };
            foreach (Order order in orders2)
            {
                if (order.OrderStatus == (Status)0)
                {
                    confirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    rejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    recentorders.Add(order);
                }
            };
            foreach (Order order in orders3)
            {
                if (order.OrderStatus == (Status)0)
                {
                    confirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    rejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    recentorders.Add(order);
                }
            };
            foreach (Order order in orders4)
            {
                if (order.OrderStatus == (Status)0)
                {
                    confirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    rejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    recentorders.Add(order);
                }
            };            
            timer.Stop();
            ConfirmedList1.ItemsSource = confirmedorders;
            RejectedList1.ItemsSource = rejectedorders;
            RecentList1.ItemsSource = recentorders;
            ResultTextBox1.AppendText($"{timer.ElapsedMilliseconds} ms");

            timer.Restart();            

            Parallel.ForEach(orders1, order =>
            {
                if (order.OrderStatus == (Status)0)
                {
                    taskconfirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    taskrejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    taskrecentorders.Add(order);
                }
            });

            Parallel.ForEach(orders2, order =>
            {
                if (order.OrderStatus == (Status)0)
                {
                    taskconfirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    taskrejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    taskrecentorders.Add(order);
                }
            });

            Parallel.ForEach(orders3, order =>
            {
                if (order.OrderStatus == (Status)0)
                {
                    taskconfirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    taskrejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    taskrecentorders.Add(order);
                }
            });

            Parallel.ForEach(orders4, order =>
            {
                if (order.OrderStatus == (Status)0)
                {
                    taskconfirmedorders.Add(order);
                }
                if (order.OrderStatus == (Status)1)
                {
                    taskrejectedorders.Add(order);
                }
                if (order.CreateDateTime >= cdate)
                {
                    taskrecentorders.Add(order);
                }
            });



            timer.Stop();
            ConfirmedList2.ItemsSource = taskconfirmedorders;
            RejectedList2.ItemsSource = taskrejectedorders;
            RecentList2.ItemsSource = taskrecentorders;
            ResultTextBox2.AppendText($"{timer.ElapsedMilliseconds} ms");
        }

        private ConcurrentBag<Order> GenerateOrders()
        {
            Random random = new Random();
            ConcurrentBag<Order> orders = new ConcurrentBag<Order>();


            for (int i = 0; i < N; i++)
            {
                orders.Add(new Order
                {
                    ProductId = random.Next(0, 101),
                    CreateDateTime = new DateTime(random.NextInt64(new DateTime(2020, 1, 1).Ticks, new DateTime(2023, 1, 1).Ticks)),
                    FinishDateTime = new DateTime(random.NextInt64(new DateTime(2023, 1, 1).Ticks, DateTime.Now.Ticks)),
                    OrderStatus = (Status)random.Next(0, 2),
                }) ;
                
            }

            return orders;
        }

    }
}
