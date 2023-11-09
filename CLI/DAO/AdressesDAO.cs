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
    class AdressesDAO
    {
        private readonly List<Adresa> _adresses;
        private readonly Storage<Adresa> _storage;


        public AdressesDAO()
        {
            _storage = new Storage<Adresa>("adresses.txt");
            _adresses = _storage.Load();
        }

        private int GenerateId()
        {
            if (_adresses.Count == 0) return 0;
            return _adresses[^1].AdresaId + 1;
        }

        public Adresa AddAdresa(Adresa adresa)
        {
            adresa.AdresaId = GenerateId();
            _adresses.Add(adresa);
            _storage.Save(_adresses);
            return adresa;
        }

        public Adresa? UpdateAdresa(Adresa adresa)
        {
            Adresa? oldAdresa = GetAdresaById(adresa.AdresaId);
            if (oldAdresa is null) return null;

            oldAdresa.Ulica = adresa.Ulica;
            oldAdresa.Broj = adresa.Broj;
            oldAdresa.Grad = adresa.Grad;
            oldAdresa.Drzava = adresa.Drzava;

            _storage.Save(_adresses);
            return oldAdresa;
        }

        public Adresa? RemoveAdresa(int id)
        {
            Adresa? adresa = GetAdresaById(id);
            if (adresa == null) return null;

            _adresses.Remove(adresa);
            _storage.Save(_adresses);
            return adresa;
        }

        private Adresa? GetAdresaById(int id)
        {
            return _adresses.Find(s => s.AdresaId == id);
        }

        public List<Adresa> GetAllAdresas()
        {
            return _adresses;
        }
    }
}
