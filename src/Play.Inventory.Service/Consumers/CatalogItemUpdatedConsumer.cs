using System.Threading.Tasks;
using MassTransit;
using Play.Catalog.Contracts;
using Play.Common;
using Play.Inventory.Service.Entities;

namespace Play.Inventory.Consumers
{
    public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
    {

        private readonly IRepository<CatalogItem> _repository;

        public CatalogItemUpdatedConsumer(IRepository<CatalogItem> repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
        {
            var message = context.Message;

            var item = await _repository.GetItemAsync(message.ItemId);

            if (item == null) 
            {
                item = new CatalogItem{
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description
                };

                await _repository.CreateAsync(item);
            }
            else 
            {
                item.Name = message.Name;
                item.Description = message.Description;

                await _repository.UpdateAsync(item);
            }
        }
    }
}