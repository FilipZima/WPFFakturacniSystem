using FakturacniSystem.Database;
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

            InvoiceItem[] invoiceItems = [
                new("Item1", 3, 4, 0.21f, Currency.CZK),
                new("Item2", 5, 100, 0.21f, Currency.CZK),
                new("Item3", 2, 400, 0.31f, Currency.USD),
                new("Item4", 1, 800, 0.51f, Currency.CZK),
                new("Item2", 8, 100, 0.21f, Currency.CZK),
                new("Item6", 3, 5000, 0.21f, Currency.CZK),
                ];

            Invoice invoice = new Invoice();
            foreach (var invoiceItem in invoiceItems)
            {
                invoice.AddItem(invoiceItem);
            }
        }

        public void NewInvoiceButtonClick(object sender, RoutedEventArgs e)
        {
            InvoiceBuilder invoiceBuilder = new InvoiceBuilder();
            invoiceBuilder.ShowDialog();
        }
    }
}