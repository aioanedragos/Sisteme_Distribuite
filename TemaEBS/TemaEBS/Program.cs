using System;
using System.Collections;

namespace TemaEBS
{
    class Program
    {
        //====================
        //Global Variables
        //====================
        public static ArrayList fields = new ArrayList();
        public static ArrayList percentages = new ArrayList();
        public static ArrayList fieldsToBePublish = new ArrayList();
        public static populate writer = new populate();
        public static int publicationTotalNumber;

        //====================
        //Functional Code
        //====================
        static void Main(string[] args)
        {
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
                percentages.Add(Int32.Parse(option));
                indexFields++;
            }

            //get number of publications
            Console.Write("Enter number of publications:");
            publicationTotalNumber = Int32.Parse(Console.ReadLine());

            //get number of the fields to be published
            for(int indexOfPercentages = 0; indexOfPercentages < percentages.Count; indexOfPercentages++)
            {
                fieldsToBePublish.Add(publicationTotalNumber * percentages.IndexOf(indexOfPercentages) / 100);
            }
        }
    }
}
