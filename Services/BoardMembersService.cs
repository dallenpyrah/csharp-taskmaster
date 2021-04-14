using System;
using taskmaster.Models;
using taskmaster.Repositorys;

namespace taskmaster.Services
{
    public class BoardMembersService
    {

        private readonly BoardMembersRepository _repo;

        public BoardMembersService(BoardMembersRepository repo)
        {
            _repo = repo;
        }

        internal BoardMember CreateOne(BoardMember newBoardMem)
        {
            return _repo.CreateOne(newBoardMem);
        }

        internal BoardMember DeleteOne(int id, string userInfoId)
        {
            BoardMember board = _repo.GetOne(id);
            if (board == null)
            {
                throw new SystemException("Invalid Id");
            }
            else
            {
                return _repo.DeleteOne(id);
            }
        }
    }
}