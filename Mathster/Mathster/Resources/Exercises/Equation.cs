using System;
using static Mathster.Resources.Helpers.Utilities;

namespace Mathster.Resources.Exercises
{
    public class Equation : Exercise
    {
        #region Contructors

        public Equation()
        {
        }

        public Equation(byte id, string assignment, int result, byte exerciseType,
            byte experience) : base(id, assignment, result, exerciseType, experience)
        {
        }

        public Equation(byte id, string assignment, int result, int result2, byte exerciseType,
            byte experience) : base(id, assignment, result, result2, exerciseType, experience)
        {
            Result2 = result2;
        }

        #endregion

        public Exercise GenerateExercise(byte id, byte exercise)
        {
            switch (exercise)
            {
                case 5:
                    return GenerateEquationNormal(id);

                case 6:
                    return GenerateQuadraticEquation(id);

                case 7:
                    return GenerateSquareEquation(id);

                default:
                    return null;
            }
        }

        private Exercise GenerateEquationNormal(byte id)
        {
            Random random = new Random();

            int result = random.Next(-3, 5),
                numMul = random.Next(2, 5),
                numExtra = random.Next(-20, 21),
                xExtra = random.Next(-20, 21);

            byte experience = (byte) numMul.ToString().Length;
            experience += (byte) numExtra.ToString().Length;
            experience += (byte) xExtra.ToString().Length;

            string assignment = String.Empty;

            switch (random.Next(0, 3))
            {
                case 0:
                    if (numExtra >= 0)
                    {
                        assignment = $"{numMul}x +{numExtra} = ";
                    }
                    else
                    {
                        assignment = $"{numMul}x {numExtra} = ";
                    }

                    assignment += $"{numMul * result + numExtra}";
                    break;

                case 1:
                    if (xExtra >= 0)
                    {
                        assignment = $"{numMul + xExtra}x = {numMul * result} +{xExtra}x";
                    }
                    else
                    {
                        assignment = $"{numMul + xExtra}x = {numMul * result} {xExtra}x";
                    }

                    break;

                default:
                    switch (numExtra >= 0, xExtra >= 0)
                    {
                        case (true, true):
                            assignment = $"{numMul + xExtra}x +{numExtra} = {result * numMul + numExtra} +{xExtra}x";
                            break;
                        case (true, false):
                            assignment = $"{numMul + xExtra}x +{numExtra} = {result * numMul + numExtra} {xExtra}x";
                            break;
                        case (false, true):
                            assignment = $"{numMul + xExtra}x {numExtra} = {result * numMul + numExtra} +{xExtra}x";
                            break;
                        case (false, false):
                            assignment = $"{numMul + xExtra}x {numExtra} = {result * numMul + numExtra} {xExtra}x";
                            break;
                    }

                    break;
            }

            return new Equation(id, assignment, result, 5, experience);
        }

        private Exercise GenerateSquareEquation(byte id)
        {
            Random random = new Random();
            int res1 = random.Next(-10, 10), res2 = random.Next(-10, 10);
            byte experience = (byte) ((res1.ToString().Length + res2.ToString().Length) * 3);
            return new Equation(id, GenerateCompletingTheSquare(res1, res2), res1, res2, 7, experience);
        }

        private Exercise GenerateQuadraticEquation(byte id, int numMin = -10, int numMax = 10)
        {
            Random random = new Random();
            int res1 = random.Next(numMin, numMax), res2 = random.Next(numMin, numMax);
            byte experience = (byte) ((res1.ToString().Length + res2.ToString().Length) * 2);
            return new Equation(id, CreateEquation(res1, res2), res1, res2, 6, experience);
        }

        // DOPLNĚNÍ NA ČTVEREC
        // - tady u tohodle jsou dvě možnosti - jedna se z hlavy počítá docela těžce je tady ta jednodušší
        // - řešení tady těchlentěch je ve skutečnosti celý výraz (například (x + 3)^2 - 10 ), ale jediné co se tu mění jsou 
        // dvě čísla, takže ty jsem dal že jsou ty odpovědi co by uživatel zadával 
        private string GenerateCompletingTheSquare(int res1, int res2)
        {
            return $"x^2{FormartNumber(2 * res1, "x")}{FormartNumber(res2 + res1 * res1)}";
        }

        // KVADRATICKÁ ROVNICE
        private string CreateEquation(int res1, int res2)
        {
            return $"x^2{FormartNumber(-(res1 + res2), "x")}{FormartNumber(res1 * res2)} = 0";
        }

        // metoda na generování jednočlenu - šla by použít i v rovnicích co už jsme implementovali 


        public override string FormatAssigmentResult()
        {
            switch (ExerciseType)
            {
                case 5:
                    return $"{Assignment}\nx = {Result}";

                case 6:
                    return $"{Assignment}\nx1 = {Result}\nx2 = {Result2}";

                case 7:
                    return $"{Assignment}\n= (x{FormartNumber(Result)})^2{FormartNumber(Result2)}";

                default:
                    return null;
            }
        }

        public override string FormatAssigmentUserInput()
        {
            switch (ExerciseType)
            {
                case 5:
                    return $"{Assignment}\nx = {UserInput}";

                case 6:
                    return $"{Assignment}\nx1 = {UserInput}\nx2 = {UserInput2}";

                case 7:
                    return $"{Assignment}\n= (x{FormartNumber(UserInput)})^2{FormartNumber(UserInput2)}";

                default:
                    return null;
            }
        }

        public override string FormatUserInput()
        {
            switch (ExerciseType)
            {
                case 5:
                    return $"{UserInput}";

                case 6:
                    return $"x1 = {UserInput}  x2 = {UserInput2}";

                case 7:
                    return null;

                default:
                    return null;
            }
        }

        public override string FormatResult()
        {
            switch (ExerciseType)
            {
                case 5:
                    return $"{Result}";

                case 6:
                    return $"x1 = {Result}  x2 = {Result2}";

                case 7:
                    return null;

                default:
                    return null;
            }
        }
    }
}