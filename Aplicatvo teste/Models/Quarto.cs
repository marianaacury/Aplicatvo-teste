using System;
using System.Collections.Generic;
using System.Text;

namespace Aplicatvo_teste.Models
{
    public class Quarto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public string Icone { get; set; } = "🏡";

        public string Imagem { get; set; }

        public bool Selecionado { get; set; }

    }
}
