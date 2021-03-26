using System;
using System.Collections;
using System.IO;

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
		public static date randomDate = new date();
		public static int publicationTotalNumber;
		public static string[] operators = { "<", ">", "<=", ">=" };
		public static string[][] jaggedArray2 = new string[][]
		{
		new string[] { "Iasi", "Bucuresti", "Cluj", "Arad", "Timisoara", "Galati" },
		new string[] { "N", "NE", "S", "SE", "W", "SW", "NW" }
		};
		static int number = 4;

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

			Console.WriteLine("Enter percentage of operator =:");
			string percentageOperator = Console.ReadLine();

			int numberOfOperator = (Int32.Parse(percentageOperator) * publicationTotalNumber) / percentage;

			//write them
			int indexId = 0;
			using StreamWriter file = new StreamWriter("SubscriptionFile.txt");
			while(publicationTotalNumber != 0)
			{
				for(int indexOfPublicationNumber = 0; indexOfPublicationNumber < fieldsToBePublish.Length; indexOfPublicationNumber++)
				{
					switch (indexOfPublicationNumber)
					{
						case 0:
							if (fieldsToBePublish[indexOfPublicationNumber] > 0)
							{
								file.Write(fields[indexOfPublicationNumber].ToString() + "=" + indexId.ToString() + " ");
							}
								fieldsToBePublish[indexOfPublicationNumber]--;
							indexId++;
							break;
						case 1:
							if (fieldsToBePublish[indexOfPublicationNumber] > 0)
							{
								file.Write(fields[indexOfPublicationNumber].ToString() + "=" + jaggedArray2[0][rand.Next(number)] + " ");
							}
							fieldsToBePublish[indexOfPublicationNumber]--;
							break;
						case 2:
							if (fieldsToBePublish[indexOfPublicationNumber] > 0)
							{
								if(numberOfOperator > 0)
                                {
									file.Write(fields[indexOfPublicationNumber].ToString() + "=" + rand.Next(100).ToString() + " ");
									numberOfOperator--;
								}
                                else
                                {
									file.Write(fields[indexOfPublicationNumber].ToString() + operators[rand.Next(number)].ToString() + rand.Next(100).ToString() + " ");

								}
							}
							fieldsToBePublish[indexOfPublicationNumber]--;
							break;
						case 3:
							if (fieldsToBePublish[indexOfPublicationNumber] > 0)
							{
								if(numberOfOperator > 0)
                                {
									file.Write(fields[indexOfPublicationNumber].ToString() + "=" + rand.Next(100).ToString() + " ");
									numberOfOperator--;
								}
                                else
                                {
									file.Write(fields[indexOfPublicationNumber].ToString() + operators[rand.Next(number)].ToString() + rand.Next(100).ToString() + " ");
								}
							}
							fieldsToBePublish[indexOfPublicationNumber]--;
							break;
						case 4:
							if (fieldsToBePublish[indexOfPublicationNumber] > 0)
							{
								if (numberOfOperator > 0)
								{
									file.Write(fields[indexOfPublicationNumber].ToString() + "=" + rand.Next(100).ToString() + " ");
									numberOfOperator--;
								}
                                else
                                {
									file.Write(fields[indexOfPublicationNumber].ToString() + operators[rand.Next(number)].ToString() + rand.Next(100).ToString() + " ");
								}
							}
							fieldsToBePublish[indexOfPublicationNumber]--;
							break;
						case 5:
							if (fieldsToBePublish[indexOfPublicationNumber] > 0)
							{
								file.Write(fields[indexOfPublicationNumber].ToString() + "=" + jaggedArray2[1][rand.Next(number)] + " ");
							}
							fieldsToBePublish[indexOfPublicationNumber]--;
							break;
						default:
							if (fieldsToBePublish[indexOfPublicationNumber] > 0)
							{
								if(numberOfOperator > 0)
                                {
									file.Write(fields[indexOfPublicationNumber].ToString() + "=" + (rand.Next(31) + 1).ToString() + "." + (rand.Next(12) + 1).ToString() + "." + (rand.Next(34) + 2000).ToString() + " ");
								}
                                else 
								{
									file.Write(fields[indexOfPublicationNumber].ToString() + operators[rand.Next(number)].ToString() + (rand.Next(31) + 1).ToString() + "." + (rand.Next(12) + 1).ToString() + "." + (rand.Next(34) + 2000).ToString() + " ");
								}
							}
							fieldsToBePublish[indexOfPublicationNumber]--;
							break;
					}
				}
				file.WriteLineAsync();
				publicationTotalNumber--;
				Console.WriteLine();
			}
		}
	}
}
