using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Profiles
{
    public class Edit
    {
        public class Command: IRequest<Result<Unit>> 
        {
            public string DisplayName { get; set; }
            public string Bio { get; set; }
        }

        public class CommandValidator: AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, Result<Unit>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;   

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<Result<Unit>> Handle(Command request, CancellationToken cancellationToken)
            {
                var profile = await _context.Users.FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());
                    //.ProjectTo<Profile>(_mapper.ConfigurationProvider)
                    //.SingleOrDefaultAsync(x => x.Username == request.Profile.Username);

                if (profile == null) return null;

                profile.Bio = request.Bio ?? profile.Bio;
                profile.DisplayName = request.DisplayName ?? profile.DisplayName;

                var result = await _context.SaveChangesAsync() > 0;

                if (!result) return Result<Unit>.Failure("Failed to update the profile");

                return Result<Unit>.Success(Unit.Value);
            }
        }
    }
}