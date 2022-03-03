﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Core.Models.Entities.User;
using Core.IService;
using DTO.Account;
using Core.IService.User;
using Core;
using Core.IRepositories.User;
using DTO.Enumarations;
using System.Text.RegularExpressions;
using static DTO.Enumarations.UserEmums;
using System.Diagnostics;

namespace Service.Classes.User
{
    public class UserService : EntityService<AppUser>, IUserService, IEntityService<AppUser>
    {
        IUnitOfWork _unitOfWork;
        IUserRepository _userRepository;

        public UserService(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, userRepository)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }


        public async Task<MemberDTO> GetByEmail(string email)
        {
            return await _userRepository.GetUserByEmailAsync(email);
        }
        public async Task<MemberDTO> GetByUsername(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
        public async Task<IEnumerable<MemberDTO>> GetAllMembers()
        {
            return await _userRepository.GetAllMembers();
        }


        public async Task<bool> IsPasswordCurrect(int userId, string password)
        {
            var user = await GetById(userId);

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i])
                    return false;
            }

            return true;
        }
        public AppUser CreateAppUserForRegisteration(RegisterDTO registerDTO)
        {
            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDTO.UserName,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDTO.Password)),
                PasswordSalt = hmac.Key
            };

            return user;
        }
        public LoginInputType CheckUserInputForLogin(string input)
        {
            if (IsInputEmail(input))
                return LoginInputType.Email;
            
            if (IsInputPhone(input))
                return LoginInputType.Phone;

            return LoginInputType.Username;
        }
        
        public bool IsInputEmail(string email)
        {
            var trimmedEmail = email.Trim();

            if (trimmedEmail.EndsWith("."))
            {
                return false; // suggested by @TK-421
            }
            try
            {
                if (!Debugger.IsAttached)
                {
                    var addr = new System.Net.Mail.MailAddress(email);
                    return addr.Address == trimmedEmail;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        public bool IsInputPhone(string phone)
        {
            return Regex.Match(phone, @"^(\+[0-9]{9})$").Success;
        }
    }
}
