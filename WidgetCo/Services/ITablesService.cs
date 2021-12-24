using Models;

namespace WidgetAndCo.Services
{
    public interface ITablesService
    {
        public void InsertTableEntity(Order model);
        public void UpsertTableEntity(Order model);
    }

}
