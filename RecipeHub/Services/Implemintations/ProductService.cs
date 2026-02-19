using RecipeHub.Common;
using RecipeHub.DTOs.ProductDTOs;
using RecipeHub.Repositories.Interfaces;
using RecipeHub.Services.Interfaces;

namespace RecipeHub.Services.Implemintations;

public class ProductService : IProductService
{
    readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ServiceResponse<List<ProductDtoResponse>>> GetAllProductsAsync()
    {
        try
        {
            var productList = await _productRepository.GetAllProductsAsync();
            return ServiceResponse<List<ProductDtoResponse>>.SuccessResponse(productList,
                $"Products list fetched with {productList.Count} product(s)");
        }
        catch (Exception e)
        {
            return ServiceResponse<List<ProductDtoResponse>>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<ProductDtoResponse>> CreateProductAsync(ProductDtoRequest dto)
    {
        try
        {
            var isProductExist = await _productRepository.ValidateIfProductExistAsync(dto.Name);
            if (isProductExist)
            {
                return ServiceResponse<ProductDtoResponse>.ErrorResponse("Product already exist", 404);
            }

            var result = await _productRepository.CreateProductAsync(dto);
            return ServiceResponse<ProductDtoResponse>.SuccessResponse(result, "Product created");
        }
        catch (Exception e)
        {
            return ServiceResponse<ProductDtoResponse>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<ProductDtoResponse>> UpdateProductAsync(int id, ProductDtoRequest dto)
    {
        try
        {
            var isValidProduct = await _productRepository.ValidateProductAsync(id);
            if (!isValidProduct)
            {
                return ServiceResponse<ProductDtoResponse>.NotFoundResponse();
            }

            var result = await _productRepository.UpdateProductAsync(id, dto);
            return ServiceResponse<ProductDtoResponse>.SuccessResponse(result!, "Product updated");
        }
        catch (Exception e)
        {
            return ServiceResponse<ProductDtoResponse>.ErrorResponse(e.Message, 500);
        }
    }

    public async Task<ServiceResponse<bool>> DeleteProductAsync(int id)
    {
        try
        {
            var isValidProduct = await _productRepository.ValidateProductAsync(id);
            if (!isValidProduct)
            {
                return ServiceResponse<bool>.NotFoundResponse();
            }

            var result = await _productRepository.DeleteProductAsync(id);
            return ServiceResponse<bool>.SuccessResponse(result);
        }
        catch (Exception e)
        {
            return ServiceResponse<bool>.ErrorResponse(e.Message, 500);
        }
    }
}