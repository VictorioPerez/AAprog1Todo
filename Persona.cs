using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABMPersonaMio1
{
    internal class Persona
    {
        private string apellido, nombres;
        private int tipoDocumento, documento, estadoCivil, sexo;
        private bool fallecio;

        public Persona()
        {
            apellido = nombres = "";
            tipoDocumento = documento = estadoCivil = sexo = 0;
            fallecio = false;
        }

        public Persona(string apellido, string nombres, int tipoDocumento,
                       int documento, int estadoCivil, int sexo, bool fallecio)
        {
            this.apellido = apellido;
            this.nombres = nombres;
            this.tipoDocumento = tipoDocumento;
            this.documento = documento;
            this.estadoCivil = estadoCivil;
            this.sexo = sexo;
            this.fallecio = fallecio;
        }

        public string pApellido
        {
            set { apellido = value; }
            get { return apellido; }
        }

        public string pNombres
        {
            set { nombres = value; }
            get { return nombres; }
        }

        public int pTipoDocumento
        {
            set { tipoDocumento = value; }
            get { return tipoDocumento; }
        }

        public int pDocumento
        {
            set { documento = value; }
            get { return documento; }
        }

        public int pEstadoCivil
        {
            set { estadoCivil = value; }
            get { return estadoCivil; }
        }

        public int pSexo
        {
            set { sexo = value; }
            get { return sexo; }
        }

        public bool pFallecio
        {
            set { fallecio = value; }
            get { return fallecio; }
        }

        override public string ToString()
        {
            return apellido + ", " + nombres;
        }
    }
}
