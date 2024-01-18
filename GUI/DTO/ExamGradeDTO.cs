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
        private Student studentPolozio { get; set; }
        public Student StudentPolozio
        {
            get
            {
                return studentPolozio;
            }
            set
            {
                if (studentPolozio != value)
                {
                    studentPolozio = value;
                    OnPropertyChanged("StudentPolozio");
                }
            }
        }
        public Predmet predmetStudenta { get; set; }
        public Predmet PredmetStudenta
        {
            get
            {
                return predmetStudenta;
            }
            set
            {
                if (predmetStudenta != value)
                {
                    predmetStudenta = value;
                    OnPropertyChanged("PredmetStudenta");
                }
            }
        }
        public string sifraPredmeta { get; set; }
        public string SifraPredmeta
        {
            get
            {
                return sifraPredmeta;
            }
            set
            {
                if (sifraPredmeta != value)
                {
                    sifraPredmeta = value;
                    OnPropertyChanged("SifraPredmeta");
                }
            }
        }
        public string naziv { get; set; }
        public string Naziv
        {
            get
            {
                return naziv;
            }
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    OnPropertyChanged("Naziv");
                }
            }
        }

        public int espb { get; set; }
        public int ESPB
        {
            get
            {
                return espb;
            }
            set
            {
                if (espb != value)
                {
                    espb = value;
                    OnPropertyChanged("ESPB");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public OcenaNaIspitu toExamGrade()
        {
            return new OcenaNaIspitu(studentID, predmetId, ocena, datumPolaganja);
        }

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
            studentPolozio = examGrade.StudentPolozio;
            predmetStudenta = examGrade.PredmetStudenta;
            sifraPredmeta = examGrade.PredmetStudenta.SifraPredmeta;
            naziv = examGrade.PredmetStudenta.Naziv;
            espb = examGrade.PredmetStudenta.ESPB;
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
