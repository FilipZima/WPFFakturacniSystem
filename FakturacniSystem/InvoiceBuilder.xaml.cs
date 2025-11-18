using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace FakturacniSystem
{
    /// <summary>
    /// Interaction logic for InvoiceBuilder.xaml
    /// </summary>
    public partial class InvoiceBuilder : Window
    {
        public static Invoice? finishedInvoice;

        List<InvoiceItem> items = new List<InvoiceItem>();

        Dictionary<string, Currency> supportedCurrencies = new Dictionary<string, Currency>()
        {
            { "CZK", Currency.CZK },
            { "USD", Currency.USD },
        };

        public InvoiceBuilder()
        {
            InitializeComponent();

            ItemCurrencyCombo.ItemsSource = supportedCurrencies.Keys;
            ItemCurrencyCombo.SelectedIndex = 0;
        }

        public void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            string srcName, srcCIN, destName, destCIN, title;
            srcName = SrcName.Text;
            srcCIN = SrcCIN.Text;
            destName = DestName.Text;
            destCIN = DestCIN.Text;
            title = InvoiceTitle.Text;
            DateTime date = DatePicker.SelectedDate ?? DateTime.Now;

            if (items.Count < 1)
            {
                MessageBox.Show("Add at least one item");
                return;
            }

            if (string.IsNullOrEmpty(srcName) 
                || string.IsNullOrEmpty(srcCIN) 
                || string.IsNullOrEmpty(destName) 
                || string.IsNullOrEmpty(destCIN)
                || string.IsNullOrEmpty(title))
            {
                MessageBox.Show("Invalid company details");
                return;
            }

            Invoice invoice = new Invoice()
            {
                title = title,
                sourceCIN = srcCIN,
                sourceCompanyName = srcName,
                destinationCIN = destCIN,
                destCompanyName = destName,
            };

            foreach (InvoiceItem item in items)
            {
                invoice.AddItem(item);
            }

            finishedInvoice = invoice;

            Close();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                InvoiceItem? item = InvoiceDisplay.SelectedItem as InvoiceItem;
                if (item != null)
                {
                    items.Remove(item);
                    RefreshDisplay();
                }
            }

            base.OnKeyUp(e);
        }

        public void ConfirmButtonClick(object sender, RoutedEventArgs e)
        {
            string name = ItemNameInput.Text;

            bool flowControl = ScrapeAmount(out int amount);
            if (!flowControl)
            {
                return;
            }

            flowControl = ScrapeCost(out float cost);
            if (!flowControl)
            {
                return;
            }

            flowControl = ScrapeCurrency(out Currency currency);
            if (!flowControl)
            {
                return;
            }

            flowControl = ScrapeTax(out float taxRate);
            if (!flowControl)
            {
                return;
            }

            InvoiceItem newItem = new InvoiceItem(name, amount, cost, taxRate * 0.01f, currency);
            items.Add(newItem);
            RefreshDisplay();
        }

        private bool ScrapeAmount(out int amount)
        {
            string amountText = ItemAmountInput.Text;
            if (!int.TryParse(amountText, out amount))
            {
                MessageBox.Show("Invalid amount");
                return false;
            }

            return true;
        }

        private bool ScrapeCost(out float cost)
        {
            string costText = ItemCostInput.Text;
            if (!float.TryParse(costText, out cost))
            {
                MessageBox.Show("Invalid cost");
                return false;
            }

            return true;
        }

        private bool ScrapeCurrency(out Currency currency)
        {
            if (ItemCurrencyCombo.SelectedItem is string currencyText)
            {
                if (supportedCurrencies.ContainsKey(currencyText))
                {
                    currency = supportedCurrencies[currencyText];
                }
                else
                {
                    MessageBox.Show("Unsupported currency");
                    currency = Currency.CZK;
                    return false;
                }
            }
            else
            {
                MessageBox.Show("No currency selected");
                currency = Currency.CZK;
                return false;
            }

            return true;
        }

        private bool ScrapeTax(out float taxRate)
        {
            string taxText = ItemTaxInput.Text;
            if (!float.TryParse(taxText, out taxRate))
            {
                MessageBox.Show("Invalid tax rate");
                return false;
            }

            return true;
        }

        public void RefreshDisplay()
        {
            InvoiceDisplay.ItemsSource = null;
            InvoiceDisplay.ItemsSource = items;
        }

    }
}
