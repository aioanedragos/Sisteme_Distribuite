using System;
using System.Collections;

namespace TemaEBS
{
    class Program
    {
        //====================
        //Constants
        //====================
        public const int percentage = 100;

        //====================
        //Global Variables
        //====================
        public static ArrayList fields = new ArrayList();
        public static int[] percentages = new int[7];
        public static int[] fieldsToBePublish = new int[7];
        public static populate writer = new populate();
        public static int publicationTotalNumber;
        public static string[] operators = { "<", ">", "=", "<=", ">=" };
        static int number = 5;

        //====================
        //Functional Code
        //====================
        static void Main(string[] args)
        {
            var rand = new Random();
            //populate obtions
            writer.populateFields(fields);

            //get percentage
            Console.WriteLine("Enter percentage for every field:");
            string option = "Initialized";
            int indexFields = 0;
            while ((option != "done") && (indexFields < fields.Count))
            {
                Console.Write(fields[indexFields] + ": ");
                option = Console.ReadLine();
                percentages[indexFields] = Int32.Parse(option);
                indexFields++;
            }

            //get number of publications
            Console.Write("Enter number of publications:");
            publicationTotalNumber = Int32.Parse(Console.ReadLine());
            var ceva = percentages[1];

            //get number of the fields to be published and max of the number
            int maxNumberOfPublish = 0;
            for(int indexOfPercentages = 0; indexOfPercentages < percentages.Length; indexOfPercentages++)
            {
                fieldsToBePublish[indexOfPercentages] = (publicationTotalNumber * percentages[indexOfPercentages] / percentage);
                if((publicationTotalNumber * percentages[indexOfPercentages] / percentage) > maxNumberOfPublish)
                {
                    maxNumberOfPublish = publicationTotalNumber * percentages[indexOfPercentages] / percentage;
                }
            }
            //write them
            while(maxNumberOfPublish !=0)
            {
                for(int indexOfPublicationNumber = 0; indexOfPublicationNumber < fieldsToBePublish.Length; indexOfPublicationNumber++)
                {
                    if(fieldsToBePublish[indexOfPublicationNumber] > 0)
                    {
                        if(indexOfPublicationNumber == 0)
                        {
                            Console.Write(fields[indexOfPublicationNumber].ToString() + "=" + "Iasi" + " ");
                        }
                        else
                        {
                            Console.Write(fields[indexOfPublicationNumber].ToString() + operators[rand.Next(number)].ToString() + "Iasi" + " ");
                        }
                        fieldsToBePublish[indexOfPublicationNumber]--;
                    }
                }
                maxNumberOfPublish--;
                Console.WriteLine();
            }
        }
    }
}
