using System;

namespace Mathster.Resources.Exercises
{
    public class Equation : Exercise
    {
        #region Contructors

        public Equation() {}

        public Equation(byte id, string assignment, string assignmentUnder, int result, byte exerciseType,
            byte experience) : base(id, assignment, assignmentUnder, result, exerciseType, experience)
        {
        }

        #endregion


        public Exercise GenerateExercise(byte id, int numMin, int numMax)
        {
            return null;
        }

        private Exercise GenerateExerciseAdd()
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
            // TODO 
            return new Equation();
        }

        public override string FormatExercise()
        {
            throw new System.NotImplementedException();
        }

        public override int GetExperience(bool correct)
        {
            throw new System.NotImplementedException();
        }
    }
}