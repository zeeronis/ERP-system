namespace ERP_system.Entities
{
    public interface IHaveId<T>
    {
        string Id { get; set; }

        void UpdateItem(T item);
    }
}
