﻿using System;
using System.IO;

namespace DocumentMerger
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Document Merger");
            do
            {
                string document1 = GetValidDocument();
                string document2 = GetValidDocument();
                string mergeFileName = document1.Substring(0, document1.Length - 4) + document2;
                StreamWriter writer = null;
                try
                {
                    writer = new StreamWriter(mergeFileName);
                    int count = WriteFileContents(writer, document1);
                    count += WriteFileContents(writer, document2);
                    Console.WriteLine("{0} was successfully saved. The document contains {1} characters", mergeFileName);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error writing to {0}: {1}", mergeFileName, e.Message);
                }
                finally
                {
                    if (writer != null)
                    {
                        writer.Close();
                    }
                }
                Console.Write("Did you want to merge both files?: ");
            }
            while (Console.ReadLine().ToLower() == "y");
        }

        static string GetValidDocument()
        {
            Console.Write("Enter the name of the document: ");
            string document;
            while ((document = Console.ReadLine()).Length == 0 || !File.Exists(document))
            {
                Console.Write("Document could not be found, please try again. ");
            }

            return document;
        }

        static int WriteFileContents(StreamWriter writer, string file)
        {
            StreamReader reader = null;
            int count = 0;

            try
            {
                reader = new StreamReader(file);
                string line = null;
                while ((line = reader.ReadLine()) != null)
                {
                    count += line.Length;
                    writer.WriteLine(line);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error writing {0} to new file: {1}", file, e.Message);
            }

            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
            }
            return count;
        }
    }
}