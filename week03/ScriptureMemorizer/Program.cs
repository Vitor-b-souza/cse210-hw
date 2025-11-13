using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

/*
 * Scripture Memorizer Program
 * 
 * Features implemented (how I exceeded requirements):
 * - Encapsulation: classes Reference, Word, Scripture encapsulate data and behavior with appropriate access modifiers.
 * - Reference has multiple constructors (single verse and verse range).
 * - Scripture preserves punctuation and spacing while treating only word tokens as hidable words.
 * - When hiding words, the program selects only words that are not already hidden (improved stretch requirement).
 * - The program supports a small built-in library of scriptures and will choose one at random if not provided.
 * - The program allows loading scriptures from a simple text file (one scripture per line) if the file path is provided as a command-line argument.
 * - The Program.cs contains this comment describing exceeded requirements as requested in the assignment.
 */

namespace ScriptureMemorizer
{
    public class Reference
    {
        public string Book { get; private set; }
        public int Chapter { get; private set; }
        public int StartVerse { get; private set; }
        public int? EndVerse { get; private set; }

        public Reference(string book, int chapter, int verse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = verse;
            EndVerse = null;
        }

        public Reference(string book, int chapter, int startVerse, int endVerse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public static Reference Parse(string text)
        {
            var bookChapter = text.Split(new[] { ' ' }, 2);
            if (bookChapter.Length < 2)
                throw new ArgumentException("Invalid reference format.");

            string book = bookChapter[0];
            if (char.IsDigit(book[0]) && bookChapter.Length > 2)
            {
                book = bookChapter[0] + " " + bookChapter[1];
            }

            var chapVerse = bookChapter[1].Split(':');
            int chapter = int.Parse(chapVerse[0]);
            var versePart = chapVerse[1];

            if (versePart.Contains('-'))
            {
                var parts = versePart.Split('-');
                int start = int.Parse(parts[0]);
                int end = int.Parse(parts[1]);
                return new Reference(book, chapter, start, end);
            }
            else
            {
                int verse = int.Parse(versePart);
                return new Reference(book, chapter, verse);
            }
        }

        public override string ToString()
        {
            return EndVerse.HasValue
                ? $"{Book} {Chapter}:{StartVerse}-{EndVerse.Value}"
                : $"{Book} {Chapter}:{StartVerse}";
        }
    }

    public class Word
    {
        private string _text;
        private bool _hidden;

        public Word(string text)
        {
            _text = text;
            _hidden = false;
        }

        public bool IsHidden => _hidden;
        public int Length => _text.Length;

        public void Hide()
        {
            _hidden = true;
        }

        public string Display()
        {
            if (!_hidden) return _text;
            return new string('_', _text.Length);
        }
    }

    public class Scripture
    {
        private Reference _reference;
        private List<object> _tokens;
        private List<Word> _words;
        private Random _rand;

        public Scripture(Reference reference, string text)
        {
            _reference = reference;
            _tokens = new List<object>();
            _words = new List<Word>();
            _rand = new Random();

            TokenizeAndBuild(text);
        }

        private void TokenizeAndBuild(string text)
        {
            var matches = Regex.Matches(text, "(\\w+)|(\\W+)");
            foreach (Match m in matches)
            {
                if (Regex.IsMatch(m.Value, "^\\w+$"))
                {
                    var w = new Word(m.Value);
                    _tokens.Add(w);
                    _words.Add(w);
                }
                else
                {
                    _tokens.Add(m.Value);
                }
            }
        }

        public string Display()
        {
            var parts = new List<string>();
            foreach (var t in _tokens)
            {
                if (t is Word w)
                    parts.Add(w.Display());
                else
                    parts.Add((string)t);
            }

            return _reference.ToString() + " - " + string.Concat(parts);
        }

        public bool AllHidden()
        {
            return _words.All(w => w.IsHidden);
        }

        public void HideRandomWords(int count)
        {
            var candidates = _words.Where(w => !w.IsHidden).ToList();
            if (!candidates.Any()) return;

            count = Math.Min(count, candidates.Count);

            for (int i = 0; i < count; i++)
            {
                int idx = _rand.Next(candidates.Count);
                var selected = candidates[idx];
                selected.Hide();
                candidates.RemoveAt(idx);
            }
        }

        public int VisibleWordCount()
        {
            return _words.Count(w => !w.IsHidden);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var scriptures = new List<(Reference, string)>
            {
                (new Reference("John", 3, 16), "For God so loved the world, that he gave his only begotten Son, that whosoever believeth in him should not perish, but have everlasting life."),
                (new Reference("Proverbs", 3, 5, 6), "Trust in the Lord with all thine heart; and lean not unto thine own understanding; In all thy ways acknowledge him, and he shall direct thy paths."),
                (new Reference("1 Nephi", 3, 7), "And it came to pass that I, Nephi, said unto my father: I will go and do the things which the Lord hath commanded, for I know that the Lord giveth no commandments unto the children of men, save he shall prepare a way for them that they may accomplish the thing which he commandeth them.")
            };

            if (args.Length > 0)
            {
                try
                {
                    var path = args[0];
                    if (System.IO.File.Exists(path))
                    {
                        var lines = System.IO.File.ReadAllLines(path);
                        scriptures.Clear();
                        foreach (var line in lines)
                        {
                            var parts = line.Split('|', 2);
                            if (parts.Length != 2) continue;
                            var reference = Reference.Parse(parts[0].Trim());
                            var text = parts[1].Trim();
                            scriptures.Add((reference, text));
                        }
                    }
                }
                catch { }
            }

            var rnd = new Random();
            var pick = scriptures[rnd.Next(scriptures.Count)];

            Scripture scripture = new Scripture(pick.Item1, pick.Item2);

            while (true)
            {
                Console.Clear();
                Console.WriteLine(scripture.Display());

                if (scripture.AllHidden())
                {
                    Console.WriteLine();
                    Console.WriteLine("All words are hidden. Press any key to exit.");
                    Console.ReadKey();
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Press Enter to hide a few words, or type 'quit' and press Enter to exit.");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input) && input.Trim().ToLower() == "quit")
                {
                    break;
                }

                scripture.HideRandomWords(rnd.Next(1, 4));
            }
        }
    }
}
