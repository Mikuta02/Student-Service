using CLI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.DTO
{
    public class ExamGradeDTO : INotifyPropertyChanged
    {
        public int OcenaNaIspituId { get; set; }
        private int studentID { get; set; }
        public int StudentID
        {
            get
            {
                return studentID;
            }
            set
            {
                if (studentID != value)
                {
                    studentID = value;
                    OnPropertyChanged("StudentID");
                }
            }
        }
        private int predmetId { get; set; }
        public int PredmetId
        {
            get
            {
                return predmetId;
            }
            set
            {
                if (predmetId != value)
                {
                    predmetId = value;
                    OnPropertyChanged("PredmetId");
                }
            }
        }
        private int ocena { get; set; }
        public int Ocena
        {
            get
            {
                return ocena;
            }
            set
            {
                if (ocena != value)
                {
                    ocena = value;
                    OnPropertyChanged("Ocena");
                }
            }
        }
        private DateTime datumPolaganja { get; set; }
        public DateTime DatumPolaganja
        {
            get
            {
                return datumPolaganja;
            }
            set
            {
                if (datumPolaganja != value)
                {
                    datumPolaganja = value;
                    OnPropertyChanged("DatumPolaganja");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public ExamGradeDTO()
        {

        }

        public ExamGradeDTO(OcenaNaIspitu examGrade)
        {
            OcenaNaIspituId = examGrade.OcenaNaIspituId;
            studentID = examGrade.StudentId;
            predmetId = examGrade.PredmetId;
            ocena = examGrade.Ocena;
            datumPolaganja = examGrade.DatumPolaganja;
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
