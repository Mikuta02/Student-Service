﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    class Katedra : Serialization.ISerializable
    {
        public int SifraKatedre { get; set; }
        public string NazivKatedre { get; set; }

        public string Sef {  get; set; } //Pitati asistente sta dodje sef
        public List<Profesor> Profesori { get; set; }

        public Katedra()
        {
            Profesori = new List<Profesor>();
        }

        public Katedra(int sifra, string nazivKatedre)
        {
            SifraKatedre = sifra;
            NazivKatedre = nazivKatedre;
            Profesori = new List<Profesor>();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SIFRA KATEDRE: " + SifraKatedre + ", ");
            sb.Append("NAZIV KATEDRE: " + NazivKatedre + ", ");
            sb.Append("PROFESORI: ");
            sb.AppendJoin(", ", Profesori.Select(profesor => profesor.Ime));
           

            return sb.ToString();
        }
        public string[] ToCSV()
        {
            string[] csvValues =
            {
            SifraKatedre.ToString(),
            NazivKatedre,
            Sef
        };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            SifraKatedre = int.Parse(values[0]);
            NazivKatedre = values[1];
            Sef = values[2];
        }


    }
}
