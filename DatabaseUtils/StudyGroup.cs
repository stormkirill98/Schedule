using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public struct StudyGroup
    {
        public int Id { get; }
        public string Name { get; }

        public StudyGroup(DataRow row)
        {
            Id = (int) row["Key"];
            Name = (string) row["Group"];
        }
    }
}
