using System;
using System.Collections.Generic;
using System.Data;
using DAL.Entities;
using DAL.Entities.Enums;
using DAL.Helpers;
using Microsoft.Data.SqlClient;

namespace DAL.Repositories.WorkerRepo
{
    /// <summary>
    /// Provide interface between SQL and <see cref="Worker"/> Entity
    /// </summary>
    public class WorkerRepository : IWorkerRepository
    {
        public IDbConnection Connection { get; set; }

        private string ConnectionString { get; init; }

        public WorkerRepository(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Send request to get all <see cref="Worker"/>s from database
        /// </summary>
        /// <returns>Enumerable of <see cref="Worker"/> entities</returns>
        public IEnumerable<Worker> GetAll()
        {
            var workers = new List<Worker>();
            var expression = "SELECT * FROM Worker";
            using var connection = new SqlConnection(ConnectionString);
            var dataReader = connection.ExecuteReader(expression);
            while (dataReader.Read())
            {
                workers.Add(new Worker
                {
                    Id = (int) dataReader["Id"],
                    FirstName = (string) dataReader["FirstName"],
                    LastName = (string) dataReader["LastName"],
                    Patronymic = (string) dataReader["Patronymic"],
                    EmploymentDate = (DateTime) dataReader["EmploymentDate"],
                    Position = (Position) Enum.Parse(typeof(Position), (string) dataReader["Position"]),
                    CompanyId = (int) dataReader["CompanyId"]
                });
            }

            return workers;
        }

        /// <summary>
        /// Get single record of <see cref="Worker"/>
        /// </summary>
        /// <param name="id"><see cref="Worker"/>'s identity number</param>
        /// <returns>Entity of <see cref="Worker"/></returns>
        public Worker GetById(int id)
        {
            var expression = $"SELECT * FROM Worker WHERE WorkerId = {id}";
            using var connection = new SqlConnection(ConnectionString);
            var dataReader = connection.ExecuteReader(expression);
            dataReader.Read();
            return new Worker
            {
                Id = (int) dataReader["Id"],
                FirstName = (string) dataReader["FirstName"],
                LastName = (string) dataReader["LastName"],
                Patronymic = (string) dataReader["Patronymic"],
                EmploymentDate = (DateTime) dataReader["EmploymentDate"],
                Position = (Position) Enum.Parse(typeof(Position), (string) dataReader["Position"]),
                CompanyId = (int) dataReader["CompanyId"]
            };
        }

        /// <summary>
        /// Create new record of <see cref="Worker"/>
        /// </summary>
        /// <param name="entity"><see cref="Worker"/></param>
        /// <returns>Entity of <see cref="Worker"/></returns>
        public Worker Insert(Worker entity)
        {
            var expression =
                "INSERT INTO Worker (LastName, FirstName, Patronymic, EmploymentDate, Position, CompanyId) VALUES" +
                $"('{entity.LastName}'," +
                $"'{entity.FirstName}'," +
                $"'{entity.Patronymic}'," +
                $"convert(date,'{entity.EmploymentDate}',104)," +
                $"'{entity.Position.ToString()}'," +
                $"{entity.CompanyId})";

            using var connection = new SqlConnection(ConnectionString);
            connection.Execute(expression);

            return entity;
        }

        /// <summary>
        /// Update fields of record by id
        /// </summary>
        /// <param name="id"><see cref="Worker"/>'s identity number</param>
        /// <param name="entity"><see cref="Worker"/></param>
        /// <returns>Entity of <see cref="Worker"/></returns>
        public Worker Update(int id, Worker entity)
        {
            var expression = "UPDATE Worker SET " +
                             $"FirstName = '{entity.FirstName}', " +
                             $"LastName = '{entity.LastName}', " +
                             $"Patronymic = '{entity.Patronymic}', " +
                             $"EmploymentDate = '{entity.EmploymentDate}', " +
                             $"Position =  '{entity.Position.ToString()}', " +
                             $"CompanyId = '{entity.CompanyId}', " +
                             $"WHERE Id = {id}";

            using var connection = new SqlConnection(ConnectionString);
            connection.Execute(expression);

            entity.Id = id;

            return entity;
        }

        /// <summary>
        /// Remove record from table by id
        /// </summary>
        /// <param name="id"><see cref="Worker"/>'s identity number</param>
        public void Delete(int id)
        {
            var expression = $"DELETE FROM Worker WHERE Id = {id}";

            using var connection = new SqlConnection(ConnectionString);
            connection.Execute(expression);
        }
    }
}