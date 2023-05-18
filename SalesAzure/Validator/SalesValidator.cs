using FluentValidation;
using SalesAzure.DTO;
using SalesData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesAzure.Validator
{
    public class SalesValidator : AbstractValidator<SalesDTO>
    {
        public SalesValidator()
        {
            RuleFor(v => v.BranchId)
                .NotEmpty()
                .WithMessage("Branch ID is required.");
            RuleFor(v => v.TransactionId)
                .NotEmpty()
                .WithMessage("Transaction ID is required.");
            RuleFor(v => v.Amount)
                .NotEmpty()
                .WithMessage("Amount is required.");
        }
    }
}
