using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _3
{
 
    internal class Program
    {
        static int CountLines(string s)
        {
            return s.Split('\n').Length;
        }

        static int CountWords(string s)
        {
            return Regex.Matches(s, @"\w+").Count;
        }

        static string RemoveEmptyLinesAndComments(string s)
        {
            var lines = s.Split('\n');
            var result = new List<string>();
            foreach (var line in lines)
            {
                var trimmedLines = line.Trim();
                if (trimmedLines != "" && !trimmedLines.StartsWith("//"))
                {
                    result.Add(line);
                }
            }
            return string.Join("\n", result);
        }

        static Dictionary<string, int> CountWordsFrequency(string s)
        {
            var matches = Regex.Matches(s, @"\w+");
            var words = new List<string>();
            foreach (Match match in matches)
            {
                words.Add(match.Value);
            }
            var result = new Dictionary<string, int>();
            foreach (var word in words)
            {
                if (result.ContainsKey(word))
                {
                    result[word]++;
                }
                else
                {
                    result[word] = 1;
                }
            }
            return result;

        }
        static void Main(string[] args)
        {
            string path;
            while(true)
            {
                Console.WriteLine("Please enter the path of the .cs file, type 'q' or 'quit' to quit:");
                path = Console.ReadLine();
                if (path == "q" || path == "quit")
                    return;
                if (!File.Exists(path) || Path.GetExtension(path) != ".cs")
                {
                    Console.WriteLine("Error: Invalid path.");
                    continue;
                }
                string content = File.ReadAllText(path);
                int originalLines = CountLines(content);
                int originalWords = CountWords(content);
                Console.WriteLine("There are {0} lines, {1} words in the original file.", originalLines, originalWords);


                string formattedContent = RemoveEmptyLinesAndComments(content);
                int formattedLines = CountLines(formattedContent);
                int formattedWords = CountWords(formattedContent);
                Console.WriteLine("There are {0} lines, {1} words in the formatted file.", formattedLines, formattedWords);

                Dictionary<string, int> wordCounts = CountWordsFrequency(formattedContent);
                Console.WriteLine("The frequency of each word in the formatted file is:");
                foreach (var word in wordCounts)
                {
                    Console.WriteLine("{0}: {1}", word.Key, word.Value);
                }
                /*
                 * 
                StreamReader reader = null;
                StreamWriter writer = null;
                FileStream fin = new FileStream(
                            path, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(
                            path[0..(path.Length - 3)] + ".out", FileMode.Create, FileAccess.Write);
                reader = new StreamReader(fin, Encoding.UTF8);
                writer = new StreamWriter(fout, Encoding.UTF8);
                *
                */
            }
        }
    }
}