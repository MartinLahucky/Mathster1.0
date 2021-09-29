using System;
using static Sterform.Utilities;

namespace Sterform.Mathster.Exercise
{
    public class Equation : Exercise
    {
        #region Contructors

        public Equation()
        {
        }

        private Equation(byte id, string assignment, int result, byte exerciseType,
            byte experience) : base(id, assignment, result, exerciseType, experience)
        {
        }

        private Equation(byte id, string assignment, int result, int result2, byte exerciseType,
            byte experience) : base(id, assignment, result, result2, exerciseType, experience)
        {
            Result2 = result2;
        }

        #endregion

        # region Methods

        public Exercise GenerateExercise(byte id, byte exercise)
        {
            return exercise switch
            {
                5 => GenerateEquationNormal(id),
                6 => GenerateQuadraticEquation(id),
                7 => GenerateSquareEquation(id),
                _ => null
            };
        }

        private Exercise GenerateEquationNormal(byte id)
        {
            var random = new Random();

            int result = random.Next(-3, 5),
                numMul = random.Next(2, 5),
                numExtra = random.Next(-20, 21),
                xExtra = random.Next(-20, 21);

            var experience = (byte) numMul.ToString().Length;
            experience += (byte) numExtra.ToString().Length;
            experience += (byte) xExtra.ToString().Length;

            string assignment;

            switch (random.Next(0, 3))
            {
                case 0:
                    assignment = numExtra >= 0 ? $"{numMul}x +{numExtra} = " : $"{numMul}x {numExtra} = ";
                    assignment += $"{numMul * result + numExtra}";
                    break;

                case 1:
                    assignment = xExtra >= 0
                        ? $"{numMul + xExtra}x = {numMul * result} +{xExtra}x"
                        : $"{numMul + xExtra}x = {numMul * result} {xExtra}x";

                    break;

                default:
                    assignment = (numExtra >= 0, xExtra >= 0) switch
                    {
                        (true, true) => $"{numMul + xExtra}x +{numExtra} = {result * numMul + numExtra} +{xExtra}x",
                        (true, false) => $"{numMul + xExtra}x +{numExtra} = {result * numMul + numExtra} {xExtra}x",
                        (false, true) => $"{numMul + xExtra}x {numExtra} = {result * numMul + numExtra} +{xExtra}x",
                        (false, false) => $"{numMul + xExtra}x {numExtra} = {result * numMul + numExtra} {xExtra}x"
                    };

                    break;
            }

            return new Equation(id, assignment, result, 5, experience);
        }

        private Exercise GenerateSquareEquation(byte id)
        {
            var random = new Random();
            int res1 = random.Next(-10, 10), res2 = random.Next(-10, 10);
            var experience = (byte) ((res1.ToString().Length + res2.ToString().Length) * 3);
            return new Equation(id, GenerateCompletingTheSquare(res1, res2), res1, res2, 7, experience);
        }

        private Exercise GenerateQuadraticEquation(byte id, int numMin = -10, int numMax = 10)
        {
            var random = new Random();
            int res1 = random.Next(numMin, numMax), res2 = random.Next(numMin, numMax);
            var experience = (byte) ((res1.ToString().Length + res2.ToString().Length) * 2);
            return new Equation(id, CreateEquation(res1, res2), res1, res2, 6, experience);
        }

        // DOPLNĚNÍ NA ČTVEREC
        // - tady u tohodle jsou dvě možnosti - jedna se z hlavy počítá docela těžce je tady ta jednodušší
        // - řešení tady těchlentěch je ve skutečnosti celý výraz (například (x + 3)^2 - 10 ), ale jediné co se tu mění jsou 
        // dvě čísla, takže ty jsem dal že jsou ty odpovědi co by uživatel zadával 
        private string GenerateCompletingTheSquare(int res1, int res2)
        {
            return $"x^2{FormatNumber(2 * res1, "x")}{FormatNumber(res2 + res1 * res1)}";
        }

        // KVADRATICKÁ ROVNICE
        private string CreateEquation(int res1, int res2)
        {
            return $"x^2{FormatNumber(-(res1 + res2), "x")}{FormatNumber(res1 * res2)} = 0";
        }

        // metoda na generování jednočlenu - šla by použít i v rovnicích co už jsme implementovali 


        public override string FormatAssigmentResult()
        {
            return ExerciseType switch
            {
                5 => $"{Assignment}\nx = {Result}",
                6 => $"{Assignment}\nx1 = {Result}\nx2 = {Result2}",
                7 => $"{Assignment}\n= (x{FormatNumber(Result)})^2{FormatNumber(Result2)}",
                _ => null
            };
        }

        public override string FormatAssigmentUserInput()
        {
            return ExerciseType switch
            {
                5 => $"{Assignment}\nx = {UserInput}",
                6 => $"{Assignment}\nx1 = {UserInput}\nx2 = {UserInput2}",
                7 => $"{Assignment}=\n (x{FormatNumber(UserInput)})^2{FormatNumber(UserInput2)}",
                _ => null
            };
        }

        public override string FormatUserInput()
        {
            return ExerciseType switch
            {
                5 => $"{UserInput}",
                6 => $"x1 = {UserInput}  x2 = {UserInput2}",
                7 => $"= (x{FormatNumber(UserInput)})^2{FormatNumber(UserInput2)}",
                _ => null
            };
        }

        public override string FormatResult()
        {
            return ExerciseType switch
            {
                5 => $"{Result}",
                6 => $"x1 = {Result}  x2 = {Result2}",
                7 => $"= (x{FormatNumber(Result)})^2{FormatNumber(Result2)}",
                _ => null
            };
        }

        # endregion
    }
}