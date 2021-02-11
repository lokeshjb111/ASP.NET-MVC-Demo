using Demo.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage ="Name should not be greater than 30 characters")]
        public string Name { get; set; }
        public int? Age { get; set; }
        public float? Phone { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }
    }

    public interface IUser
    {
        public string addUser(User userViewModel);
        public List<User> getUsers();

    }

    public class UserRepository : IUser
    {
        private readonly DemoContext _context;
        public UserRepository(DemoContext context)
        {
            _context = context;
        }
        public string addUser(User userViewModel)
        {
            _context.Add(userViewModel);
            _context.SaveChanges();

            var xx = userViewModel.Id;
            return "Added";
        }
        public List<User> getUsers()
        {
            return _context.Users.ToList();
        }
    }
}
