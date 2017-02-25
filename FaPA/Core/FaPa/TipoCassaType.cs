using System;
using System.ComponentModel;

namespace FaPA.Core.FaPa
{
    [Serializable]
    public enum TipoCassaType
    {
        [Description( "Cassa Nazionale Previdenza e Assistenza Avvocati e Procuratori Legali" )]
        TC01,
        [Description( "Cassa Previdenza Dottori Commercialisti" )]
        TC02,
        [Description( "Cassa Previdenza e Assistenza Geometri" )]
        TC03,
        [Description( "Cassa Nazionale Previdenza e Assistenza Ingegneri e Architetti Liberi Professionisti" )]
        TC04,
        [Description( "Cassa Nazionale del Notariato" )]
        TC05,
        [Description( "Cassa Nazionale Previdenza e Assistenza Ragionieri e Periti Commerciali" )]
        TC06,
        [Description( "Ente Nazionale Assistenza Agenti e Rappresentanti di Commercio (ENASARCO)" )]
        TC07,
        [Description( "Ente Nazionale Previdenza e Assistenza Consulenti del Lavoro (ENPACL)" )]
        TC08,
        [Description( "Ente Nazionale Previdenza e Assistenza Medici (ENPAM)" )]
        TC09,
        [Description( "Ente Nazionale Previdenza e Assistenza Farmacisti (ENPAF)" )]
        TC10,
        [Description( "Ente Nazionale Previdenza e Assistenza Veterinari (ENPAV)" )]
        TC11,
        [Description( "Ente Nazionale Previdenza e Assistenza Impiegati dell'Agricoltura (ENPAIA)" )]
        TC12,
        [Description( "Fondo Previdenza Impiegati Imprese di Spedizione e Agenzie Marittime" )]
        TC13,
        [Description( "Istituto Nazionale Previdenza Giornalisti Italiani (INPGI)" )]
        TC14,
        [Description( "Opera Nazionale Assistenza Orfani Sanitari Italiani (ONAOSI)" )]
        TC15,
        [Description( "Cassa Autonoma Assistenza Integrativa Giornalisti Italiani (CASAGIT)" )]
        TC16,
        [Description( "Ente Previdenza Periti Industriali e Periti Industriali Laureati (EPPI)" )]
        TC17,
        [Description( "Ente Previdenza e Assistenza Pluricategoriale (EPAP)" )]
        TC18,
        [Description( "Ente Nazionale Previdenza e Assistenza Biologi (ENPAB)" )]
        TC19,
        [Description( "Ente Nazionale Previdenza e Assistenza Professione Infermieristica (ENPAPI)" )]
        TC20,
        [Description( "Ente Nazionale Previdenza e Assistenza Psicologi (ENPAP)" )]
        TC21,
        [Description( "INPS" )]
        TC22
    }
}