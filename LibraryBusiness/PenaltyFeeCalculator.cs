using System;
using System.Collections.Generic;
using System.Text;
using LibraryConfigUtilities;

namespace LibraryBusiness
{
    /* Description,
     * settingList member holds configuration parameters stored in the App.config file, 
     * please explore the properties and methods in the Country class to get a better understanding.
     * 
     * Please implement this class accordingly to accomplish requirements.
     * Feel free to add any parameters, methods, class members, etc. if necessary
     */
    public class PenaltyFeeCalculator
    {

        private List<Country> settingList = new LibrarySetting().LibrarySettingList;
        //settingList--> app.config parametreleri tutuyor!!
        //listede olup olmad���n� kontrol ediyor.


        // Gereksinimlerde 1. S�radaki benden istenilen de�i�kenlerimi tan�mlad�m.
        //De�i�ken isimleri program.cs i�indeki isimlerle ayn� verdim.
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public string CountryCode { get; set; }
        public DateTime NextDate { get; set; }
        public int DateDifference { get; set; }

        public PenaltyFeeCalculator()
        {
        }

        public String Calculate()
        {
            //burada  private olarak tan�mlad���m �rnek verilerimi al�yorum.
            getCountryCode();
            getDates();
            NextDate = DateStart;//�imdi tarihi ba�lang�� tarihi olarak kabul ediyorum 

            //app.confige gidip arama yap�p �lke koduna bak�p kontrol ediliyor.
            foreach (var item in settingList)
            {
                int BusinessDay = 0;
                decimal penalty = item.DailyPenaltyFee;
                if (item.Culture.Contains(CountryCode))
                {
                    for (int i = 0; i< DateDifference; i++)
                    {
                        //tatil g�n�ne denk geliyor mu ve hafta sonumu bu ko�ullara bak�yoruz e�er de�ilse i� g�n�n� artt�r�yoruz
                        if (!item.WeekendList.Contains(NextDate.DayOfWeek) && !item.HolidayList.Contains(NextDate))
                        {
                            BusinessDay++;
                        }
                        NextDate = DateStart.AddDays(i + 1); 
                        //Burada g�n� artt�r�yor 
                    }
                    //ceza business g�nleri i�in sa�lanmal�d�r
                    //ka� g�nde bir ceza uygulanacaksa bunu i� g�n�nden ��kart�yoruz
                    BusinessDay -= item.PenaltyAppliesAfter;
                    if (BusinessDay > 0)
                    {
                        BusinessDay++;
                        //Artt�rmam�n sebebi son g�n� de saymak i�in
                        return (BusinessDay * penalty).ToString();
                        //ceza*i� g�n� say�s� �arp�l�r ve paran�n t�r� eklenir.
                    }
                }
            }
            return "Culture not found";
        }

        private void getCountryCode()
        {
            CountryCode = Convert.ToString("ar-AE");

        }

        private void getDates()
        {
            DateStart = Convert.ToDateTime("05.09.2020");
            DateEnd = Convert.ToDateTime("15.09.2020");
            TimeSpan difference = DateEnd - DateStart;
            DateDifference = difference.Days;
        }


    }

}
