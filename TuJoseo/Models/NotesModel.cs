namespace TuJoseo.Models
{
    public class NotesModel
    {
        public List<Note> NoteList;
    }

    public class Note
    {
        public int NoteID { get; set; }
        public int NoteUserID { get; set; } = 0;
        public string NoteDescription { get; set; } = string.Empty;
        public DateTime NoteTime { get; set; }
        public bool NoteDone { get; set; } = false;
    }
}
