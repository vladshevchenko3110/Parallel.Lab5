using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using static TaskParallel3.MainWindow;

namespace TaskParallel3
{
    
    public partial class MainWindow : Window
    {

        const int N = 12500000;
        List<Customer> customers1 = new List<Customer>();
        List<Customer> customers2 = new List<Customer>();
        List<Customer> customers3 = new List<Customer>();
        List<Customer> customers4 = new List<Customer>();
        private CancellationTokenSource cancellationTokenSource;
        public class Customer
        {
            public int ID;
            public int Age;
            public string FirstName;
            public string LastName;
        }

        public MainWindow()
        {
            InitializeComponent();
            customers1 = GenerateCustomer(0*N);
            customers2 = GenerateCustomer(1*N);
            customers3 = GenerateCustomer(2*N);
            customers4 = GenerateCustomer(3*N);
            InfoTextBox.Text = $"{N * 4}";
        }

        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "";
            ResultTextBox1.Text = "";
            ResultTextBox2.Text = "";
            ResultTextBox3.Text = "";

            var timer = new Stopwatch();
            Customer neededcustomer = new Customer();
            Customer neededcustomer2 = new Customer();

            bool customerisfoundlist1 = false;
            bool customerisfoundlist2 = false;
            bool customerisfoundlist3 = false;
            bool customerisfoundlist4 = false;
            timer.Start();
        found:
            while (!customerisfoundlist1 && !customerisfoundlist2 && !customerisfoundlist3 && !customerisfoundlist4)
            {
                foreach (Customer customer in customers1)
                {
                    if (customer.FirstName == "FirstName45" && customer.LastName == "LastName1195" && customer.Age == 54)
                    {
                        customerisfoundlist1 = true;
                        neededcustomer = customer;
                        goto found;
                    }
                }
                foreach (Customer customer in customers2)
                {
                    if (customer.FirstName == "FirstName45" && customer.LastName == "LastName1195" && customer.Age == 54)
                    {
                        customerisfoundlist2 = true;
                        neededcustomer = customer;
                        goto found;
                    }
                }
                foreach (Customer customer in customers3)
                {
                    if (customer.FirstName == "FirstName45" && customer.LastName == "LastName1195" && customer.Age == 54)
                    {
                        customerisfoundlist3 = true;
                        neededcustomer = customer;
                        goto found;
                    }
                }
                foreach (Customer customer in customers4)
                {
                    if (customer.FirstName == "FirstName45" && customer.LastName == "LastName1195" && customer.Age == 54)
                    {
                        customerisfoundlist4 = true;
                        neededcustomer = customer;
                        goto found;
                    }
                }
                break;
            }
            timer.Stop();
            ResultTextBox2.AppendText($"{neededcustomer.ID}  {neededcustomer.FirstName}  {neededcustomer.LastName}  {neededcustomer.Age}");

            ResultTextBox3.AppendText($"{timer.ElapsedMilliseconds} ms");

            
            cancellationTokenSource = new CancellationTokenSource();

            
            var taskFactory = new TaskFactory();
            var tasks = new List<Task>();
            timer.Restart();
            tasks.Add(taskFactory.StartNew(() => SearchInList(ref neededcustomer2, customers1, cancellationTokenSource.Token)));
            tasks.Add(taskFactory.StartNew(() => SearchInList(ref neededcustomer2, customers2, cancellationTokenSource.Token)));
            tasks.Add(taskFactory.StartNew(() => SearchInList(ref neededcustomer2, customers3, cancellationTokenSource.Token)));
            tasks.Add(taskFactory.StartNew(() => SearchInList(ref neededcustomer2, customers4, cancellationTokenSource.Token)));
           
            await Task.WhenAny(tasks);            
            cancellationTokenSource.Cancel();
            timer.Stop();
            ResultTextBox.AppendText($"{neededcustomer2.ID}  {neededcustomer2.FirstName}  {neededcustomer2.LastName}  {neededcustomer2.Age}");

            ResultTextBox1.AppendText($"{timer.ElapsedMilliseconds} ms");



        }

        private void SearchInList(ref Customer neededcustomer, List<Customer> customers, CancellationToken cancellationToken)
        {
            foreach (var customer in customers)
            {
                
                if (customer.FirstName == "FirstName45" && customer.LastName == "LastName1195" && customer.Age == 54)
                {
                    neededcustomer = customer;
                    cancellationTokenSource.Cancel();
                    break;
                }

                
                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }
            }
        }

        private List<Customer> GenerateCustomer(int a)
        {
            Random random = new Random();
            List<Customer> customers = new List<Customer>();
            string[] FirstNames = new string[200];
            for (int i = 0; i < 200; i++)
            {
                FirstNames[i] = $"FirstName{i}";
            }
            string[] LastNames = new string[2000];
            for (int i = 0; i < 2000; i++)
            {
                LastNames[i] = $"LastName{i}";
            }

            for (int i = 0; i < N; i++)
            {
                customers.Add(new Customer
                {
                    ID = i+a,
                    Age = random.Next(20, 70),
                    FirstName = FirstNames[random.Next(0, FirstNames.Length)],
                    LastName = LastNames[random.Next(0, LastNames.Length)],
                });
            }

            return customers;
        }
    }
}
