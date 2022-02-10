using MongoDB.Driver;
using Senai.SPMedicalGroup.webApi.Domains;
using Senai.SPMedicalGroup.webApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.SPMedicalGroup.webApi.Repositories
{
    public class LocalizacaoRepository : ILocalizacaoRepository

    {
        private readonly IMongoCollection<Localizacao> _localizacoes;
        public LocalizacaoRepository()
        {
            var client = new MongoClient("mongodb://127.0.0.1:27017");
            var database = client.GetDatabase("SPMedicalGroup");
            _localizacoes = database.GetCollection<Localizacao>("Localizacao");
        }
        public void Cadastrar(Localizacao novaLocalizacao)
        {
            _localizacoes.InsertOne(novaLocalizacao);
        }

        public List<Localizacao> ListarTodas()
        {
            return _localizacoes.Find(localizacao => true).ToList();
        }
    }
}
