using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace ConsoleApplication1
{
    class Program
    {

        static void Main(string[] args)
        {

            List<int> numbers = new List<int>();
            Regex conditions = new Regex("^[a-zA-Z]");
            List<User> users = new List<User>();
            try
            {
                string file = @"C:\Users\tysoncoupr\Documents\Visual Studio 2012\Projects\GoodMatch\GoodMatch\matcher.csv";

                string[] rawCsv = System.IO.File.ReadAllLines(file);



                for (int i = 0; i < rawCsv.Length; i++)
                {
                    string[] rowData = rawCsv[i].Split(',');
                    users.Add(new User(rowData[0], rowData[1]));
                }
            }
            catch (FileNotFoundException e)
            {

                Console.WriteLine(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);

            }
            catch (IOException)
            {
                Console.WriteLine("File could not be opened!");

            }

            HashSet<string> males = new HashSet<string>();
            HashSet<string> females = new HashSet<string>();

            foreach (var user in users)
            {

                if (user.getGender().Equals("m"))
                {

                    males.Add(user.getName());

                }
                else if (user.getGender().Equals("f"))
                {
                    females.Add(user.getName());
                }
            }


            List<String> tempHolderForMales = new List<String>();
            List<string> tempFemales = new List<string>();

            tempFemales = females.ToList();

            tempHolderForMales = males.ToList();

            List<User> newMensList = new List<User>();
            List<User> newWomensList = new List<User>();

            foreach (var man in tempHolderForMales)
            {
                newMensList.Add(new User(man));

            }

            foreach (var woman in tempFemales)
            {
                newWomensList.Add(new User(woman));
            }



            // TODO: Implement Equals and HashCode methods for use with Comparator when I wake up
            // TODO: Make sure to check each male against every female. Finish up on exporting as txt


            string checker = "";


            List<string> matches = new List<string>();

            foreach (var man in newMensList)
            {

                foreach (var woman in newWomensList)
                {

                    matches.Add(man + " matches " + woman);
                }
            }




            foreach (var match in matches)
            {

                string removeCommas = match.Replace(",", "");
                checker = removeCommas;
                string sanitizedString = Regex.Replace(checker, "\\s", "");

                if (!Regex.IsMatch(sanitizedString, @"^[a-zA-Z]+$"))
                {
                    Console.Write("The program only allows alphabetical characters. Please restart the program and enter valid input.");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    long numToFindPercentageOf = findLongNumber(sanitizedString);
                    findPercentage(checker, numToFindPercentageOf);
                }



            }

            Console.ReadLine();




        }


        public static void findPercentage(string originalString, long numberToGetPercentageFrom)
        {

            long sumOfNumbers = 0;


            while (numberToGetPercentageFrom > 0 || sumOfNumbers > 9)
            {

                if (numberToGetPercentageFrom == 0)
                {
                    numberToGetPercentageFrom = sumOfNumbers;
                    sumOfNumbers = 0;
                }

                sumOfNumbers += numberToGetPercentageFrom % 10;
                numberToGetPercentageFrom /= 10;
            }

            if ((sumOfNumbers * 10) >= 80)
            {

                Console.Write(originalString + " " + (sumOfNumbers * 10) + "%, good match");
            }
            else
                Console.Write(originalString + " " + (sumOfNumbers * 10) + "%");

        }

        public static long findLongNumber(string sanitizedString)
        {
            long sanitizedNumber = 0;
            List<int> numbers = new List<int>();

            while (sanitizedString.Length > 0)
            {
                int count = 0;
                for (int i = 0; i < sanitizedString.Length; i++)
                {
                    if (sanitizedString[0] == sanitizedString[i])
                    {
                        count += 1;
                    }
                }



                sanitizedString = sanitizedString.Replace(sanitizedString[0].ToString(), string.Empty);

                // Testing to see if chars are getting removed as per instruction
                // Console.WriteLine(sanitizedString);
                numbers.Add(count);

            }

            // printing out the number of occurences
            //  foreach (int g in numbers)
            //  {
            //    Console.Write(g);

            //  }

            Console.WriteLine();



            // using StringBuilder class to help collect the numbers from the list and convert them to one string
            StringBuilder builder = new StringBuilder();

            foreach (int nums in numbers)
            {
                builder.Append(nums);
            }

            // storing the builder into a string variable
            string newNums = builder.ToString();

            // Console.Write(newNums);

            Console.WriteLine();
            Console.WriteLine();
            // converting the string to long in order to make calculations. Can't store the number as an int as it's too big
            sanitizedNumber = long.Parse(newNums);
            //Console.WriteLine("after sanitisation");
            //Console.Write(sanitizedNumber);

            return sanitizedNumber;
        }

    }
}
