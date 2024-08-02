﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Concrete;
using SignalR.DtoLayer.FeatureDto;
using SignalR.DtoLayer.ProductDto;
using SignalR.EntityLayer.Entities;

namespace SignalRApi.Controllers;

[Route("api/[controller]")]
[ApiController]

public class ProductController : ControllerBase
{
    private readonly IProductService _productService;
    private readonly IMapper _mapper;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public IActionResult ProductList()
    {
        var value = _mapper.Map<List<ResultProductDto>>(_productService.TGetListAll());
        return Ok(value);
    }
    
    [HttpGet("GetProductsWithCategories")]
    public IActionResult GetProductsWithCategories()
    {
        var context = new SignalRContext();
        var values = context.Products.Include(x => x.Category)
            .Select(y => new ResultProductWithCategory
            {
                Description = y.Description,
                ImageUrl = y.ImageUrl,
                Price = y.Price,
                ProductId = y.ProductId,
                ProductName = y.ProductName,
                ProductStatus = y.ProductStatus,
                CategoryName = y.Category.CategoryName

            });
            return Ok(values.ToList());
    }
    
    [HttpPost]
    public IActionResult CreateProduct(CreateProductDto createProductDto)
    {
        _productService.TAdd(new Product()
        {
            ProductName = createProductDto.ProductName,
            Description = createProductDto.Description,
            Price = createProductDto.Price,
            ImageUrl = createProductDto.ImageUrl,
            ProductStatus = createProductDto.ProductStatus
        });
        return Ok("Ürün Bilgisi  Eklendi");
    }
    
    [HttpDelete]
    public IActionResult DeleteProduct(int id)
    {
        var value = _productService.TGetById(id);
        _productService.TDelete(value);
        return Ok("Öne Çıkan Bilgisi Silindi");
    }

    [HttpGet("GetProduct")]
    public IActionResult GetProduct(int id)
    {
        var value = _productService.TGetById(id);
        return Ok(value);
    }
    
    [HttpPut]
    public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
    {
        _productService.TUpdate(new Product()
        {
            ProductName = updateProductDto.ProductName,
            Description = updateProductDto.Description,
            Price = updateProductDto.Price,
            ImageUrl = updateProductDto.ImageUrl,
            ProductStatus = updateProductDto.ProductStatus,
            ProductId = updateProductDto.ProductId
            
        });
        return Ok("Öne Çıkan Bilgisi Güncellendi");
    }
}