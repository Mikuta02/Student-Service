﻿using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLI.DAO
{
     public class ExamGradesDAO
    {
        private readonly List<OcenaNaIspitu> _ocene;
        private readonly Storage<OcenaNaIspitu> _storage;


        public ExamGradesDAO()
        {
            _storage = new Storage<OcenaNaIspitu>("grades.txt");
            _ocene = _storage.Load();
        }

        private int GenerateId()
        {
            if (_ocene.Count == 0) return 0;
            return _ocene[^1].OcenaNaIspituId + 1;
        }

        public OcenaNaIspitu AddExamGrade(OcenaNaIspitu ocena)
        {
            ocena.OcenaNaIspituId = GenerateId();
            StudentDAO studentDAO = new StudentDAO();
            SubjectDAO subjectDAO = new SubjectDAO();

            Student? student = AddStudentToExamGrade(ocena, studentDAO);
            Predmet? subject = AddSubjectToExamGrade(ocena, subjectDAO);
            if (student == null || subject == null)
            {
                return null;
            }
            ocena.StudentPolozio = student;
            ocena.PredmetStudenta = subject;
            studentDAO.PassOrFailExam(student.StudentId, subject.PredmetId, ocena, true);
            subjectDAO.PassOrFailExam(student.StudentId, subject.PredmetId, ocena, true);
            // TO DO:
            // VRATITI PREDMET ODNOSNO STUDENT NA NEPOLOZENE LISTE KAD SE IZBRISE EXAMGRADE OBJEKAT. TAKODJE FILLOBJECTS URADITI
            // SAD ME MRZI A I NE TREBA ZA OVU KT2...
            _ocene.Add(ocena);
            _storage.Save(_ocene);
            return ocena;
        }


        private Student? AddStudentToExamGrade(OcenaNaIspitu ocena, StudentDAO studentDAO)
        {
            List<Student> students = studentDAO.GetAllStudents();

            Student? student = students.Find(p => p.StudentId == ocena.StudentId);
            if (student != null)
            {
                //subjectDAO.AddStudentToPassed(student);
                return student;
            }
            else
            {
                System.Console.WriteLine("Error: student not found");
                return null;
            }

        }

        // private Adresa? AddAdresaToStudent(Student student)
        private Predmet? AddSubjectToExamGrade(OcenaNaIspitu ocena, SubjectDAO subjectDAO)
        {
            List<Predmet> subjects = subjectDAO.GetAllPredmets();

            Predmet? subject = subjects.Find(p => p.PredmetId == ocena.PredmetId);
            if (subject != null)
            {
                return subject;
            }
            else
            {
                System.Console.WriteLine("Error: subject not found");
                return null;
            }

        }

        public OcenaNaIspitu? UpdateExamGrade(OcenaNaIspitu ocena)
        {
            OcenaNaIspitu? oldOcenaNaIspitu = GetGradeById(ocena.OcenaNaIspituId);
            if (oldOcenaNaIspitu is null) return null;

            oldOcenaNaIspitu.StudentId = ocena.StudentId;
            oldOcenaNaIspitu.PredmetId = ocena.PredmetId;
            oldOcenaNaIspitu.Ocena = ocena.Ocena;
            oldOcenaNaIspitu.DatumPolaganja = ocena.DatumPolaganja;

            _storage.Save(_ocene);
            return oldOcenaNaIspitu;
        }
        public OcenaNaIspitu? RemoveExamGrade(int id)
        {
            StudentDAO studentDAO = new StudentDAO();
            SubjectDAO subjectDAO = new SubjectDAO();

            OcenaNaIspitu? ocena = GetGradeById(id);
            if (ocena == null) return null;

            studentDAO.PassOrFailExam(ocena.StudentId, ocena.PredmetId, ocena, false);
            subjectDAO.PassOrFailExam(ocena.StudentId, ocena.PredmetId, ocena, false);

            _ocene.Remove(ocena);
            _storage.Save(_ocene);
            return ocena;
        }
        private OcenaNaIspitu? GetGradeById(int id)
        {
            return _ocene.Find(s => s.OcenaNaIspituId == id);
        }

        public List<OcenaNaIspitu> GetAllGrades()
        {
            return _ocene;
        }
        
        public void fillObjectsAndLists(StudentDAO studentsDao, SubjectDAO subjectDAO)
        {
            List<Student> students = studentsDao.GetAllStudents();
            List<Predmet> subjects = subjectDAO.GetAllPredmets();

            foreach(Student student in students)
            {
                foreach(OcenaNaIspitu examGrade in student.SpisakPolozenihIspita)
                {
                    examGrade.StudentPolozio = student;
                    examGrade.PredmetStudenta = subjects.Find(s => s.PredmetId == examGrade.PredmetId);
                }
            }
        }
    }
}
