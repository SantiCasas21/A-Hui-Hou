using AHuiHou.Application.Common;
using AHuiHou.Application.DTOs.Requests;
using AHuiHou.Application.DTOs.Responses;
using AHuiHou.Application.Interfaces;
using AHuiHou.Domain.Entities;
using AHuiHou.Domain.Interfaces;

namespace AHuiHou.Application.Services;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

    public async Task<Result<IEnumerable<ProductResponse>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var products = await _unitOfWork.Products.GetAllAsync(cancellationToken);
        var response = products.Select(p => new ProductResponse(
            p.Id, p.Name, p.CategoryId, p.Category?.Name ?? "", p.Price, p.PointsAwarded));

        return Result<IEnumerable<ProductResponse>>.Success(response);
    }

    public async Task<Result<ProductResponse>> CreateAsync(CreateProductRequest request, CancellationToken cancellationToken = default)
    {
        var product = new Product
        {
            Name = request.Name,
            CategoryId = request.CategoryId,
            Price = request.Price,
            PointsAwarded = request.PointsAwarded
        };

        await _unitOfWork.Products.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result<ProductResponse>.Success(new ProductResponse(
            product.Id, product.Name, product.CategoryId, "", product.Price, product.PointsAwarded));
    }
}

