using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Pharmacy.Application.Resources.Static;
using Pharmacy.Domain.Dto;
using Pharmacy.Domain.Entities.Identity;

namespace Pharmacy.Application.Features.AccountsManagement.Command.Create;

public class CreateAccountCommandHandler(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole>  roleManager, IMapper mapper)
 : BaseHandler<CreateAccountCommand, Result<string>>
 {
     private readonly UserManager<ApplicationUser> _userManager = userManager;
     private readonly RoleManager<ApplicationRole> _roleManager = roleManager;
     private readonly IMapper _mapper = mapper;

     public override async Task<Result<string>> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
     {
         var user = _mapper.Map<ApplicationUser>(request);

         var result = await _userManager.CreateAsync(user, request.Password);
         if (!result.Succeeded)
             return Result<string>.Fail(result.Errors.Select(x => x.Description).ToList());

         var role = await _roleManager.FindByIdAsync(request.RoleId.ToString());
         if (role == null)
             return Result<string>.Fail(Messages.RoleNotFound);

         await _userManager.AddToRoleAsync(user, role.Name!);

         return Result<string>.Success(Messages.AccountCreatedSuccessfully);
     }
 }


