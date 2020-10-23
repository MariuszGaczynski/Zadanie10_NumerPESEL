using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Zadanie10_NumerPESEL
{
    class Program
    {
        static void Main(string[] args)
        {
            //szyld graficzny
            Graphic1();

            // powitanie , wytłumaczenie


            Console.WriteLine("\n    PESEL (Polish Powszechny Elektroniczny System Ewidencji Ludności,\n" +
                "  Universal Electronic System for Registration of the Population)\n" +
                "  is the national identification number used in Poland since 1979.\n" +
                "  It always has 11 digits, identifies just one person\n" +
                "  and cannot be changed to another one.");
            //zapytanie o pesel
            string givenPESEL;
            bool isParsable;
            bool reStart = true; //  zrestartuje sprawdzanie
            do
            {
 
                do
                {
                    Console.Write("\n\tType PESEL number to check : ...");

                    givenPESEL = Console.ReadLine();
                    

                    isParsable = Int64.TryParse(givenPESEL, out long PESELNum); // sprawdzam czy ten string ma sens konwertując na long
                    if (isParsable)
                    {
                        if (PESELNum <= 99999999999 && PESELNum > 10000000)
                        {
                            isParsable = true;
                        }
                        else
                        {
                            Console.WriteLine("   This is definitely not a valid PESEL number.\n" +
                                "       It must have 11 digits.");
                            isParsable = false;
                        }

                    }
                } while (isParsable == false);

                //operacje na podanym peselu

                Console.WriteLine("\n\t   Is it a correct PESEL number ? ? ?");

                if (IsItValidPESELNumber(givenPESEL))
                {
                    Console.WriteLine("\n\t\tYes, it is valid PESEL number.\n");
                }
                else
                {
                            Console.WriteLine("\n\t\tNo, it is not valid PESEL number.\n");
                }


                
                    if (IsItValidPESELNumber(givenPESEL) == true)
                {
                    GetBirthDateAndSex(givenPESEL);
                }

                // restart sprawdzania
                string answer = " ";
                do
                {
                    Console.Write("\nDo you want to preform another check of PESEL number ?\n" +
                        "\n\t\t\t(Y)es or (N)o ? ... ");
                    answer = Console.ReadLine().ToLower();
                    if (answer == "y")
                    {

                    }
                    else if (answer == "n")
                    {
                        reStart = false;
                    }
                    else
                    {
                        Console.WriteLine("\nUnrecognizable answer. Try again\n");
                    }
                } while (answer != "y" && answer != "n");

            } while (reStart == true);

            Graphic2();
           
            Console.ReadLine();
        }


        public static bool IsItValidPESELNumber (string givenPESEL)
        {
                

                    char[] numbersInPESEL = givenPESEL.ToCharArray();

                    int[] intNumbersInPESEL = new int[11];

                    byte count = 0;
                    foreach (int item in numbersInPESEL)
                    {
                        int singleDigitFromPESEL = Int32.Parse(numbersInPESEL[count].ToString());
                        intNumbersInPESEL[count] = singleDigitFromPESEL;
                        count++;
                    }

                    // sprawdzić rachunek i liczbe kontrolną 
                    int controlNumber = intNumbersInPESEL[10];
                    int numberToMeetControl = intNumbersInPESEL[0]*1 + intNumbersInPESEL[1]*3 +
                        intNumbersInPESEL[2]*7 + intNumbersInPESEL[3]*9 + intNumbersInPESEL[4]*1 +
                        intNumbersInPESEL[5]*3 + intNumbersInPESEL[6]*7 + intNumbersInPESEL[7]*9 +
                        intNumbersInPESEL[8]*1 + intNumbersInPESEL[9]*3;
                    numberToMeetControl = (10 - (numberToMeetControl %= 10))%10;

                    if (controlNumber == numberToMeetControl)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

               

               
        }

        public static void GetBirthDateAndSex(string givenPESEL)
        {
          
                char[] numbersInPESEL = givenPESEL.ToCharArray();

                int[] intNumbersInPESEL = new int[11];

                byte count = 0;
                foreach (int item in numbersInPESEL)
                {
                    int singleDigitFromPESEL = Int32.Parse(numbersInPESEL[count].ToString());
                    intNumbersInPESEL[count] = singleDigitFromPESEL;
                    count++;
                }

                int year;
                int month;
                int day;

                string sex = "Male";
                if ((intNumbersInPESEL[8]*10 + intNumbersInPESEL[9]) %2 ==0)
                {
                    sex = "Female";
                }


                year = 10 * intNumbersInPESEL[0] + intNumbersInPESEL[1];

                if (intNumbersInPESEL[2] > 80)
                {
                    year += 1800;
                    intNumbersInPESEL[2] -= 8;

                }
                else if (intNumbersInPESEL[2] > 60)
                {
                    year += 2200;
                    intNumbersInPESEL[2] -= 6;
                }
                else if (intNumbersInPESEL[2] > 40)
                {
                    year += 2100;
                    intNumbersInPESEL[2] -= 4;
                }
                else if (intNumbersInPESEL[2] > 20)
                {
                    year += 2000;
                    intNumbersInPESEL[2] -= 2;
                }
                else
                {
                    year += 1900;
                }

                month = intNumbersInPESEL[2] + intNumbersInPESEL[3];

                day = intNumbersInPESEL[4] * 10 + intNumbersInPESEL[5];


            string person = "His";
            if (sex == "Female")
            {
                person = "Her";
            }
            string nameOfMonth= "";
            switch (month)
            {
                case 1:
                    nameOfMonth = "January";
                    break;
                case 2:
                    nameOfMonth = "February";
                    break;
                case 3:
                    nameOfMonth = "March";
                    break;
                case 4:
                    nameOfMonth = "April";
                    break;
                case 5:
                    nameOfMonth = "May";
                    break;
                case 6:
                    nameOfMonth = "June";
                    break;
                case 7:
                    nameOfMonth = "July";
                    break;
                case 8:
                    nameOfMonth = "August";
                    break;
                case 9:
                    nameOfMonth = "September";
                    break;
                case 10:
                    nameOfMonth = "October";
                    break;
                case 11:
                    nameOfMonth = "November";
                    break;
                case 12:
                    nameOfMonth = "December";
                    break;
                default:
                    break;
            }
            string numeral = "";

            if (day == 1 || day == 21 || day == 31)
            {
                numeral = "st";
            }
            else if (day == 2 || day == 22)
            {
                numeral = "nd";
            }
            else if (day ==3 || day == 23)
            {
                numeral = "rd";
            }
            else if (day == 3 || day == 23)
            {
                numeral = "rd";
            }
            else
            {
                numeral = "th";
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;

            Console.WriteLine("___________________________________________________________________");
            Console.ResetColor();
            Console.WriteLine("\t   {0} birth date is : year {1}  {2}{3}  {4}.", person, year, day, numeral, nameOfMonth );

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;

            Console.WriteLine("___________________________________________________________________");
            Console.ResetColor();

        }

        public static void Graphic1()
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;

            Console.WriteLine("  XXX                                                         XXX  ");
            Console.WriteLine("  XXX  %%%%%%%%    %%%%%%%%     %%%%%%%     %%%%%%%  %%%      XXX  ");
            Console.WriteLine("  XXX  %%%  %%%    %%%         %%%    %%    %%%      %%%      XXX  ");
            Console.WriteLine("  XXX  %%%%%%%%    %%%%%%%        %%%       %%%%%%%  %%%      XXX  ");
            Console.WriteLine("  XXX  %%%         %%%         %%    %%%    %%%      %%%      XXX  ");
            Console.WriteLine("  XXX  %%%         %%%%%%%%     %%%%%%%     %%%%%%%  %%%%%%%  XXX  ");
            Console.WriteLine("  XXX                                                         XXX  ");

            Console.ResetColor();
        }

        public static void Graphic2()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("\n\nXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
            Console.WriteLine("                                                                 ");
            Console.WriteLine("\tThan you for your attention ! See you next time !\t ");
            Console.WriteLine("                                                                 ");
            Console.WriteLine("XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX");
        }


    }

}
