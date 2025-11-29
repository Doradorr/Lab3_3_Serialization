using System.Collections.Generic;
using MyApp.DAL;

namespace MyApp.BLL
{
    
    public class EntityService
    {
        private readonly EntityContext _context;

        public EntityService(EntityContext context)
        {
            _context = context;
        }

        public List<Vector> GetAll()
        {
            return _context.Open();
        }

        public void Add(Vector v)
        {
            if (v == null) return;
            var list = _context.Open();
            list.Add(v);
            _context.Update(list);
        }

        public void RemoveAt(int index)
        {
            var list = _context.Open();
            if (index < 0 || index >= list.Count) return;
            list.RemoveAt(index);
            _context.Update(list);
        }

        public List<Vector> FindByColor(string color)
        {
            var list = _context.Open();
            if (string.IsNullOrEmpty(color)) return new List<Vector>();
            return list.FindAll(v => v.LineColor?.ToLower() == color.ToLower());
        }

        public void SaveAll(List<Vector> list)
        {
            _context.Update(list);
        }
    }
}

