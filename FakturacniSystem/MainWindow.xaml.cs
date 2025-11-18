using FakturacniSystem.Database;
using FakturacniSystem.Models;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Text;
using System.Windows;

namespace FakturacniSystem
{
    public partial class MainWindow : Window
    {
        public static readonly DataContext DbDataContext = new DataContext();
        public static readonly ContextMgr DbContextMgr = new ContextMgr(DbDataContext);

        public MainWindow()
        {
            InitializeComponent();

            InvoiceList.ItemsSource = DbContextMgr.GetAll();
        }

        public void NewInvoiceButtonClick(object sender, RoutedEventArgs e)
        {
            InvoiceBuilder invoiceBuilder = new InvoiceBuilder();
            invoiceBuilder.ShowDialog();

            if (InvoiceBuilder.finishedInvoice is null) return;

            DbContextMgr.Add(InvoiceBuilder.finishedInvoice);
        }
    }
}