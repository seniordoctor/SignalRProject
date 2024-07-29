﻿using SignalR.BusinessLayer.Abstract;
using SignalR.DataAccessLayer.Abstract;
using SignalR.EntityLayer.Entities;

namespace SignalR.BusinessLayer.Concrete;

public class ProductManager : IProductService
{
    private readonly IProductDal _productDal;

    public ProductManager(IProductDal productDal)
    {
        _productDal = productDal;
    }

    public void TAdd(Product entity)
    {
        _productDal.Add(entity);
    }

    public void TUpdate(Product entity)
    {
        _productDal.Update(entity);
    }

    public void TDelete(Product entity)
    {
        _productDal.Delete(entity);
    }

    public Product TGetById(int id)
    {
        return _productDal.GetById(id);
    }

    public List<Product> TGetListAll()
    {
        return _productDal.GetListAll();
    }
}