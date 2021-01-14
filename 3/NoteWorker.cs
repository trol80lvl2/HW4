using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _3
{
    public class NoteWorker
    {
        private string Path { get; }
        private List<NoteModel> Notes { get; set; }

        public NoteWorker()
        {
            Path = "notes.json";
            if (!File.Exists(Path))
                File.Create(Path);
            string jsonNotes = File.ReadAllText(Path);
            if (jsonNotes.Length > 0)
                Notes = JsonSerializer.Deserialize<List<NoteModel>>(jsonNotes);
            else
                Notes = new List<NoteModel>();
        }

        private void UpdateNotesCache()
        {
            string jsonNotes = File.ReadAllText(Path);
            Notes = JsonSerializer.Deserialize<List<NoteModel>>(jsonNotes);
        }

        public List<NoteModel> SearchNotes(string filter)
        {
            return String.IsNullOrWhiteSpace(filter) ? Notes.OrderBy(x=>x.Id).ToList() : Notes.Where(x=>x.Id.ToString().Contains(filter)
                                                                                                        || x.Text.Contains(filter) 
                                                                                                        || x.Title.Contains(filter)
                                                                                                        || x.CreatedOn.ToString().Contains(filter)).OrderBy(x=>x.Id).ToList();
        }

        private NoteModel GetNoteById(int id)
        {
            return Notes.Where(x => x.Id == id).FirstOrDefault();
        }

        public bool TryGetNoteById(int id, out NoteModel note)
        {
            note = GetNoteById(id);
            return note != null;
        }

        public bool CreateNote(string noteBody)
        {
            if (String.IsNullOrWhiteSpace(noteBody))
                return false;
            int id;
            noteBody = noteBody.Trim();
            if (Notes == null || Notes.Count == 0)
                id = 1;
            else
                id = Notes.Select(x => x.Id).Max() + 1;

            
            NoteModel note = new NoteModel(id, noteBody.Substring(0, (noteBody.Length < 32) ? noteBody.Length : 32).Trim(), noteBody);
            Notes.Add(note);

            File.WriteAllText(Path, JsonSerializer.Serialize(Notes.ToArray()));

            UpdateNotesCache();
            return true;
        }

        //rewrite
        public bool DeleteNote(int id)
        {
            NoteModel toDeleteNote = Notes.Where(x => x.Id == id).FirstOrDefault();
            if (toDeleteNote == null)
                return false;

            UpdateNotesCache();

            Notes.RemoveAll(x=>x.Id==id);
            File.WriteAllText(Path, JsonSerializer.Serialize(Notes));

            return true;
        }

        
    }
}
