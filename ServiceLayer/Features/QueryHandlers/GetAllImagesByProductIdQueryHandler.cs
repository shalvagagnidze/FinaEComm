using AutoMapper;
using DomainLayer.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ServiceLayer.Features.Queries.ProductQueries;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Features.QueryHandlers
{
    public class GetAllImagesByProductIdQueryHandler : IRequestHandler<GetAllImagesByProductIdQuery, List<string>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public GetAllImagesByProductIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<string>> Handle(GetAllImagesByProductIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

            string imageUrl = string.Empty;
            List<string> images = new List<string>();

            try
            {
                //string Filepath = GetFilepath(productcode);
                List<string> imagepath = product.Images!;

                //foreach (var image in imagepath)
                //{
                //    if (System.IO.File.Exists(image))
                //    {
                //        imageUrl = request.hostUrl + "/Upload/product/" + request.Id + "/" + request.Id + ".png";
                //        imageUrl = $"{request.hostUrl.TrimEnd('/')}/Images/product/{request.Id}/{request.Id}.png";
                //        images.Add(imageUrl);
                //    }
                //    else
                //    {
                //        throw new FileNotFoundException();
                //    }
                //}

                return product.Images!;


            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
