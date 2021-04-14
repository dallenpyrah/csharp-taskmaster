using System;
using System.Collections.Generic;
using taskmaster.Models;
using taskmaster.Repositorys;

namespace taskmaster.Services
{
    public class TodosService
    {
        private readonly TodosRepository _repo;

        public TodosService(TodosRepository repo)
        {
            _repo = repo;
        }

        internal IEnumerable<Todo> GetAll()
        {
            return _repo.GetAll();
        }

        internal Todo GetOne(int id)
        {
            var todo = _repo.GetOne(id);
            if (todo == null)
            {
                throw new SystemException("Invalid Id");
            }
            else
            {
                return todo;
            }
        }

        internal Todo CreateOne(Todo newTodo)
        {
            return _repo.CreateOne(newTodo);
        }

        internal Todo EditOne(Todo editTodo)
        {
            Todo current = GetOne(editTodo.Id);
            editTodo.Description = editTodo.Description != null ? editTodo.Description : current.Description;
            editTodo.Title = editTodo.Title != null ? editTodo.Title : current.Title;
            return _repo.EditOne(editTodo);

        }

        internal String DeleteOne(int id, string userInfoId)
        {
            Todo current = GetOne(id);
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