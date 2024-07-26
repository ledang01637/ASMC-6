using ASMC6.Server.Data;
using ASMC6.Shared;
using System.Collections.Generic;
using System.Linq;

namespace ASMC6.Server.Service
{
    public class UserService
    {
        private AppDBContext _context;
        public UserService(AppDBContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetUser()
        {
            return _context.User.ToList();
        }
        public User AddUser(User User)
        {
            _context.Add(User);
            _context.SaveChanges();
            return User;
        }
        public User DeleteUser(int id)
        {
            var existingUser = _context.User.Find(id);
            if (existingUser == null)
            {
                return null;
            }
            else
            {
                _context.Remove(existingUser);
                _context.SaveChanges();
                return existingUser;
            }
        }
        public User GetIdUser(int id)
        {
            var prod = _context.User.Find(id);
            if (prod == null)
            {
                return null;
            }
            return prod;
        }
        public User UpdateUser(int id, User updateUser)
        {
            var existingUser = _context.User.Find(id);
            if (existingUser == null)
            {
                return null;
            }
            existingUser.RoleId = updateUser.RoleId;
            existingUser.Name = updateUser.Name;
            existingUser.Email = updateUser.Email;
            existingUser.Password = updateUser.Password;
            existingUser.Phone = updateUser.Phone;
            existingUser.Address = updateUser.Address;
            existingUser.IsDelete = updateUser.IsDelete;


            _context.Update(existingUser);
            _context.SaveChanges();
            return existingUser;
        }
    }
}
