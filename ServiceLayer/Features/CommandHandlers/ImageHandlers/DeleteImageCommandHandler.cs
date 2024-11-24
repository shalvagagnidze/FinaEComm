using Amazon.S3;
using Amazon.S3.Model;
using DomainLayer.Interfaces;
using MediatR;
using Microsoft.Extensions.Options;
using ServiceLayer.Features.Commands.ImageCommands;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.CommandHandlers.ImageHandlers
{
    public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand>
    {
        private readonly IAmazonS3 _s3;
        private readonly IOptions<S3Settings> _s3Settings;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteImageCommandHandler(IAmazonS3 s3, IOptions<S3Settings> s3Settings,IUnitOfWork unitOfWork)
        {
            _s3 = s3;
            _s3Settings = s3Settings;
            _unitOfWork = unitOfWork;
        }
        public async Task Handle(DeleteImageCommand request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.productId);

            var image = product.Images?.FirstOrDefault(x => x.Contains(request.key));

            var getRequest = new DeleteObjectRequest
            {
                BucketName = _s3Settings.Value.BucketName,
                Key = $"images/{request.productId}/{request.key}"
            };

            var a = await _s3.DeleteObjectAsync(getRequest,cancellationToken);

            product.Images?.Remove(image!);

            await _unitOfWork.SaveAsync();
        }
    }
}
