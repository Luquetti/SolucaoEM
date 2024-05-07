﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EM.Domain.Interface;

namespace EM.Domain
{
    public  class Cidade : IEntidade
    {
        public string? NomeCidade { get; set; }
        public string? UF { get; set; }
        public int? ID_Cidade { get; set; }
        public Cidade()
        {
        }
        public Cidade(string nome, string uf)
        {
            NomeCidade = nome;
            UF = uf;
        }
    }
}
