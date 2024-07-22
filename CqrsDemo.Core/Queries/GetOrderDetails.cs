using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Core.Queries
{
    public class GetOrderDetails : IQuery<GetOrderDetails.Result>
    {
        public GetOrderDetails(int orderId)
        {
            OrderId = orderId;
        }

        public int OrderId { get; set; }

        public class Result
        {
            public required int Id { get; set; }

            public required string OrderNumber { get; set; }

            public required string Name { get; set; }

            public required OrderStatusInfo Status { get; set; }

            public required ProductInfo Product { get; set; }

            public required DateTime? DeliveryDate { get; set; }

            public required List<Position> Positions { get; set; } = [];

            public class Position
            {
                public required int Id { get; set; }

                public required string Name { get; set; }

                public required int Quantity { get; set; }
            }
        }

        public class Handler(SalesContext context) : IRequestHandler<GetOrderDetails, Result>
        {
            public async Task<Result> Handle(GetOrderDetails query, CancellationToken cancellationToken)
            {
                return await (from o in context.Orders
                              where o.Id == query.OrderId
                              select new Result
                              {
                                  Id = o.Id,
                                  OrderNumber = o.OrderNumber,
                                  Name = o.Name,
                                  Status = (OrderStatusInfo)o.Status,
                                  DeliveryDate = o.DeliveryDate,
                                  Product = new ProductInfo() { Id = o.ProductId, Name = o.Product!.Name },
                                  Positions = o.Positions.Select(p =>
                                  new Result.Position()
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      Quantity = p.Quantity
                                  }).ToList()
                              }).SingleRequiredAsync(cancellationToken);
            }
        }
    }
}