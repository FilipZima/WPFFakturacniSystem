using FakturacniSystem.Models;

namespace FakturacniSystem.Database
{
    public class ContextMgr
    {
        private readonly DataContext _context;

        public ContextMgr(DataContext context)
        {
            _context = context;
        }

        public void Add(InvoiceEntity entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }

        public void Remove(InvoiceEntity entity)
        {
            _context.Remove(entity);
            _context.SaveChanges();
        }

        public void Update(InvoiceEntity entity)
        {
            _context.Update(entity);
            _context.SaveChanges();
        }

        public InvoiceEntity? GetByTitle(string title)
        {
            foreach (InvoiceEntity e in _context.InvoiceSet)
            {
                if (e.Name == title) return e;
            }
            return null;
        }

        public InvoiceEntity? GetByID(int id) => _context.InvoiceSet.Find(id);

        public List<InvoiceEntity> GetAll() => _context.InvoiceSet.ToList();
    }
}
