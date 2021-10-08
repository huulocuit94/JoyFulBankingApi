using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Deals;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Deals
{
    class AddDealCommandHandler : IRequestHandler<AddDealCommand, ResponseDto<Entity>>
    {
        private readonly IUnitOfWork unitOfWork;
        public AddDealCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<Entity>> Handle(AddDealCommand request, CancellationToken cancellationToken)
        {
            var deal = new Deal
            {
                Title = request.Title,
                Status = Shared.Enums.DealStatus.Open,
                Rules = request.Rules,
                Code = request.Code,
                Description = request.Description,
                CompaignId = request.CompaignId,
                CreatedByUserId = request.CurrentUserId,
                ModifiedByUserId = request.CurrentUserId,
                CategoryId = request.CategoryId
            };
            if (request.Picture != null)
            {
                var folderName = "Attachments";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var originalFileName = ContentDispositionHeaderValue.Parse(request.Picture.ContentDisposition).FileName.Trim('"');
                var fileName = string.Format("{0}_{1}", Guid.NewGuid(), originalFileName);
                var fullPath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    request.Picture.CopyTo(stream);
                    deal.FileData = string.Format("{0}/{1}", folderName, fileName);
                }
            }
            await unitOfWork.GetRepository<Deal>().AddAsync(deal);
            await unitOfWork.SaveChangesAsync();
            return new ResponseDto<Entity> { Result = new Entity { Id = deal.Id } };

        }
    }
}
