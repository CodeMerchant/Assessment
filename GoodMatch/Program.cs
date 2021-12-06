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

            var watch = System.Diagnostics.Stopwatch.StartNew();
          
            List<User> users = new List<User>();

            // Opening the file
            try
            {
                string file = @"matcher.csv";

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

            // Creating two sets, one for males, the other for females
            HashSet<string> males = new HashSet<string>();
            HashSet<string> females = new HashSet<string>();

            // grouping users based on gender and adding to respective set
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

            // temporary lists to hold the new, non duplicate data from each set
            List<String> tempHolderForMales = new List<String>();
            List<string> tempHolderForFemales = new List<string>();

            // assigning set data to respective list
            tempHolderForFemales = females.ToList();
            tempHolderForMales = males.ToList();



            /* creating new User lists that will help track the data better by adding each element in temp list
             * into its respective gender based User list
             */
            List<User> newMensList = new List<User>();
            List<User> newWomensList = new List<User>();

            // looping through temp list and adding to User list
            foreach (var man in tempHolderForMales)
            {
                newMensList.Add(new User(man));
            }

            foreach (var woman in tempHolderForFemales)
            {
                newWomensList.Add(new User(woman));
            }


            
            // creating new List that will concatenate each male with each female string for testing for matches
             
            List<string> matches = new List<string>();

            // adding the strings to the matches List
            foreach (var man in newMensList)
            {
                foreach (var woman in newWomensList)
                {
                    matches.Add(man + " matches " + woman);
                }
            }

            // creating a list that will hold Match objects
            List<Match> matchesList = new List<Match>();
            string originalString = "";
           
            foreach (var match in matches)
            {

                string removeCommas = match.Replace(",", "");
                originalString = removeCommas;
                string sanitizedString = Regex.Replace(originalString, "\\s", "");

                // using Regex to check for invalid input
                if (!Regex.IsMatch(sanitizedString, @"^[a-zA-Z]+$"))
                {
                    Console.Write("The program only allows alphabetical characters. Please restart the program and enter valid input.");
                    Console.ReadLine();
                    break;
                }
                else
                {
                    long numToFindPercentageOf = findLongNumber(sanitizedString);
          
                    string line = findPercentage(originalString, numToFindPercentageOf);

                    string[] temp = line.Split(' ');
                    matchesList.Add(new Match(temp[0],temp[2], int.Parse(temp[3])));
                }

            }

            // sorting Match list objects as per given instructions
            List<Match> sortedMatchesList = matchesList
                .OrderByDescending(match => match.getPercentage())
                .ThenBy(match => match.getMale())
                .ThenBy(match => match.getFemale()).ToList();

            // creating new txt file
            TextWriter text = new StreamWriter("output.txt");

            // printing sorted list to console as well as writing to txt file
            foreach (var match in sortedMatchesList) {
                text.WriteLine(match);
                Console.WriteLine(match);    
            }
            text.Close();
            
            watch.Stop();

           
            TextWriter executionTime = new StreamWriter(@"execution_times.txt");
            // saving execution time log into file
            executionTime.WriteLine("Execution time in milliseconds: " + watch.ElapsedMilliseconds + "ms");
            executionTime.Close();

            Console.WriteLine("");
            Console.Write("Hit [Enter] to exit the application");

            Console.ReadLine();




        }

        
        public static string findPercentage(string originalString, long numberToGetPercentageFrom)
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

             return   originalString + " " + (sumOfNumbers * 10);


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




            // using StringBuilder class to help collect the numbers from the list and convert them to one string
            StringBuilder builder = new StringBuilder();

            foreach (int nums in numbers)
            {
                builder.Append(nums);
            }

            // storing the builder into a string variable
            string newNums = builder.ToString();

            // Console.Write(newNums);

           // Console.WriteLine();
           // Console.WriteLine();
           
            // converting the string to long in order to make calculations. Can't store the number as an int as it's too big
            sanitizedNumber = long.Parse(newNums);
            
            //Console.WriteLine("after sanitisation");
            //Console.Write(sanitizedNumber);

            return sanitizedNumber;
        }

    }
}
