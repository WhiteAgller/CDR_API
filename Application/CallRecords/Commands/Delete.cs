using Domain.Interfaces;
using MediatR;

namespace Application.CallRecords.Commands;

public record DeleteCallRecordsCommand : IRequest<int>
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
}

public class DeleteCallRecordsCommandHandler : IRequestHandler<DeleteCallRecordsCommand, int>
{
    private readonly ICallRecordRepository _repository;

    public DeleteCallRecordsCommandHandler(ICallRecordRepository repository)
    {
        _repository = repository;
    }

    public Task<int> Handle(DeleteCallRecordsCommand request, CancellationToken cancellationToken)
    {
        return _repository.Delete(request.From, request.To, cancellationToken);
    }
}