using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clause_Verification
{
    internal class Clause
    {
        public string clauseID { get; set; }
        public string P_C { get; set; }
        public string IBR { get; set; }
        public string FILL_IN { get; set; }
        public string MONEY { get; set; }
        public string UCF { get; set; }
        public string FP_SUP { get; set; }
        public string CR_SUP { get; set; }
        public string FP_R_D { get; set; }
        public string CR_R_D { get; set; }
        public string FP_SVC { get; set; }
        public string CR_SVC { get; set; }
        public string FP_CON { get; set; }
        public string CR_CON { get; set; }
        public string T_M_LH { get; set; }
        public string LMV { get; set; }
        public string COM_SVC { get; set; }
        public string DDR { get; set; }
        public string A_E { get; set; }
        public string FAC { get; set; }
        public string IND_DEL { get; set; }
        public string TRN { get; set; }
        public string UTL_SVC { get; set; }

        //We made this variable an integer, we weren't sure if we would need it or not
        // public int Dep_P_C { get; set; }


        public Clause()
        {
            clauseID = string.Empty;
            P_C = string.Empty;
            IBR = string.Empty;
            FILL_IN = string.Empty;
            MONEY = string.Empty;
            UCF = string.Empty;
            FP_SUP = string.Empty;
            CR_SUP = string.Empty;
            FP_R_D = string.Empty;
            CR_R_D = string.Empty;
            FP_SVC = string.Empty;
            CR_SVC = string.Empty;
            FP_CON = string.Empty;
            CR_CON = string.Empty;
            T_M_LH = string.Empty;
            LMV = string.Empty;
            COM_SVC = string.Empty;
            DDR = string.Empty;
            A_E = string.Empty;
            FAC = string.Empty;
            IND_DEL = string.Empty;
            TRN = string.Empty;
            UTL_SVC = string.Empty;
            //Dep_P_C = 0;
        }


        public Clause(string line)
        {
            var pieces = line.Split(',');

            clauseID = pieces[0];
            P_C = pieces[1];
            IBR = pieces[2];
            FILL_IN = pieces[3];
            MONEY = pieces[4];
            UCF = pieces[5];
            FP_SUP = pieces[6];
            CR_SUP = pieces[7];
            FP_R_D = pieces[8];
            CR_R_D = pieces[9];
            FP_SVC = pieces[10];
            CR_SVC = pieces[11];
            FP_CON = pieces[12];
            CR_CON = pieces[13];
            T_M_LH = pieces[14];
            LMV = pieces[15];
            COM_SVC = pieces[16];
            DDR = pieces[17];
            A_E = pieces[18];
            FAC = pieces[19];
            IND_DEL = pieces[20];
            TRN = pieces[21];
            UTL_SVC = pieces[22];


        }

    }
}
