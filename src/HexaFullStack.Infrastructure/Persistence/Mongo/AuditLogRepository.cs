using MongoDB.Driver;

namespace HexaFullStack.Infrastructure.Persistence.Mongo;

public sealed class AuditLogRepository
{
    private readonly IMongoCollection<AuditLog> _col;
    public AuditLogRepository(MongoSettings s)
    {
        var client = new MongoClient(s.ConnectionString);
        _col = client.GetDatabase(s.Database).GetCollection<AuditLog>("AuditLogs");
    }
    public Task InsertAsync(AuditLog log, CancellationToken ct) => _col.InsertOneAsync(log, cancellationToken: ct);
}

public sealed record AuditLog(string Type, object Payload, DateTime AtUtc);
