using Application.CallRecords.BaseClasses;
using Domain.CallRecordAggregate;
using Domain.Interfaces;
using MediatR;

namespace Application.CallRecords.Queries;

public record GetAllQuery : BaseDateQuery, IRequest<List<CallRecord>>
{
    public int? Skip { get; set; }

    public int? Take { get; set; }
}

public class GetAllHandler : IRequestHandler<GetAllQuery, List<CallRecord>>
{
    private readonly ICallRecordRepository _repository;

    public GetAllHandler(ICallRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<CallRecord>> Handle(GetAllQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.DateFrom, request.DateTo, request.Skip, request.Take, cancellationToken);
    }
}