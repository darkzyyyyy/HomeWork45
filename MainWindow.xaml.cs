using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public DataTable Select(string selectSQL) 
        {
            DataTable dataTable = new DataTable("dataBase");                
                                                                           
            SqlConnection sqlConnection = new SqlConnection("server=DESKTOP-AB9A3BT;Trusted_Connection=Yes;DataBase=HomeWork;");
            sqlConnection.Open();                                         
            SqlCommand sqlCommand = sqlConnection.CreateCommand();       
            sqlCommand.CommandText = selectSQL;                          
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); 
            sqlDataAdapter.Fill(dataTable);                                
            return dataTable;
        }
        

        string sql;
        DataTable data;
        public MainWindow()
        {
            InitializeComponent();
             data = Select("SELECT * FROM [dbo].[Customers]");
            DataGrid.ItemsSource = data.DefaultView;


        }

        private void ComboBox_Initialized(object sender, EventArgs e)
        {
            ComboBox.Items.Add("1.Найти покупателей, проживающих в городе Казань");
            ComboBox.Items.Add("2.Найти покупателей, фамилии которых начинаются с заданного символа");
            ComboBox.Items.Add("3.Найти покупателей, фамилии которых содержат заданную последовательность символов");
            ComboBox.Items.Add("4.Найти покупателей, фамилии которых оканчиваются заданным символом");
            ComboBox.Items.Add("5.Выдать список покупателей с указанием значения выражения Balance*100");
            ComboBox.Items.Add("6.Определить число поставщиков каждого товара");
            ComboBox.Items.Add("7.Найти минимальную цену заданного товара");
            ComboBox.Items.Add("8.Выдать упорядоченный по возрастанию цен список поставщиков заданного товара");
            ComboBox.Items.Add("9.Найти покупателей, некоторые заказы которых можно выполнить, не нарушая лимитирующего ограничения");
            ComboBox.Items.Add("10.Найти всех покупателей указанного товара");
            ComboBox.Items.Add("11.Найти максимальный по стоимости заказ");
            ComboBox.Items.Add("12.Найти все тройки <покупатель,поставщик,заказ>, удовлетворяющие условию");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboBox.SelectedIndex)
            {
                case 0:
                    sql = "SELECT * FROM Customers WHERE Adress LIKE '%Казань'";
                    data = Select(sql);
                    break;
                case 1:
                    sql = "SELECT * FROM Customers WHERE Family LIKE 'К%'";
                    data = Select(sql);
                    break;
                case 2:
                    sql = "SELECT * FROM Customers WHERE Family LIKE '%ь%'";
                    data = Select(sql);
                    break;
                case 3:
                    sql = "SELECT * FROM Customers WHERE Family LIKE '%в'";
                    data = Select(sql);
                    break;
                case 4:
                    sql = "SELECT Family, Balance *100 FROM Customers";
                    data = Select(sql);
                    break;
                case 5:
                    sql = "SELECT Name, Count (*) FROM Providers WHERE Name = Name GROUP BY Name";
                    data = Select(sql);
                    break;
                case 6:
                    sql = "SELECT Min(Price) FROM Providers where Commodity = 'Диван'";
                    data = Select(sql);
                    break;
                case 7:
                    sql = "SELECT Name,Price FROM Providers ORDER BY Price ASC,Name";
                    data = Select(sql);
                    break;
                case 8:
                    sql = "SELECT Customers.Family, Customers.Balance, Orders.Limit FROM Customers, Orders WHERE Customers.IdCs = Orders.IdCs AND Customers.Balance > Orders.Limit";
                    data = Select(sql);
                    break;
                case 9:
                    sql = "SELECT Customers.Family,Orders.Commodity FROM Customers,Orders WHERE Customers.IdCs = Orders.IdCs AND Orders.Commodity LIKE 'Диван'";
                    data = Select(sql);
                    break;
                case 10:
                    sql = "SELECT MAX(Orders.Limit) FROM Orders";
                    data = Select(sql);
                    break;
                case 11:
                    sql = "SELECT Customers.Family,Providers.Name,Orders.Commodity FROM Customers,Orders,Providers WHERE Customers.IdCs = Orders.IdCs AND Providers.Commodity = Orders.Commodity AND Providers.Adress LIKE '%Казань' AND Customers.Adress LIKE '%Казань' AND Customers.Balance > Orders.Limit";
                    data = Select(sql);
                    break;
            }
            TextBox.Text = sql;
            DataGrid.ItemsSource = data.DefaultView;

        }

   
        private void ComboboxParam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ComboboxParam.SelectedIndex)
            {
                case 0:
                    sql = $"SELECT * FROM Customers WHERE Address like '{TexBoxParam.Text}%'";
                    data = Select(sql);
                    break;
                case 1:
                    sql = $"SELECT * FROM Customers WHERE Family like '{TexBoxParam.Text}%'";
                    data = Select(sql);
                    break;
                case 2:
                    sql = $"SELECT * FROM Customers WHERE Family like '%{TexBoxParam.Text}%'";
                    data = Select(sql);
                    break;
                case 3:
                    sql = $"SELECT * FROM Customers WHERE Family like '%{TexBoxParam.Text}'";
                    data = Select(sql);
                    break;
            }
            TextBox.Text = sql;
            DataGrid.ItemsSource = data.DefaultView;
        }

        private void TexBoxParam_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TexBoxParam.Text = "";
        }

        private void TexBoxParam_TouchDown(object sender, TouchEventArgs e)
        {
            TexBoxParam.Text = "";
        }

        private void ComboboxParam_Initialized(object sender, EventArgs e)
        {
            ComboboxParam.Items.Add("1.Найти покупателей, проживающих в заданном городе ");
            ComboboxParam.Items.Add("2.Найти покупателей, фамилии которых начинаются с заданного символа");
            ComboboxParam.Items.Add("3.Найти покупателей, фамилии которых содержат заданную последовательность символов");
            ComboboxParam.Items.Add("4.Найти покупателей, фамилии которых оканчиваются заданным символом");

        }
    }
}
