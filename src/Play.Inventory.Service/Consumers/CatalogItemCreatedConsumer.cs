using System.Threading.Tasks;
using MassTransit;
using Play.Catalog.Contracts;
using Play.Common;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Consumers
{
    public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
    {

        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemCreatedConsumer(IRepository<CatalogItem> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemCreated> context)
        {
            var message = context.Message;

            var item = await _repository.GetItemAsync(message.ItemId);

            if(item!=null) return;
            item = new CatalogItem{
                Id = item.Id,
                Name = item.Name,
                Description = item.Description
            };

            await _repository.CreateAsync(item);
        }
    }
}