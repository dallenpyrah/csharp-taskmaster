using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using taskmaster.Models;

namespace taskmaster.Repositorys
{
    public class BoardsRepository
    {
        private readonly IDbConnection _db;

        public BoardsRepository(IDbConnection db)
        {
            _db = db;
        }

        internal IEnumerable<Board> GetAll()
        {
            string sql = @"SELECT
            b.*,
            p.*
            FROM boards b
            JOIN profiles p ON b.creatorId = p.id;";
            return _db.Query<Board, Profile, Board>(sql, (board, profile) =>
            {
                board.Creator = profile;
                return board;
            }, splitOn: "id");
        }

        internal Board GetOne(int id)
        {
            string sql = @"SELECT
            b.*,
            p.*
            FROM boards b
            JOIN profiles p ON b.creatorId = p.id
            WHERE b.id = @id;";
            return _db.Query<Board, Profile, Board>(sql, (board, profile) =>
            {
                board.Creator = profile;
                return board;
            }, new { id }, splitOn: "id").FirstOrDefault();
        }

        internal Board CreateOne(Board newBoard)
        {
            string sql = @"INSERT INTO boards
            (title, description, open, creatorId)
            VALUES
            (@Title, @Description, @Open, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newBoard);
            newBoard.Id = id;
            return newBoard;
        }

        internal Board EditOne(Board editBoard)
        {
            string sql = @"UPDATE boards
            SET
                title = @Title,
                description = @Description
            WHERE id = @Id;";
            _db.Execute(sql, editBoard);
            return editBoard;
        }

        internal void DeleteOne(int id)
        {
            string sql = "DELETE FROM boards WHERE id = @id LIMIT 1;";
            _db.Execute(sql, new { id });
        }
    }
}