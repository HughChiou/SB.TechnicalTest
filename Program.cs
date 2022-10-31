using System.ComponentModel;
using System;

using SB.CoreTest;

/// <summary>
/// SchoolsBuddy Technical Test.
///
/// Your task is to find the highest floor of the building from which it is safe
/// to drop a marble without the marble breaking, and to do so using the fewest
/// number of marbles. You can break marbles in the process of finding the answer.
///
/// The method Building.DropMarble should be used to carry out a marble drop. It
/// returns a boolean indicating whether the marble dropped without breaking.
/// Use Building.NumberFloors for the total number of floors in the building.
///
/// A very basic solution has already been implemented but it is up to you to
/// find your own, more efficient solution.
///
/// Please use the function Attempt2 for your answer.
/// </summary>
namespace SB.TechnicalTest
{
    class Program
    {
        /// <summary>
        /// Main entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine($"Attempt 1 Highest Safe Floor: {Attempt1()}");
            Console.WriteLine($"Attempt 1 Total Drops: {Building.TotalDrops}");

            Console.WriteLine();
            Building.Reset();

            Console.WriteLine($"Attempt 2 Highest Safe Floor: {Attempt2()}");
            Console.WriteLine($"Attempt 2 Total Drops: {Building.TotalDrops}");

            Console.WriteLine();
            Building.Reset();

            Console.WriteLine($"Attempt 3 Highest Safe Floor: {Attempt3()}");
            Console.WriteLine($"Attempt 3 Total Drops: {Building.TotalDrops}");
        }

        /// <summary>
        /// First attempt - start at first floor and work up one floor at a time
        /// until you reach a floor at which marble breaks.
        /// The highest safe floor is one below this.
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt1()
        {
            var i = 0;
            while (++i <= Building.NumberFloors && Building.DropMarble(i));

            return i - 1;
        }

        /// <summary>
        /// Second attempt - you do this one!
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt2()
        {
            int[] range ={0,Building.NumberFloors+1};
            double maxDropTimes = Math.Ceiling(Math.Log2(Convert.ToDouble(Building.NumberFloors+1)));
            var nextDrop = 0;
            var isSafe = true;

            for (double i = maxDropTimes; i >0; i--) {   
                nextDrop += Convert.ToInt32(Math.Pow(2.0,i-1)) * (isSafe ? 1:-1);
                if(nextDrop > Building.NumberFloors){
                    isSafe = false;
                    continue;
                }
                isSafe = Building.DropMarble(nextDrop);
                
                if(isSafe){
                    range[0]=nextDrop;
                }else{
                    range[1]=nextDrop;
                }
            }
            
            return isSafe?nextDrop:Array.Find(range,item=>item!=nextDrop);
        }

        /// <summary>
        /// Third attempt - I found I was wrong in attempt 2, just another try
        /// </summary>
        /// <returns>Highest safe floor.</returns>
        static int Attempt3()
        {
            int[] range = {0,Building.NumberFloors+1};
            bool isSafe = false;
            int nextDrop=0;
            while(range[1]-range[0]>1){
                nextDrop= Convert.ToInt32(Math.Ceiling((range[1]-range[0])/2.0))+range[0];
                isSafe = Building.DropMarble(nextDrop);

                if(isSafe){
                    range[0] = nextDrop;
                }else{
                    range[1] = nextDrop;
                }
            }

            return isSafe?nextDrop:Array.Find(range,item=>item!=nextDrop);
        }
    }
}
