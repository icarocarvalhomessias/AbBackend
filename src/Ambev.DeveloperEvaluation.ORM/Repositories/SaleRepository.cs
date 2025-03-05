using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DefaultContext _context;
        public SaleRepository(DefaultContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Sale sale, CancellationToken cancellationToken)
        {
            await _context.Sales.AddAsync(sale, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Sales.ToListAsync(cancellationToken);
        }

        public async Task<Sale> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.Sales
                .Include(s => s.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<IEnumerable<Sale>> GetByCustomerIdAsync(Guid customerId, CancellationToken cancellationToken)
        {
            return await _context.Sales
                .Where(s => s.UserId == customerId)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<Sale>> GetSales(int page, int pageSize, string order, CancellationToken cancellationToken)
        {
            var query = _context.Sales.AsQueryable();

            //// Apply ordering
            //if (!string.IsNullOrEmpty(order))
            //{
            //    var orderParams = order.Split(' ');
            //    var propertyName = orderParams[0];
            //    var direction = orderParams.Length > 1 ? orderParams[1] : "asc";

            //    if (direction.ToLower() == "desc")
            //    {
            //        query = query.OrderByDescending(e => EF.Property<object>(e, propertyName));
            //    }
            //    else
            //    {
            //        query = query.OrderBy(e => EF.Property<object>(e, propertyName));
            //    }
            //}

            // Apply pagination
            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            return await query.
                Include(u => u.User).
                Include(s => s.Items)
                    .ThenInclude(i => i.Product).
                ToListAsync(cancellationToken);
        }

        public async Task UpdateAsync(Sale sale, CancellationToken cancellationToken)
        {
            _context.Sales.Update(sale);
            await _context.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteAsync(Sale sale, CancellationToken cancellationToken)
        {
            sale.Cancel();
            await UpdateAsync(sale, cancellationToken);
        }
    }
}
