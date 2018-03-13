using Competition.Context;
using System.Collections.Generic;

namespace Competition.Models
{
    public class TeamModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Saugojama tik Komandos narių Vardas ir Pavardė string formatu;
        // Ir ne visada bus užpildomas šitas laukas
        public List<string> Teammates { get; set; }

        public TeamModel(TblTeam row)
        {
            Id = row.Id;
            Name = row.Name;
        }


    }
}