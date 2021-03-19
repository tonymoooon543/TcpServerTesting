using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JBAddons
{
    public class JBConsole
    {
        /// <summary>
        /// Gets the screen width and height and gives it to the user
        /// </summary>
        /// <param name="height">The color of the text</param>
        /// <param name="width">The message</param>
        public static string Size(bool height, bool width)
        {
            float width2 = System.Console.WindowWidth;
            float height2 = System.Console.WindowHeight;

            if (height == true && width == false)
            {
                return height2.ToString();
            }
            else if (height == false && width == true)
            {
                return width2.ToString();
            }
            else if (height == true && width == true)
            {
                return width2.ToString() + " " + height2.ToString();
            }
            else
            {
                return "Error, nothing speceifed";
            }
        }

        /// <summary>
        /// Sets a  color for text and prints it 
        /// </summary>
        /// <param name="color">The color of the text</param>
        /// <param name="message">The message</param>
        public static void ColoredMessage(ConsoleColor color, string message)
        {
            // Sets the color of the text
            System.Console.ForegroundColor = color;

            // Prints the message
            System.Console.WriteLine(message);

            // Resets the color
            System.Console.ResetColor();
        }

        /// <summary>
        /// Sets the size of the console window
        /// </summary>
        /// <param name="width">New width of the console</param>
        /// <param name="height">New height of the console</param>
        public static void SetSize(int width, int height)
        {
            Console.SetWindowSize(width, height);
        }

        /// <summary>
        /// Reads all of the lines from the File
        /// </summary>
        /// <param name="file">the file that you want to read</param>
        public static void ReadAllFromFile(string file)
        {
            string[] fileContents = File.ReadAllLines(file);
            foreach (string line in fileContents)
            {
                Console.WriteLine(line);
            }
        }

        public static void ReadLineFromFile(string file, int line)
        {
            StreamReader reader = new StreamReader(file);
            string lineContents = reader.ReadLine();
            Console.WriteLine(lineContents);
        }
    }
}