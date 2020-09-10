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
        //listede olup olmadýðýný kontrol ediyor.


        // Gereksinimlerde 1. Sýradaki benden istenilen deðiþkenlerimi tanýmladým.
        //Deðiþken isimleri program.cs içindeki isimlerle ayný verdim.
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
            //burada  private olarak tanýmladýðým örnek verilerimi alýyorum.
            getCountryCode();
            getDates();
            NextDate = DateStart;//þimdi tarihi baþlangýç tarihi olarak kabul ediyorum 

            //app.confige gidip arama yapýp ülke koduna bakýp kontrol ediliyor.
            foreach (var item in settingList)
            {
                int BusinessDay = 0;
                decimal penalty = item.DailyPenaltyFee;
                if (item.Culture.Contains(CountryCode))
                {
                    for (int i = 0; i< DateDifference; i++)
                    {
                        //tatil gününe denk geliyor mu ve hafta sonumu bu koþullara bakýyoruz eðer deðilse iþ gününü arttýrýyoruz
                        if (!item.WeekendList.Contains(NextDate.DayOfWeek) && !item.HolidayList.Contains(NextDate))
                        {
                            BusinessDay++;
                        }
                        NextDate = DateStart.AddDays(i + 1); 
                        //Burada günü arttýrýyor 
                    }
                    //ceza business günleri için saðlanmalýdýr
                    //kaç günde bir ceza uygulanacaksa bunu iþ gününden çýkartýyoruz
                    BusinessDay -= item.PenaltyAppliesAfter;
                    if (BusinessDay > 0)
                    {
                        BusinessDay++;
                        //Arttýrmamýn sebebi son günü de saymak için
                        return (BusinessDay * penalty).ToString();
                        //ceza*iþ günü sayýsý çarpýlýr ve paranýn türü eklenir.
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
