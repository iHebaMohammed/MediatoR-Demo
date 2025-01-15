using Demo.Domain.Common;
using Demo.Domain.Entities;
using Demo.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Application.Feature.Preson.Queries
{
    public record GetPresonByIdQurey(int id) : IRequest<GetPersonByIdQureyResult>;

    public record GetPersonByIdQureyResult : BaseCommandResult
    {
        public Person Person { get; set; } = new();
    }
    public class GetPersonByIdQureyHandler : IRequestHandler<GetPresonByIdQurey, GetPersonByIdQureyResult>
    {
        private readonly ApplicationDbContext _context;

        public GetPersonByIdQureyHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<GetPersonByIdQureyResult> Handle(GetPresonByIdQurey request, CancellationToken cancellationToken)
        {

            try
            {
                var persons = await _context.People.Where(P => P.Id == request.id).FirstOrDefaultAsync();
                return new GetPersonByIdQureyResult
                {
                    IsSuccess = true,
                    Person = persons,
                };
            }
            catch (Exception ex) 
            {
                return new GetPersonByIdQureyResult 
                {
                    IsSuccess = false,
                    Errors = { ex.Message },
                    StatusCode = StatusCode.Error
                };
            }

        }
    }
}
