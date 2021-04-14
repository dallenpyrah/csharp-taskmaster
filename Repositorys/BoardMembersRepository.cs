using System;
using System.Data;
using Dapper;
using taskmaster.Models;

namespace taskmaster.Repositorys
{
    public class BoardMembersRepository
    {
        private readonly IDbConnection _db;

        public BoardMembersRepository(IDbConnection db)
        {
            _db = db;
        }

        internal BoardMember CreateOne(BoardMember newBoardMem)
        {
            string sql = @"INSERT INTO boardmembers
            (memberId, boardId, creatorId)
            VALUES
            (@MemberId, @BoardId, @CreatorId);
            SELECT LAST_INSERT_ID();";
            int id = _db.ExecuteScalar<int>(sql, newBoardMem);
            newBoardMem.Id = id;
            return newBoardMem;
        }

        internal BoardMember DeleteOne(int id)
        {
            string sql = "DELETE FROM boardmembers WHERE id = @id LIMIT 1;";
            return _db.QueryFirstOrDefault<BoardMember>(sql, new { id });
        }

        internal BoardMember GetOne(int id)
        {
            string sql = @"SELECT * FROM boardmembers WHERE id = @id;";
            return _db.QueryFirstOrDefault<BoardMember>(sql, new { id });
        }
    }
}