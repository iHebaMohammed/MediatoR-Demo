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
    public record GetAllPresonQurey : IRequest<GetAllPresonQureyResult>;

    public record GetAllPresonQureyResult : BaseCommandResult
    {
        public List<Person> Presons { get; set; }
    }

    public class GetAllPersonQureyHandler : IRequestHandler<GetAllPresonQurey, GetAllPresonQureyResult>
    {
        private readonly ApplicationDbContext context;

        public GetAllPersonQureyHandler(ApplicationDbContext context)
        {
            this.context=context;
        }

        public async Task<GetAllPresonQureyResult> Handle(GetAllPresonQurey request, CancellationToken cancellationToken)
        {
            try
            {
                var persons = await context.People.ToListAsync(); ;
                return new GetAllPresonQureyResult()
                {
                    IsSuccess = true,
                    Presons = persons
                };
            }
            catch (Exception ex) 
            {
                return new GetAllPresonQureyResult() 
                {
                    IsSuccess = false,
                    Errors = { ex.Message},
                    StatusCode = StatusCode.Error
                };
            }
        }
    }
}
