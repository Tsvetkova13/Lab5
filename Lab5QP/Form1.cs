using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Accord.Math.Optimization;

namespace Lab5QP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        public void SolverFunc()
        {
            double x1 = 0, x2 = 0;

            // Create our objective function using a lambda expression
            var f = new QuadraticObjectiveFunction(() => 0.0001 * (x1 * x1) + 0.0001 * (x2 * x2) - 7 * x1 - 4 * x2);
            // Now, create the constraints
            List<LinearConstraint> constraints = new List<LinearConstraint>()
            {
                new LinearConstraint(f, () => (3*x1) - 3*x2 <=200),
                new LinearConstraint(f, () => (7*x1)+x2 <=380),
                new LinearConstraint(f, () => x1>=0),
                new LinearConstraint(f, () => x2>=0)

            };

            // Now we create the quadratic programming solver 
            var solver = new GoldfarbIdnani(f, constraints);
            // And attempt solve for the min:
            bool success = solver.Minimize();
            // The solution was { 10, 5 }
            double[] solution = solver.Solution;
            // With the minimum value 170.0
            double minValue = solver.Value;
        }

        public void SolverMatrix()
        {
            double[,] Q = // 2x² -1xy +4y²
                           {   
                               /*           x              y      */
                               /*x*/ { 0.0001 /*xx*/ *2,  0 /*xy*/    }, 
                               /*y*/ { 0 /*xy*/   ,  0.0001 /*yy*/ *2 },
                            };
            double[] d = { -7 /*x*/, -4 /*y*/ }; // -5x -6y
            double[,] A ={ { 3, 3 }, // This line says that x + (-y) ... (a)
                           { 7,  1 }, // This line says that x alone  ... (b)
                         };
            double[] b ={
                            200, // (a) ... should be equal to 5.
                            380, // (b) ... should be greater than or equal to 10.
                        };
            //int numberOfEqualities = 1;


            // Alternatively, we may use an explicit form:
            var constraints = new List<LinearConstraint>()
            {
                // Define the first constraint, which involves only x
                new LinearConstraint(numberOfVariables: 1)
                {
                    // x is the first variable, thus located at
                    // index 0. We are specifying that x >= 10:

                    VariablesAtIndices = new[] { 0 }, // index 0 (x)
                    ShouldBe = ConstraintType.GreaterThanOrEqualTo,
                    Value = 200
                },

                // Define the second constraint, which involves x and y
                new LinearConstraint(numberOfVariables: 2)
                {
                   // x is the first variable, located at index 0, and y is
                   // the second, thus located at 1. We are specifying that
                   // x - y = 5 by saying that the variable at position 0 
                   // times 1 plus the variable at position 1 times -1 
                   // should be equal to 5.

                    VariablesAtIndices = new int[] { 0, 1 }, // index 0 (x) and index 1 (y)
                    CombinedAs = new double[] { 1, -1 }, // when combined as x - y
                    ShouldBe = ConstraintType.EqualTo,
                    Value = 380
                }
            };
            // Now we can finally create our optimization problem
            var solver = new GoldfarbIdnani(
                function: new QuadraticObjectiveFunction(Q, d),
                constraints: constraints);


            // And attempt solve for the min:
            bool success = solver.Minimize();

            // The solution was { 10, 5 }
            double[] solution = solver.Solution;

            // With the minimum value 170.0
            double minValue = solver.Value;
        }





        private void button1_Click(object sender, EventArgs e)
        {
            double[,] Q = {   { 0.0001*2,  0    },
                              { 0       ,  0.0001*2 },
                           };
            double[] d = { -7 , -4  }; 
            double[,] A ={ { 3, 3 }, 
                           { 7,  1 }, 
                         };
            double[] b ={
                            200, 
                            380, 
                        };
            int numberOfEqualities = 1;


            // Alternatively, we may use an explicit form:
            var constraints = new List<LinearConstraint>()
            {
                // Define the first constraint, which involves only x
                new LinearConstraint(numberOfVariables: 2)
                {
                    // x is the first variable, thus located at
                    // index 0. We are specifying that x >= 10:

                   VariablesAtIndices = new int[] { 0, 1 },
                    CombinedAs = new double[] { 3, 3 },
                    ShouldBe = ConstraintType.LesserThanOrEqualTo,
                    Value = 200
                },

                // Define the second constraint, which involves x and y
                new LinearConstraint(numberOfVariables: 2)
                {
                   // x is the first variable, located at index 0, and y is
                   // the second, thus located at 1. We are specifying that
                   // x - y = 5 by saying that the variable at position 0 
                   // times 1 plus the variable at position 1 times -1 
                   // should be equal to 5.

                    VariablesAtIndices = new int[] { 0, 1 }, // index 0 (x) and index 1 (y)
                    CombinedAs = new double[] { 7, 1 }, // when combined as x - y
                    ShouldBe = ConstraintType.LesserThanOrEqualTo,
                    Value = 380
                }
            };
            // Now we can finally create our optimization problem
            var solver = new GoldfarbIdnani(
                function: new QuadraticObjectiveFunction(Q, d),
                constraints: constraints);


            // And attempt solve for the min:
            bool success = solver.Minimize();

            // The solution was { 10, 5 }
            double[] solution = solver.Solution;

            // With the minimum value 170.0
            double minValue = solver.Value;
        }
    }
}
