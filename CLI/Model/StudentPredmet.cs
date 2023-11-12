using CLI.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.Model
{
    class StudentPredmet : ISerializable
    {
        public int StudentId { get; set; }

        public int SubjectId { get; set; }

        public StudentPredmet()
        {
        }

        public StudentPredmet(int studentId, int subjectId)
        {
            StudentId = studentId;
            SubjectId = subjectId;
        }

        public void FromCSV(string[] values)
        {
            StudentId = int.Parse(values[0]);
            SubjectId = int.Parse(values[1]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
            StudentId.ToString(),
            SubjectId.ToString()
        };
            return csvValues;
        }
    }
}
