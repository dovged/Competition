﻿using Competition.Context;
using Competition.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Threading.Tasks;

namespace Competition
{
    public class AuthRepository : IDisposable
    {
        private DbContext _ctx;

        private UserManager<IdentityUser> _userManager;

        public AuthRepository()
        {
            _ctx = new DbContext();
            _userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(_ctx));
        }

        public async Task<IdentityResult> RegisterUser(AccountModel userModel)
        {
            IdentityUser user = new IdentityUser
            {
                UserName = userModel.UserName
            };

            var result = await _userManager.CreateAsync(user, userModel.Password);

            return result;
        }

        public async Task<IdentityUser> FindUser(string userName, string password)
        {
            IdentityUser user = await _userManager.FindAsync(userName, password);

            return user;
        }

        public void AddUserToRole(string username, string rolename)
        {
            var user = _userManager.FindByName(username);
            _userManager.AddToRole(user.Id, rolename);
            _ctx.SaveChanges();
        }
        public void Dispose()
        {
            _ctx.Dispose();
            _userManager.Dispose();

        }
    }
}