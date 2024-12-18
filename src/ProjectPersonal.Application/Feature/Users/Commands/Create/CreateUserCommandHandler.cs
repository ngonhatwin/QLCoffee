using AutoMapper;
using MediatR;
using ProjectPersonal.Application.Common.Interfaces.Repository;
using ProjectPersonal.Application.Common.Interfaces.Result;
using ProjectPersonal.Application.Feature.Users.Validation;
using ProjectPersonal.Domain.Enum;
namespace ProjectPersonal.Application.Feature.Users.Commands.Create
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Result<Guid>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitofwork<User> _unitOfWork;
        public CreateUserCommandHandler(IMapper mapper, IUnitofwork<User> unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<Guid>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new CreateUserCommandValidator();
                var validationResult = await validator.ValidateAsync(request, cancellationToken);
                if (!validationResult.IsValid)
                {
                    return new Result<Guid>
                    {
                        Code = (int)Eerrors.ValidExceptions,
                        Data = default,
                        Errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList(),
                    };
                }
                string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password, BCrypt.Net.BCrypt.GenerateSalt());
                var user = _mapper.Map<User>(request);
                user.Password = passwordHash;
                user.CreatedDate = DateTime.UtcNow;
                await _unitOfWork.GetRepository<User, Guid>().CreateAsync(user);
                await _unitOfWork.GetRepository<User, Guid>().SaveChangesAsync();
                return new Result<Guid>
                {
                    Code = (int)Eerrors.Success,
                    Message = "Success",
                    Data = user.Id,
                };
            }
            catch(Exception ex) 
            {
                return new Result<Guid>
                {
                    Code = (int)Eerrors.Exceptions,
                    Message = ex.Message,
                };
            }
          
        }
    }
}
