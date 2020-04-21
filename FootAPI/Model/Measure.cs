using FootAPI.Enum;
using System;

namespace FootAPI.Model
{
    public class Measure
    {
        public int UserId { get; set; }
        public int IdSexo { get; set; }
        public double Centimetros { get; set; }
        public double Inches { get => Math.Round(Centimetros / 2.54, 2); }
        public bool Infantil { get; set; }
        public double Br  { get => ((5 * Centimetros) + 28) / 4; }

        public double US
        {
            get
            {
                if(!Infantil)
                {
                    switch ((SexoEnum) IdSexo)
                    {
                        case SexoEnum.Masculino:
                            return (3 * Inches) - 22;
                        case SexoEnum.Feminino:
                            return (3 * Inches) - 21;
                        default:
                            return 0;
                    }
                }
                else
                {
                    return (3 * Inches) - 9.67;
                }
            }
        }

        public double UK
        {
            get 
            {
                if (!Infantil)
                    return (3 * Inches) - 23;
                else
                    return (3 * Inches) - 10;
            }
        }

        public double EU { get =>  1.27 * (UK + 23) + 2; }
        public DateTime DataHora { get; set; }
    }
}
