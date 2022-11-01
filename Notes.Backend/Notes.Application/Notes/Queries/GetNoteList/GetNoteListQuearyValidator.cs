using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQuearyValidator : AbstractValidator<GetNoteListQueary>
    {
        public GetNoteListQuearyValidator()
        {
            RuleFor(getNoteListQueary => getNoteListQueary.UserId).NotEqual(Guid.Empty);
        }
    }
}
