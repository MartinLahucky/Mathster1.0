namespace Sterform.Mathster.Exercise
{
    public abstract class Exercise
    {
        #region Atributes

        public byte Id { get; set; }
        public string Assignment { get; set; }
        public string AssignmentUnder { get; set; }
        public int Result { get; set; }
        public int Result2 { get; set; }
        public float UserInput { get; set; }
        public float UserInput2 { get; set; }
        public byte ExerciseType { get; set; }
        public byte Experience { get; set; }
        public long CountLenght { get; set; }

        #endregion

        #region Constructors

        public Exercise()
        {
        }

        protected Exercise(byte id, string assignment, int result, byte exerciseType, byte experience)
        {
            Id = id;
            Assignment = assignment;
            Result = result;
            ExerciseType = exerciseType;
            Experience = experience;
        }

        public Exercise(byte id, string assignment, string assignmentUnder, int result, byte exerciseType,
            byte experience)
        {
            Id = id;
            Assignment = assignment;
            AssignmentUnder = assignmentUnder;
            Result = result;
            ExerciseType = exerciseType;
            Experience = experience;
        }

        protected Exercise(byte id, string assignment, int result, int result2, byte exerciseType, byte experience)
        {
            Id = id;
            Assignment = assignment;
            Result = result;
            Result2 = result2;
            ExerciseType = exerciseType;
            Experience = experience;
        }

        #endregion

        #region Methods

        public abstract string FormatAssigmentResult();
        public abstract string FormatAssigmentUserInput();
        public abstract string FormatUserInput();
        public abstract string FormatResult();

        public int GetExperience(bool correct)
        {
            if (correct) return (ExerciseType * (ExerciseType / 4) + 1) * Experience * 20;

            return ExerciseType * (ExerciseType / 4) + 1;
        }

        #endregion
    }
}