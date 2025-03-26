using Challenge.Domain.DTOs.Logging;

namespace Challenge.Domain.Interfaces;

public interface IMongoDbLogger
{
    Task RegisterLog(ErrorLogDTO errorLogDTO);
}
