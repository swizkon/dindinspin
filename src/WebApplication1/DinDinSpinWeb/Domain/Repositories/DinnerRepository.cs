using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using DinDinSpin.Domain.Models;
using DinDinSpinWeb.Infra.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Domain.Repositories
{
    public class DinnerRepository
    {
        private readonly DinDinSpinDbContext _dbContext;

        public DinnerRepository(DinDinSpinDbContext dbContext)
        {
            _dbContext = dbContext;

            if (_dbContext.Dinners.Any())
            {
                return;
            }

            var spinId = Guid.NewGuid();
            var spinner = new Spinner("Fam Jer")
            {
                Id = spinId,
                Dinners = new List<Dinner>()
                {
                    new Dinner("Tacopaj", DateTime.Now)
                    {
                        SpinnerId = spinId,
                        MainIngredient = new Ingredient("Sak")
                    },
                    new Dinner("Truck", DateTime.Now)
                    {
                        SpinnerId = spinId,
                        MainIngredient = new Ingredient("Majs")
                    },
                    new Dinner("AGV", DateTime.Now)
                    {
                        SpinnerId = spinId,
                        MainIngredient = new Ingredient("Oil")
                    }
                }
            };
            _dbContext.Spinners.Add(spinner);
            _dbContext.SaveChanges();
        }

        public async Task<List<T>> GetAll<T>()
        {
            return await GetQuery().ProjectTo<T>().ToListAsync();
        }

        public async Task<IEnumerable<Dinner>> GetAll()
        {
            return await _dbContext
                .Dinners
                .Include(x => x.Spinner)
                .Include(x => x.MainIngredient)
                .ToListAsync();
        }

        public Dinner Get(int id)
        {
            return GetQuery().Single(x => x.Id == id);
        }

        public IIncludableQueryable<Dinner, Spinner> GetQuery()
        {
            return _dbContext
                .Dinners
                .Include(x => x.MainIngredient)
                .Include(x => x.Spinner);
        }
    }
}