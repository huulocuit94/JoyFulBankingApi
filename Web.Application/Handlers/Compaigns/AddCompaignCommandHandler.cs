using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Compaigns
{
    class AddCompaignCommandHandler : IRequestHandler<AddCompaignCommand, ResponseDto<Entity>>
    {
        private readonly IUnitOfWork unitOfWork;
        public AddCompaignCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<Entity>> Handle(AddCompaignCommand request, CancellationToken cancellationToken)
        {
            var compaign = new Compaign
            {
                Name = request.Name,
                Status = Shared.Enums.CompaignStatus.Open,
                ExpiredDate = request.ExpiredDate,
                Description = request.Description,
                CreatedByUserId = request.CurrentUserId,
                ModifiedByUserId = request.CurrentUserId
            };
            if(request.Picture!= null)
            {
                var folderName = "Attachments";
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var originalFileName = ContentDispositionHeaderValue.Parse(request.Picture.ContentDisposition).FileName.Trim('"');
                var fileName = string.Format("{0}_{1}", Guid.NewGuid(), originalFileName);
                var fullPath = Path.Combine(pathToSave, fileName);
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    request.Picture.CopyTo(stream);
                    compaign.FileData = string.Format("{0}/{1}", folderName, fileName);
                }
            }
            await unitOfWork.GetRepository<Compaign>().AddAsync(compaign);
            await unitOfWork.SaveChangesAsync();
            return new ResponseDto<Entity> { Result = new Entity { Id = compaign.Id } };
        }

    }
}
