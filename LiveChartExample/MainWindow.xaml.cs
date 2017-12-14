using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using LiveCharts;
using LiveCharts.Wpf;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        private List<double> os = new List<double>();

        public MainWindow()
        {
            InitializeComponent();
            LoadChart();
        }

        private void LoadChart()
        {
            var strConexao = @"Data Source=localhost\sqlexpress2005; Initial Catalog=TestDB; Integrated Security=SSPI";
            try
            {
                using (var conn = new SqlConnection(strConexao))
                {
                    conn.Open();
                    var comm = new SqlCommand("SELECT * FROM PRODUTOS", conn);

                    var dr = comm.ExecuteReader();
                    while (dr.Read())
                    {
                        os.Add(Convert.ToDouble(dr["OS"]));
                    }

                    SeriesCollection = new SeriesCollection
                    {
                        new LineSeries
                        {
                            Values = new ChartValues<double>(os)
                        }
                    };
                    DataContext = this;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}