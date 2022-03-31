using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math.Optimization;

namespace Lab5QP
{
    public class QPSolver
    {
        /*public double Calc()
        {
            double x1 = 0, x2 = 0;

            // Create our objective function using a lambda expression
            var f = new QuadraticObjectiveFunction(() => 0.0001 * (x1 * x1) + 0.0001 * (x2 * x2) - 7 * x1 - 4 * x2);
            // Now, create the constraints
            List<LinearConstraint> constraints = new List<LinearConstraint>()
            {
                new LinearConstraint(f, () => 3*x1 - 3*x2 <=200),
                new LinearConstraint(f, () => 7*x1+x2 <=380)
            };

            // Now we create the quadratic programming solver 
            var solver = new GoldfarbIdnani(f, constraints);
            // And attempt solve for the min:
            bool success = solver.Minimize();
            // The solution was { 10, 5 }
            double[] solution = solver.Solution;
            // With the minimum value 170.0
            double minValue = solver.Value;

            return minValue;
        }*/
        
    }
}
