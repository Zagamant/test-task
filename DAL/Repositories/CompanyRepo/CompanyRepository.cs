using System;
using System.Collections.Generic;
using System.Data;
using DAL.Entities;
using DAL.Entities.Enums;
using DAL.Helpers;
using Microsoft.Data.SqlClient;

namespace DAL.Repositories.CompanyRepo
{
    /// <summary>
    /// Provide interface between SQL and Company Entity
    /// </summary>
    public class CompanyRepository : ICompanyRepository
    {
        public IDbConnection Connection { get; set; }

        private string ConnectionString { get; init; }

        public CompanyRepository(string connectionString)
        {
            ConnectionString = connectionString;
            Connection = new SqlConnection(ConnectionString);
        }

        /// <summary>
        /// Send request to get all Companies from database
        /// </summary>
        /// <returns>Enumerable of Company entities</returns>
        public IEnumerable<Company> GetAll()
        {
            var companies = new List<Company>();
            var request = "SELECT * FROM Company";
            using var connection = new SqlConnection(ConnectionString);
            var dataReader = connection.ExecuteReader(request);
            while (dataReader.Read())
            {
                companies.Add(new Company()
                {
                    Id = (int) dataReader["Id"],
                    Title = (string) dataReader["Name"],
                    TypeOfBusiness =
                        (TypeOfBusiness) Enum.Parse(typeof(TypeOfBusiness), (string) dataReader["TypeOfBusiness"])
                });
            }

            return companies;
        }

        /// <summary>
        /// Get single record of <see cref="Company"/>
        /// </summary>
        /// <param name="id"><see cref="Company"/>'s identity number</param>
        /// <returns>Record of <see cref="Company"/></returns>
        public Company GetById(int id)
        {
            var expression = $"SELECT * FROM Company WHERE Id = {id}";
            using var connection = new SqlConnection(ConnectionString);
            var dataReader = connection.ExecuteReader(expression);
            dataReader.Read();
            return new Company
            {
                Id = (int) dataReader["Id"],
                Title = (string) dataReader["Name"],
                TypeOfBusiness =
                    (TypeOfBusiness) Enum.Parse(typeof(TypeOfBusiness), (string) dataReader["TypeOfBusiness"])
            };
        }

        /// <summary>
        /// Create new record of <see cref="Company"/>
        /// </summary>
        /// <param name="entity"><see cref="Company"/></param>
        /// <returns>Entity of <see cref="Company"/></returns>
        public Company Insert(Company entity)
        {
            var expression = "INSERT INTO Company (Name, TypeOfBusiness) VALUES" +
                             $"('{entity.Title}'," +
                             $"{entity.TypeOfBusiness.ToString()}";

            using var connection = new SqlConnection(ConnectionString);
            connection.Execute(expression);

            return entity;
        }

        /// <summary>
        /// Update fields of record by id
        /// </summary>
        /// <param name="id"><see cref="Company"/>'s identity number</param>
        /// <param name="entity"><see cref="Company"/></param>
        /// <returns>Entity of <see cref="Company"/></returns>
        public Company Update(int id, Company entity)
        {
            var expression = "UPDATE Company SET " +
                             $"Name = '{entity.Title}', " +
                             $"TypeOfBusiness = '{entity.TypeOfBusiness}'" +
                             $"WHERE Id = {id}";

            using var connection = new SqlConnection(ConnectionString);
            connection.Execute(expression);

            entity.Id = id;

            return entity;
        }

        /// <summary>
        /// Remove record from table by id
        /// </summary>
        /// <param name="id"><see cref="Company"/>'s identity number</param>
        public void Delete(int id)
        {
            var expression = $"DELETE FROM Company WHERE Id = {id}";

            using var connection = new SqlConnection(ConnectionString);
            connection.Execute(expression);
        }
    }
}