using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clase
{
    public class Enumerari
    {
        public enum CategorieMedicament
        {
            None=0,
            DurereSiFebra = 1,
            Antibiotice = 2,
            Alergii = 3,
            Digestie = 4,
            RacealaSiGripa = 5
        }

        public enum CaracteristiciMedicament
        {
            None=0,
            RecomandatPentruCopii=1,
            RecomamdatPentruAdulti=2,
            RecomandatPentruAmbii=3
        }
    }
}
