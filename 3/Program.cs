using System;
using System.Linq;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace _3
{
    class Program
    {
        static void WaitKey()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            NoteWorker noteWorker = new NoteWorker();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("'Notes' made by Roman Holub");
                Console.WriteLine("1. Search notes\n2. View notes\n3. Create note\n4. Delete note\n5. Exit");
                var key = Console.ReadKey().Key;

                switch (key)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.NumPad1:

                        Console.Clear();
                        Console.Write("Enter your filter (if empty => all notes) ->");
                        string filter = Console.ReadLine();
                        var notes = noteWorker.SearchNotes(filter);

                        if (notes.Count == 0)
                            Console.WriteLine("No notes found");

                        else
                        {
                            for (int i = 0; i < notes.Count; i++)
                            {
                                Console.WriteLine($"{notes[i].Id} {notes[i].Title,33} {notes[i].CreatedOn}");
                            }
                        }

                        WaitKey();

                        break;
                    case ConsoleKey.D2:
                    case ConsoleKey.NumPad2:
                        Console.Clear();
                        int idGet = 0;
                        NoteModel noteModel;

                        while (idGet < 1)
                        {
                            Console.Write("Enter id of note you want to search ->");
                            if (!int.TryParse(Console.ReadLine(), out idGet))
                                continue;
                        }

                        if (noteWorker.TryGetNoteById(idGet, out noteModel))
                            Console.WriteLine($"{noteModel.Id} {noteModel.Title} {noteModel.Text} {noteModel.CreatedOn}");
                        else
                            Console.WriteLine($"Not not found");

                        WaitKey();

                        break;
                    case ConsoleKey.D3:
                    case ConsoleKey.NumPad3:
                        Console.Clear();
                        Console.Write("Enter your note->");
                        string note = Console.ReadLine();

                        noteWorker.CreateNote(note);

                        break;
                    case ConsoleKey.D4:
                    case ConsoleKey.NumPad4:
                        Console.Clear();
                        int idDelete = 0;

                        while (idDelete < 1)
                        {
                            Console.Write("Enter id of note you want to delete ->");
                            if (!int.TryParse(Console.ReadLine(), out idDelete))
                                continue;
                        }
                        if (noteWorker.DeleteNote(idDelete))
                            Console.WriteLine($"Note with id {idDelete} has been deleted");
                        else
                            Console.WriteLine($"Note with id {idDelete} doensn't exist");
                        WaitKey();

                        break;
                    case ConsoleKey.D5:
                    case ConsoleKey.NumPad5:
                        return;
                }
            }
            
        }
    }
}
