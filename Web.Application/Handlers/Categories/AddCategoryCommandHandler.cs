using MediatR;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Web.Application.Commands.Categories;
using Web.Core.Dtos;
using Web.Core.Infrastructures;
using Web.Data.Models;

namespace Web.Application.Handlers.Categories
{
    public class AddCategoryCommandHandler: IRequestHandler<AddCategoryCommand, ResponseDto<Entity>>
    {
        private readonly IUnitOfWork unitOfWork;
        public AddCategoryCommandHandler(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<ResponseDto<Entity>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var newCategory = new Category
            {
                Name = request.Name,
                Description = request.Description
            };
            var isAvailableCategory = await unitOfWork.GetRepository<Category>().AnyAsync(x => x.Name.Equals(request.Name));
            if (isAvailableCategory)
            {
                return new ResponseDto<Entity> { Errors = new List<ErrorDto> { new ErrorDto { Code = 4004, Message = "Exist Category" } } };
            }
            else
            {
                if (request.Icon != null)
                {
                    var folderName = "Attachments";
                    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                    var originalFileName = ContentDispositionHeaderValue.Parse(request.Icon.ContentDisposition).FileName.Trim('"');
                    var fileName = string.Format("{0}_{1}", Guid.NewGuid(), originalFileName);
                    var fullPath = Path.Combine(pathToSave, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        request.Icon.CopyTo(stream);
                        newCategory.Icon = string.Format("{0}/{1}", folderName, fileName);
                    }
                }
                await unitOfWork.GetRepository<Category>().AddAsync(newCategory);
                await unitOfWork.SaveChangesAsync();
            }

            return new ResponseDto<Entity> { Result = new Entity { Id = newCategory.Id } };
        }
    }
}
