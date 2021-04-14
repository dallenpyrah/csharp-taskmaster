using System;
using System.Collections.Generic;
using taskmaster.Models;
using taskmaster.Repositorys;

namespace taskmaster.Services
{
    public class BoardsService
    {
        private readonly BoardsRepository _repo;

        public BoardsService(BoardsRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Board> GetAll()
        {
            return _repo.GetAll();
        }

        internal Board GetOne(int id)
        {
            var board = _repo.GetOne(id);
            if (board == null)
            {
                throw new SystemException("Invalid Id");
            }
            else
            {
                return board;
            }
        }

        internal Board CreateOne(Board newBoard)
        {
            return _repo.CreateOne(newBoard);
        }

        internal Board EditOne(Board editBoard)
        {
            Board current = GetOne(editBoard.Id);
            editBoard.Description = editBoard.Description != null ? editBoard.Description : current.Description;
            editBoard.Title = editBoard.Title != null ? editBoard.Title : current.Title;
            return _repo.EditOne(editBoard);

        }

        internal String DeleteOne(int id, string userInfoId)
        {
            Board current = GetOne(id);
            if (current.CreatorId != userInfoId)
            {
                throw new SystemException("You are not the creator");
            }
            else
            {
                _repo.DeleteOne(id);
                return "Deleted";
            }
        }
    }
}