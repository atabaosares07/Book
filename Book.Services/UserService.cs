using AutoMapper;
using Book.Data;
using Book.Data.Entities;
using Book.Dto;
using Book.Helpers;
using Book.Services.Base;
using Book.Services.Rules;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Book.Services
{
    public class UserService : IUserService
    {
        private DataContext context;

        public UserService(DataContext context)
        {
            this.context = context;
        }

        public async Task<UserDto> Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
                throw new UsernameIsRequiredException();

            if (string.IsNullOrEmpty(password))
                throw new PasswordIsRequiredException();

            var user = await context.Users
                        .Include(u => u.UserInRoles)
                            .ThenInclude(r => r.Role)
                        .Where(o => o.Username == username)
                        .SingleOrDefaultAsync();

            if (user == null)
                throw new UsernamePasswordIncorrectException();

            // check if password is correct
            if (!AuthHelper.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                throw new UsernamePasswordIncorrectException();

            // authentication successful
            return Mapper.Map<UserDto>(user);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return Mapper.Map<IEnumerable<UserDto>>(await context.Users.ToListAsync());
        }

        public async Task<UserDto> GetById(int id)
        {
            return Mapper.Map<UserDto>(await context.Users.Include(u => u.UserInRoles).SingleOrDefaultAsync(u => u.UserId == id));
        }

        public async Task<UserDto> Create(RegisterDto user)
        {
            var entity = Mapper.Map<User>(user);

            // validation
            if (string.IsNullOrWhiteSpace(user.Password))
                throw new PasswordIsRequiredException();

            if (await context.Users.Where(o => o.Username == user.Username).AnyAsync())
                throw new DuplicateConstraintException();

            byte[] passwordHash, passwordSalt;
            AuthHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            context.Add(entity);
            await context.SaveChangesAsync();

            return Mapper.Map<UserDto>(entity);
        }

        public async Task Update(UserDto dto, string password = null)
        {
            User userParam = Mapper.Map<User>(dto);

            var user = await context.Users.Where(o => o.UserId == userParam.UserId).SingleOrDefaultAsync();
            if (user == null)
                throw new NotFoundException();

            if (userParam.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (await context.Users.AnyAsync(x => x.Username == userParam.Username))
                    throw new DuplicateConstraintException();
            }

            // update user properties
            user.FirstName = userParam.FirstName;
            user.LastName = userParam.LastName;
            user.Username = userParam.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                AuthHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            context.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateRoles(int id, List<RoleDto> roles)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
                throw new NotFoundException();

            user.UserInRoles.Clear();
            foreach (var role in roles)
            {
                user.UserInRoles.Add(new UserInRole { RoleId = role.RoleId, UserId = user.UserId });
            }

            context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user != null)
            {
                context.Remove(user);
                await context.SaveChangesAsync();
            }
        }
    }
}
