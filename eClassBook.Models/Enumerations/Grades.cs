using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eClassBook.Models.Enumerations
{
    public enum Grades
    {
        [Description("Слаб")] Slab = 2,
        [Description("Среден")] Sreden = 3,
        [Description("Добър")] Dobur = 4,
        [Description("Много добър")] Mnogo_dobur = 5,
        [Description("Отличен")] Otlichen = 6
    }
}
