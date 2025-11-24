using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FakturacniSystem.Models
{
    [PrimaryKey(nameof(Id))]
    public class InvoiceEntity : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; } = "NAME NOT FOUND";
        public Invoice _invoice = new Invoice();

        public event PropertyChangedEventHandler? PropertyChanged;

        public InvoiceEntity(Invoice invoice)
        {
            _invoice = invoice;
        }

        public Invoice Invoice
        {
            get => _invoice;
            set
            {
                _invoice = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(value)));
            }
        }

        public override string ToString() => Name;
    }
}
