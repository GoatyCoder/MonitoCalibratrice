using MediatR;
using MonitoCalibratrice.Application.Features.FinishedProducts.Queries;
using MonitoCalibratrice.Application.Features.RawProducts.Queries;
using MonitoCalibratrice.Application.Features.SecondaryPackagings.Queries;
using MonitoCalibratrice.Application.Features.Varieties.Queries;
using static MonitoCalibratrice.Components.Pages.CreatePallet3;

namespace MonitoCalibratrice
{
    public interface ISearchService
    {
        Task<SearchEntityDto> SearchRawProductAsync(string code);
        Task<SearchEntityDto> SearchVarietyAsync(string code, Guid rawProductId);
        Task<SearchEntityDto> SearchFinishedProductAsync(string code);
        Task<SearchEntityDto> SearchSecondaryPackagingAsync(string code);
    }

    public class SearchService : ISearchService
    {
        private readonly IMediator _mediator;

        public SearchService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<SearchEntityDto> SearchRawProductAsync(string code)
        {
            var result = await _mediator.Send(new GetRawProductByCodeQuery(code));
            return result.IsSuccess ? new SearchEntityDto(result.Value.Id, result.Value.Code, result.Value.Name) : null;
        }

        public async Task<SearchEntityDto> SearchVarietyAsync(string code, Guid rawProductId)
        {
            var result = await _mediator.Send(new GetVarietyByCodeQuery(code, rawProductId));
            return result.IsSuccess ? new SearchEntityDto(result.Value.Id, result.Value.Code, result.Value.Name) : null;
        }

        public async Task<SearchEntityDto> SearchFinishedProductAsync(string code)
        {
            var result = await _mediator.Send(new GetFinishedProductByCodeQuery(code));
            return result.IsSuccess ? new SearchEntityDto(result.Value.Id, result.Value.Code, result.Value.Name) : null;
        }

        public async Task<SearchEntityDto> SearchSecondaryPackagingAsync(string code)
        {
            var result = await _mediator.Send(new GetSecondaryPackagingByCodeQuery(code));
            return result.IsSuccess ? new SearchEntityDto(result.Value.Id, result.Value.Code, result.Value.Name) : null;
        }
    }
}
