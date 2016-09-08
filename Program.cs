using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace acm_2015_1
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read in values, split on spaces and convert them all to int
            int[] values = Array.ConvertAll(Console.ReadLine().Trim().Split(' '), int.Parse);

            // create a new Stock_Trend object
            Stock_Trend trend = new Stock_Trend(values[0], values[1], values[2], values[3], values[4], values[5]);

            // print results
            Console.WriteLine(trend.calculate_largest_decline());

            // wait to exit
            Console.ReadLine();
        }
    }



    public class Stock_Trend
    {
        private int p, a, b, c, d, n;

        public Stock_Trend(int p, int a, int b, int c, int d, int n)
        {
            this.p = p;
            this.a = a;
            this.b = b;
            this.c = c;
            this.d = d;
            this.n = n;
        }


        // Calculates the largest decline in stock prices and return it as a double
        public double calculate_largest_decline()
        {
            // The current decline happening and the previous decline that is the largest so far
            double current_decline = 0, prev_decline = 0;

            // Current high and low values
            double current_high, current_low;

            // Current calculated value
            double current;


            // There won't be a decline if there's only 1 number to look at
            if (n == 1) return current_decline;

            
            // Calculate once to begin to set up the variables
            int k = 1;
            current_high = current_low = current = (double)p * (Math.Sin(a * k + b) + Math.Cos(c * k + d) + 2d);
            k++;

            // Calculate the rest of the values
            for (; k <= n; k++)
            {
                // calculate value at k
                current = (double)p * (Math.Sin(a * k + b) + Math.Cos(c * k + d) + 2d);


                // see if high/low values need to be changed

                // if current is the new high value, then start looking for a new decline
                if (current > current_high)
                {
                    // save old current_decline if it is bigger than prev_decline
                    if (current_decline > prev_decline) prev_decline = current_decline;

                    // reset current_low so we don't calculate high - low when the low value was actually encountered earlier
                    current_low = current;

                    // reset current_decline ... just to be sure (It wouldn't affect anything to not reset it, but there's no reason not to)
                    current_decline = 0;

                    // set new current_high
                    current_high = current;
                }
                // else if current is the new low value, the current_decline is continuing
                else if (current < current_low)
                {
                    // set current_low
                    current_low = current;

                    // new value is lower than current_low, so the decline is continuing
                    // update the current_decline
                    current_decline = current_high - current_low;
                }
                // Done checking if high/low values needed changing

            }

            // return whatever decline is the greatest
            return (current_decline > prev_decline) ? current_decline : prev_decline;
        }
    }

}
