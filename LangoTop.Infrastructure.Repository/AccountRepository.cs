﻿using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using LangoTop.Application.Contract.Account;
using LangoTop.Domain;
using LangoTop.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LangoTop.Infrastructure.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly LangoTopContext _context;

        public AccountRepository(LangoTopContext context) : base(context)
        {
            _context = context;
        }


        public Account GetBy(string email)
        {
            return _context.Accounts.FirstOrDefault(x => x.Email == email);
        }

        public EditAccount GetDetails(long id)
        {
            return _context.Accounts.Select(x => new EditAccount
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                Password = x.Password,
                ActiveCode = x.ActiveCode,
                Email = x.Email,
                RoleId = x.RoleId,
                Biography = x.Biography
            }).FirstOrDefault(x => x.Id == id);
        }

        public List<AccountViewModel> Search(AccountSearchModel searchModel)
        {
            var query = _context.Accounts.Include(x => x.Role).Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                IsActive = x.IsActive,
                Email = x.Email,
                CreationDate = x.CreationDate.ToFarsi(),
                RoleId = x.RoleId,
                Role = x.Role.Name,
                Biography = x.Biography
            });


            if (!string.IsNullOrWhiteSpace(searchModel.FullName))
                query = query.Where(x => x.Fullname.Contains(searchModel.FullName));

            if (!string.IsNullOrWhiteSpace(searchModel.Username))
                query = query.Where(x => x.Username.Contains(searchModel.Username));

            if (!string.IsNullOrWhiteSpace(searchModel.Email))
                query = query.Where(x => x.Email.Contains(searchModel.Email));

            if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
                query = query.Where(x => x.Mobile.Contains(searchModel.Mobile));

            //if (searchModel.RoleId > 0)
            //    query = query.Where(x => x.RoleId == searchModel.RoleId);


            return query.OrderByDescending(x => x.Id).ToList();
        }

        public List<AccountViewModel> GetAccounts()
        {
            return _context.Accounts.Include(x => x.Role).Select(x => new AccountViewModel
            {
                Id = x.Id,
                Fullname = x.Fullname,
                Username = x.Username,
                Mobile = x.Mobile,
                ProfilePhoto = x.ProfilePhoto,
                IsActive = x.IsActive,
                Email = x.Email,
                CreationDate = x.CreationDate.ToFarsi(),
                RoleId = x.RoleId,
                Role = x.Role.Name,
                Biography = x.Biography
            }).ToList();
        }

        public List<AccountViewModel> GetAdmins()
        {
            return _context.Accounts
                .Where(x => x.RoleId == int.Parse(Roles.Administrator) || x.RoleId == int.Parse(Roles.Admin))
                .Include(x => x.Role).Select(x => new AccountViewModel
                {
                    Id = x.Id,
                    Fullname = x.Fullname,
                    Username = x.Username,
                    Mobile = x.Mobile,
                    ProfilePhoto = x.ProfilePhoto,
                    IsActive = x.IsActive,
                    Email = x.Email,
                    CreationDate = x.CreationDate.ToFarsi(),
                    RoleId = x.RoleId,
                    Role = x.Role.Name,
                    Biography = x.Biography
                }).ToList();
        }

        public Account GetByCode(string activeCode)
        {
            return _context.Accounts.FirstOrDefault(x => x.ActiveCode == activeCode);
        }

    }
}