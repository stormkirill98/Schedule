using System;
using System.Collections.Generic;
using System.Data;

namespace Database
{
    public interface HasId
    {
        int Id { get; }
    }

    public struct DayOfWeek : HasId
    {
        public int Id { get; }
        public string Day { get; }
        public string Abbreviation { get; }

        public DayOfWeek(DataRow row)
        {
            Id = (int)row["Key"];
            Day = (string)row["DayOfWeek"];
            Abbreviation = (string)row["Abbreviation"];
        }
    }

    public struct StudyGroup : HasId
    {
        public int Id { get; }
        public string Name { get; }

        public StudyGroup(DataRow row)
        {
            Id = (int)row["Key"];
            Name = (string)row["Group"];
        }
    }

    public struct Discipline : HasId
    {
        public int Id { get; }
        public string Name { get; set; }

        public Discipline(DataRow row)
        {
            Id = (int)row["Key"];
            Name = (string)row["Discipline"];
        }
    }

    public struct DisciplineType : HasId
    {
        public int Id { get; }
        public string Type { get; }

        public DisciplineType(DataRow row)
        {
            Id = (int)row["Key"];
            Type = (string)row["Type"];
        }
    }

    public struct Parity : HasId
    {
        public int Id { get; }
        public string Week { get; }

        public Parity(DataRow row)
        {
            Id = (int)row["Key"];
            Week = (string)row["Week"];
        }
    }

    public struct Teacher : HasId
    {
        public int Id { get; }
        public string FirstName { get; set; }
        public string Patronymic { get; set; }
        public string LastName { get; set; }
        public string Cathedra { get; set; }

        public string FullName { get; set; }

        public Teacher(DataRow row)
        {
            Id = (int)row["Key"];
            FirstName = (string)row["FirstName"];
            Patronymic = (string)row["Patronymic"];
            LastName = (string)row["LastName"];
            Cathedra = (string)row["Cathedra"];

            FullName = string.Format("{0} {1} {2}", LastName, Patronymic, FirstName);
        }
    }

    public struct Cabinet : HasId
    {
        public int Id { get; }
        public int Floor { get; set; }
        public int Number { get; set; }

        public Cabinet(DataRow row)
        {
            Id = (int)row["Key"];
            Floor = (int)row["Floor"];
            Number = (int)row["Number"];
        }
    }

    public struct Lesson : HasId
    {
        public int Id { get; }
        public int Number { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public Lesson(DataRow row)
        {
            Id = (int)row["Key"];
            Number = (int)row["Number"];
            StartTime = (string)row["StartTime"];
            EndTime = (string)row["EndTime"];
        }
    }
}
