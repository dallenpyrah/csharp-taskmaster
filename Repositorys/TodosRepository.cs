using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using taskmaster.Models;

namespace taskmaster.Repositorys
{
    public class TodosRepository
    {
        private readonly IDbConnection _db;

        public TodosRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Todo> GetAll()
        {
            string sql = @"SELECT
            t.*,
            p.*
            FROM todos t
            JOIN profiles p ON t.creatorId = p.id;";
            return _db.Query<Todo, Profile, Todo>(sql, (todo, profile) =>
            {
                todo.Creator = profile;
                return todo;
            }, splitOn: "id");
        }

        internal Todo GetOne(int id)
        {
            string sql = @"SELECT
            t.*,
            p.*
            FROM todos t
            JOIN profiles p ON t.creatorId = p.id
            WHERE t.id = @id;";
            return _db.Query<Todo, Profile, Todo>(sql, (todo, profile) =>
            {
                todo.Creator = profile;
                return todo;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Todo CreateOne(Todo newTodo)
        {
            string sql = @"INSERT INTO todos
            (title, description, creatorId)
            VALUES
            (@Title, @Description, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newTodo);
            newTodo.Id = id;
            return newTodo;
        }

        internal Todo EditOne(Todo editTodo)
        {
            string sql = @"UPDATE todos
            SET
                title = @Title,
                description = @Description
            WHERE id = @Id;";
            _db.Execute(sql, editTodo);
            return editTodo;
        }

        internal void DeleteOne(int id)
        {
            string sql = "DELETE FROM todos WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}