using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using OrdersService.ReadModels;

namespace OrdersService.Queries
{
    public class GetOrdersHandler
    {
        private readonly IConfiguration _configuration;

        public GetOrdersHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<OrderReadModel>> HandleAsync(
            int pageNumber = 1,
            int pageSize = 20)
        {
            var sql = @"
                SELECT 
                    o.Id,
                    o.CustomerId,
                    o.Status,
                    o.TotalAmount,
                    o.CreatedAt
                FROM Orders o
                ORDER BY o.CreatedAt DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY;
            ";

            using IDbConnection connection =
                new SqlConnection(_configuration.GetConnectionString("OrdersDb"));

            var result = await connection.QueryAsync<OrderReadModel>(
                sql,
                new
                {
                    Offset = (pageNumber - 1) * pageSize,
                    PageSize = pageSize
                });

            return result;
        }
    }
}
