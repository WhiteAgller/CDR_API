using Domain.CallRecordAggregate;
using Domain.Interfaces;
using MediatR;

namespace Application.CallRecords.Queries;
public record GetByIdQuery : IRequest<CallRecord?>
{
    public long Id { get; set; }
}

public class GetByIdHandler : IRequestHandler<GetByIdQuery, CallRecord?>
{
    private readonly ICallRecordRepository _repository;

    public GetByIdHandler(ICallRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<CallRecord?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetById(request.Id, cancellationToken);
    }
}
