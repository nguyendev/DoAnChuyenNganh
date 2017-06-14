using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DACN.Services
{
    public class StringExtension
    {
        public static int DemChuoi(string str)
        {
            int l = 0;
            int bien_dem = 1;

            /* lap toi phan cuoi cua chuoi */
            while (l <= str.Length - 1)
            {
                /* kiem tra xem ky tu hien tai co phai la khoang trang 
                 * hay la ky tu new line hay ky tu tab */
                if (str[l] == ' ' || str[l] == '\n' || str[l] == '\t')
                {
                    bien_dem++;
                }

                l++;
            }
            return bien_dem;
        }
    }
}
