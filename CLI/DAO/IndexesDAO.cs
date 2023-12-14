using CLI.Model;
using CLI.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CLI.DAO
{
    public class IndexesDAO
    {
        /*        private readonly List<Indeks> _indexes;
                private readonly Storage<Indeks> _storage;


                public IndexesDAO()
                {
                    _storage = new Storage<Indeks>("indexes.txt");
                    _indexes = _storage.Load();
                }

                private int GenerateId()
                {
                    if (_indexes.Count == 0) return 0;
                    return _indexes[^1].IndeksId + 1;
                }

                public Indeks AddIndeks(Indeks indeks)
                {
                    indeks.IndeksId = GenerateId();
                    _indexes.Add(indeks);
                    _storage.Save(_indexes);
                    return indeks;
                }

                public Indeks? UpdateIndeks(Indeks indeks)
                {
                    Indeks? oldIndeks = GetIndeksById(indeks.IndeksId);
                    if (oldIndeks is null) return null;

                    oldIndeks.OznakaSmera = indeks.OznakaSmera;
                    oldIndeks.BrojUpisa = indeks.BrojUpisa;
                    oldIndeks.GodinaUpisa = indeks.GodinaUpisa;


                    _storage.Save(_indexes);
                    return oldIndeks;
                }

                public Indeks? RemoveIndeks(int id)
                {
                    Indeks? indeks = GetIndeksById(id);
                    if (indeks == null) return null;

                    _indexes.Remove(indeks);
                    _storage.Save(_indexes);
                    return indeks;
                }

                private Indeks? GetIndeksById(int id)
                {
                    return _indexes.Find(s => s.IndeksId == id);
                }

                public List<Indeks> GetAllIndekss()
                {
                    return _indexes;
                }*/
    }
}
