using CompanyStore.Data.Infrastructure;
using CompanyStore.Data.Repository;
using CompanyStore.Entity;
using CompanyStore.Service.Abstract;
using CompanyStore.Data.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Principal;

namespace CompanyStore.Service
{
    public class MembershipService : IMembershipService
    {
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<UserRole> _userRoleRepository;
        private readonly IEntityBaseRepository<Role> _roleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipService(
            IEntityBaseRepository<User> userRepository,
            IEntityBaseRepository<UserRole> userRoleRepository,
            IEntityBaseRepository<Role> roleRepository,
            IEncryptionService encryptionService,
            IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _encryptionService = encryptionService;
            _unitOfWork = unitOfWork;
        }

        public MembershipContext ValidateUser(string username, string password)
        {
            var membershipContext = new MembershipContext();
            var user = _userRepository.GetSingleByUsername(username);
            if (user != null && IsUserValid(user, password))
            {
                var roles = GetUserRole(user.Username);
                membershipContext.user = user;

                var identity = new GenericIdentity(username);
                membershipContext.Principal = new GenericPrincipal(
                    identity, roles.Select(r => r.Name).ToArray());
            }
            return membershipContext;
        }

        public User CreateUser(User user, string password, int[] roles)
        {
            var existUser = _userRepository.GetSingleByUsername(user.Username);
            if (existUser != null)
                throw new ApplicationException("Username is already in use.");
            var passwordSalt = _encryptionService.CreateSalt();
            var userToAdd = new User()
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = DateTime.Now,
                Salt = passwordSalt,
                HashedPassword = _encryptionService.EncryptPassword(password, passwordSalt)
            };
            _userRepository.Add(userToAdd);
            _unitOfWork.Commit();

            if (roles != null && roles.Length > 0)
            {
                foreach (var role in roles)
                    AddUserToRole(userToAdd, role);
                _unitOfWork.Commit();
            }
            return userToAdd;
        }

        public User GetUser(int id)
        {
            return _userRepository.GetSingle(id);
        }

        public List<Role> GetUserRole(string username)
        {
            List<Role> roles = new List<Role>();
            var existUser = _userRepository.GetSingleByUsername(username);
            if (existUser != null)
            {
                foreach (var userRole in existUser.UserRoles)
                    roles.Add(userRole.Role);
            }
            return roles.Distinct().ToList();
        }

        #region Helper Methods

        private void AddUserToRole(User user, int roleId)
        {
            var role = _roleRepository.GetSingle(roleId);
            if (role == null)
                throw new ApplicationException("Role does not exist.");
            var userRole = new UserRole()
            {
                RoleID = role.ID,
                UserID = user.ID
            };
            _userRoleRepository.Add(userRole);
        }

        private bool IsPasswordValid(User user, string password)
        {
            return string.Equals(_encryptionService.EncryptPassword(password, user.Salt), user.HashedPassword);
        }

        private bool IsUserValid(User user, string password)
        {
            if (IsPasswordValid(user, password))
                return !user.IsLocked;
            return false;
        }

        #endregion

    }
}
