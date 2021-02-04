using System;
using SQLite;

namespace Mathster.Resources.Database_Models
{
    public class DBModel
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }
        [MaxLength(12)] public string Name { get; set; }
        public int Experience { get; set; }
        public int TotalExercises { get; set; }
        public int TotalAdd { get; set; }
        public int TotalAddCorrect { get; set; }
        public int TotalSub { get; set; }
        public int TotalSubCorrect { get; set; }
        public int TotalMul { get; set; }
        public int TotalMulCorrect { get; set; }
        public int TotalDiv { get; set; }
        public int TotalDivCorrect { get; set; }
        public int TotalLinear { get; set; }
        public int TotalLinearCorrect { get; set; }
        public int TotalQuadratic { get; set; }
        public int TotalQuadraticCorrect { get; set; }
        public int TotalSquare { get; set; }
        public int TotalSquareCorrect { get; set; }
        public int TotalExercisesCorrect { get; set; }

        public void GetLevel(out int level, out double progress, DBModel table)
        {
            level = (int) Math.Sqrt(table.Experience) / 20;
            progress = Math.Sqrt(table.Experience) / 20 - level;
        }

        public void AddGoodStats(byte exerciseType, DBModel table)
        {
            switch (exerciseType)
            {
                case 1:
                    table.TotalAddCorrect++;
                    break;

                case 2:
                    table.TotalSubCorrect++;
                    break;

                case 3:
                    table.TotalMulCorrect++;
                    break;

                case 4:
                    table.TotalDivCorrect++;
                    break;

                case 5:
                    table.TotalLinearCorrect++;
                    break;

                case 6:
                    table.TotalQuadraticCorrect++;
                    break;

                case 7:
                    table.TotalSquareCorrect++;
                    break;
            }

            table.TotalExercisesCorrect++;
            AddStats(exerciseType, table);
        }

        public void AddStats(byte exerciseType, DBModel table)
        {
            switch (exerciseType)
            {
                case 1:
                    table.TotalAdd++;
                    break;

                case 2:
                    table.TotalSub++;
                    break;

                case 3:
                    table.TotalMul++;
                    break;

                case 4:
                    table.TotalDiv++;
                    break;

                case 5:
                    table.TotalLinear++;
                    break;

                case 6:
                    table.TotalQuadratic++;
                    break;

                case 7:
                    table.TotalSquare++;
                    break;
            }

            table.TotalExercises++;
        }
    }
}