﻿using Entities;

namespace Service
{
    public interface IProductService
    {
        Task<List<Product>> Get(string? descreption, int? min, int? max, string? name, int?[] categoryIds, int position = 20, int skip = 1);
        Task<List<Product>> Get();
    }
}