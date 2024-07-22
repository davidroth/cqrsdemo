using Microsoft.EntityFrameworkCore;

namespace CqrsDemo.Core.Queries
{
    public class GetDispoDeviceList : PagedQuery, IQuery<GetDispoDeviceList.Result>
    {
        public class Result
        {
            public required List<ListModel> Dispos { get; set; }
        }

        public class ListModel
        {
            public required int Id { get; set; }
            public required string Name { get; set; }
            public required DateTime AvailabilityDate { get; set; }
        }

        public class Handler(SalesContext context) : IRequestHandler<GetDispoDeviceList, Result>
        {
            public async Task<Result> Handle(GetDispoDeviceList query, CancellationToken cancellationToken)
            {
                return new Result
                {
                    Dispos = await (from o in context.DispoDevices
                                    select new ListModel
                                    {
                                        Id = o.Id,
                                        Name = o.Name,
                                        AvailabilityDate = o.AvailabilityDate,
                                    })
                                    .ToListAsync()
                };
            }
        }
    }
}