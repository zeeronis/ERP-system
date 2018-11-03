using ERP_system.Entities;
using System;
using System.Collections.ObjectModel;

namespace ERP_system.Controllers
{
    public class ControllerBase<T> where T: IHaveId<T>
    {
        protected static Random rnd = new Random();

        protected ObservableCollection<T> _list = new ObservableCollection<T>();

        public ObservableCollection<T> GetList() => _list;

        public T GetItemById(string id)
        {
            foreach (var item in _list)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }
            return default(T);
        }

        public T GetItemByIndex(int index) => _list[index];

        public void Add(T item) => _list.Add(item);

        public void Save(T item)
        {
            foreach (T _item in _list)
            {
                if (_item.Id == item.Id)
                {
                    _item.UpdateItem(item);
                    break;
                }
            }
            _list.Add(item);
        }

        public void UpdateItem(T item, T newItem) => GetItemById(item.Id).UpdateItem(newItem);

        public void RemoveById(string id) => _list.Remove(GetItemById(id));

        public void Remove(T item) => _list.Remove(item);
    }
}
