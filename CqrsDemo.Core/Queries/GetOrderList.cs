using CqrsDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Core.Queries
{
    public class GetOrderList : PagedQuery, IQuery<GetOrderList.Result>, IProvideCachingKey
    {
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        string IProvideCachingKey.GetCacheKey() => string.Concat(From, Skip, Take, To);

        public class Handler : IRequestHandler<GetOrderList, Result>
        {
            private readonly SalesContext context;
            public Handler(SalesContext context) => this.context = context;

            public async Task<Result> Handle(GetOrderList query, CancellationToken cancellationToken)
            {
                return new Result
                {
                    Orders = await (from o in context.Orders
                                    select new Dto
                                    {
                                        Id = o.Id,
                                        OrderNumber = o.OrderNumber,
                                        Name = o.Name,
                                        Status = (OrderStatusInfo)o.Status,
                                        DeliveryDate = o.DeliveryDate,
                                        Product = new ProductInfo()
                                        {
                                            Id = o.ProductId,
                                            Name = o.Product!.Name
                                        },
                                        DispoName = o.DispoDevice!.Name,
                                    }).ToListAsync()
                };
            }
        }

        public class Result
        {
            public required List<Dto> Orders { get; set; }
        }

        public class Dto
        {
            public required int Id { get; set; }

            public required string OrderNumber { get; set; }

            public required string Name { get; set; }

            public required OrderStatusInfo Status { get; set; }

            public required DateTime? DeliveryDate { get; set; }
            public required ProductInfo Product { get; set; }
            public required string DispoName { get; set; }
        }
    }
}