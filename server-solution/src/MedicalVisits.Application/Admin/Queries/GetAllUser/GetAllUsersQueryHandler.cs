using MediatR;
using MedicalVisits.Models.Dtos;
using MedicalVisits.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MedicalVisits.Application.Admin.Queries.GetAllUser;

    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<List<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var query = _userManager.Users;


                var users = await query.ToListAsync(cancellationToken);
                var userDtos = new List<UserDto>();

                
                
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    var userDto = new UserDto
                    {
                        Id = user.Id,
                        Email = user.Email,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        Role = roles.FirstOrDefault() ?? "No Role"
                    };
                    userDtos.Add(userDto);
                }

                return userDtos;
            }
            catch (Exception ex)
            {
                // Тут можна додати логування
                throw new ApplicationException("Error retrieving users", ex);
            }
        }
    }