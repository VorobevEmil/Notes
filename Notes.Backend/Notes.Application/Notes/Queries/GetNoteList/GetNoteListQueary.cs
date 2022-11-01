using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    public class GetNoteListQueary : IRequest<NoteListVm>
    {
        public Guid UserId { get; set; }
    }
}
