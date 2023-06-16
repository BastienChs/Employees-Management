using Application.Employees.Queries;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employees.QueryHandlers
{
    public class LoginHandler : IRequestHandler<Login, int>
    {
        private readonly IAuthRepository _authRepository;
        public LoginHandler(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        public Task<int> Handle(Login request, CancellationToken cancellationToken)
        {
            return _authRepository.Login(request.Username, request.Password);
        }
    }
}
