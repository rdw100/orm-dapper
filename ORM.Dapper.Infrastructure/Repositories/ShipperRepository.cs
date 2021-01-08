﻿using Dapper;
using Microsoft.Extensions.Configuration;
using ORM.Dapper.Application.Interfaces;
using ORM.Dapper.Core.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace ORM.Dapper.Infrastructure.Repositories
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly IConfiguration configuration;

        public ShipperRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<int> AddAsync(Shipper entity)
        {
            var sql = "INSERT INTO Shippers (CompanyName,Phone) VALUES (@CompanyName,@Phone)";
            using (var connection = new SqlConnection(configuration.GetConnectionString("NorthwindContext")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }

        public async Task<int> DeleteAsync(int id)
        {
            var sql = "DELETE FROM Shippers WHERE ShipperId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("NorthwindContext")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { Id = id });
                return result;
            }
        }

        public async Task<IReadOnlyList<Shipper>> GetAllAsync()
        {
            var sql = "SELECT * FROM Shippers";
            using (var connection = new SqlConnection(configuration.GetConnectionString("NorthwindContext")))
            {
                connection.Open();
                var result = await connection.QueryAsync<Shipper>(sql);
                return result.ToList();
            }
        }

        public async Task<Shipper> GetByIdAsync(int id)
        {
            var sql = "SELECT * FROM Shippers WHERE ShipperId = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("NorthwindContext")))
            {
                connection.Open();
                var result = await connection.QuerySingleOrDefaultAsync<Shipper>(sql, new { Id = id });
                return result;
            }
        }

        public async Task<int> UpdateAsync(Shipper entity)
        {
            var sql = "UPDATE Shippers SET CompanyName = @CompanyName, Phone = @Phone WHERE Id = @Id";
            using (var connection = new SqlConnection(configuration.GetConnectionString("NorthwindContext")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, entity);
                return result;
            }
        }
    }
}