using Azure.Data.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;

namespace WidgetAndCo.Services
{
    public class TablesService
    {
        private readonly TableClient _tableClient;

        public TablesService(TableClient tableClient)
        {
            _tableClient = tableClient;
        }

        public void InsertTableEntity(Order model)
        {
            TableEntity entity = new TableEntity();
            entity.PartitionKey = model.id;
            entity["OrderDate"] = DateTime.Now;

            _tableClient.AddEntity(entity);
        }

        public void UpsertTableEntity(Order model)
        {
            TableEntity entity = new TableEntity();
            entity.PartitionKey = model.id;
            
            entity["ShippingDate"] = DateTime.Now;

            _tableClient.UpsertEntity(entity);
        }
    }
}
