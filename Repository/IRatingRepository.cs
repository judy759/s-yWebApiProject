using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public interface IRatingRepository
    {
        Task<int> AddRating(Rating rating);
    }
}
