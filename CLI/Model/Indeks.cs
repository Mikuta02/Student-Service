﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.Model
{
    internal class Indeks
    {

        public string OznakaSmera {  get; set; }
        public int BrojUpisa { get; set; }
        public int GodinaUpisa { get; set; }


        public Indeks(string oznakaSmera, int brojUpisa, int godinaUpisa)
        {
            OznakaSmera = oznakaSmera;
            BrojUpisa = brojUpisa;
            GodinaUpisa = godinaUpisa;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("OZNAKA SMERA: {OznakaSmera}, ");
            sb.Append("BROJ UPISA: {BrojUpisa}, ");
            sb.Append("GODINA UPISA: {GodinaUpisa}");
            return base.ToString();
        }
    }
}