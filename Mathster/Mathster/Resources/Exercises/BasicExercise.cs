using System;

namespace Mathster.Resources.Exercises
{
    public class BasicExercise : Exercise
    {
        #region Constructors

        public BasicExercise() { }

        private BasicExercise(byte id, string assignment, string assignmentUnder, int result, byte exerciseType,
            byte experience) : base(id, assignment, assignmentUnder, result, exerciseType, experience) { }

        #endregion

        #region Methods

        public BasicExercise GenerateExercise(byte id, int numMin, int numMax, byte exerciseType, byte mulDivMin = 0,
            byte mulDivMax = 0)
        {
            Random random = new Random();

            int firstNum = random.Next(numMin, numMax), secondNum = random.Next(numMin, numMax);
            switch (exerciseType)
            {
                case 1:
                    return new BasicExercise(id, $"{firstNum} + {secondNum} = ",
                        $" {firstNum}\n+{secondNum}\n—",
                        firstNum + secondNum, exerciseType, (byte) firstNum.ToString().Length);

                case 2:
                    return new BasicExercise(id, $"{firstNum} - {secondNum} = ",
                        $" {firstNum}\n-{secondNum}\n—",
                        firstNum - secondNum, exerciseType, (byte) firstNum.ToString().Length);

                case 3:
                    secondNum = random.Next(mulDivMin, mulDivMax);
                    return new BasicExercise(id, $"{firstNum} X {secondNum} = ",
                        $" {firstNum}\nX{secondNum}\n—",
                        firstNum * secondNum, exerciseType, (byte) firstNum.ToString().Length);

                case 4:
                    secondNum = random.Next(mulDivMin, mulDivMax);
                    while (firstNum % secondNum != 0)
                    {
                        firstNum = random.Next(numMin, numMax);
                    }

                    return new BasicExercise(id, $"{firstNum} ÷ {secondNum} = ",
                        $" {firstNum}\n-{secondNum}\n—",
                        firstNum / secondNum, exerciseType, (byte) firstNum.ToString().Length);

                case 0:
                    return GenerateExercise(id, numMin, numMax, (byte) random.Next(1, 5), mulDivMin, mulDivMax);
            }

            return null;
        }

        public override string FormatExercise()
        {
            if (Assignment.Length >= 13)
            {
                return $"{Assignment}\n= {UserInput}";
            }

            return $"{Assignment}{UserInput}";
        }

        #endregion
    }
}