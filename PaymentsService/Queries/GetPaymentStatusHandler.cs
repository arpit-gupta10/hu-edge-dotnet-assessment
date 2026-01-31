using Dapper;
using Microsoft.Data.SqlClient;

namespace PaymentsService.Queries;

public class GetPaymentStatusHandler
{
    private readonly IConfiguration _configuration;

    public GetPaymentStatusHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<PaymentStatusReadModel?> HandleAsync(Guid orderId)
    {
        const string sql = """
            SELECT Id AS PaymentId,
                   OrderId,
                   Status,
                   Amount
            FROM Payments
            WHERE OrderId = @OrderId
        """;

        using var connection =
            new SqlConnection(_configuration.GetConnectionString("PaymentsDb"));

        return await connection.QueryFirstOrDefaultAsync<PaymentStatusReadModel>(
            sql, new { OrderId = orderId });
    }
}
