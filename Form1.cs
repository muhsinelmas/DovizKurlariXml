using System.Xml;

namespace DovizKurlariXml
{
    public partial class FrmMainPage : Form
    {
        readonly XmlDocument _xmlDoc = new();
        public FrmMainPage()
        {
            InitializeComponent();
        }

        private void FrmMainPage_Load(object sender, EventArgs e)
        {
            dateTimePicker.MaxDate = DateTime.Now;
            GetExchangeRates();
        }

        private void GetExchangeRates()
        {
            _xmlDoc.Load("https://www.tcmb.gov.tr/kurlar/today.xml");

            try
            {
                if (_xmlDoc != null)
                {
                    DateTime dateTime = Convert.ToDateTime(_xmlDoc.SelectSingleNode("Tarih_Date")?.Attributes?["Tarih"]?.Value);
                    LblDate.Text = dateTime.ToLongDateString();

                    string usdBuying = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
                    string usdSelling = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
                    LblDolarAlis.Text = usdBuying;
                    LblDolarSatis.Text = usdSelling;

                    string eurBuying = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
                    string eurSelling = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
                    LblEuroAlis.Text = eurBuying;
                    LblEuroSatis.Text = eurSelling;
                }
            }
            catch (System.Xml.XPath.XPathException a)
            {
                MessageBox.Show(a.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetExchangeRates();
            dateTimePicker.Value = DateTime.Now;
        }

        private void dateTimePicker_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker.Value.DayOfWeek != DayOfWeek.Saturday && dateTimePicker.Value.DayOfWeek != DayOfWeek.Sunday)
            {
                string year = dateTimePicker.Value.Year.ToString();
                string month = dateTimePicker.Value.Month.ToString();
                if (month.Length == 1)
                {
                    month = "0" + month;
                }
                string day = dateTimePicker.Value.Day.ToString();
                if (day.Length == 1)
                {
                    day = "0" + day;
                }
                string yearMonth = year + month;
                string dayMonthYear = day + month + year;

                string url = "https://www.tcmb.gov.tr/kurlar/" + yearMonth + "/" + dayMonthYear + ".xml";

                try
                {
                    _xmlDoc.Load(url);
                    DateTime dateTime = Convert.ToDateTime(_xmlDoc.SelectSingleNode("Tarih_Date")?.Attributes?["Tarih"]?.Value);
                    LblDate.Text = dateTime.ToLongDateString();

                    string usdBuying = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteBuying").InnerXml;
                    string usdSelling = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='USD']/BanknoteSelling").InnerXml;
                    LblDolarAlis.Text = usdBuying;
                    LblDolarSatis.Text = usdSelling;

                    string eurBuying = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteBuying").InnerXml;
                    string eurSelling = _xmlDoc.SelectSingleNode("Tarih_Date/Currency[@Kod='EUR']/BanknoteSelling").InnerXml;
                    LblEuroAlis.Text = eurBuying;
                    LblEuroSatis.Text = eurSelling;
                }
                catch (Exception a)
                {
                    MessageBox.Show(a.Message);
                }
            }
        }
    }
}