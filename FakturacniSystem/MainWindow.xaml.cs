using FakturacniSystem.Database;
using Microsoft.Win32;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace FakturacniSystem
{
    public partial class MainWindow : Window
    {
        public static Invoice? SelectedInvoice { get; private set; } = null;

        public static readonly DataContext DbDataContext = new DataContext();
        public static readonly ContextMgr DbContextMgr = new ContextMgr(DbDataContext);

        public MainWindow()
        {
            InitializeComponent();
            InvoiceList.ItemsSource = DbContextMgr.GetAll();
            InvoiceList.SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (InvoiceList.SelectedItem is Invoice selectedInvoice)
            {
                SelectedInvoice = selectedInvoice;
                OpenInvoiceButton.IsEnabled = true;

                InvoicePreview.Text = SelectedInvoice.ToPage();

                ExportButton.IsEnabled = true;
            }
            else
            {
                SelectedInvoice = null;
                OpenInvoiceButton.IsEnabled = false;

                InvoicePreview.Text = string.Empty;

                ExportButton.IsEnabled = false;
            }
        }

        public void NewInvoiceButtonClick(object sender, RoutedEventArgs e)
        {
            SelectedInvoice = null;
            HandleBuilder();
        }

        private void OpenInvoiceButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedInvoice is null) return;
            HandleBuilder();
        }

        private void HandleBuilder()
        {
            string? oldTitle = SelectedInvoice?.title;

            InvoiceBuilder invoiceBuilder = new InvoiceBuilder();
            invoiceBuilder.ShowDialog();

            if (InvoiceBuilder.FinishedInvoice == null) return;

            Invoice? foundInvoice = DbContextMgr.GetByTitle(InvoiceBuilder.FinishedInvoice.title);
            if (oldTitle is not null) foundInvoice ??= DbContextMgr.GetByTitle(oldTitle);
            if (foundInvoice is not null)
            {
                DbContextMgr.Remove(foundInvoice);
            }

            DbContextMgr.Add(InvoiceBuilder.FinishedInvoice);

            InvoiceList.ItemsSource = null;
            InvoiceList.ItemsSource = DbContextMgr.GetAll();
        }

        private void ExportButtonClick(object sender, RoutedEventArgs e)
        {
            if (SelectedInvoice is null) return;

            FileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = SelectedInvoice.title,
                DefaultExt = ".txt",
                Filter = "Text documents (.txt)|*.txt",
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                // Save to file
                File.WriteAllText(saveFileDialog.FileName, SelectedInvoice.ToPage(), Encoding.UTF8);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.Key is Key.Enter && SelectedInvoice is not null)
            {
                InvoiceBuilder invoiceBuilder = new InvoiceBuilder();
                invoiceBuilder.ShowDialog();
            }

            base.OnKeyDown(e);
        }
    }
}